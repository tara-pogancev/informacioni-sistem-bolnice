namespace SIMS.Model
{
    public class OccupiedRoom
    {
        public string RoomNumber { get; set; }
        public string OccupationTime { get; set; }

        public OccupiedRoom(string roomNumber)
        {
            RoomNumber = roomNumber;
        }
    }
}
