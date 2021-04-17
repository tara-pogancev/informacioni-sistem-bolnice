using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class AlergeniStorage : Storage<string, Alergen, AlergeniStorage>
    {
        protected override string getKey(Alergen entity)
        {
            return entity.ID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\alergeni.json";
        }

        protected override void RemoveReferences(string key)
        {
        }
    }
}
