using System.Net;
using System.Net.Mail;
using UniquomeApp.Utilities.Models;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class EmailUtilities
{
    public static bool SendEmail(string smtpServerLocation, bool enableSsl, int port, string fromEmail, IList<string> recipients, string subject, string body, string username, string password, IList<string> filenames)
    {
        if (string.IsNullOrEmpty(smtpServerLocation))
            throw new Exception("Undefined Smtp");
        if (string.IsNullOrEmpty(fromEmail))
            throw new Exception("Undefined Sender");
        if (recipients == null || recipients.Count == 0)
            throw new Exception("Undefined recipients");

        var mail = new MailMessage();
        try
        {
            var smtpServer = new SmtpClient(smtpServerLocation);
            mail.From = new MailAddress(fromEmail);
            foreach (var recipient in recipients)
            {
                if (!string.IsNullOrEmpty(recipient))
                    mail.To.Add(recipient);
            }
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;                
            if (filenames != null)
            {
                foreach (var file in filenames)
                {
                    var attachment = new Attachment(file);
                    mail.Attachments.Add(attachment);
                }
            }

            smtpServer.Port = port;
            smtpServer.Credentials = new NetworkCredential(username, password);
            smtpServer.EnableSsl = enableSsl;
            smtpServer.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to send E-mail \r\n{ExceptionUtilities.GetExceptionMessage(ex)}");
        }
        finally
        {
            mail.Dispose();
        }
    }

    public static MailMessage BuildMessage(string subject, string body, string recipient, IList<string> attachments)
    {
        var recipients = new List<string> { recipient };
        return BuildMessage(subject, body, recipients, attachments);
    }

    public static MailMessage BuildMessage(string subject, string body, IList<string> recipients, IList<string> attachments)
    {
        var mail = new MailMessage();
        foreach (var recipient in recipients)
        {
            if (recipient != "")
            {
                mail.To.Add(recipient);
            }
        }
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;
        if (attachments != null && attachments.Count > 0)
        {
            foreach (var file in attachments)
            {
                var attachment = new Attachment(file);
                mail.Attachments.Add(attachment);
            }
        }
        return mail;
    }

    public static IList<string> SendEmail(EmailLocation fromEmail, MailMessage mailMessage, short mailsPerHour = 1)
    {
        var mails = new List<MailMessage> { mailMessage };
        return SendEmails(fromEmail, mails, mailsPerHour);
    }

    public static IList<string> SendEmails(EmailLocation fromEmail, IList<MailMessage> mailMessages, short mailsPerHour = 1)
    {
        int mailsSend = 0;
        var sendLog = new List<string>();
        if (fromEmail.IsValidLocation())
        {
            var smtpServer = new SmtpClient(fromEmail.SmtpLocation)
            {
                Port = fromEmail.Port,
                Credentials = new NetworkCredential(fromEmail.Username, fromEmail.Password),
                EnableSsl = fromEmail.EnableSsl
            };
            var mailAddress = new MailAddress(fromEmail.Email);
            foreach (var mail in mailMessages)
            {
                try
                {
                    mail.From = mailAddress;
                    smtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                    sendLog.Add($"Impossible Send {ex.Message} ");
                }
                mailsSend++;
            }
            sendLog.Add($"{mailsSend} messages send !");
        }
        else
        {
            sendLog.Add("Wrong Sender Address  !");
        }
        return sendLog;
    }

    public static void SendRelayMail(string fromAddress, string toAddress, string smtpServer, string subject, string body, IList<string> filenames = null)
    {
        var smtpClient = new SmtpClient(smtpServer, 25);
        var mailMessage = new MailMessage { From = new MailAddress(fromAddress) };
        mailMessage.To.Add(toAddress);
        mailMessage.Subject = subject;

        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;
        if (filenames != null)
        {
            foreach (var file in filenames)
            {
                var attachment = new Attachment(file);
                mailMessage.Attachments.Add(attachment);
            }
        }
        smtpClient.Send(mailMessage);
    }

    public static bool SendEmailWithoutSmtp(string fromEmail, IList<string> recipients, string subject, string body)
    {
        if (string.IsNullOrEmpty(fromEmail))
            throw new Exception("Undefined Sender");
        if (recipients == null || recipients.Count == 0)
            throw new Exception("Undefined recipients");

        var mail = new MailMessage();
        try
        {
            var smtpServer = new SmtpClient("");
            mail.From = new MailAddress(fromEmail);
            foreach (string recipient in recipients)
            {
                if (!string.IsNullOrEmpty(recipient))
                    mail.To.Add(recipient);
            }
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;                

//                smtpServer.Port = port;
//                smtpServer.Credentials = new NetworkCredential(username, password);
//                smtpServer.EnableSsl = enableSsl;
            smtpServer.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to send E-mail \r\n{ExceptionUtilities.GetExceptionMessage(ex)}");
        }
        finally
        {
            mail.Dispose();
        }
    }
}