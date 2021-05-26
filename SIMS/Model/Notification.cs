using Newtonsoft.Json;
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

        [JsonIgnore]
        public string DateAndTimeString
        {
            get
            {
                return Time.ToString("dd.MM.yyyy. HH:mm");
            }
        }

        [JsonIgnore]
        public String AutorUppercase
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

        public bool ContainsTarget(string key)
        {
            foreach (string s in Target)
            {
                if (s.Equals(key))
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
