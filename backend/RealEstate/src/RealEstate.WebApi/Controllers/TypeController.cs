using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Types.Queries.GetByCodeTypeDetail;
using RealEstate.Core;

namespace RealEstate.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController : BaseController
    {
        [HttpGet("{TypeCode}")]
        public async Task<Response<List<GetTypeDetailByTypeCodeQueryResponse>>> GetById([FromRoute] GetTypeDetailByTypeCodeQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return result;
        }
    }
}