using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.ReceiptRepo
{
    interface IReceiptRepository:IGenericRepository<Receipt,String>
    {
        List<Receipt> ReadByPatient(Patient p);
    }
}
