using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Logistics.Entity
{
    public enum EnumUnitWeigh
    {
        [Display(Name = "Kg")]
        Kg = 1,
        [Display(Name = "Ton")]
        Ton = 3
    }
}
