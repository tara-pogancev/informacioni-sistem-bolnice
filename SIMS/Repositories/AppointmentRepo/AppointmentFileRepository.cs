using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AppointmentRepo
{
    class IAppointmentFileRepository : GenericFileRepository<string, Appointment, IAppointmentFileRepository>, IAppointmentRepository
    {
       

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

            foreach (Appointment t in this.GetAll())
            {
                if (t.Lekar.EqualJmbg(lekar.Jmbg))
                    termini.Add(t);
            }

            return termini;
        }

        public IEnumerable<Appointment> GetPatientAppointments(Patient pacijent)
        {
            List<Appointment> termini = new List<Appointment>();

            foreach (Appointment t in this.GetAll())
            {
                if (t.Pacijent.EqualJmbg(pacijent.Jmbg))
                    termini.Add(t);
            }

            return termini;
        }
    }
}
