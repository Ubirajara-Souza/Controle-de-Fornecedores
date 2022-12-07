using Bira.Providers.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bira.Providers.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifier _notifier;

        public SummaryViewComponent(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Notifications = await Task.FromResult(_notifier.GetNotifications());
            Notifications.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Message));

            return View();
        }
    }
}
