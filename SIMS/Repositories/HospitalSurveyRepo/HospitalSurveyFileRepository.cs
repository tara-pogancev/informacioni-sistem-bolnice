﻿using SIMS.Repositories.SecretaryRepo;
using SIMS.PacijentGUI;
using SIMS.Repositories.HospitalSurveyRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class HospitalSurveyFileRepository : GenericFileRepository<string, HospitalSurvey, HospitalSurveyFileRepository>,IHospitalSurveyRepository
    {
        protected override string getKey(HospitalSurvey entity)
        {
            return entity.SurveyID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\anketaBolnice.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(HospitalSurvey entity)
        {
            //ne treba logika za serijalizaciju
        }
    }
}