using Model;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AppointmentLogRepo
{
    class AppointmentLogFileRepository : GenericFileRepository<string, AppointmentLog, AppointmentLogRepository>, IAppointmentLogRepository
    {
        public void Delete(string key)
        {
            this.DeleteEntity(key);
        }

        public AppointmentLog FindById(string key)
        {
            return this.ReadEntity(key);
        }

        public IEnumerable<AppointmentLog> GetAll()
        {
            return this.ReadEntities();
        }


        public void Save(AppointmentLog entity)
        {
            this.CreateEntity(entity);
        }

        public void Update(AppointmentLog entity)
        {
            this.UpdateEntity(entity);
        }

        public IEnumerable<AppointmentLog> GetPatientLogs(Patient patient)
        {
            List<AppointmentLog> appointmentLogs = ReadEntities();
            for (int i = 0; i < appointmentLogs.Count; i++)
            {
                if (patient.EqualJmbg(appointmentLogs[i].PacijentKey) || appointmentLogs[i].Istekao == true)
                {
                    appointmentLogs.RemoveAt(i);
                    i--;
                }
            }
            return appointmentLogs;
        }


        protected override string getKey(AppointmentLog entity)
        {
            return entity.TerminLogKey;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\terminLog.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }
    }
}
