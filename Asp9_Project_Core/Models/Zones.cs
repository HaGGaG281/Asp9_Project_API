using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.Models
{
    public class Zones
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Governments))]
        public int Gov_Id{ get; set; }
        [ForeignKey(nameof(Cities))]
        public int City_Id{ get; set; }
        public ICollection<Users> Users { get; set; } = new HashSet<Users>();
        public Governments Governments { get; set; }
        public Cities Cities{ get; set; }

    }
}
