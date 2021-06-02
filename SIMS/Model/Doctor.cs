// File:    Lekar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:32:13 PM
// Purpose: Definition of Class Lekar

using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorSurveyRepo;
using System;
using System.Collections.Generic;

namespace SIMS.Model
{
    public class Doctor : LoggedUser
    {
        public int VacationDays { get; set; }
        public double Grade { get; set; }
        public Specialization DoctorSpecialization { get; set; }

        public List<VacationPeriod> VacationPeriods { get; set; }

        public Doctor()
        {
            Grade = 0.0;
        }

        public Doctor(string name, string lastName, string jmbg, string username, string password, string email, 
            string phone, Address address, Specialization specialization, int vacationDays) 
            : base(name, lastName, jmbg, username, password, email, phone, address)
        {
            VacationDays = vacationDays;
            DoctorSpecialization = specialization;
            Grade = 0.0;
            VacationPeriods = new List<VacationPeriod>();
        }

        public Doctor(Doctor doctor)
        {
            Name = doctor.Name;
            LastName = doctor.LastName;
            Jmbg = doctor.Jmbg;
            Username = doctor.Username;
            Password = doctor.Password;
            Email = doctor.Email;
            Phone = doctor.Phone;
            Address = doctor.Address;
            Serialize = doctor.Serialize;
            VacationDays = doctor.VacationDays;
            DoctorSpecialization = doctor.DoctorSpecialization;
            Grade = doctor.Grade;
            VacationPeriods = doctor.VacationPeriods;
        }

        // Prebaciti u servis klasu?
        public bool Unavailable(Appointment appointment)
        {
            return appointment.Doctor.Jmbg == this.Jmbg;
        }

        public bool ShouldSerializeVacationDays()
        {
            return Serialize;
        }

        public bool ShouldSerializeDoctorSpecialization()
        {
            return Serialize;
        }

        public bool ShouldSerializeGrade()
        {
            return Serialize;
        }

        public bool ShouldSerializeVacationPeriods()
        {
            return Serialize;
        }

    }
}