using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class AllergenRepository : GenericFileRepository<string, Allergen, AllergenRepository>
    {
        protected override string getKey(Allergen entity)
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
