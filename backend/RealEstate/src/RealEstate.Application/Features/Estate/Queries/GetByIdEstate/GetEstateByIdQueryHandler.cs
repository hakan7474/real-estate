using MediatR;
using RealEstate.Application.Features.Estate.Mapping;
using RealEstate.Application.Features.Estate.Rules;
using RealEstate.Core;
using System.Net;

namespace RealEstate.Application.Features.Estate.Queries.GetByIdEstate
{
    public class GetEstateByIdQueryHandler : IRequestHandler<GetEstateByIdQueryRequest, Response<GetEstateByIdQueryResponse>>
    {
        private readonly EstateBusinessRule _estateBusinessRule;

        public GetEstateByIdQueryHandler(EstateBusinessRule estateBusinessRule)
        {
            _estateBusinessRule = estateBusinessRule;
        }
        public async Task<Response<GetEstateByIdQueryResponse>> Handle(GetEstateByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var estate = await _estateBusinessRule.EstateCheckById(request.EstateId);

            return Response<GetEstateByIdQueryResponse>.CreateResponse(estate.AsResponse(), 0, (int)HttpStatusCode.OK);
        }
    }
}
