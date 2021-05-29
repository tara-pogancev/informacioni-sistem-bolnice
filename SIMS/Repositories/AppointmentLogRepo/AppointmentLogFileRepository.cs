using SIMS.Model;
using SIMS.Repositories.AppointmentLogRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AppointmentLogRepo
{
    class AppointmentLogFileRepository : GenericFileRepository<string, AppointmentLog, AppointmentLogFileRepository>,IAppointmentLogRepository
    {
        protected override string getKey(AppointmentLog entity)
        {
            return entity.AppointmentLogID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\terminLog.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(AppointmentLog entity)
        {
            //ne treba implementacija
        }
    }
}
