using SIMS.DTO;
using System;

namespace SIMS.Filters
{
    class AppointmentsFilter : TableFilter<AppointmentDTO>
    {
        public override bool KeywordFilter(AppointmentDTO entity, string keyword)
        {
            return entity.DoctorName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   entity.PatientName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   entity.StartTime.ToString().Contains(keyword, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
