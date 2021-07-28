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
            string From = "Bahrainmotorfederation@outlook.com";
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(From, "Bahrain Motor Federation");
                message.From = fromAddress;
                message.To.Add(To);
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

            msg += "Dear " + FullName + ", <br/>";
            msg += "Your registration has been received.<br/><br/>";
            msg += "Kindly accept this email as a confirmation that you have successfully applied for the your license. <br/><br/>";
            msg += "We will contact you shortly to complete the process of your license, for any further information please contact us at info@bmf.com.bh <br/><br/>";
            msg += "Regards, <br/>";
            msg += "Bahrain Motor Federation <br/>";
            msg += "bmf.com.bh";

            return msg;
        }
    }
}