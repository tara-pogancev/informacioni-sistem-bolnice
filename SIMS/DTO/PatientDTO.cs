using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.DTO
{
    public class PatientDTO : Patient
    {
        public String BloodTypeString { get => GetBloodTypeString();  }
        public String GenderString { get => GetGenderString();  }
        public String DateOfBirthString { get => GetDateOfBirthString();  }
        public String IsGuest { get => GetIfGuestString(); }

        public PatientDTO()
        {
            
        }

        public PatientDTO(Patient patient) : base(patient)
        {

        }

    }
}
