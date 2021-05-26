using SIMS.DTO;
using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    public class AppointmentController
    {
        private AppointmentService appointmentService;

        public AppointmentController()
        {
            appointmentService = new AppointmentService();
        }

        public List<Appointment> GetAllAppointments()
        {
            return appointmentService.GetAllAppointments();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            appointmentService.UpdateAppointment(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            appointmentService.DeleteAppointment(appointment);
        }

        public void SaveAppointment(Appointment appointment)
        {
            appointmentService.SaveAppointment(appointment);
        }

        public Appointment GetAppointment(String key)
        {
            return appointmentService.GetAppointment(key);
        }

        public List<String> GetAvailableTimeOfAppointment(Doctor doctor, String date, Patient patient)
        {
            return appointmentService.GetAvailableTimeOfAppointment(doctor, date, patient);
        }

        public bool ScheduleAppointment(Doctor doctor, DateTime date, Patient patient)
        {
            return appointmentService.ScheduleAppointment(doctor, date, patient);
        }

        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            return appointmentService.GetAppointmentsByDoctor(doctor);
        }

        public Appointment CheckIfActiveAppointment(Doctor doctor)
        {
            return appointmentService.CheckIfActiveAppointment(doctor);
        }

        public int GetRecordedAppointmentsByDoctor(Doctor doctor)
        {
            return appointmentService.GetRecordedAppointmentsByDoctor(doctor);
        }

        public int GetNumberOfFinishedAppointments(Patient patient)
        {
            return appointmentService.GetNumberOfFinishedAppointments(patient);
        }

        public List<Appointment> GetPastAppointments()
        {
            return appointmentService.GetPastAppointments();
        }

        public List<Appointment> GetFutureAppointments(Patient patient)
        {
            return appointmentService.GetFutureAppointments(patient);
        }

        public void CancelAppointment(Appointment appointment)
        {
            appointmentService.CancelAppointment(appointment);
        }

        public void ChangeAppointment(Appointment appointment)
        {
            appointmentService.ChangeAppointment(appointment);
        }

        public AppointmentDTO GetDTO(Appointment appointment)
        {
            return appointmentService.GetDTO(appointment);
        }

        public List<int> GetAppointmentsCountForCurrentWeek(AppointmentType type, Doctor doctor)
        {
            return appointmentService.GetAppointmentsCountForCurrentWeek(type, doctor);
        }

        public List<Appointment> GetUpcommingAppointmentsByDoctor(Doctor doctor)
        {
            return appointmentService.GetUpcommingAppointmentsByDoctor(doctor);
        }

        public List<AppointmentDTO> GetDTOFromList(List<Appointment> list)
        {
            return appointmentService.GetDTOFromList(list);
        }

        public List<Appointment> GetUnrecordedAppointmentsByDoctorList(Doctor doctor)
        {
            return appointmentService.GetUnrecordedAppointmentsByDoctorList(doctor);
        }

        public List<Appointment> GetRecordedAppointmentsByDoctorList(Doctor doctor) => appointmentService.GetRecordedAppointmentsByDoctorList(doctor);

        public List<Appointment> GetFutureAppointmentsByDoctor(Doctor doctor) => appointmentService.GetFutureAppointmentsByDoctor(doctor);

        public List<DateTime> GetNearPotentialAppointments() => appointmentService.GetNearPotentialAppointments();

        public List<Appointment> SortAppointmentsByTimeA(List<Appointment> appointments) => appointmentService.SortAppointmentsByTimeA(appointments);

        public List<Appointment> GetAvailableAppointmentsForAllDoctors(Specialization specialization, int duration, Patient patient) 
            => appointmentService.GetAvailableAppointmentsForAllDoctors(specialization, duration, patient);


    }
}
