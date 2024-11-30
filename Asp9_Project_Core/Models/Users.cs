﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.Models
{
    public class Users : IdentityUser<int>
    {
        [ForeignKey(nameof(Governments))]
        public int Gov_Id { get; set; }

        [ForeignKey(nameof(Cities))]
        public int City_Id { get; set; }

        [ForeignKey(nameof(Zones))]
        public int Zone_Id { get; set; }

        [ForeignKey(nameof(Classifications))]
        public int Class_Id{ get; set; }

        public Classifications Classifications { get; set; }
        public Zones Zones{ get; set; }

        public Governments Governments { get; set; }
        public Cities Cities{ get; set; }
        public ICollection<CustomerStores> CustomerStores { get; set; } = new HashSet<CustomerStores>();


    }
}