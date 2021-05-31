using System;
using Newtonsoft.Json;
using SIMS.Model;

namespace SIMS.DTO
{
    public class DoctorDTO : Doctor
    {
        [JsonIgnore]
        public String NameAndSpecialization { get { return FullName + ", " + SpecializationString; } }

        [JsonIgnore]
        public String SpecializationString
        {
            get
            {
                if (DoctorSpecialization == Specialization.OpstaPraksa)
                    return "Lekar opšte prakse";
                else if (DoctorSpecialization == Specialization.Hirurg)
                    return "Hirurg";
                else if (DoctorSpecialization == Specialization.Internista)
                    return "Internista";
                else if (DoctorSpecialization == Specialization.Dermatolog)
                    return "Dermatolog";
                else if (DoctorSpecialization == Specialization.Kardiolog)
                    return "Kardiolog";
                else if (DoctorSpecialization == Specialization.Otorinolaringolog)
                    return "Otorinolaringolog";
                else if (DoctorSpecialization == Specialization.Stomatolog)
                    return "Stomatolog";
                else if (DoctorSpecialization == Specialization.Urolog)
                    return "Urolog";
                else if (DoctorSpecialization == Specialization.Ginekolog)
                    return "Ginekolog";
                else
                    return "Neurolog";
            }

        }

        public DoctorDTO()
        {

        }

        public DoctorDTO(Doctor doctor) : base(doctor)
        {

        }

    }
}
