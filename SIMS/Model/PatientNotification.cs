using SIMS.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class PatientNotification:Notification
    {
        private DateTime fireTime;
        private bool checkStatus;
        private NotificationType notificationType;

        public PatientNotification(string author, DateTime time, 
            string content, List<string> target,DateTime 
            fireTime,bool checkStatus,NotificationType notificationType) : base(author,time,content,target){

            this.fireTime = fireTime;
            this.checkStatus = checkStatus;
            this.notificationType = notificationType;
        }
    }
}
