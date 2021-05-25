using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.DoctorSurveyRepo;
using System;
using System.Collections.Generic;

namespace SIMS.Service
{
    class DoctorService
    {
        private IDoctorRepository doctorRepository; 

        public DoctorService()
        {
            doctorRepository = new DoctorFileRepository();
        }

        public List<Doctor> GetAllDoctors()=>doctorRepository.GetAll();

        public void UpdateDoctor(Doctor doctor)=>doctorRepository.Update(doctor);

        public void DeleteDoctor(String key)=>doctorRepository.Delete(key);

        public void SaveDoctor(Doctor doctor)=>doctorRepository.Save(doctor);

        public void CreateOrUpdateDoctor(Doctor doctor)=>doctorRepository.CreateOrUpdate(doctor);

        public Doctor FindByIdDoctor(String key)=>doctorRepository.FindById(key);

        public List<Specialization> GetAvailableSpecialization()
        {
            return doctorRepository.GetAvailableSpecialization();
        }

        public List<string> GetAvailableSpecializationString()
        {
            return doctorRepository.GetAvailableSpecializationString();
        }

        public List<String> GetAllIds()
        {
            return doctorRepository.GetAllId();
        }

        public List<Doctor> ReadBySpecialization(Specialization specialization)
        {
            return doctorRepository.ReadBySpecialization(specialization);
        }

        public Doctor ReadUserByUsername(String username)
        {
            return doctorRepository.ReadUser(username);
        }

        // Salje informacije o novom terminu
        public Boolean CheckIfFree(Doctor doctor, Appointment newAppointment)
        {
            foreach (Appointment t in AppointmentFileRepository.Instance.GetDoctorAppointments(doctor))
            {
                if (newAppointment.GetEndTime() > t.StartTime && newAppointment.GetEndTime() <= t.GetEndTime())
                    return false;

                if (newAppointment.StartTime >= t.StartTime && newAppointment.StartTime < t.GetEndTime())
                    return false;
            }

            return true;
        }

        //Salje izmenjen termin ali njega ignorise prilikom provere
        public Boolean CheckIfFreeUpdate(Doctor doctor, Appointment newAppointment)
        {
            foreach (Appointment t in AppointmentFileRepository.Instance.GetDoctorAppointments(doctor))
            {
                if (t.AppointmentID != newAppointment.AppointmentID)
                {
                    if ((newAppointment.GetEndTime() > t.StartTime && newAppointment.GetEndTime() <= t.GetEndTime()) || 
                        (newAppointment.StartTime >= t.StartTime && newAppointment.StartTime < t.GetEndTime()))
                        return false;
                }
            }
            return true;
        }

        public void RecalulateGrade(Doctor doctor)
        {
            IDoctorSurveyRepository doctorSurveyRepository = new DoctorSurveyFileRepository();
            double Grades = 0;
            int counter = 0;

            foreach (var survey in doctorSurveyRepository.GetAll())
            {
                if (survey.DoctorId == doctor.Jmbg)
                {
                    Grades += survey.Grade;
                    counter++;
                }
            }
            if (counter == 0)
            {
                doctor.Grade = 0;
            }
            else
            {
                doctor.Grade = Grades / counter;
            }
        }

    }
}
