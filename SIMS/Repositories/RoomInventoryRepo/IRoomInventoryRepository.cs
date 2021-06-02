using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.RoomInventoryRepo
{
    public interface IRoomInventoryRepository:IGenericRepository<RoomInventory,String>
    {
        RoomInventory ReadNoConsistifying(string brojProstorije, string idInventara);
        RoomInventory Read(string brojProstorije, string idInventara);
    }
}
