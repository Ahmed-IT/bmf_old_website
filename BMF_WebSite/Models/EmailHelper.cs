using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BMF_WebSite.Models
{
    public class EmailHelper
    {
        public static string SendMail(string To, string subject, string body)
        {
            string status = "";

            string ccList = "";
            string From = "noreply@bahrainmmc.com";
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(From);
                message.From = fromAddress;
                message.To.Add(To);
                if (ccList != null && ccList != string.Empty)
                    message.CC.Add(ccList);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                smtpClient.Host = "relay-hosting.secureserver.net";
                smtpClient.Port = 25;
                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("noreply@bahrainmmc.com", "Reply@369");

                smtpClient.Send(message);

                status = "Email Sent";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return status;
        }

        public static string SendMailWithAttachment(string To, string subject, string body, byte[] bytes)
        {
            string status = "";

            string ccList = "";
            string From = "noreply@bahrainmmc.com";
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(From);
                message.From = fromAddress;
                message.To.Add(To);
                if (ccList != null && ccList != string.Empty)
                    message.CC.Add(ccList);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                message.Attachments.Add(new Attachment(new MemoryStream(bytes), "MarshalsList.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
                smtpClient.Host = "relay-hosting.secureserver.net";
                smtpClient.Port = 25;
                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("noreply@bahrainmmc.com", "Reply@369");

                smtpClient.Send(message);

                status = "Email Sent";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return status;
        }

    }
}