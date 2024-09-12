using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pencil.ContentManagement.Application.Contracts.Infrastructure;
using Pencil.ContentManagement.Application.Models.Mail;
using Pencil.ContentManagement.Infrastructure.Mail;

namespace Pencil.ContentManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<GmailSettings>(configuration.GetSection("GmailSettings"));

        services.AddScoped<IMailService, GmailEmailService>();
        return services;
    }
}