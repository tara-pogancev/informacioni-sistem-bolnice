using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class ScheduleAppointmentService
    {
        private AppointmentService appointmentService;
        private IAppointmentRepository appointmentRepository;

        public ScheduleAppointmentService()
        {
            IAppointmentRepository appointmentRepository = new AppointmentFileRepository();
        }

        public void CancelAppointment(Appointment appointment)
        {
            appointmentService.DeleteAppointment(appointment);

            AppointmentLog terminLog = new AppointmentLog(appointment.AppointmentID + appointment.Patient.Jmbg + DateTime.Now.ToString("hhmmss"), appointment.AppointmentID, appointment.Patient.Jmbg, DateTime.Now, SurgeryType.Brisanje);
            new AppointmentLogFileRepository().Save(terminLog);
        }

        public void ChangeAppointment(Appointment appointment)
        {
            appointmentService.UpdateAppointment(appointment);
            AppointmentLog terminLog = new AppointmentLog(appointment.AppointmentID + appointment.Patient.Jmbg + DateTime.Now.ToString("hhmmss"), appointment.AppointmentID, appointment.Patient.Jmbg, DateTime.Now, SurgeryType.Brisanje);
            new AppointmentLogFileRepository().Save(terminLog);
        }

        public List<String> GetAvailableTimeOfAppointment(Doctor doctor, String date, Patient patient)
        {
            List<String> timeOfAppointment = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            foreach (Appointment appointment in GetScheduledAppointmentsForDate(date))
            {
                if (doctor.Unavailable(appointment) || patient.Unavailable(appointment))
                {
                    timeOfAppointment.Remove(appointment.GetAppointmentTime());
                }
            }
            return timeOfAppointment;
        }

        public bool ScheduleAppointment(Doctor doctor, DateTime date, Patient patient)
        {
            RoomAvailabilityService roomAvailabilityService = new RoomAvailabilityService();
            List<Room> availableRooms = roomAvailabilityService.GetAvailableRooms(date);
            if (!roomAvailabilityService.IsFreeRoomExists(date))
            {
                return false;
            }

            appointmentRepository.Save(new Appointment(date, 30, AppointmentType.examination, doctor, patient, availableRooms[0]));
            return true;
        }

        private List<Appointment> GetScheduledAppointmentsForDate(String date)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetAll();
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (scheduledAppointments[i].GetAppointmentDate() != date)
                {
                    scheduledAppointments.RemoveAt(i);
                    i--;
                }
            }
            return scheduledAppointments;
        }
    }

}
