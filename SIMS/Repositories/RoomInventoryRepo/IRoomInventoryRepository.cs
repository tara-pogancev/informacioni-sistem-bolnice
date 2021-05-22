using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    interface IRoomInventoryRepository:IGenericRepository<RoomInventory,String>
    {
        RoomInventory ReadNoConsistifying(string brojProstorije, string idInventara);
        RoomInventory Read(string brojProstorije, string idInventara);
    }
}
