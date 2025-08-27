using StockMode.Domain.Customers;
using System.Diagnostics;

namespace StockMode.WebApi.Diagnostics.Extensions
{
    public static class ActivityExtensions
    {
        public static Activity? EnrichWithClient(this Activity? activity, Customer customer) // TODO: Get the HTTPCONTEXT and treat the logged user
        {
            activity?.SetTag("customer.id", customer.Id);
            activity?.SetTag("customer.name", customer.Name);
            activity?.SetTag("customer.email", customer.Email);
            return activity;
        }
    }
}
