using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Estate.Commands.CreateEstate;
using RealEstate.Application.Features.Estate.Commands.DeleteEstate;
using RealEstate.Application.Features.Estate.Commands.UpdateEstate;
using RealEstate.Application.Features.Estate.Queries.GetByIdEstate;
using RealEstate.Application.Features.Estate.Queries.GetListEstate;
using RealEstate.Core;

namespace RealEstate.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstateController : BaseController
    {
        [HttpGet]
        public async Task<Response<List<GetListEstateQueryResponse>>> Get()
        {
            var result = await Mediator.Send(new GetListEstateQueryRequest());
            return result;
        }

        [HttpGet("{EstateId}")]
        public async Task<Response<GetEstateByIdQueryResponse>> GetById([FromRoute] GetEstateByIdQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return result;
        }

        [HttpPost]
        public async Task<Response<bool>> Add([FromBody] CreateEstateCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return result;
        }

        [HttpPut]
        public async Task<Response<bool>> Update([FromBody] UpdateEstateCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return result;
        }

        [HttpDelete("{EstateId}")]
        public async Task<Response<bool>> Delete([FromRoute] DeleteEstateCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return result;
        }
    }
}