using SIMS.Repositories.SecretaryRepo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SIMS.Model;

namespace SIMS.Repositories.AppointmentRepo
{
    public class AppointmentFileRepository : GenericFileRepository<string, Appointment, AppointmentFileRepository>,IAppointmentRepository
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\termini.json";
        }

        protected override void shouldSerialize(Appointment entity)
        {
            entity.Patient.Serialize = false;
            entity.Doctor.Serialize = false;
            entity.Room.Serialize = false;
        }

        public List<Appointment> GetPatientAppointments(Patient pacijent)
        {
            List<Appointment> termini = new List<Appointment>();

            foreach (Appointment t in this.GetAll())
            {
                if (pacijent.EqualJmbg(t.Patient.Jmbg))
                    termini.Add(t);
            }

            return termini;
        }


        public List<Appointment> GetDoctorAppointments(Doctor lekar)
        {
            List<Appointment> termini = new List<Appointment>();

            foreach(Appointment t in this.GetAll())
            {
                if (lekar.EqualJmbg(t.Doctor.Jmbg))
                    termini.Add(t);
            }

            return termini;
        }

        protected override string getKey(Appointment entity)
        {
            return entity.AppointmentID;
        }

        protected override void RemoveReferences(string key)
        {
            //Metoda jos uvek nije neophodna za klasu TerminStorage
            return;
        }

       

        private int getAppointmentsCountByDate(DateTime date, AppointmentType tip, Doctor l)
        {
            List<Appointment> retVal = new List<Appointment>();

            foreach (Appointment t in AppointmentFileRepository.Instance.GetDoctorAppointments(l))
            {
                DateTime day = t.StartTime.Date;
                if (t.Type == tip && day == date)
                    retVal.Add(t);
            }

            return retVal.Count;
        }

        public List<int> GetAppointmentsCountForCurrentWeek(AppointmentType tip, Doctor l)
        {
            List<int> retVal = new List<int>();
            DateTime startOfWeek = GetStartOfWeek();

            for (int i = 0; i < 7; i++)
            {
                DateTime dayOfWeek = startOfWeek.AddDays(i);
                retVal.Add(getAppointmentsCountByDate(dayOfWeek, tip, l));
            }

            return retVal;
        }

        private static DateTime GetStartOfWeek()
        {
            return DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek) + 1);
        }

        
    }

}      