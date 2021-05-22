using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AllergenRepo
{
    class AllergenFileRepository:GenericFileRepository<string, Allergen, AllergenRepository>,IAllergenRepository
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
