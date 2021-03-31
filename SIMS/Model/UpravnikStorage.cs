// File:    UpravnikStorage.cs
// Author:  paracelsus
// Created: Thursday, March 25, 2021 4:23:30 PM
// Purpose: Definition of Class UpravnikStorage

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Model
{
    public class UpravnikStorage : Storage<string, Upravnik, UpravnikStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\upravnici.json";
        }
        protected override string getKey(Upravnik entity)
        {
            return entity.Jmbg;
        }
        protected override void RemoveReferences(string key)
        {

        }

        public Upravnik ReadUser(String user)
        {
            foreach (Upravnik u in this.ReadList())
            {
                if (u.KorisnickoIme == user)
                    return u;
            }

            return null;
        }
    }
}