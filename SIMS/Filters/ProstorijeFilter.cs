﻿using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    class ProstorijeFilter : TableFilter<Room>
    {
        public override bool KeywordFilter(Room prostorija, string keyword)
        {
            return (prostorija.Number.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    prostorija.AvailableToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    prostorija.TypeToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
