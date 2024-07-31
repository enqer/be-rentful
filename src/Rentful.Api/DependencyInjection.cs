namespace Rentful.Api
{
    public static class DependencyInjection
    {
        public static void ConfigureOptions(this WebApplicationBuilder webApplicationBuilder)
        {
        }

        private static WebApplicationBuilder ConfigureOption<T>(this WebApplicationBuilder webApplicationBuilder) where T : class
        {
            webApplicationBuilder.Services.Configure<T>(webApplicationBuilder.Configuration.GetSection(typeof(T).Name));
            return webApplicationBuilder;
        }
    }
}
