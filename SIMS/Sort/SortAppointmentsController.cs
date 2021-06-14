using SIMS.DTO;
using System.Collections.Generic;

namespace SIMS.Sort
{
    public class SortAppointmentsController : ISortAppointments
    {
        private readonly ISortAppointments sortAppointmentsService;

        public SortAppointmentsController(ISortAppointments sortAppointmentsService)
        {
            this.sortAppointmentsService = sortAppointmentsService;
        }

        public void SortAppointments(List<AppointmentDTO> appointments)
        {
            sortAppointmentsService.SortAppointments(appointments);
        }
    }
}
