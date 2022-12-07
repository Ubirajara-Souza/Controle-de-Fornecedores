using Bira.Providers.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bira.Providers.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotifier _notifier;

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}
