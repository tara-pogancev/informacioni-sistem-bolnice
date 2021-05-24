using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Filters
{
    class InventarFilter : TableFilter<Inventory, InventarFilter>
    {
        public override bool CheckBoxFilter(Inventory oprema, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Inventory oprema, string keyword)
        {
            return (oprema.Id.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Naziv.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.TipToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
