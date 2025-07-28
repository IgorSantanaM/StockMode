using Microsoft.Extensions.Logging;
using MimeKit;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Features.Sales.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Infra.Services.Email
{
    public class Mailer(IHtmlMailRenderer htmlRenderer,
        IMailSender mailSender,
        ILogger<Mailer> logger) : IMailer
    {

        public MimeMessage CreateMessage(SaleCompletedEmail saleCompletedEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("StockMode", "naoresponda@stockmode.com.br")); // TODO: PEGAR DO CONTEXT DA LOJA
            message.To.Add(new MailboxAddress(saleCompletedEmail.Email, saleCompletedEmail.Email));
            message.Subject = $"Venda concluida!";
            var bb = new BodyBuilder
            {
                HtmlBody = htmlRenderer.RenderSaleCompletedEmail(saleCompletedEmail)
            };
            message.Body = bb.ToMessageBody();
            return message;
        }
        public async Task SendSaleCompletedAsync(SaleCompletedEmail saleCompletedEmail, CancellationToken token)
        {
            var message = CreateMessage(saleCompletedEmail);

            try
            {
                await mailSender.SendAsync(message, token);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error sendin email for {accountEmail}", saleCompletedEmail.Email);
                throw;
            }
               
        }
    }
}
