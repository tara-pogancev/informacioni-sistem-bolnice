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


        
    }
}