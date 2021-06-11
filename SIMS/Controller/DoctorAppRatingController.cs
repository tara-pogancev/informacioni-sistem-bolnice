using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    public class DoctorAppRatingController
    {
        private DoctorAppRatingService doctorAppRatingService = new DoctorAppRatingService();

        public void SaveRating(DoctorAppRating rating) => doctorAppRatingService.SaveRating(rating);

        public void UpdateRating(DoctorAppRating rating) => doctorAppRatingService.UpdateRating(rating);

    }
}
