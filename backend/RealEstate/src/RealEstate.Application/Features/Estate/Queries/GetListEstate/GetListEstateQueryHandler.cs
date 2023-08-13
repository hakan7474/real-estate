using MediatR;
using RealEstate.Application.Features.Estate.Mapping;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core;
using System.Collections.Generic;
using System.Net;

namespace RealEstate.Application.Features.Estate.Queries.GetListEstate
{
    public class GetListEstateQueryHandler : IRequestHandler<GetListEstateQueryRequest, Response<List<GetListEstateQueryResponse>>>
    {
        private readonly IEstateRepository _estateRepository;

        public GetListEstateQueryHandler(IEstateRepository estateRepository)
        {
            _estateRepository = estateRepository;
        }

        public async Task<Response<List<GetListEstateQueryResponse>>> Handle(GetListEstateQueryRequest request, CancellationToken cancellationToken)
        {
            var estates = await _estateRepository.GetListAsync(x => x.IsActive, x => x.OrderBy(y => y.EstateCode), 0, 0, false, x => x.EstateType);
            return Response<List<GetListEstateQueryResponse>>.CreateResponse(estates.AsResponse(), 0, (int)HttpStatusCode.OK);
        }
    }
}
