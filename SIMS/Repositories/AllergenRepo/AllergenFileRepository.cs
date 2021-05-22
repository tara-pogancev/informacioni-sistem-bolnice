﻿using SIMS.Repositories.PatientRepo;
using SIMS.Repositories.AllergenRepo;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
