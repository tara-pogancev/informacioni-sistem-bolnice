using Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace SIMS.Model
{
    public class Uput
    {
        private static int refferalValidDays = 7;

        public Lekar Doctor { get; set; }
        public Pacijent Patient { get; set; }
        public DateTime RefferalDate { get; set; }
        public String RefferalKey { get; set; }

        public Uput()
        {
            RefferalKey = DateTime.Now.ToString("HHmmssDDMMyy");
        }

        public Uput(Lekar doctor, Pacijent patient)
        {
            RefferalDate = DateTime.Today;
            Doctor = doctor;
            Patient = patient;
            RefferalKey = DateTime.Now.ToString("HHmmssDDMMyy");
        }

        public void InitData()
        {
            Doctor = LekarStorage.Instance.Read(Doctor.Jmbg);
            Patient = PacijentStorage.Instance.Read(Patient.Jmbg);
        }

        public Boolean IsRefferalValid()
        {
            DateTime today = DateTime.Today;
            DateTime endValidDay = RefferalDate.AddDays(refferalValidDays);

            if (today <= endValidDay)
            {
                return true;
            }

            return false;

        }


    }
}
