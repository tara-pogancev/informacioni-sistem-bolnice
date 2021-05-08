using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace SIMS.Filters
{
    class InventarProstorijeFilter : TableFilter<Oprema, InventarProstorijeFilter>
    {
        public override bool CheckBoxFilter(Oprema oprema, bool checkboxChecked)
        {
            return !checkboxChecked || oprema.Kolicina != 0;
        }

        public override bool KeywordFilter(Oprema oprema, string keyword)
        {
            return (oprema.Id.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Naziv.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Kolicina.ToString().Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.TipToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
