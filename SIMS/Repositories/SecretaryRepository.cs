// File:    SekretarStorage.cs
// Author:  paracelsus
// Created: Friday, March 26, 2021 4:38:56 PM
// Purpose: Definition of Class SekretarStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class SecretaryRepository : Repository<string, Secretary, SecretaryRepository>
    {
        protected override string getKey(Secretary entity)
        {
            return entity.Jmbg;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\sekretari.json";
        }

        protected override void RemoveReferences(string key)
        {
            
        }

        public Secretary ReadUser(String user)
        {
            foreach (Secretary s in this.ReadList())
            {
                if (s.KorisnickoIme == user)
                    return s;
            }

            return null;
        }
    }
}