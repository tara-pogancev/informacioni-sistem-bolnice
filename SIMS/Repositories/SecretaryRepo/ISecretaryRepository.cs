using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    interface ISecretaryRepository:IGenericRepository<Secretary,String>
    {
        Secretary ReadUser(String user);
    }
}
