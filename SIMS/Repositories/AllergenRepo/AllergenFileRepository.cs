using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.AllergenRepo
{
    class AllergenFileRepository : GenericFileRepository<string, Allergen, AllergenFileRepository>,IAllergenRepository
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

        protected override void ShouldSerialize(Allergen entity)
        {
            //ne treba logika za serijalizaciju
        }
    }
}
