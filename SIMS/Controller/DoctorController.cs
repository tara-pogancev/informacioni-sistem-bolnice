using System;
using System.Collections.Generic;
using SIMS.Service;
using SIMS.Model;
using SIMS.DTO;

namespace SIMS.Controller
{
    public class DoctorController
    {
        private DoctorService doctorService;

        public DoctorController()
        {
            doctorService = new DoctorService();
        }

        public List<Doctor> GetAllDoctors() => doctorService.GetAllDoctors();

        public void UpdateDoctor(Doctor doctor) => doctorService.UpdateDoctor(doctor);

        public void DeleteDoctor(Doctor doctor) => doctorService.DeleteDoctor(doctor);

        public void SaveDoctor(Doctor doctor) => doctorService.SaveDoctor(doctor);

        public Doctor GetDoctor(String key) => doctorService.GetDoctor(key);

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

        public List<DoctorDTO> GetAllDoctorsDTO()
        {
            return doctorService.GetAllDoctorsDTO();
        }
        public DoctorDTO GetDTO(Doctor doctor)
        {
            return doctorService.GetDTO(doctor);
        }

        public List<DoctorDTO> GetDTOFromList(List<Doctor> list)
        {
            return doctorService.GetDTOFromList(list);
        }

    }
}
