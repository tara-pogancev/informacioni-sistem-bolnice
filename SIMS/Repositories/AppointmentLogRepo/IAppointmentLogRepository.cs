using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.PatientRepo;
using SIMS.Model;

namespace SIMS.Repositories.AppointmentLogRepo
{
    interface IAppointmentLogRepository:IGenericRepository<AppointmentLog,String>
    {
        IEnumerable<AppointmentLog> GetPatientLogs(Patient patient);
        void MakeLogExpired(Patient pacijent); //mozda bi trebala da se nadje u servisu
    }
}
