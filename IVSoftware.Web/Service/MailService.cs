using IVSoftware.Data.Models;
using IVSoftware.Web.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.Service
{
    public class MailService : IMailService
    {
        private MailSettings _mailSettings;
        private readonly IEntityService<NotificationSetting, int> _notificationService;

        public MailService(IEntityService<NotificationSetting, int> notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                NotificationSetting notificationSetting = (await _notificationService.GetAllAsync()).FirstOrDefault();
                if(notificationSetting != null)
                {
                    _mailSettings = new MailSettings()
                    {
                        DisplayName = "Sistema de Notificación",
                        Host = "smtp.gmail.com",
                        Mail = notificationSetting.Email,
                        Password = ClsCipher.Decrypt(notificationSetting.Password, ClsCipher.PasswordKey),
                        Port = 587
                    };

                    var email = new MimeMessage();
                    email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                    email.Subject = mailRequest.Subject;
                    if (!string.IsNullOrEmpty(mailRequest.CC))
                    {
                        List<MailboxAddress> CC = mailRequest.CC
                            .Split(";", StringSplitOptions.RemoveEmptyEntries)
                            .Select(oe => MailboxAddress.Parse(oe))
                            .ToList();
                        email.Cc.AddRange(CC);
                    }
                    var builder = new BodyBuilder();
                    builder.HtmlBody = mailRequest.Body;
                    email.Body = builder.ToMessageBody();
                    using var smtp = new SmtpClient();
                    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}