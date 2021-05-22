using SIMS.Repositories.PatientRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.ManagerRepo
{
    interface IManagerRepository:IGenericRepository<Manager,String>
    {
        Manager ReadUser(String user);
    }
}
