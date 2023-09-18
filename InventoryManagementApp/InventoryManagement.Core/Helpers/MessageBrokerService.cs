using System.Threading.Tasks;
using MassTransit;
using InventoryManagement.Core.Interface;

namespace InventoryManagement.Core.Helpers
{
    public class MessageBrokerService : IMessageBroker
    {
        private readonly IBus _publishEndpoint;

        public MessageBrokerService(IBus publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T message) where T : class
            => await _publishEndpoint.Publish<T>(message);

        public async Task PublishAsync<T>(object message) where T : class
            => await _publishEndpoint.Publish<T>(message);
    }
}
