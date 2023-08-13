using MediatR;
using RealEstate.Core;

namespace RealEstate.Application.Features.Estate.Queries.GetByIdEstate
{
    public class GetEstateByIdQueryRequest : IRequest<Response<GetEstateByIdQueryResponse>>
    {
        public Guid EstateId { get; set; }
    }
}
