using SIMS.Model;
using SIMS.Repositories.NotificationRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class NotificationService
    {
        private INotificationRepository notificationRepository = new NotificationFileRepository();
        

        public NotificationService()
        {
           
        }

        
        public List<Notification> GetAllNotification() => notificationRepository.GetAll();

        public void UpdateNotification(Notification notification) => notificationRepository.Update(notification);

        public void DeleteNotification(Notification notification) => notificationRepository.Delete(notification.ID);

        public void SaveNotification(Notification notification) => notificationRepository.Save(notification);

        public void DeleteNotification(string ID) => notificationRepository.Delete(ID);

        public Notification GetNotification(String key) => notificationRepository.FindById(key);

        public void SaveOrUpdate(Notification notification) => notificationRepository.CreateOrUpdate(notification);

        public List<Notification> ReadByUser(String userID)
        {
            return notificationRepository.ReadByUser(userID);
        }

        public List<Notification> ReadPastNotificationsByUser(String userID)
        {
            return notificationRepository.ReadPastNotificationsByUser(userID);
        }
    }
}
