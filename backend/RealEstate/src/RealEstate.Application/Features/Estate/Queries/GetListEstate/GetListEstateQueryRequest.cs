using MediatR;
using RealEstate.Core;

namespace RealEstate.Application.Features.Estate.Queries.GetListEstate
{
    public class GetListEstateQueryRequest : IRequest<Response<List<GetListEstateQueryResponse>>>
    {
    }
}
