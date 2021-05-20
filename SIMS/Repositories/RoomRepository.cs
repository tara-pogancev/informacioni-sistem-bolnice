using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Model
{
    public class RoomRepository : GenericFileRepository<string, Room, RoomRepository>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\prostorije.json";
        } 

        protected override string getKey(Room entity)
        {
            return entity.Number;
        }

        protected override void RemoveReferences(string key)
        {
            AppointmentRepository storageT = new AppointmentRepository();
            foreach (Appointment t in storageT.ReadEntities())
            {
                if (t.Prostorija.Number == key)
                {
                    storageT.DeleteEntity(t.TerminKey);
                }
            }

            foreach (var prosInv in RoomInventoryRepository.Instance.ReadAll().Values)
            {
                if (prosInv.BrojProstorije == key)
                {
                    RoomInventoryRepository.Instance.DeleteEntity(prosInv);
                }
            }

            foreach (var command in InventoryMovingCommandStorage.Instance.ReadAll().Values)
            {
                if (command.DstID == key || command.SrcID == key)
                {
                    InventoryMovingCommandStorage.Instance.DeleteEntity(command);
                }
            }
        }

        public List<Room> UcitajProstorijeZaPreglede()
        {
            List<Room> prostorije = ReadEntities();
            for (int i = 0; i < prostorije.Count; i++)
            {
                if (prostorije[i].RoomType != RoomType.zaPreglede || prostorije[i].Available==false)
                {
                    prostorije.RemoveAt(i);
                    i--;
                }
            }
            return prostorije;
        }
    }
}