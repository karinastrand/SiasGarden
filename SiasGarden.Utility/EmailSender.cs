
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace SiasGarden.Utility;

public class EmailSender : IEmailSender
{//Will add when i have a grid mail account
    public string MailerSendSecret { get; set; }
   
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}
