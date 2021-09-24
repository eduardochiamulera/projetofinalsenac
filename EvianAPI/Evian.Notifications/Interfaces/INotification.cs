using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evian.Notifications.Interfaces
{
    public interface INotification
    {
        List<Error> Errors { get; set; }
        bool HasErrors { get; }
        bool HasNotifications { get; }

        string Get();
    }
}
