using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities.Workshop
{
    public class Country
    {
        public int CountryId { get; set; }

        public string Name { get; set; } = "";

       // public ICollection<State> States { get; set; } = new List<State>();
    }
}
