using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    interface IReceiptRepository:IGenericRepository<Receipt,String>
    {
        List<Receipt> ReadByPatient(Patient p);
    }
}
