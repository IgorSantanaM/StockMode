using System.Reflection;

namespace StockMode.WebApi.Endpoints.Internal
{
    public static class EnpointExtensions
    {
        public static void UseEndpoints<TMarker>(this IApplicationBuilder app)  =>
            UseEndpoints(app, typeof(TMarker));

        public static void UseEndpoints(this IApplicationBuilder app, Type typeMarker)
        {
            IEnumerable<TypeInfo> endpointType = GetEndpointsTypesFromAssemblyContaining(typeMarker);

            foreach (TypeInfo type in endpointType)
                type.GetMethod(nameof(IEndpoints.DefineEndpoint))?.Invoke(null, new object[] { app });
        }
        private static IEnumerable<TypeInfo> GetEndpointsTypesFromAssemblyContaining(Type typeMarker) =>
            typeMarker.Assembly.DefinedTypes.Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEndpoints).IsAssignableFrom(x));
    }
}
