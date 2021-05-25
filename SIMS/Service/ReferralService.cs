using SIMS.Repositories.ReferralRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Service
{
    public class ReferralService
    {
        private IReferralRepository referralRepository = new ReferralFileRepository();

        public ReferralService()
        {

        }

        public void UpdateReferral(Referral referral) => referralRepository.Update(referral);

        public void DeleteReferral(String referralKey) => referralRepository.Delete(referralKey);

        public void DeleteReferral(Referral referral) => referralRepository.Delete(referral.RefferalKey);

        public void SaveReferral(Referral referral) => referralRepository.Save(referral);

        public Referral GetReferral(String referralKey) => referralRepository.FindById(referralKey);


    }
}
