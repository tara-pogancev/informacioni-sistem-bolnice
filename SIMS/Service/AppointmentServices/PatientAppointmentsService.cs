using System.Collections.Generic;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;

namespace SIMS.Service.AppointmentServices
{
    public class PatientAppointmentsService
    {
        private IAppointmentRepository appointmentRepository;

        public PatientAppointmentsService()
        {
            this.appointmentRepository = new AppointmentFileRepository();
        }

        public List<Appointment> GetPatientAppointments(Patient pacijent) => appointmentRepository.GetPatientAppointments(pacijent);

        public int GetNumberOfFinishedAppointments(Patient patient)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetPatientAppointments(patient);
            int finishedAppointmentCounter = 0;
            foreach (Appointment appointment in scheduledAppointments)
            {
                if (appointment.GetIfPast())
                {
                    finishedAppointmentCounter++;
                }
            }
            return finishedAppointmentCounter;
        }

        public List<Appointment> GetPastAppointmentsForPatient(Patient patient){
            List<Appointment> scheduledAppointments = appointmentRepository.GetPatientAppointments(patient);
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (!scheduledAppointments[i].GetIfPast() || scheduledAppointments[i].Patient.Jmbg!=patient.Jmbg)
                {
                    scheduledAppointments.RemoveAt(i);
                    i--;
                }
            }
            return scheduledAppointments;
        }

        public List<Appointment> GetFutureAppointments(Patient patient)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetPatientAppointments(patient);
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (scheduledAppointments[i].GetIfPast())
                {
                    scheduledAppointments.RemoveAt(i);
                    i--;
                }
            }
            return scheduledAppointments;
        }
    }
}