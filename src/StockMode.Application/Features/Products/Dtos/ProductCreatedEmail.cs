namespace StockMode.Application.Features.Products.Dtos
{
    public record ProductCreatedEmail(string ProductName,
                                    string ProductDescription,
                                    IReadOnlyCollection<VariationsForEmailSendingDto> Variations,
                                    IReadOnlyCollection<string> CustomersEmails);
}
