using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    class NotificationController
    {
        private NotificationService notificationService = new NotificationService();

        public List<Notification> GetAllNotification() => notificationService.GetAllNotification();

        public void UpdateNotification(Notification notification) => notificationService.UpdateNotification(notification);

        public void DeleteNotification(Notification notification) => notificationService.DeleteNotification(notification.ID);

        public void SaveNotification(Notification notification) => notificationService.SaveNotification(notification);

        public void DeleteNotification(string ID) => notificationService.DeleteNotification(ID);

        public Notification GetNotification(String key) => notificationService.GetNotification(key);

        public void SaveOrUpdate(Notification notification) => notificationService.SaveOrUpdate(notification);

        public List<Notification> ReadByUser(String userID)
        {
            return notificationService.ReadByUser(userID);
        }

        public List<Notification> ReadPastNotificationsByUser(String userID)
        {
            return notificationService.ReadPastNotificationsByUser(userID);
        }

        public bool ExistsUnreadNotification(String userID) => notificationService.ExistsUnreadNotification(userID);

        public void NotificationOpened(String userID) => notificationService.NotificationOpened(userID);

    }
}
