using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Service;
using Common.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alteration.Pages
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AlterationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderAlterationService _service;
        public AlterationController(IMediator mediator, IOrderAlterationService service)
        {
            _mediator = mediator;
            _service = service;
        }
        // GET: api/Alteration
        [HttpGet("{status}")]
        public async Task<ActionResult<OrderAlterationViewModel>> Get(byte status)
        {
            var result = await _service.Get(status);

            return Ok(result);
        }

        // GET: api/Alteration/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Alteration
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateOrderAlterationCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeStatus([FromBody] ChangeStatusOrderAlterationCommend cmd)
        {
            var result = await _mediator.Send(cmd);

            return Ok(result);
        }

        // PUT: api/Alteration/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
