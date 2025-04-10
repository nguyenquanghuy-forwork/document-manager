using DocumentManagement.Application.Commands;
using DocumentManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{documentNumber}")]
        public async Task<IActionResult> Update(string documentNumber, [FromBody] UpdateDocumentCommand command)
        {
            if (documentNumber != command.DocumentNumber) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{documentNumber}")]
        public async Task<IActionResult> Delete(string documentNumber)
        {
            await _mediator.Send(new DeleteDocumentCommand { DocumentNumber = documentNumber });
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetDocumentListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
