using SIMS.DTO;
using System.Collections.Generic;

namespace SIMS.Adapters
{
    public class SortAppointmentsAscendingController : ISortAppointments
    {
        private readonly ISortAppointments sortAppointmentsService = new SortAppointmentsAscendingService();
        public SortAppointmentsAscendingController()
        {

        }

        public void SortAppointments(List<AppointmentDTO> appointments)
        {
            sortAppointmentsService.SortAppointments(appointments);
        }
    }
}
