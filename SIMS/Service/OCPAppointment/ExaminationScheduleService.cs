using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Service.OCPAppointment
{
    class ExaminationScheduleService : AppointmentScheduleAbstractService
    {
        protected override void SetDuration()
        {
            int[] durations = { 30, 60, 90 };
            foreach (int duration in durations)
                this.validAppointmentDuration.Add(duration);
        }

        protected override void SetRoomType()
        {
            RoomType[] types = { RoomType.eximantionRoom, RoomType.operatingRoom };
            foreach (RoomType type in types)
                this.validRoomType.Add(type);
        }

        protected override void SetSpecialization()
        {
            foreach (Specialization specialization in Enum.GetValues(typeof(Specialization)))
                this.validSpecialization.Add(specialization);
        }
    }
}
