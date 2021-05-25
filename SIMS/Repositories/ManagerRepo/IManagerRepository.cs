using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.ManagerRepo
{
    interface IManagerRepository:IGenericRepository<Manager,String>
    {
        Manager ReadUser(String user);
    }
}
