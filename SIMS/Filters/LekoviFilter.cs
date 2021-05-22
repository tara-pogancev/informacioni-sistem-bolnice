using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class LekoviFilter : TableFilter<Medication, LekoviFilter>
    {
        public override bool CheckBoxFilter(Medication lek, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Medication lek, string keyword)
        {
            return (lek.MedicineID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    lek.MedicineName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
