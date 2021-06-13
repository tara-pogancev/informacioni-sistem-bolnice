using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{

   
    class NotificationVisibilityController
    {
        private NotificationVisibilityService notificationVisibilityService;

        public NotificationVisibilityController()
        {
            notificationVisibilityService = new NotificationVisibilityService();
        }
        public bool ExistsUnreadNotification(String userID) => notificationVisibilityService.ExistsUnreadNotification(userID);

        public void NotificationOpened(String userID) => notificationVisibilityService.NotificationOpened(userID);
    }
}
