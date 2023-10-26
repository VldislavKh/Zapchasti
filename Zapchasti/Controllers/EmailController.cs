using MediatR;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Presentation.Commands;
using Presentation.Services;
//pbzvlcijbpzriatr
namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/Mail")]
    public class EmailController : Controller
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<string>> RecieveLastCsvEmail([FromBody] PutPriceItemsToContextCommand command, CancellationToken token)
        {
            return await _mediator.Send(command, token);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> ClearDataBase([FromBody] ClearDataBaseCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
