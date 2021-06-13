using SIMS.Model;
using System;

namespace SIMS.Filters
{
    class PatientsFilter : TableFilter<Patient, PatientsFilter>
    {
        public override bool CheckBoxFilter(Patient entity, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Patient entity, string keyword)
        {
            return entity.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   entity.LastName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
