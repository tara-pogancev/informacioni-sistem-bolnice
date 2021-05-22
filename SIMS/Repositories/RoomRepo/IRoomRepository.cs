using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    interface IRoomRepository:IGenericRepository<Room,String>
    {
        List<Room> UcitajProstorijeZaPreglede();
    }
}
