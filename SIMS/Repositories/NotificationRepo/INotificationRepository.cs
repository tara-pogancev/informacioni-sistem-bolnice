using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.SecretaryRepo
{
    interface INotificationRepository:IGenericRepository<Notification,String>
    {
        List<Notification> ReadByUser(String key);
        List<Notification> ReadPastNotificationsByUser(String key);
    }
}
