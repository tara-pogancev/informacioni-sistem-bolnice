using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class AlergeniFilter : TableFilter<Alergen, AlergeniFilter>
    {
        public override bool CheckBoxFilter(Alergen alergen, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Alergen alergen, string keyword)
        {
            return (alergen.ID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    alergen.Naziv.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
