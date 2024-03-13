using System.IO;
using System.Threading.Tasks;

namespace Consumer.API.Helper
{
    public interface IEmailHelper
    {
        Task SendMailAsync(string subject, string body, string? recipients = null, Stream? stream = null, string? ccRecipients = null);
    }
}
