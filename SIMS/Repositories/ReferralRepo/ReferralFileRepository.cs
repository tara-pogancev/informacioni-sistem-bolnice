using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.ReferralRepo;

namespace SIMS.Model
{
    public class ReferralFileRepository : GenericFileRepository<string, Referral, ReferralFileRepository>,IReferralRepository
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

        protected override void shouldSerialize(Referral entity)
        {
        }
    }
}
