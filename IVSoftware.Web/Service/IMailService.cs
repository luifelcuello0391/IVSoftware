using IVSoftware.Web.Data;
using System.Threading.Tasks;

namespace IVSoftware.Web.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}