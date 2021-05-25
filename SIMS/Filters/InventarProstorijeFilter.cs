using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;

namespace SIMS.Filters
{
    class InventarProstorijeFilter : TableFilter<Inventory, InventarProstorijeFilter>
    {
        public override bool CheckBoxFilter(Inventory oprema, bool checkboxChecked)
        {
            return !checkboxChecked || oprema.Amount != 0;
        }

        public override bool KeywordFilter(Inventory oprema, string keyword)
        {
            return (oprema.ID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Amount.ToString().Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.TypeToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
