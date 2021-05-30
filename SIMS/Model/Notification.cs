using Newtonsoft.Json;
using SIMS.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class Notification
    {
        public string Author { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public string ID { get; set; }
        public List<string> Target { get; set; }
        public bool CheckStatus { get; set; }
        public  NotificationType NotificationType { get; set; }

        public Notification()
        {
            
        }

        public Notification(string author, DateTime time, string content, List<string> target)
        {
            Author = author;
            Time = time;
            Content = content;
            ID = author+time.ToString("yyMMddHHmmssffffff");
            Target = target;
        }
        public Notification(string author, DateTime time, string content, List<string> target,bool checkStatus,NotificationType notificationType)
        {
            Author = author;
            Time = time;
            Content = content;
            ID = author + time.ToString("yyMMddHHmmssffffff");
            Target = target;
            this.CheckStatus = checkStatus;
            this.NotificationType = notificationType;
        }

        [JsonIgnore]
        public string DateAndTimeString
        {
            get
            {
                return Time.ToString("dd.MM.yyyy. HH:mm");
            }
        }

        [JsonIgnore]
        public String AuthorUppercase
        {
            get { return Author.ToUpper(); }
        }

        [JsonIgnore]
        public String VaryingTimeString
        {
            get 
            {
                if (Time.Date == (DateTime.Today))
                    return Time.ToString("HH:mm");

                return Time.ToString("dd.MM.yyyy.");
            }
        }

        public bool ContainsTarget(string target)
        {
            foreach (string s in Target)
            {
                if (s.Equals(target))
                    return true;
            }
            return false;
        }

        public Boolean GetIfPast()
        {
            DateTime currentTime = DateTime.Now;
            if (currentTime >= Time)
                return true;
            else return false;
        }

       
    }
}
