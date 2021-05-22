﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class NotificationRepository : GenericFileRepository<string, Notification, NotificationRepository>
    {
        protected override string getKey(Notification entity)
        {
            return entity.ID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\obavestenja.json";
        }

        protected override void RemoveReferences(string key)
        {
        
        }

        public List<Notification> ReadByUser(String key)
        {
            List<Notification> retVal = this.GetAll();

            for (int i=0;i<retVal.Count;i++)
            {
                if (!(retVal[i].Target[0].Equals("All") || retVal[i].ContainsTarget(key)))
                {
                    retVal.RemoveAt(i);
                    i--;
                }
                    
            }

            return retVal;

        }

        public List<Notification> ReadPastNotificationsByUser(String key)
        {
            List<Notification> retVal = this.GetAll();

            for (int i = 0; i < retVal.Count; i++)
            {
                if (!(retVal[i].Target[0].Equals("All") || retVal[i].ContainsTarget(key)) || !retVal[i].isPast())
                {
                    retVal.RemoveAt(i);
                    i--;
                }

            }

            return retVal;

        }
    }
}