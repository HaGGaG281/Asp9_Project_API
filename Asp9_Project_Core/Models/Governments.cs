using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.Models
{
    public class Governments
    {
        public int Id  { get; set; }
        public string Name { get; set; }

        public ICollection<Users> Users { get; set; } = new HashSet<Users>();
        public ICollection<Zones> Zones { get; set; } = new HashSet<Zones>();

        public ICollection<Cities> Cities { get; set; } = new HashSet<Cities>();
        public ICollection<Stores> Stores { get; set; } = new HashSet<Stores>();



    }
}
