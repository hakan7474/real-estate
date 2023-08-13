using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Features.Types.Mapping;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core;
using RealEstate.Core.GenericUnitOfWork;
using System.Net;

namespace RealEstate.Application.Features.Types.Queries.GetByCodeTypeDetail
{
    public class GetTypeDetailByTypeCodeQueryHandler : IRequestHandler<GetTypeDetailByTypeCodeQueryRequest, Response<List<GetTypeDetailByTypeCodeQueryResponse>>>
    {
        private readonly ITypeRepository _typeRepository;
        public GetTypeDetailByTypeCodeQueryHandler(IUnitOfWork unitOfWork)
        {
            _typeRepository = unitOfWork.Repository<ITypeRepository>();
        }
        public async Task<Response<List<GetTypeDetailByTypeCodeQueryResponse>>> Handle(GetTypeDetailByTypeCodeQueryRequest request, CancellationToken cancellationToken)
        {
            var type = await _typeRepository.FirstOrDefaultAsync(a => a.TypeCode == request.TypeCode && a.IsActive, null, default, false, false, a => a.Include(q => q.TypeDetails.Where(b => b.IsActive).OrderBy(x => x.OrderIndex)));

            var response = type.TypeDetails.AsResponse();

            return Response<List<GetTypeDetailByTypeCodeQueryResponse>>.CreateResponse(response, 0, (int)HttpStatusCode.OK);
        }
    }
}
