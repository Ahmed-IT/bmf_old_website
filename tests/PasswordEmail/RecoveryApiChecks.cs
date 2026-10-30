using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BMF_WebSite;
using BMF_WebSite.Controllers;
using BMF_WebSite.Models;

internal static class RecoveryApiChecks
{
    private sealed class Controller : UserAccountController
    {
        public List<M_UserAccount> Accounts = new List<M_UserAccount>();
        public string LookupEmail, Recipient, SentPassword;
        public int Lookups, Sends;
        public Exception LookupFailure, SendFailure;
        public Task Completion = Task.CompletedTask;

        protected override Task<List<M_UserAccount>> FindPasswordRecoveryAccountsAsync(string normalizedEmail)
        {
            Lookups++;
            LookupEmail = normalizedEmail;
            if (LookupFailure != null) throw LookupFailure;
            return Task.FromResult(Accounts);
        }

        protected override Task SendPasswordRecoveryEmailAsync(M_UserAccount account, string recipient)
        {
            Sends++;
            Recipient = recipient;
            SentPassword = account.Password;
            return SendFailure == null ? Completion : Task.FromException(SendFailure);
        }
    }

    private static M_UserAccount Account() => new M_UserAccount
    {
        ID = 123, Email_Address = "Member@Example.invalid ", Password = "stored-password", IsActive = true
    };

    private static void Assert(bool condition, string description)
    {
        if (!condition) throw new Exception(description);
        Console.WriteLine("PASS API: " + description);
    }

    private static NegotiatedContentResult<string> Request(Controller controller, string email = "member@example.invalid")
    {
        return (NegotiatedContentResult<string>)controller.SendAccountPassword(new ClsUser
        {
            Email_Address = email, ID = 999, Password = "untrusted-client-password", CPR_Number = "untrusted"
        }).GetAwaiter().GetResult();
    }

    public static void Run()
    {
        foreach (var email in new[] { "not-an-email", "Member <member@example.invalid>", "member@example.invalid,other@example.invalid",
            "member@example.invalid\r\nBcc: other@example.invalid", new string('a', 255) + "@example.invalid" })
        using (var controller = new Controller())
            Assert(Request(controller, email).StatusCode == HttpStatusCode.BadRequest && controller.Lookups == 0 && controller.Sends == 0,
                "400 rejects malformed, multiple, display-name, header-injection, or overlong recipients before lookup");

        using (var controller = new Controller())
            Assert(Request(controller).StatusCode == HttpStatusCode.NotFound && controller.Sends == 0, "404 for an unregistered email; no email sent");

        foreach (var deleted in new[] { false, true })
        using (var controller = new Controller())
        {
            var account = Account();
            if (deleted) account.DeletedOnUtc = DateTime.UtcNow;
            else account.IsActive = false;
            controller.Accounts.Add(account);
            Assert(Request(controller).StatusCode == HttpStatusCode.NotFound && controller.Sends == 0, "Inactive/deleted accounts cannot recover passwords");
        }

        using (var controller = new Controller())
        {
            controller.Accounts.Add(Account());
            Assert(Request(controller, " MEMBER@example.invalid ").StatusCode == HttpStatusCode.OK
                && controller.LookupEmail == "member@example.invalid" && controller.Recipient == "Member@Example.invalid"
                && controller.SentPassword == "stored-password" && controller.Sends == 1,
                "200 uses normalized lookup, exactly the registered recipient, and stored credentials; client credentials are ignored");
        }

        using (var controller = new Controller())
        {
            controller.Accounts.Add(Account()); controller.Accounts.Add(Account());
            Assert(Request(controller).StatusCode == HttpStatusCode.Conflict && controller.Sends == 0, "409 prevents sending for ambiguous active accounts");
        }

        foreach (var badData in new[] { "wrong-recipient", "empty-password" })
        using (var controller = new Controller())
        {
            var account = Account();
            if (badData == "wrong-recipient") account.Email_Address = "someone-else@example.invalid";
            else account.Password = "";
            controller.Accounts.Add(account);
            Assert(Request(controller).StatusCode == HttpStatusCode.InternalServerError && controller.Sends == 0,
                "500 prevents sending when stored recipient or credentials are invalid");
        }

        using (var controller = new Controller { LookupFailure = new InvalidOperationException("private database details") })
        {
            var result = Request(controller);
            Assert(result.StatusCode == HttpStatusCode.InternalServerError && !result.Content.Contains("private") && controller.Sends == 0,
                "500 reports lookup failure without disclosing database details");
        }

        foreach (var failure in new Exception[] { new MailKit.Security.AuthenticationException("private OAuth token"),
            new System.IO.IOException("private connection details"), new System.Configuration.ConfigurationErrorsException("private credential"),
            new MailKit.Net.Smtp.SmtpCommandException(MailKit.Net.Smtp.SmtpErrorCode.RecipientNotAccepted,
                (MailKit.Net.Smtp.SmtpStatusCode)450, "private temporary SMTP rejection") })
        using (var controller = new Controller { SendFailure = failure })
        {
            controller.Accounts.Add(Account());
            var result = Request(controller);
            Assert(result.StatusCode == HttpStatusCode.ServiceUnavailable && !result.Content.Contains("private"), "503 reports email-service failures safely");
        }

        using (var controller = new Controller
        {
            SendFailure = new MailKit.Net.Smtp.SmtpCommandException(MailKit.Net.Smtp.SmtpErrorCode.RecipientNotAccepted,
                MailKit.Net.Smtp.SmtpStatusCode.MailboxUnavailable, "private SMTP diagnostics")
        })
        {
            controller.Accounts.Add(Account());
            var result = Request(controller);
            Assert(result.StatusCode == HttpStatusCode.BadGateway && !result.Content.Contains("private"), "502 reports SMTP rejection without reporting Sent");
        }

        using (var controller = new Controller())
        {
            controller.Accounts.Add(Account());
            var completion = new TaskCompletionSource<bool>();
            controller.Completion = completion.Task;
            var pending = controller.SendAccountPassword(new ClsUser { Email_Address = "member@example.invalid" });
            Assert(!pending.IsCompleted, "The API waits for mail submission before responding");
            completion.SetResult(true);
            var result = (NegotiatedContentResult<string>)pending.GetAwaiter().GetResult();
            Assert(result.StatusCode == HttpStatusCode.OK && result.Content == "Sent", "Only completed mail submission returns the legacy 200 Sent contract");
        }
    }
}
