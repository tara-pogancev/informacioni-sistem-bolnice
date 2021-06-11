using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SIMS.Model
{
    public class DoctorAppRating
    {
        public String ID { get; set; }
        public Doctor Doctor { get; set; }
        public String Text { get; set; }
        public int Rating { get; set; }

        public DoctorAppRating(Doctor doctor, String text, int rating)
        {
            Doctor = doctor;
            Text = text;
            Rating = rating;
            ID = DateTime.Today.ToString("ddMMyyyyHHmmss");
            Serialize = true;
        }

        [JsonIgnore]
        public bool Serialize { get; set; }

        public bool ShouldSerializeDoctor()
        {
            return Serialize;
        }




    }
}
