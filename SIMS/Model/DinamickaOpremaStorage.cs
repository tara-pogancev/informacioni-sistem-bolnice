using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DinamickaOpremaStorage : Storage<string, DinamickaOprema, DinamickaOpremaStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\dinamickaOprema.json";
        }
        protected override string getKey(DinamickaOprema dinamickaOprema)
        {
            return dinamickaOprema.id;
        }

        protected override void removeReferences(string key)
        {

        }
    }
}
