using System;
using System.Collections.Generic;
using SIMS.Model;

namespace SIMS.Service
{
    class NotificationVisibilityService
    {
        private NotificationService notificationService;

        public NotificationVisibilityService()
        {
            this.notificationService = new NotificationService();
        }

        public bool ExistsUnreadNotification(String userID)
        {
            List<Notification> notifications = notificationService.ReadPastNotificationsByUser(userID);
            notifications.Reverse();
            foreach(var notification in notifications)
            {
                if (notification.CheckStatus == false)
                {
                    return true;
                }
            }

            return false;
        }

        public void NotificationOpened(String userID)
        {
            List<Notification> notifications = notificationService.ReadPastNotificationsByUser(userID);
            foreach (var notification in notifications)
            {

                notification.CheckStatus = true;
                notificationService.UpdateNotification(notification);
            }
        }
    }
}