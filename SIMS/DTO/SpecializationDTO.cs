using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.DTO
{
    public class SpecializationDTO
    {
        public String SpecializationName { get; set; }
        public Specialization Specialization { get; set; }

        public SpecializationDTO()
        {

        }

        public SpecializationDTO(Specialization specialization)
        {
            Specialization = specialization;
            SpecializationName = GetSpecializationName(specialization);
        }

        public String GetSpecializationName(Specialization specialization)
        {
            if (specialization == Specialization.OpstaPraksa)
                return "Lekar opšte prakse";
            else if (specialization == Specialization.Hirurg)
                return "Hirurg";
            else if (specialization == Specialization.Internista)
                return "Internista";
            else if (specialization == Specialization.Dermatolog)
                return "Dermatolog";
            else if (specialization == Specialization.Kardiolog)
                return "Kardiolog";
            else if (specialization == Specialization.Otorinolaringolog)
                return "Otorinolaringolog";
            else if (specialization == Specialization.Stomatolog)
                return "Stomatolog";
            else if (specialization == Specialization.Urolog)
                return "Urolog";
            else if (specialization == Specialization.Ginekolog)
                return "Ginekolog";
            else
                return "Neurolog";
        }


    }
}
