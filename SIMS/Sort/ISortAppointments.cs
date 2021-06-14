using SIMS.DTO;
using System.Collections.Generic;

namespace SIMS.Sort
{
    public interface ISortAppointments
    {
        public void SortAppointments(List<AppointmentDTO> appointments);
    }
}
