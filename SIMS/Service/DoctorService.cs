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

        public List<Doctor> GetDoctorsForExamination()
        {
            List<Doctor> doctorsForExamination = GetAllDoctors();
            for (int i = 0; i < doctorsForExamination.Count; i++)
            {
                if (doctorsForExamination[i].DoctorSpecialization != Specialization.OpstaPraksa)
                {
                    doctorsForExamination.RemoveAt(i);
                    i--;
                }
            }

            return doctorsForExamination;
        }

        // Salje informacije o novom terminu
        public Boolean CheckIfFree(Doctor doctor, Appointment newAppointment)
        {
            foreach (Appointment currentAppointment in AppointmentFileRepository.Instance.GetDoctorAppointments(doctor))
            {
                if (newAppointment.GetEndTime() > currentAppointment.StartTime && newAppointment.GetEndTime() <= currentAppointment.GetEndTime())
                    return false;

                if (newAppointment.StartTime >= currentAppointment.StartTime && newAppointment.StartTime < currentAppointment.GetEndTime())
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
            foreach (Appointment currentAppointment in AppointmentFileRepository.Instance.GetDoctorAppointments(doctor))
            {
                if (currentAppointment.AppointmentID != newAppointment.AppointmentID)
                {
                    if ((newAppointment.GetEndTime() > currentAppointment.StartTime && newAppointment.GetEndTime() <= currentAppointment.GetEndTime()) ||
                        (newAppointment.StartTime >= currentAppointment.StartTime && newAppointment.StartTime < currentAppointment.GetEndTime()))
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

            new DoctorService().UpdateDoctor(doctor);
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

        public List<string> GetExaminationDoctorsID()
        {
            List<String> ids = new List<String>();
            List<Doctor> lekari = this.GetDoctorsForExamination();
            foreach (Doctor l in lekari)
            {
                ids.Add(l.Jmbg);
            }
            return ids;
        }

    }
}
