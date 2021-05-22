using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.PatientRepo
{
    interface INotificationRepository:IGenericRepository<Notification,String>
    {
        List<Notification> ReadByUser(String key);
        List<Notification> ReadPastNotificationsByUser(String key);
    }
}
