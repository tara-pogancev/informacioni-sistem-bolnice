using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
namespace SIMS.Filters
{
    class InventarProstorijeFilter : TableFilter<Inventory, InventarProstorijeFilter>
    {
        public override bool CheckBoxFilter(Inventory oprema, bool checkboxChecked)
        {
            return !checkboxChecked || oprema.Kolicina != 0;
        }

        public override bool KeywordFilter(Inventory oprema, string keyword)
        {
            return (oprema.Id.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Naziv.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Kolicina.ToString().Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.TipToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
