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
        public static string SendMail(string To,string CC, string subject, string body)
        {
            string status = "";

            string ccList = "";
            string From = "Bahrainmotorfederation@outlook.com";
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(From, "Bahrain Motor Federation");
                message.From = fromAddress;
                message.To.Add(To);

                if (CC != "")
                {
                    message.CC.Add(CC);
                }
                
                if (ccList != null && ccList != string.Empty)
                    message.CC.Add(ccList);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                smtpClient.Host = "smtp.live.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("Bahrainmotorfederation@outlook.com", "ixundpjdqjjcltnb");

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

        public static string EmailMessage(string FullName)
        {
            string msg = "";

            msg += "Dear " + FullName + ", <br/>";
            msg += "On behalf of Bahrain eSport Gran Turismo Championship Committee (BEGTC), we would like to thank you for showing interest in our championship. <br/><br/>";
            msg += "Your registration has been received.<br/><br/>";
            msg += "Kindly accept this email as a confirmation that you have successfully registered for the championship, Please refer to our website for details concerning per-qualifying, to be published shortly. <br/><br/>";
            msg += "Tow extra steps we need you to do: <br/><br/>";
            msg += "Download Discord mobile app and join BEGTC 2021 Season 3 - Qualifying Round link below: <br/>";
            msg += "https://discord.gg/Xkncctv5BA <br/><br/>";
            msg += "Read carefully the <a href='https://bmfstorage.blob.core.windows.net/br-subcategoryrulesregs-pdf/begtc3regulations2021final.pdf-ced02ebc22c54ae6b976e7ddda33c6fa.pdf'>Rules and Regulations</a> <br/><br/>";
            msg += "May the odds be in your favor. <br/>";
            msg += "For any further information please contact us on info@bmf.com.bh <br/><br/>";
            msg += "Regards, <br/>";
            msg += "Bahrain eSport Committee <br/>";
            msg += "bmf.com.bh";


            return msg;
        }

        public static string LicenseEmailMessage(string FullName)
        {
            string msg = "";

            msg = "<html>" +
"<head>" +
"<meta name='viewport' content='width=device-width' />" +
"<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />" +
"<title>Simple Transactional Email</title>" +
"<style>" +
"img {" +
"border: none;" +
"-ms-interpolation-mode: bicubic;" +
"max-width: 100%;" +
"}" +
"body {" +
"background-color: #f6f6f6;" +
"font-family: sans-serif;" +
"-webkit-font-smoothing: antialiased;" +
"font-size: 14px;" +
"line-height: 1.4;" +
"margin: 0;" +
"padding: 0;" +
"-ms-text-size-adjust: 100%;" +
"-webkit-text-size-adjust: 100%;" +
"}" +
"table {" +
"border-collapse: separate;" +
"mso-table-lspace: 0pt;" +
"mso-table-rspace: 0pt;" +
"width: 100%; }" +
"table td {" +
"font-family: sans-serif;" +
"font-size: 14px;" +
"vertical-align: top;" +
"}" +
".body {" +
"background-color: #f6f6f6;" +
"width: 100%;" +
"}" +
".container {" +
"display: block;" +
"margin: 0 auto !important;" +
"max-width: 580px;" +
"padding: 10px;" +
"width: 580px;" +
"}" +
".content {" +
"box-sizing: border-box;" +
"display: block;" +
"margin: 0 auto;" +
"max-width: 580px;" +
"padding: 10px;" +
"}" +
".main {" +
"background: #ffffff;" +
"border-radius: 3px;" +
"width: 100%;" +
"}" +
".wrapper {" +
"box-sizing: border-box;" +
"padding: 20px;" +
"}" +
".content-block {" +
"padding-bottom: 10px;" +
"padding-top: 10px;" +
"}" +
".footer {" +
"clear: both;" +
"margin-top: 10px;" +
"text-align: center;" +
"width: 100%;" +
"}" +
".footer td," +
".footer p," +
".footer span," +
".footer a {" +
"color: #999999;" +
"font-size: 12px;" +
"text-align: center;" +
"}" +
"h1," +
"h2," +
"h3," +
"h4 {" +
"color: #000000;" +
"font-family: sans-serif;" +
"font-weight: 400;" +
"line-height: 1.4;" +
"margin: 0;" +
"margin-bottom: 30px;" +
"}" +
"h1 {" +
"font-size: 35px;" +
"font-weight: 300;" +
"text-align: center;" +
"text-transform: capitalize;" +
"}" +
"p," +
"ul," +
"ol {" +
"font-family: sans-serif;" +
"font-size: 14px;" +
"font-weight: normal;" +
"margin: 0;" +
"margin-bottom: 15px;" +
"}" +
"p li," +
"ul li," +
"ol li {" +
"list-style-position: inside;" +
"margin-left: 5px;" +
"}" +
"a {" +
"color: #3498db;" +
"text-decoration: underline;" +
"}" +
".first {" +
"margin-top: 0;" +
"}" +
".align-center {" +
"text-align: center;" +
"}" +
".align-right {" +
"text-align: right;" +
"}" +
".align-left {" +
"text-align: left;" +
"}" +
".clear {" +
"clear: both;" +
"}" +
".mt0 {" +
"margin-top: 0;" +
"}" +
".mb0 {" +
"margin-bottom: 0;" +
"}" +
".preheader {" +
"color: transparent;" +
"display: none;" +
"height: 0;" +
"max-height: 0;" +
"max-width: 0;" +
"opacity: 0;" +
"overflow: hidden;" +
"mso-hide: all;" +
"visibility: hidden;" +
"width: 0;" +
"}" +
".powered-by a {" +
"text-decoration: none;" +
"}" +
"hr {" +
"border: 0;" +
"border-bottom: 1px solid #f6f6f6;" +
"margin: 20px 0;" +
"}" +
"@media only screen and (max-width: 620px) {" +
"table[class=body] h1 {" +
"font-size: 28px !important;" +
"margin-bottom: 10px !important;" +
"}" +
"table[class=body] p," +
"table[class=body] ul," +
"table[class=body] ol," +
"table[class=body] td," +
"table[class=body] span," +
"table[class=body] a {" +
"font-size: 16px !important;" +
"}" +
"table[class=body] .wrapper," +
"table[class=body] .article {" +
"padding: 10px !important;" +
"}" +
"table[class=body] .content {" +
"padding: 0 !important;" +
"}" +
"table[class=body] .container {" +
"padding: 0 !important;" +
"width: 100% !important;" +
"}" +
"table[class=body] .main {" +
"border-left-width: 0 !important;" +
"border-radius: 0 !important;" +
"border-right-width: 0 !important;" +
"}" +
"table[class=body] .btn table {" +
"width: 100% !important;" +
"}" +
"table[class=body] .btn a {" +
"width: 100% !important;" +
"}" +
"table[class=body] .img-responsive {" +
"height: auto !important;" +
"max-width: 100% !important;" +
"width: auto !important;" +
"}" +
"}" +
"@media all {" +
".ExternalClass {" +
"width: 100%;" +
"}" +
".ExternalClass," +
".ExternalClass p," +
".ExternalClass span," +
".ExternalClass font," +
".ExternalClass td," +
".ExternalClass div {" +
"line-height: 100%;" +
"}" +
".apple-link a {" +
"color: inherit !important;" +
"font-family: inherit !important;" +
"font-size: inherit !important;" +
"font-weight: inherit !important;" +
"line-height: inherit !important;" +
"text-decoration: none !important;" +
"}" +
"#MessageViewBody a {" +
"color: inherit;" +
"text-decoration: none;" +
"font-size: inherit;" +
"font-family: inherit;" +
"font-weight: inherit;" +
"line-height: inherit;" +
"}" +
".btn-primary table td:hover {" +
"background-color: #34495e !important;" +
"}" +
".btn-primary a:hover {" +
"background-color: #34495e !important;" +
"border-color: #34495e !important;" +
"}" +
"}" +
"</style>" +
"</head>" +
"<body class=''>" +
"<table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>" +
"<tr>" +
"<td>&nbsp;</td>" +
"<td class='container'>" +
"<div class='content'>" +
"<table role='presentation' class='main'>" +
"<tr>" +
"<td class='wrapper'>" +
"<table role='presentation' border='0' cellpadding='0' cellspacing='0'>" +
"<tr>" +
"<td>" +
"<b>Dear " + FullName + ",</b>" +
"<br /><br />" +
"<p>Your license request has been received." +
"<br /><br />" +
"Kindly accept this email as a confirmation that you have successfully applied for the your license, " +
"We will contact you shortly to complete the process of your license, for any further information please contact us at" + "info@bmf.com.bh.</p><tbody>" +
"<tr>" +
"<td align='left'>" +
"<br /><br />" +
"<p>Regards," +
"<br />" +
"Bahrain Motor Federation" +
"</p>" +
"</td>" +
"</tr>" +
"</table>" +
"</td>" +
"</tr>" +
"</table>" +
"<div class='footer'>" +
"<table role='presentation' border='0' cellpadding='0' cellspacing='0'>" +
"<tr>" +
"<td class='content-block'>" +
"<span class='apple-link'>Bahrain Motor Federation</span>" +
"<br><a href='http://bmf.com.bh'>http://www.bmf.com.bh</a>." +
"</td>" +
"</tr>" +
"</table>" +
"</div>" +
"</div>" +
"</td>" +
"</tr>" +
"</table>" +
"</body>" +
"</html>";

            return msg;
        }

        public static string LicenseEmailMessageToBMF(ClsLicForm form)
        {
            string msg = "";

            msg = "<html>" +
"<head>" +
"<meta name='viewport' content='width=device-width' />" +
"<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />" +
"<title>Simple Transactional Email</title>" +
"<style>" +
"img {" +
"border: none;" +
"-ms-interpolation-mode: bicubic;" +
"max-width: 100%;" +
"}" +
"body {" +
"background-color: #f6f6f6;" +
"font-family: sans-serif;" +
"-webkit-font-smoothing: antialiased;" +
"font-size: 14px;" +
"line-height: 1.4;" +
"margin: 0;" +
"padding: 0;" +
"-ms-text-size-adjust: 100%;" +
"-webkit-text-size-adjust: 100%;" +
"}" +
"table {" +
"border-collapse: separate;" +
"mso-table-lspace: 0pt;" +
"mso-table-rspace: 0pt;" +
"width: 100%; }" +
"table td {" +
"font-family: sans-serif;" +
"font-size: 14px;" +
"vertical-align: top;" +
"}" +
".body {" +
"background-color: #f6f6f6;" +
"width: 100%;" +
"}" +
".container {" +
"display: block;" +
"margin: 0 auto !important;" +
"max-width: 580px;" +
"padding: 10px;" +
"width: 580px;" +
"}" +
".content {" +
"box-sizing: border-box;" +
"display: block;" +
"margin: 0 auto;" +
"max-width: 580px;" +
"padding: 10px;" +
"}" +
".main {" +
"background: #ffffff;" +
"border-radius: 3px;" +
"width: 100%;" +
"}" +
".wrapper {" +
"box-sizing: border-box;" +
"padding: 20px;" +
"}" +
".content-block {" +
"padding-bottom: 10px;" +
"padding-top: 10px;" +
"}" +
".footer {" +
"clear: both;" +
"margin-top: 10px;" +
"text-align: center;" +
"width: 100%;" +
"}" +
".footer td," +
".footer p," +
".footer span," +
".footer a {" +
"color: #999999;" +
"font-size: 12px;" +
"text-align: center;" +
"}" +
"h1," +
"h2," +
"h3," +
"h4 {" +
"color: #000000;" +
"font-family: sans-serif;" +
"font-weight: 400;" +
"line-height: 1.4;" +
"margin: 0;" +
"margin-bottom: 30px;" +
"}" +
"h1 {" +
"font-size: 35px;" +
"font-weight: 300;" +
"text-align: center;" +
"text-transform: capitalize;" +
"}" +
"p," +
"ul," +
"ol {" +
"font-family: sans-serif;" +
"font-size: 14px;" +
"font-weight: normal;" +
"margin: 0;" +
"margin-bottom: 15px;" +
"}" +
"p li," +
"ul li," +
"ol li {" +
"list-style-position: inside;" +
"margin-left: 5px;" +
"}" +
"a {" +
"color: #3498db;" +
"text-decoration: underline;" +
"}" +
".first {" +
"margin-top: 0;" +
"}" +
".align-center {" +
"text-align: center;" +
"}" +
".align-right {" +
"text-align: right;" +
"}" +
".align-left {" +
"text-align: left;" +
"}" +
".clear {" +
"clear: both;" +
"}" +
".mt0 {" +
"margin-top: 0;" +
"}" +
".mb0 {" +
"margin-bottom: 0;" +
"}" +
".preheader {" +
"color: transparent;" +
"display: none;" +
"height: 0;" +
"max-height: 0;" +
"max-width: 0;" +
"opacity: 0;" +
"overflow: hidden;" +
"mso-hide: all;" +
"visibility: hidden;" +
"width: 0;" +
"}" +
".powered-by a {" +
"text-decoration: none;" +
"}" +
"hr {" +
"border: 0;" +
"border-bottom: 1px solid #f6f6f6;" +
"margin: 20px 0;" +
"}" +
"@media only screen and (max-width: 620px) {" +
"table[class=body] h1 {" +
"font-size: 28px !important;" +
"margin-bottom: 10px !important;" +
"}" +
"table[class=body] p," +
"table[class=body] ul," +
"table[class=body] ol," +
"table[class=body] td," +
"table[class=body] span," +
"table[class=body] a {" +
"font-size: 16px !important;" +
"}" +
"table[class=body] .wrapper," +
"table[class=body] .article {" +
"padding: 10px !important;" +
"}" +
"table[class=body] .content {" +
"padding: 0 !important;" +
"}" +
"table[class=body] .container {" +
"padding: 0 !important;" +
"width: 100% !important;" +
"}" +
"table[class=body] .main {" +
"border-left-width: 0 !important;" +
"border-radius: 0 !important;" +
"border-right-width: 0 !important;" +
"}" +
"table[class=body] .btn table {" +
"width: 100% !important;" +
"}" +
"table[class=body] .btn a {" +
"width: 100% !important;" +
"}" +
"table[class=body] .img-responsive {" +
"height: auto !important;" +
"max-width: 100% !important;" +
"width: auto !important;" +
"}" +
"}" +
"@media all {" +
".ExternalClass {" +
"width: 100%;" +
"}" +
".ExternalClass," +
".ExternalClass p," +
".ExternalClass span," +
".ExternalClass font," +
".ExternalClass td," +
".ExternalClass div {" +
"line-height: 100%;" +
"}" +
".apple-link a {" +
"color: inherit !important;" +
"font-family: inherit !important;" +
"font-size: inherit !important;" +
"font-weight: inherit !important;" +
"line-height: inherit !important;" +
"text-decoration: none !important;" +
"}" +
"#MessageViewBody a {" +
"color: inherit;" +
"text-decoration: none;" +
"font-size: inherit;" +
"font-family: inherit;" +
"font-weight: inherit;" +
"line-height: inherit;" +
"}" +
".btn-primary table td:hover {" +
"background-color: #34495e !important;" +
"}" +
".btn-primary a:hover {" +
"background-color: #34495e !important;" +
"border-color: #34495e !important;" +
"}" +
"}" +
"</style>" +
"</head>" +
"<body class=''>" +
"<table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>" +
"<tr>" +
"<td>&nbsp;</td>" +
"<td class='container'>" +
"<div class='content'>" +
"<table role='presentation' class='main'>" +
"<tr>" +
"<td class='wrapper'>" +
"<table role='presentation' border='0' cellpadding='0' cellspacing='0'>" +
"<tr>" +
"<td>" +
"<br /><br />" +
"<p>New request has been received for " + form.FirstName + " " + form.LastName + " with CPR Number " + form.CPR +
"<br /><br />" +
"<tr>" +
"<td align='left'>" +
"<br /><br />" +
"<p>Regards," +
"<br />" +
"Bahrain Motor Federation" +
"</p>" +
"</td>" +
"</tr>" +
"</table>" +
"</td>" +
"</tr>" +
"</table>" +
"<div class='footer'>" +
"<table role='presentation' border='0' cellpadding='0' cellspacing='0'>" +
"<tr>" +
"<td class='content-block'>" +
"<span class='apple-link'>Bahrain Motor Federation</span>" +
"<br><a href='http://bmf.com.bh'>http://www.bmf.com.bh</a>." +
"</td>" +
"</tr>" +
"</table>" +
"</div>" +
"</div>" +
"</td>" +
"</tr>" +
"</table>" +
"</body>" +
"</html>";

            return msg;
        }
    }
}