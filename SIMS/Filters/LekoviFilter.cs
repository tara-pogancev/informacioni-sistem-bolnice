using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class LekoviFilter : TableFilter<Lek, LekoviFilter>
    {
        public override bool CheckBoxFilter(Lek lek, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Lek lek, string keyword)
        {
            return (lek.MedicineID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    lek.MedicineName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
