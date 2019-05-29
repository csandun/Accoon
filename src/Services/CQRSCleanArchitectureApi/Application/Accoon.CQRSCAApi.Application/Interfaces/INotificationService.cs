using Accoon.CQRSCAApi.Application.Notification.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.CQRSCAApi.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
