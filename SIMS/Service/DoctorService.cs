using SIMS.DTO;
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
        private IDoctorRepository doctorRepository = new DoctorFileRepository();

        public DoctorService()
        {

        }

        public List<Doctor> GetAllDoctors() => doctorRepository.GetAll();

        public void UpdateDoctor(Doctor doctor) => doctorRepository.Update(doctor);

        public void DeleteDoctor(Doctor doctor) => doctorRepository.Delete(doctor.Jmbg);

        public void SaveDoctor(Doctor doctor) => doctorRepository.Save(doctor);

        public Doctor GetDoctor(String key) => doctorRepository.FindById(key);

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

        public List<DoctorDTO> GetAllDoctorsDTO()
        {
            List<Doctor> doctors = doctorRepository.GetAll();
            List<DoctorDTO> doctorsDTO = new List<DoctorDTO>();
            foreach (Doctor doctor in doctors)
            {
                doctorsDTO.Add(new DoctorDTO(doctor));
            }
            return doctorsDTO;
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

        public DoctorDTO GetDTO(Doctor doctor)
        {
            return new DoctorDTO(doctor);
        }

        public List<DoctorDTO> GetDTOFromList(List<Doctor> list)
        {
            List<DoctorDTO> retVal = new List<DoctorDTO>();
            foreach (Doctor doctor in list)
                retVal.Add(GetDTO(doctor));

            return retVal;
        }

        public bool OnVacation(Doctor doctor, DateTime dateTime)
        {
            foreach (VacationPeriod vacationPeriod in doctor.VacationPeriods)
            {
                if (dateTime >= vacationPeriod.StartTime && dateTime < vacationPeriod.EndTime)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
