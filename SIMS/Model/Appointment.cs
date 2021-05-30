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
using SIMS.Controller;

namespace SIMS.Model
{
   public class Appointment 
   {
        private AppointmentController appointmentController = new AppointmentController();
        private PatientController patientController = new PatientController();
        private DoctorController doctorController = new DoctorController();
        private RoomController roomController = new RoomController();

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
            anamnesisAppointment = appointmentController.GetAppointment(anamnesisAppointment.AppointmentID);
            anamnesisAppointment.InitData();

            StartTime = anamnesisAppointment.StartTime;
            InitialTime = anamnesisAppointment.InitialTime;
            Duration = anamnesisAppointment.Duration;
            Type = anamnesisAppointment.Type;
            Doctor = anamnesisAppointment.Doctor;
            Patient = anamnesisAppointment.Patient;
            Room = anamnesisAppointment.Room;
            AppointmentID = anamnesisAppointment.AppointmentID;
            Serialize = anamnesisAppointment.Serialize;
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
            return (GetEndTime() < DateTime.Now);
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
            Patient = patientController.GetPatient(Patient.Jmbg);
            Room = roomController.GetRoom(Room.Number);
            Doctor = doctorController.GetDoctor(Doctor.Jmbg);
        }

        private static string GenerateID()
        {
            return DateTime.Now.ToString("yyMMddhhmmssffffff");
        }

        public bool GetIfCurrent()
        {
            return (StartTime <= DateTime.Now && GetEndTime() >= DateTime.Now);
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

        [JsonIgnore]
        public String AppointmentFullInfo
        {
            get => GetDoctorName() + ", " + GetAppointmentTime() + " " + GetAppointmentDate();
        }

    }
}