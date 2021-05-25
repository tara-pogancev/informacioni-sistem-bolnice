using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;

namespace SIMS.Repositories.AppointmentLogRepo
{
    interface IAppointmentLogRepository:IGenericRepository<AppointmentLog,String>
    {
        
    }
}
