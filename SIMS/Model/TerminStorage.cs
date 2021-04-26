using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Model
{
    public class TerminStorage : Storage<string, Termin, TerminStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\termini.json";
        }

        public List<Termin> ReadByPatient(Pacijent p)
        {
            List<Termin> retVal = new List<Termin>();

            foreach (Termin t in this.ReadList())
            {
                if (t.Pacijent.Jmbg == p.Jmbg)
                    retVal.Add(t);
            }

            return retVal;
        }

        public List<Termin> ReadByDoctor(Lekar l)
        {
            List<Termin> retVal = new List<Termin>();

            foreach(Termin t in this.ReadList())
            {
                if (t.Lekar.Jmbg == l.Jmbg)
                    retVal.Add(t);
            }

            return retVal;
        }

        protected override string getKey(Termin entity)
        {
            return entity.TerminKey;
        }

        protected override void RemoveReferences(string key)
        {
            //Metoda jos uvek nije neophodna za klasu TerminStorage
            return;
        }

        public bool UpdateSingle(Termin termin, String keyOld)
        {
            Dictionary<String, Termin> entities = this.ReadAll();

            String key = keyOld;

            if (!entities.ContainsKey(key))
            {
                return false;
            }

            entities[key] = termin;

            string path = this.getPath();
            string json = System.Text.Json.JsonSerializer.Serialize(entities);

            File.WriteAllText(path, json);

            return true;

        }

        public int getTerminByDateAndType(DateTime date, TipTermina tip)
        {
            List<Termin> retVal = new List<Termin>();

            foreach (Termin t in TerminStorage.Instance.ReadList())
            {
                DateTime day = t.PocetnoVreme.Date;
                if (t.VrstaTermina == tip && day == date)
                    retVal.Add(t);
            }

            return retVal.Count;
        }

        public List<int> getTerminByWeekAndType(TipTermina tip)
        {
            List<int> retVal = new List<int>();

            DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek) + 1);

            for (int i = 0; i < 5; i++)
            {
                DateTime dayOfWeek = startOfWeek.AddDays(i);
                retVal.Add(getTerminByDateAndType(dayOfWeek, tip));
            }

            return retVal;
        }

    }

}      