using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Logistics.Entity
{
    public enum EnumOrderStatus
    {
        [Display(Name = "Sipariş Alındı")]
        OrderReceived = 1,

        [Display(Name = "Yola Çıktı")]
        HitTheRoad = 3,

        [Display(Name = "Dağıtım Merkezinde")]
        AtTheDistribution = 5,

        [Display(Name = "Dağıtıma Çıktı")]
        OutForDistribution = 7,

        [Display(Name = "Teslim Edildi")]
        Delivered = 9,

        [Display(Name = "Teslim Edilemedi")]
        Undeliverable = 11
    }
}
