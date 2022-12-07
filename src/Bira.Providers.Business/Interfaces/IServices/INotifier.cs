using Bira.Providers.Business.Notifications;

namespace Bira.Providers.Business.Interfaces.IServices
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
