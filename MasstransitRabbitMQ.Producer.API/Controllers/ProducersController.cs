using MassTransit;
using MasstransitRabbitMQ.Contract.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MasstransitRabbitMQ.Contract.IntegrationEvent.DomainEvent;

namespace MasstransitRabbitMQ.Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ProducersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost(Name = "publish-sms-notification")]
        public async Task<IActionResult> PublishSmsNotificationEvent()
        {
            await _publishEndpoint.Publish(new SmsNotificationEvent(){
                Id = Guid.NewGuid(),
                Description = "Sms description",
                Name = "sms notification",
                TimeStamp = DateTime.Now,
                TransactionId = Guid.NewGuid(),
                Type = NotificationType.sms
            });
            return Ok();
        }
    }
}
