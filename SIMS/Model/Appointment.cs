// File:    Termin.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:56 PM
// Purpose: Definition of Class Termin

using Newtonsoft.Json;
using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIMS.Repositories.SecretaryRepo
{
   public class Appointment : INotifyPropertyChanged
   {
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public AppointmentType Type { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public Room Room { get; set; }
        public DateTime InitialTime { get; set; }
        public String AppointmentID { get; set; }
        

        [JsonIgnore]
        public bool Serialize { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public Appointment(DateTime startTime, int duration, AppointmentType type, Doctor doctor, Patient patient, Room room)
        {
            StartTime = startTime;
            InitialTime = startTime;
            Duration = duration;
            Type = type;
            Doctor = doctor;
            Patient = patient;
            Room = room;
            AppointmentID = GenerateID();
            Serialize = true;
        }


        public Appointment()
        {
            Type = AppointmentType.examination;
            AppointmentID = GenerateID();
            Serialize = true;
        }

        public bool ShouldSerializeInitialTime()
        {
            return Serialize;
        }

        public bool ShouldSerializeVremeTrajanja()
        {
            return Serialize;
        }
        
        public bool ShouldSerializeVrstaTermina()
        {
            return Serialize;
        }

        public bool ShouldSerializeLekar()
        {
            return Serialize;
        }

        public bool ShouldSerializePacijent()
        {
            return Serialize;
        }

        public bool ShouldSerializeProstorija()
        {
            return Serialize;
        }
        public bool ShouldSerializeInicijalnoVrijeme()
        {
            return Serialize;
        }

        [JsonIgnore]
        public String AppointmentDate { get => StartTime.ToString("dd.MM.yyyy."); }

        [JsonIgnore]
        public String AppointmentTime { get => StartTime.ToString("HH:mm"); }
                
        [JsonIgnore]
        public String TypeString
        {
            get {

                if (Type == AppointmentType.examination)
                {
                    return "Pregled";
                }
                else
                {
                    return "Operacija";
                }
            }
            
        }
        public bool EqualDate(DateTime date)
        {
            return this.StartTime == date;
        }
        [JsonIgnore]
        public String PatientName
        {
            get
            { 
                return (Patient.FullName); 
            }
        }

        [JsonIgnore]
        public String DoctorName
        {
            get
            {
                return (Doctor.FullName);
            }
        }

        [JsonIgnore]
        public bool IsRecorded
        {
            get
            {
                if (AnamnesisFileRepository.Instance.FindById(this.AppointmentID) == null && SurgeryReportFileRepository.Instance.FindById(this.AppointmentID) == null)
                    return false;
                else return true;
            }
        }

        [JsonIgnore]
        public DateTime EndTime
        {
            get
            {
                return StartTime.AddMinutes(Duration);
            }
        }

        [JsonIgnore]
        public bool IsPast
        {
            get
            {
                // Vraca termine koji su se u potpunosti zavrsili
                if (this.EndTime <= DateTime.Now)
                    return true;
                else return false;
            }
        }



        [JsonIgnore]
        public bool IsCurrent
        {
            get
            {
                //Vraca termine koji trenutno traju
                if (this.StartTime <= DateTime.Now && this.EndTime >= DateTime.Now)
                    return true;

                return false;
            }
        }

        [JsonIgnore]
        public String AppointmentFullInfo
        {
            get
            {
                return DoctorName + ", " + AppointmentTime + " " + AppointmentDate;
            }
        }

        public void InitData()
        {
            Patient = new PatientFileRepository().FindById(Patient.Jmbg);
            Room = new RoomFileRepository().FindById(Room.Number);
            Doctor = new DoctorFileRepository().FindById(Doctor.Jmbg);
        }

        private static string GenerateID()
        {
            return DateTime.Now.ToString("yyMMddhhmmssffffff");
        }

    }
}