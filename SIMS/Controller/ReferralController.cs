using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    public class ReferralController
    {
        private ReferralService referralService = new ReferralService();

        public ReferralController()
        {

        }

        public void UpdateReferral(Referral referral) => referralService.UpdateReferral(referral);

        public void DeleteReferral(String referralKey) => referralService.DeleteReferral(referralKey);

        public void DeleteReferral(Referral referral) => referralService.DeleteReferral(referral);

        public void SaveReferral(Referral referral) => referralService.SaveReferral(referral);

        public Referral GetReferral(String referralKey) => referralService.GetReferral(referralKey);


    }
}
