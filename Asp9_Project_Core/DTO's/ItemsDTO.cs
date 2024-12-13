using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.DTO_s
{
    public class ItemsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public string Description { get; set; }

        public List<string> ItemUnits { get; set; }
        public List<string> Stores { get; set; }

    }
}
