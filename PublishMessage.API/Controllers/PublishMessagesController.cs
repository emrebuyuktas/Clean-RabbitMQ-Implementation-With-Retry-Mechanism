using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublishMessage.API.Services;

namespace PublishMessage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishMessagesController : ControllerBase
    {
        private readonly ISendMessageService _sendMessageService;

        public PublishMessagesController(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            _sendMessageService.SendMessageAsync();
            return Ok();
        }
    }
}
