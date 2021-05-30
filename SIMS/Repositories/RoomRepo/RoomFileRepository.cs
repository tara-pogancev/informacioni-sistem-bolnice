using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using SIMS.Model;

namespace SIMS.Repositories.SecretaryRepo
{
    public class RoomFileRepository : GenericFileRepository<string, Room, RoomFileRepository>,IRoomRepository
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
            AppointmentFileRepository storageT = new AppointmentFileRepository();
            foreach (Appointment t in storageT.GetAll())
            {
                if (t.Room.Number == key)
                {
                    storageT.Delete(t.AppointmentID);
                }
            }

            foreach (var prosInv in RoomInventoryFileRepository.Instance.ReadAll().Values)
            {
                if (prosInv.BrojProstorije == key)
                {
                    RoomInventoryFileRepository.Instance.Delete(prosInv);
                }
            }

            foreach (var command in InventoryMovingCommandFileRepository.Instance.ReadAll().Values)
            {
                if (command.DstID == key || command.SrcID == key)
                {
                    InventoryMovingCommandFileRepository.Instance.Delete(command);
                }
            }
        }

        public List<Room> UcitajProstorijeZaPreglede()
        {
            List<Room> prostorije = GetAll();
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

        protected override void ShouldSerialize(Room entity)
        {
        }
    }
}