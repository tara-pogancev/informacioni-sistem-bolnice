// File:    LekarStorage.cs
// Author:  paracelsus
// Created: Friday, March 26, 2021 4:38:57 PM
// Purpose: Definition of Class LekarStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class LekarStorage : Storage<string, Lekar, LekarStorage>
    {
        protected override string getKey(Lekar entity)
        {
            return entity.Jmbg;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\lekari.json";
        }

        protected override void RemoveReferences(string key)
        {
            TerminStorage storageT = new TerminStorage();
            foreach (Termin t in storageT.ReadList())
            {
                if (t.Lekar.Jmbg == key)
                {
                    storageT.Delete(t.TerminKey);
                }
            }
        }

        public Lekar ReadUser(String user)
        {
            foreach(Lekar l in this.ReadList())
            {
                if (l.KorisnickoIme == user)
                    return l;
            }

            return null;
        }

        public List<String> getAllId()
        {
            List<String> ids = new List<String>();
            List<Lekar> lekari = this.ReadList();
            foreach(Lekar l in lekari)
            {
                ids.Add(l.Jmbg);
            }
            return ids;
        }

        public List<Lekar> ReadBySpecialization(Specijalizacija specialization)
        {
            List<Lekar> retVal = new List<Lekar>();

            foreach(Lekar l in ReadList())
            {
                if (l.SpecijalizacijaLekara == specialization)
                    retVal.Add(l);
            }

            return retVal;
        }

        public List<Specijalizacija> GetAvailableSpecialization()
        {
            List<Specijalizacija> retVal = new List<Specijalizacija>();

            foreach (Lekar l in ReadList())
            {
                if (!retVal.Contains(l.SpecijalizacijaLekara))
                    retVal.Add(l.SpecijalizacijaLekara);
            }

            return retVal;
        }

        public List<String> GetAvailableSpecializationString()
        {
            List<String> retVal = new List<String>();

            foreach (Lekar l in ReadList())
            {
                if (!retVal.Contains(l.Specialization))
                    retVal.Add(l.Specialization);
            }

            return retVal;
        }

    }
}