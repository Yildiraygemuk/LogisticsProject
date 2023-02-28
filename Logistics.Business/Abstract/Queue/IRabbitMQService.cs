using Logistics.Core.Utilities.Results;
using Logistics.Entity;

namespace Logistics.Business
{
    public interface IRabbitMQService
    {
        IResult SendToQueue(Order order);
    }
}
