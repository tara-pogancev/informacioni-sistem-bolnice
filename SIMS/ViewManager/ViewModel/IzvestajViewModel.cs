using SIMS.Controller;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.ViewManager.ViewModel
{
    public class IzvestajViewModel
    {
        public DateTime pocetakIntervala { get; set; }
        public DateTime krajIntervala { get; set; }
        public List<Room> Renovations { get; set; }

        private RoomAvailibilityController roomAvailibilityController = new RoomAvailibilityController();

        public IzvestajViewModel(DateTime intervalStart, DateTime intervalEnd)
        {
            pocetakIntervala = intervalStart;
            krajIntervala = intervalEnd;
            Renovations = roomAvailibilityController.GetRoomsWithRenovationInInterval(intervalStart, intervalEnd);
        }
    }
}
