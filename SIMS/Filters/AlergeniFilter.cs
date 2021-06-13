using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class AlergeniFilter : TableFilter<Allergen>
    {
        public override bool KeywordFilter(Allergen alergen, string keyword)
        {
            return (alergen.ID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    alergen.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
