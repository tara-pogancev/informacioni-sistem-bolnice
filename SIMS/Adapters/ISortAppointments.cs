using SIMS.DTO;
using System.Collections.Generic;

namespace SIMS.Adapters
{
    public interface ISortAppointments
    {
        public void SortAppointments(List<AppointmentDTO> appointments);
    }
}
