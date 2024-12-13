using Asp9_Project_Core.DTO_s;
using Asp9_Project_Core.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.Mapping_Profiles
{
    public class Mapping_Profile
    {
        private static readonly TypeAdapterConfig _config = new TypeAdapterConfig();
        static Mapping_Profile()
        {
            _config.NewConfig<Items, ItemsDTO>()
                .Map(dest => dest.ItemUnits, src => src.ItemsUnits.Select(unit => unit.Units.Name).ToList())
                .Map(dest => dest.Stores, src => src.InvItemStores.Select(store => store.Stores.Name).ToList());
        }
        public static TypeAdapterConfig Config => _config;
    }
}
