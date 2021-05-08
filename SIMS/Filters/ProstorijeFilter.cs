using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class ProstorijeFilter : TableFilter<Prostorija, ProstorijeFilter>
    {
        public override bool CheckBoxFilter(Prostorija entity, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Prostorija prostorija, string keyword)
        {
            return (prostorija.Broj.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    prostorija.DostupnaToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    prostorija.TipProstorijeToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
