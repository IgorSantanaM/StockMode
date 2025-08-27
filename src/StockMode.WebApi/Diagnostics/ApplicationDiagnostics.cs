using System.Diagnostics.Metrics;

namespace StockMode.WebApi.Diagnostics
{
    public static class ApplicationDiagnostics
    {
        private const string ServiceName = "StockMode.WebApi";
        public static readonly Meter Meter = new(ServiceName);

        public static readonly Counter<long> CustomerCreatedCounter = Meter.CreateCounter<long>("customers.created");
    }
}
