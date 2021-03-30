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
        protected override void removeReferences(string key)
        {

        }
    }
}