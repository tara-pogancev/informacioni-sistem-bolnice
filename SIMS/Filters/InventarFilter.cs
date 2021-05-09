using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class InventarFilter : TableFilter<Oprema, InventarFilter>
    {
        public override bool CheckBoxFilter(Oprema oprema, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Oprema oprema, string keyword)
        {
            return (oprema.Id.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Naziv.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.TipToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
