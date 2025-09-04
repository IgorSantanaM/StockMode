namespace StockMode.Application.Features.Products.Dtos
{
    public record ProductCreatedEmail(string ProductName,
                                    string ProductDescription,
                                    IReadOnlyCollection<VariationDetailDto> Variations,
                                    IReadOnlyCollection<string> CustomersEmails);
}
