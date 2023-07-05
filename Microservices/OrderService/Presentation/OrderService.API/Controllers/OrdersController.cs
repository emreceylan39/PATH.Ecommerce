using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Queries.GetOrderDetailById;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailsById(Guid id)
        {

            var result = await _mediator.Send(new GetOrderDetailsQuery(id));

            return Ok(result);
        }
    }
}
