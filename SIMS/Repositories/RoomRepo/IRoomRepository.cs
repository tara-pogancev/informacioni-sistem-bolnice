using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.RoomRepo
{
    interface IRoomRepository:IGenericRepository<Room,String>
    {
        List<Room> UcitajProstorijeZaPreglede();
    }
}
