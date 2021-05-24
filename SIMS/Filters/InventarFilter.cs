﻿using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

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
            return (oprema.ID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.TypeToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
