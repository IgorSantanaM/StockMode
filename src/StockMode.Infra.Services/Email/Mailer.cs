using Microsoft.Extensions.Logging;
using MimeKit;
using StockMode.Application.Common.Dtos;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Application.PDF;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace StockMode.Infra.Services.Email
{
    public class Mailer(IHtmlMailRenderer htmlRenderer,
        IMailSender mailSender,
        ILogger<Mailer> logger) : IMailer
    {
        public async Task SendGenericAsync(string to, string subject, string templateName, string modelJson, Type modelType, IEnumerable<EmailAttachment>? attachments, CancellationToken token)
        {
            try
            {
                var model = JsonSerializer.Deserialize(modelJson, modelType);

                var htmlBody = await htmlRenderer.RenderAsync(templateName, model);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("StockMode", "naoresponda@stockmode.com.br"));

                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;

                var bb = new BodyBuilder
                {
                    HtmlBody = htmlBody
                };

                if(attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        bb.Attachments.Add(attachment.FileName, attachment.Data, ContentType.Parse(attachment.MimeType));
                    }
                }

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
