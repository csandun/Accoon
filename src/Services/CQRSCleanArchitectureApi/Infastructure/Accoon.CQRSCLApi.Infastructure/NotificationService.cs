using Accoon.CQRSCAApi.Application.Interfaces;
using Accoon.CQRSCAApi.Application.Notification.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.CQRSCLApi.Infastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
