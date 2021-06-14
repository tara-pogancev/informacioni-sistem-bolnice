using SIMS.DTO;
using System.Collections.Generic;

namespace SIMS.Sort
{
    public class SortAppointmentsAscendingService : ISortAppointments
    {
        public SortAppointmentsAscendingService()
        {

        }

        public void SortAppointments(List<AppointmentDTO> appointments)
        {
            for (int i = 0; i < appointments.Count - 1; i++)
                for (int j = 0; j < appointments.Count - i - 1; j++)
                    if (appointments[j].StartTime > appointments[j + 1].StartTime)
                    {
                        var temp = appointments[j];
                        appointments[j] = appointments[j + 1];
                        appointments[j + 1] = temp;
                    }
        }
    }
}
