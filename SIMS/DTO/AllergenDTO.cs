using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
using SIMS.Model;

namespace SIMS.DTO
{
    public class AllergenDTO
    {
        public string AlergenID { get; set; }
        public string AlergenNaziv { get; set; }
        public Boolean IsIncludedInMedicine { get; set; }

        public AllergenDTO()
        {

        }

        public AllergenDTO(Allergen alergen, Medication medicine)
        {
            AlergenID = alergen.ID;
            AlergenNaziv = alergen.Name;
            IsIncludedInMedicine = medicine.IncludesAllergen(alergen);
        }

        public List<AllergenDTO> GetAllAlergenList(Medication medicine)
        {
            List<AllergenDTO> retVal = new List<AllergenDTO>();

            foreach (Allergen currentAlergen in AllergenFileRepository.Instance.GetAll())
                retVal.Add(new AllergenDTO(currentAlergen, medicine));

            return retVal;
        }

    }
}
