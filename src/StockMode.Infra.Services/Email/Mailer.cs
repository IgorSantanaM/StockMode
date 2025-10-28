using Microsoft.Extensions.Logging;
using MimeKit;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using System.Text.Json;

namespace StockMode.Infra.Services.Email
{
    public class Mailer(IHtmlMailRenderer htmlRenderer,
        IMailSender mailSender,
        ILogger<Mailer> logger) : IMailer
    {
        public async Task SendAsync<TModel>(EmailMessage<TModel> emailMessage, CancellationToken token)
        {
            try
            {
                var htmlBody = await htmlRenderer.RenderAsync(emailMessage.TemplateName, emailMessage.Model);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("StockMode", "naoresponda@stockmode.com.br"));

                message.To.Add(new MailboxAddress(emailMessage.To, emailMessage.To));
                message.Subject = emailMessage.Subject;

                var bb = new BodyBuilder
                {
                    HtmlBody = htmlBody
                };

                message.Body = bb.ToMessageBody();

                await mailSender.SendAsync(message, token);

                logger.LogInformation("Email sent to {To} with subject {Subject}", emailMessage.To, emailMessage.Subject);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending email to {To} with subject {Subject}", emailMessage.To, emailMessage.Subject);
                throw;
            }
        }

        public async Task SendGenericAsync(string to, string subject, string templateName, string modelJson, CancellationToken token)
        {
            try
            {
                // Deserialize the JSON model to a dynamic object for template rendering
                var model = JsonSerializer.Deserialize<dynamic>(modelJson);

                var htmlBody = await htmlRenderer.RenderAsync(templateName, model);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("StockMode", "naoresponda@stockmode.com.br"));

                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;

                var bb = new BodyBuilder
                {
                    HtmlBody = htmlBody
                };

                message.Body = bb.ToMessageBody();

                await mailSender.SendAsync(message, token);

                logger.LogInformation("Generic email sent to {To} with subject {Subject} using template {Template}", 
                    to, subject, templateName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending generic email to {To} with subject {Subject} using template {Template}", 
                    to, subject, templateName);
                throw;
            }
        }
    }
}
