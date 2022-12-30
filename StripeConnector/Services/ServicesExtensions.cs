using Stripe;

namespace StripeConnector.Services;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStripeAppService, StripeAppService>();
    }

    public static void AddStripe(this IServiceCollection services, IConfiguration config)
    {
        StripeConfiguration.ApiKey = config["Stripe:SecretKey"];

        services.AddScoped<CustomerService>();
        services.AddScoped<ChargeService>();
        services.AddScoped<TokenService>();
    }
}