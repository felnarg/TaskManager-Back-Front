using Application.Interfaces;
using Infrastructure._Resources;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using System.Reflection.Metadata;

namespace Infrastructure.SendGrid
{
    public class SendGridService : ISendGridService
    {
        public async Task SendNotificationEmail(string userEmail, string plainTextContent, string user)
        {
            var apiKey = Constants.API_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Constants.OPI_EMAIL, Constants.OPI_USER);
            var to = new EmailAddress(userEmail, user);
            var subject = Constants.OPI_EMAIL_SUBJECT;
            var htmlContent = plainTextContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }        
        
}

