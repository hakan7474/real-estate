using MediatR;
using RealEstate.Core;

namespace RealEstate.Application.Features.Types.Queries.GetByCodeTypeDetail
{
    public class GetTypeDetailByTypeCodeQueryRequest : IRequest<Response<List<GetTypeDetailByTypeCodeQueryResponse>>>
    {
        public string TypeCode { get; set; }
    }
}
