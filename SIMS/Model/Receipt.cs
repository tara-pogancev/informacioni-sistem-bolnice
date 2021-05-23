using Newtonsoft.Json;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    public class Receipt
    {
        public String MedicineName { get; set; }
        public String Amount { get; set; }
        public String Diagnosis { get; set; }
        public String RecieptKey { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }

        public Receipt()
        {
            RecieptKey = DateTime.Now.ToString("yyMMddhhmmss");
        }

        public Receipt(Doctor doctor, Patient patient, String medicineName, String amount, String diagnosis)
        {
            Doctor = doctor;
            Patient = patient;
            MedicineName = medicineName;
            Amount = amount;
            Diagnosis = diagnosis;

            RecieptKey = DateTime.Now.ToString("yyMMddhhmmss");
            Date = DateTime.Today;

        }   

        [JsonIgnore]
        public String DoctorName { get { return Doctor.FullName; } }

        [JsonIgnore]
        public String PatientName { get { return Patient.FullName; } }

        [JsonIgnore]
        public String DateString { get { return Date.ToString("dd.MM.yyyy."); } }

        [JsonIgnore]
        public String NameAndQuantity { get { return (MedicineName + " " + Amount); } }

        public void InitData()
        {
            Doctor = DoctorFileRepository.Instance.FindById(Doctor.Jmbg);
            Patient = PatientFileRepository.Instance.FindById(Patient.Jmbg);
        }

    }
}
