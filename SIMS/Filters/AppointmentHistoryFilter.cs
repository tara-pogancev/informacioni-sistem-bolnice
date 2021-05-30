using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class AppointmentHistoryFilter : TableFilter<Appointment, AppointmentHistoryFilter>
    {
        public override bool CheckBoxFilter(Appointment appointment, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Appointment appointment, string keyword)
        {
            return(appointment.Doctor.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   appointment.Doctor.LastName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                   ||appointment.Room.Number.Contains(keyword,StringComparison.InvariantCultureIgnoreCase)||
                     appointment.StartTime.ToString("dd.MM.yyyy.").Contains(keyword,StringComparison.InvariantCultureIgnoreCase)||
                      appointment.StartTime.ToString("hh:mm").Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
