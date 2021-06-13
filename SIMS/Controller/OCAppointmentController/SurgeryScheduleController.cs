using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service.OCPAppointment;

namespace SIMS.Controller
{
    public class SurgeryScheduleController
    {
        private SurgeryScheduleService scheduleService = new SurgeryScheduleService();

        public int GetDurationFromString(String duration) => scheduleService.GetDurationFromString(duration);

        public List<Doctor> GetDoctorsForAppointment() => scheduleService.GetDoctorsForAppointment();

        public List<Room> GetRoomsForAppointment() => scheduleService.GetRoomsForAppointment();

        public List<String> GetDurationList() => scheduleService.GetDurationList();


    }
}
