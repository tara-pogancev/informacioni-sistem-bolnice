using SIMS.Repositories.PatientRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.PatientRepo
{
    interface IInventoryRepository:IGenericRepository<Inventory,String>
    {
    }
}
