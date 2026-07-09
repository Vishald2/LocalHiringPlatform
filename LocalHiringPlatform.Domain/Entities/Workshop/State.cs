using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities.Workshop
{
    public class State
    {
        public int StateId { get; set; }

        public int CountryId { get; set; }

        public string Name { get; set; } = "";
        
      //  public Country? Country { get; set; }
    }
}
