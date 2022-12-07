using Bira.Providers.Business.Interfaces.IServices;

namespace Bira.Providers.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notification;

        public Notifier()
        {
            _notification = new List<Notification>();
        }
        public void Handle(Notification notification)
        {
            _notification.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notification;
        }

        public bool HasNotification()
        {
            return _notification.Any();
        }
    }
}
