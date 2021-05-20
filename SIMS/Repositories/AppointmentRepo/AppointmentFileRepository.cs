using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AppointmentRepo
{
    class IAppointmentFileRepository : GenericFileRepository<string, Appointment, IAppointmentFileRepository>, IAppointmentRepository
    {
        public void Delete(string key)
        {
            this.DeleteEntity(key);
        }

        public Appointment FindById(string key)
        {
            return this.ReadEntity(key);
        }

        public IEnumerable<Appointment> GetAll()
        {
            return this.ReadEntities();
        }

        public void Save(Appointment entity)
        {
            this.CreateEntity(entity);
        }

        public void Update(Appointment entity)
        {
            this.UpdateEntity(entity);
        }

        protected override string getKey(Appointment entity)
        {
            return entity.TerminKey;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\termini.json"; 
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetDoctorAppoinments(Doctor lekar)
        {
            List<Appointment> termini = new List<Appointment>();

            foreach (Appointment t in this.ReadEntities())
            {
                if (t.Lekar.EqualJmbg(lekar.Jmbg))
                    termini.Add(t);
            }

            return termini;
        }

        public IEnumerable<Appointment> GetPatientAppointments(Patient pacijent)
        {
            List<Appointment> termini = new List<Appointment>();

            foreach (Appointment t in this.ReadEntities())
            {
                if (t.Pacijent.EqualJmbg(pacijent.Jmbg))
                    termini.Add(t);
            }

            return termini;
        }
    }
}
