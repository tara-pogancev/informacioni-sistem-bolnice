using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OpremaStorage : Storage<string, Oprema, OpremaStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\oprema.json";
        }
        protected override string getKey(Oprema dinamickaOprema)
        {
            return dinamickaOprema.Id;
        }

        protected override void RemoveReferences(string key)
        {

        }
    }
}
