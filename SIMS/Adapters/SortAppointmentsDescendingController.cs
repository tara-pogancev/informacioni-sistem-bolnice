using SIMS.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Adapters
{
    public class SortAppointmentsDescendingController : ISortAppointments
    {
        private readonly ISortAppointments sortAppointmentsService = new SortAppointmentsDescendingService();

        public SortAppointmentsDescendingController()
        {

        }

        public void SortAppointments(List<AppointmentDTO> appointments)
        {
            sortAppointmentsService.SortAppointments(appointments);
        }
    }
}
