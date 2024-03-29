﻿using SIMS.Model;
using System;

namespace SIMS.Filters
{
    class PatientsFilter : TableFilter<Patient>
    {
        public override bool KeywordFilter(Patient entity, string keyword)
        {
            return entity.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   entity.LastName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   entity.Jmbg.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   entity.Lbo.Contains(keyword, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
