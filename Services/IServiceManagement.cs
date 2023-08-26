
namespace FireApp.Services;
    public interface IServiceManagement
    {
        // void SendEmail(string to, string subject, string body);
        void SendEmail();
        void UpdateDatabase();

        void SendSms();
        // void SendSms(string to, string message);

        void SyncData();
    }