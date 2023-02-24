using System;

namespace Logistics.Core.Entities.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(object data)
        {
            Id = data.ToString();
        }

        public string Id { get; set; }
    }
}