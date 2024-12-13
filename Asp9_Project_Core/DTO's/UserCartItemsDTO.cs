using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Asp9_Project_Core.DTO_s
{
    public class UserCartItemsDTO
    {
        public string name { get; set; }
        public double price { get; set; }
        public string ItemUnit { get; set; }
        public double Quantity { get; set; }

        //public double TotalPrice { get; set; }
    }
}
