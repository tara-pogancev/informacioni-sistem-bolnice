// File:    Termin.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:56 PM
// Purpose: Definition of Class Termin

using Newtonsoft.Json;
using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.ComponentModel;

namespace SIMS.Model
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

        public Appointment(Appointment anamnesisAppointment)
        {
            StartTime = anamnesisAppointment.StartTime;
            InitialTime = anamnesisAppointment.InitialTime;
            Duration = anamnesisAppointment.Duration;
            Type = anamnesisAppointment.Type;
            Doctor = anamnesisAppointment.Doctor;
            Patient = anamnesisAppointment.Patient;
            Room = anamnesisAppointment.Room;
            AppointmentID = anamnesisAppointment.AppointmentID;
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

        public bool EqualDate(DateTime date)
        {
            return this.StartTime == date;
        }

        public DateTime GetEndTime()
        {
            return StartTime.AddMinutes(Duration);
        }

        public bool GetIfPast()
        {
            return (GetEndTime() > DateTime.Now);
        }

        public String GetAppointmentDate() 
        { 
            return StartTime.ToString("dd.MM.yyyy."); 
        }

        public String GetAppointmentTime()
        {
            return StartTime.ToString("HH:mm");
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

        public bool GetIfCurrent()
        {
            return (this.StartTime <= DateTime.Now && GetEndTime() >= DateTime.Now);
        }

        public bool GetIfRecorded()
        {
            return (!(AnamnesisFileRepository.Instance.FindById(this.AppointmentID) == null
                    && SurgeryReportFileRepository.Instance.FindById(this.AppointmentID) == null));
        }

        public String GetPatientName()
        {
            return Patient.FullName;
        }

        public String GetDoctorName()
        {
            return Doctor.FullName;
        }

    }
}