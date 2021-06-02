using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.InventoryRepo
{
    interface IInventoryRepository:IGenericRepository<Inventory,String>
    {
        
    }
}
