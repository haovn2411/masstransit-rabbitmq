using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.Messages;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.Abstractions.Messages
{
    public abstract class Consumer<TMessage> : IConsumer<TMessage> where TMessage : class, INotificationEvent
    {
        private readonly ISender sender;

        protected Consumer(ISender sender)
        {
            this.sender = sender;
        }

        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await sender.Send(context.Message);
        }
    }
}
