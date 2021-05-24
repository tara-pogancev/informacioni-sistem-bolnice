using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.AppointmentRepo
{
    interface IAppointmentRepository : IGenericRepository<Appointment, String>
    {
        List<int> GetAppointmentsCountForCurrentWeek(AppointmentType tip, Doctor l); //nisam siguran da li trebaju da idu u storage class ili u service* */
        List<Appointment> GetPatientAppointments(Patient pacijent);
        List<Appointment> GetDoctorAppointments(Doctor lekar);
    }
}