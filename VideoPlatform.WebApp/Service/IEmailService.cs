using VideoPlatform.WebApp.Data.Entities;

namespace VideoPlatform.WebApp.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
