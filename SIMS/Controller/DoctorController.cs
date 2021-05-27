﻿using System;
using System.Collections.Generic;
using SIMS.Service;
using SIMS.Model;

namespace SIMS.Controller
{
    public class DoctorController
    {
        private DoctorService doctorService;

        public DoctorController()
        {
            doctorService = new DoctorService();
        }

        public List<Specialization> GetAvailableSpecialization()
        {
            return doctorService.GetAvailableSpecialization();
        }

        public List<string> GetAvailableSpecializationString()
        {
            return doctorService.GetAvailableSpecializationString();
        }

        public List<String> GetAllIds()
        {
            return doctorService.GetAllIds();
        }

        public List<Doctor> ReadBySpecialization(Specialization specialization)
        {
            return doctorService.ReadBySpecialization(specialization);
        }

        public Doctor ReadUserByUsername(String username)
        {
            return doctorService.ReadUserByUsername(username);
        }

        // Salje informacije o novom terminu
        public Boolean CheckIfFree(Doctor doctor, Appointment newAppointment)
        {
            return doctorService.CheckIfFree(doctor, newAppointment);
        }

        //Salje izmenjen termin ali njega ignorise prilikom provere
        public Boolean CheckIfFreeUpdate(Doctor doctor, Appointment newAppointment)
        {
            return doctorService.CheckIfFreeUpdate(doctor, newAppointment);
        }

        public void RecalulateGrade(Doctor doctor)
        {
            doctorService.RecalulateGrade(doctor);
        }

    }
}