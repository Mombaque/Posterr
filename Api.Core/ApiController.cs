using Domain.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Core
{
    [ApiController]
    public abstract class ApiController : Controller
    {

        private readonly DomainNotificationHandler _notificationsHandler;

        protected ApiController(IRequestHandler<DomainNotification, bool> notificationHandler)
        {
            _notificationsHandler = (DomainNotificationHandler)notificationHandler;
        }

        protected new IActionResult Response(object result = null)
        {
            if (_notificationsHandler.HasErrors())
            {
                return BadRequest(new
                {
                    success = false,
                    data = _notificationsHandler.GetNotifications().Select(n => n.Value)
                });            
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}