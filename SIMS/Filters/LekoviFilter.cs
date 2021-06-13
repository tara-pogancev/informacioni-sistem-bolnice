using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Filters
{
    class LekoviFilter : TableFilter<Medication>
    {

        public override bool KeywordFilter(Medication lek, string keyword)
        {
            return (lek.MedicineID.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    lek.MedicineName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    lek.ApprovalStatusString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
