using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Logistics.Entity
{
    public enum EnumUnitQuantities
    {
        [Display(Name = "Adet")]
        Piece = 1,
        [Display(Name = "Koli")]
        Box = 3,
        [Display(Name = "Paket")]
        Package = 5,
        [Display(Name = "Palet")]
        Pallet = 7
    }
}
