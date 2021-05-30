using System.Collections.Generic;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    public class PatientAppointmentController
    {
        private PatientAppointmentsService patientAppointmentsService;

        public PatientAppointmentController()
        {
            this.patientAppointmentsService = new PatientAppointmentsService();
        }

        public List<Appointment> GetPatientAppointments(Patient patient) => patientAppointmentsService.GetPatientAppointments(patient);

        public int GetNumberOfFinishedAppointments(Patient patient)
        {
            return patientAppointmentsService.GetNumberOfFinishedAppointments(patient);
        }

        public List<Appointment> GetPastAppointmentsForPatient(Patient patient)
        {
            return patientAppointmentsService.GetPastAppointmentsForPatient(patient);
        }

        public List<Appointment> GetFutureAppointments(Patient patient)
        {
            return patientAppointmentsService.GetFutureAppointments(patient);
        }
    }
}