using System.Threading.Tasks;
namespace InventoryManagement.Core.Interface
{
    public interface IMessageBroker
    {
        Task PublishAsync<T>(T message) where T : class;
        Task PublishAsync<T>(object message) where T : class;
    }
}
