using System;
using System.Collections.Generic;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Service;
using SIMS.Service.AppointmentServices;

namespace SIMS.Controller
{
    public class DoctorAppointmentController
    {
        private DoctorAppointmentService doctorAppointmentService;

        public DoctorAppointmentController()
        {
            this.doctorAppointmentService = new DoctorAppointmentService();
        }

        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            return doctorAppointmentService.GetAppointmentsByDoctor(doctor);
        }

        public Appointment CheckIfActiveAppointment(Doctor doctor)
        {
            return doctorAppointmentService.CheckIfActiveAppointment(doctor);
        }

        public int GetRecordedAppointmentsByDoctor(Doctor doctor)
        {
            return doctorAppointmentService.GetRecordedAppointmentsByDoctor(doctor);
        }

        public AppointmentDTO GetDTO(Appointment appointment)
        {
            return doctorAppointmentService.GetDTO(appointment);
        }

        public List<int> GetAppointmentsCountForCurrentWeek(AppointmentType type, Doctor doctor)
        {
            return doctorAppointmentService.GetAppointmentsCountForCurrentWeek(type, doctor);
        }

        public List<Appointment> GetUpcommingAppointmentsByDoctor(Doctor doctor)
        {
            return doctorAppointmentService.GetUpcommingAppointmentsByDoctor(doctor);
        }

        public List<AppointmentDTO> GetDTOFromList(List<Appointment> list)
        {
            return doctorAppointmentService.GetDTOFromList(list);
        }

        public List<Appointment> GetUnrecordedAppointmentsByDoctorList(Doctor doctor)
        {
            return doctorAppointmentService.GetUnrecordedAppointmentsByDoctorList(doctor);
        }

        public List<Appointment> GetRecordedAppointmentsByDoctorList(Doctor doctor) => doctorAppointmentService.GetRecordedAppointmentsByDoctorList(doctor);
        public List<Appointment> GetFutureAppointmentsByDoctor(Doctor doctor) => doctorAppointmentService.GetFutureAppointmentsByDoctor(doctor);
        public List<DateTime> GetNearPotentialAppointments() => doctorAppointmentService.GetNearPotentialAppointments();
        public List<Appointment> SortAppointmentsByTimeA(List<Appointment> appointments) => doctorAppointmentService.SortAppointmentsByTimeA(appointments);

        public List<Appointment> GetAvailableAppointmentsForAllDoctors(Specialization specialization, int duration, Patient patient) 
            =>
                doctorAppointmentService.GetAvailableAppointmentsForAllDoctors(specialization, duration, patient);
    }
}