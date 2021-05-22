using SIMS.Repositories.PatientRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AppointmentRepo
{
    interface IAppointmentRepository : IGenericRepository<Appointment, String>
    {
        public List<int> GetAppointmentsCountForCurrentWeek(AppointmentType tip, Doctor l); //nisam siguran da li trebaju da idu u storage class ili u service* */
        public List<Appointment> GetPatientAppointments(Patient pacijent);
        public List<Appointment> GetDoctorAppointments(Doctor lekar);
    }
}