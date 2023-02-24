using System;

namespace Logistics.Core.Entities.Concrete
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            this.IsActive = true;
        }

        public Guid Id { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}