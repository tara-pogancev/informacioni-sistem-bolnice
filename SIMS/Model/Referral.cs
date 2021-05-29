using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.PatientRepo;

namespace SIMS.Model
{
    public class Referral
    {
        private static int refferalValidDays = 7;

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime RefferalDate { get; set; }
        public String RefferalKey { get; set; }

        public Referral()
        {
            RefferalKey = DateTime.Now.ToString("HHmmssDDMMyy");
        }

        public Referral(Doctor doctor, Patient patient)
        {
            RefferalDate = DateTime.Today;
            Doctor = doctor;
            Patient = patient;
            RefferalKey = DateTime.Now.ToString("HHmmssDDMMyy");
        }

        public void InitData()
        {
            Doctor = DoctorFileRepository.Instance.FindById(Doctor.Jmbg);
            Patient = PatientFileRepository.Instance.FindById(Patient.Jmbg);
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
