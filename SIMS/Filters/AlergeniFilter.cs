using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class AlergeniFilter : TableFilter<Allergen, AlergeniFilter>
    {
        public override bool CheckBoxFilter(Allergen alergen, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Allergen alergen, string keyword)
        {
            return (alergen.ID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    alergen.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
