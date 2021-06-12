using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Filters
{
    class InventarFilter : TableFilter<Inventory>
    {

        public override bool KeywordFilter(Inventory oprema, string keyword)
        {
            return (oprema.ID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.TypeToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
