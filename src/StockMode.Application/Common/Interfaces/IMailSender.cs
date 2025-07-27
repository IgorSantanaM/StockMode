using MimeKit;

namespace StockMode.Application.Common.Interfaces
{
    public interface IMailSender
    {
        Task<string> SendAsync(MimeMessage message, CancellationToken token);
    }
}
