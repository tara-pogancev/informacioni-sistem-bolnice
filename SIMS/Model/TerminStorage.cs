using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            List<Termin> termini_all = new List<Termin>(this.ReadAll().Values.ToList());
            for (int i = 0; i < termini_all.Count; i++)
            {
                if (!termini_all[i].PacijentKey.Equals(p.Jmbg))
                {
                    termini_all.RemoveAt(i);
                    i--;
                }
            }
            return termini_all;
        }

        public List<Termin> ReadByDoctor(Lekar l)
        {
            List<Termin> termini_all = new List<Termin>(this.ReadAll().Values.ToList());
            for (int i = 0; i < termini_all.Count; i++)
            {
                if (!termini_all[i].LekarKey.Equals(l.Jmbg))
                {
                    termini_all.RemoveAt(i);
                    i--;
                }
            }
            return termini_all;
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

        public void UpdateSingle()
        {
            //TODO
        }




    }

}      