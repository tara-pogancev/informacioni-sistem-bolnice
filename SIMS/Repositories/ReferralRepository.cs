﻿using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.PatientRepo;

namespace SIMS.Model
{
    public class ReferralRepository : GenericFileRepository<string, Referral, ReferralRepository>
    {
        protected override string getKey(Referral entity)
        {
            return entity.RefferalKey;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\uputi.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }
    }
}
