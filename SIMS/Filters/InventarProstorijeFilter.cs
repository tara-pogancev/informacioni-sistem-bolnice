using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;

namespace SIMS.Filters
{
    class InventarProstorijeFilter : TableCheckBoxFilter<Inventory>
    {
        public override bool CheckBoxFilter(Inventory inventory, bool checkboxChecked)
        {
            return !checkboxChecked || inventory.Amount != 0;
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
