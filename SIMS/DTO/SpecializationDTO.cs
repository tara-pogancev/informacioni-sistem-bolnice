using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.DTO
{
    public class SpecializationDTO
    {
        public String SpecializationName { get; set; }
        public Specijalizacija Specialization { get; set; }

        public SpecializationDTO()
        {

        }

        public SpecializationDTO(Specijalizacija specialization)
        {
            Specialization = specialization;
            SpecializationName = GetSpecializationName(specialization);
        }

        public String GetSpecializationName(Specijalizacija specialization)
        {
            if (specialization == Specijalizacija.OpstaPraksa)
                return "Lekar opšte prakse";
            else if (specialization == Specijalizacija.Hirurg)
                return "Hirurg";
            else if (specialization == Specijalizacija.Internista)
                return "Internista";
            else if (specialization == Specijalizacija.Dermatolog)
                return "Dermatolog";
            else if (specialization == Specijalizacija.Kardiolog)
                return "Kardiolog";
            else if (specialization == Specijalizacija.Otorinolaringolog)
                return "Otorinolaringolog";
            else if (specialization == Specijalizacija.Stomatolog)
                return "Stomatolog";
            else if (specialization == Specijalizacija.Urolog)
                return "Urolog";
            else if (specialization == Specijalizacija.Ginekolog)
                return "Ginekolog";
            else
                return "Neurolog";
        }


    }
}
