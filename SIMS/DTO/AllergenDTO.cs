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
        public string AllergenID { get; set; }
        public string AllergenName { get; set; }
        public Boolean IsIncludedInMedicine { get; set; }

        public AllergenDTO()
        {

        }

        public AllergenDTO(Component alergen, Medication medicine)
        {
            AllergenID = alergen.ID;
            AllergenName = alergen.Name;
            IsIncludedInMedicine = medicine.IncludesAllergen(alergen);
        }

        public List<AllergenDTO> GetAllAlergenList(Medication medicine)
        {
            List<AllergenDTO> retVal = new List<AllergenDTO>();

            foreach (Component currentAlergen in ComponentFileRepository.Instance.GetAll())
                retVal.Add(new AllergenDTO(currentAlergen, medicine));

            return retVal;
        }

    }
}
