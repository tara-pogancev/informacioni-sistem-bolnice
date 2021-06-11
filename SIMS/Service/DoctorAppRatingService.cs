using SIMS.Model;
using SIMS.Repositories.DoctorAppRatingRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class DoctorAppRatingService
    {
        private IDoctorAppRatingRepository doctorAppRatingRepository = new DoctorAppRatingFileRepository();

        public void SaveRating(DoctorAppRating rating) => doctorAppRatingRepository.Save(rating);

        public void UpdateRating(DoctorAppRating rating) => doctorAppRatingRepository.Update(rating);


    }
}
