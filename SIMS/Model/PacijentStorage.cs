// File:    PacijentStorage.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:17:45 PM
// Purpose: Definition of Class PacijentStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class PacijentStorage : Storage<string, Pacijent, PacijentStorage>
    {
        protected override string getKey(Pacijent entity)
        {
            return entity.Jmbg;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\pacijenti.json";
        }

        protected override void RemoveReferences(string key)
        {
            TerminStorage storageT = new TerminStorage();
            foreach (Termin t in storageT.ReadList())
            {
                if (t.PacijentKey == key)
                {
                    storageT.Delete(t.TerminKey);
                }
            }
        }
    }
}