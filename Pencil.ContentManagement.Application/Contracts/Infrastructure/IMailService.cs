using Pencil.ContentManagement.Application.Models.Mail;

namespace Pencil.ContentManagement.Application.Contracts.Infrastructure;

public interface IMailService
{
    Task SendAsync(Email email);
}