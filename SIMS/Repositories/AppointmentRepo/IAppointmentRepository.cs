using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AppointmentRepo
{
    interface IAppointmentRepository : IGenericRepository<Appointment, String>
    {
        /*public IEnumerable<int> GetAppointmentsCountForCurrentWeek(AppointmentType tip, Doctor l); nisam siguran da li trebaju da idu u storage class ili u service* */
        public IEnumerable<Appointment> GetPatientAppointments(Patient pacijent);
        public IEnumerable<Appointment> GetDoctorAppoinments(Doctor lekar);
    }
}