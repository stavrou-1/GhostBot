using GhostBot.Mvc.Interfaces;

namespace GhostBot.Mvc.Services {
    public class SMSService : IMobileService {
        public void Execute () {
            WriteLine ("This is SMSService");
        }
    }

    public class EmailService : IMailService {
        public void Execute () {
            WriteLine ("This is EmailService");
        }
    }

    public class NotificationSender {
        public IMobileService _OIMobileService = null;
        public IMailService _OIMailService = null;

        public NotificationSender (IMobileService oiMobileService) {
            _OIMobileService = oiMobileService;
        }

        public IMailService SetMailService {
            set {
                _OIMailService = value;
            }
        }

        public void SendNotification () {
            _OIMobileService.Execute ();
            _OIMailService.Execute ();
        }
    }
}