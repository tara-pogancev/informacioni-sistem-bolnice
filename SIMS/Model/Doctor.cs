// File:    Lekar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:32:13 PM
// Purpose: Definition of Class Lekar

using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.DoctorSurveyRepo;
using System;
using System.Collections.Generic;

namespace SIMS.Repositories.SecretaryRepo
{
    public class Doctor : LoggedUser
    {
        public int VacationDays { get; set; }
        public double Grade { get; set; }
        public Specialization DoctorSpecialization { get; set; }

        public Doctor()
        {
            Grade = 0.0;
        }

        public Doctor(string name, string lastName, string jmbg, string username, string password, string email, string phone, Address address, Specialization specialization, int vacationDays) : base(name, lastName, jmbg, username, password, email, phone, address)
        {
            VacationDays = vacationDays;
            DoctorSpecialization = specialization;
            Grade = 0.0;
        }
             
        public List<Appointment> getZauzetiTermini()
        {
            List<Appointment> retVal = new List<Appointment>();

            retVal = AppointmentFileRepository.Instance.GetDoctorAppointments(this);

            return retVal;

        }

        public bool Unavailable(Appointment appointment)
        {
            return appointment.Doctor.Jmbg == this.Jmbg;

        }

        // Salje informacije o novom terminu
        public Boolean IsFree(Appointment terminNew)
        {
            foreach (Appointment t in AppointmentFileRepository.Instance.GetDoctorAppointments(this))
            {
                if (terminNew.EndTime > t.StartTime && terminNew.EndTime <= t.EndTime)
                    return false;

                if (terminNew.StartTime >= t.StartTime && terminNew.StartTime < t.EndTime)
                    return false;
            }

            return true;
        }

        public void RecalulateGrade()
        {
            IDoctorSurveyRepository doctorSurveyRepository = new DoctorSurveyFileRepository();
            double Grades = 0;
            int counter = 0;
            
            foreach (var survey in doctorSurveyRepository.GetAll())
            {
                if (survey.DoctorId == this.Jmbg){
                    Grades += survey.Ocjena;
                    counter++;
                }
            }
            if (counter == 0)
            {
                this.Grade = 0;
            }
            else
            {
                this.Grade = Grades / counter;
            }              

        }

        // Salje izmenjen termin ali njega ignorise prilikom provere
        public Boolean IsFreeUpdate(Appointment terminNew)
        {
            foreach (Appointment t in AppointmentFileRepository.Instance.GetDoctorAppointments(this))
            {
                if (t.AppointmentID != terminNew.AppointmentID)
                {
                    if (terminNew.EndTime > t.StartTime && terminNew.EndTime <= t.EndTime)
                        return false;

                    if (terminNew.StartTime >= t.StartTime && terminNew.StartTime < t.EndTime)
                        return false;
                }
            }
            return true;
        }

        public bool ShouldSerializeDaniGodisnjegOdmora()
        {
            return Serialize;
        }

        public bool ShouldSerializeSpecijalizacijaLekara()
        {
            return Serialize;
        }

        [JsonIgnore]
        public String NameAndSpecialization { get { return FullName + ", " + Specialization; } }

        [JsonIgnore]
        public String Specialization 
        { 
            get 
            {
                if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.OpstaPraksa)
                    return "Lekar opšte prakse";
                else if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.Hirurg)
                    return "Hirurg";
                else if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.Internista)
                    return "Internista";
                else if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.Dermatolog)
                    return "Dermatolog";
                else if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.Kardiolog)
                    return "Kardiolog";
                else if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.Otorinolaringolog)
                    return "Otorinolaringolog";
                else if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.Stomatolog)
                    return "Stomatolog";
                else if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.Urolog)
                    return "Urolog";
                else if (DoctorSpecialization == SIMS.Repositories.SecretaryRepo.Specialization.Ginekolog)
                    return "Ginekolog";
                else
                    return "Neurolog";
            } 

        }       

    }
}