using SendGrid;
using SendGrid.Helpers.Mail;
using System;

namespace Kosmisch.Sample.OnPremisesAspnetApp.Helpers
{
    public static class EmailHelper
    {
        public static void Send()
        {
            var from = new EmailAddress()
            {
                Name = "Kosmisch",
                Email = "from@kosmischsample.net"
            };
            var to = new EmailAddress()
            {
                Name = "Test",
                Email = "to@kosmischsample.net"
            };
            var subject = "Kosmisch Sample";
            var body = "Test message";
            var message = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var response = client.SendEmailAsync(message).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
