
namespace FireApp.Services;

    public class ServiceManagement : IServiceManagement
    {

        private ILogger<ServiceManagement> _logger;

    public ServiceManagement(ILogger<ServiceManagement> logger)
    {
        _logger = logger;
    }

    public void SendEmail()
        {
            _logger.LogInformation($"SendEmail to all users at {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }

        public void SendSms()
        {
            _logger.LogInformation($"SendSms to all users at {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }

        public void SyncData()
        {
            _logger.LogInformation($"SyncData to all users at {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }

        public void UpdateDatabase()
        {
            _logger.LogInformation($"UpdateDatabase to all users at {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }
    }