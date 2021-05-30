using Newtonsoft.Json;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Controller;

namespace SIMS.Model
{
    public class Receipt
    {
        private DoctorController doctorController = new DoctorController();
        private PatientController patientController = new PatientController();

        public String MedicineName { get; set; }
        public String Amount { get; set; }
        public String Diagnosis { get; set; }
        public String RecieptID { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }

        public Receipt()
        {
            RecieptID = DateTime.Now.ToString("yyMMddhhmmss");
        }

        public Receipt(Doctor doctor, Patient patient, String medicineName, String amount, String diagnosis)
        {
            Doctor = doctor;
            Patient = patient;
            MedicineName = medicineName;
            Amount = amount;
            Diagnosis = diagnosis;

            RecieptID = DateTime.Now.ToString("yyMMddhhmmss");
            Date = DateTime.Today;

        }   

        public String GetDoctorName() 
        { 
            return Doctor.FullName; 
        }

        public String GetPatientName() 
        { 
            return Patient.FullName;
        }

        public String GetRecieptDateString() 
        { 
            return Date.ToString("dd.MM.yyyy.");
        }

        [JsonIgnore]
        public String NameAndQuantity { get { return (MedicineName + " " + Amount); } }

        public void InitData()
        {
            Doctor = doctorController.GetDoctor(Doctor.Jmbg);
            Patient = patientController.GetPatient(Patient.Jmbg);
        }

    }
}
