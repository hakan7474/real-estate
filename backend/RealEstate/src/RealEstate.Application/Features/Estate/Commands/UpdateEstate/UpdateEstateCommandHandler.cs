using MediatR;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Features.Estate.Mapping;
using RealEstate.Application.Features.Estate.Rules;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core;
using RealEstate.Core.GenericUnitOfWork;
using System.Net;

namespace RealEstate.Application.Features.Estate.Commands.UpdateEstate;

public class UpdateEstateCommandRequestHandler : IRequestHandler<UpdateEstateCommandRequest, Response<bool>>
{
    private readonly IEstateRepository _estateRepository;
    private readonly EstateBusinessRule _estateBusinessRule;
    public UpdateEstateCommandRequestHandler(IUnitOfWork unitOfWork, EstateBusinessRule estateBusinessRule)
    {
        _estateRepository = unitOfWork.Repository<IEstateRepository>();
        _estateBusinessRule = estateBusinessRule;
    }

    public async Task<Response<bool>> Handle(UpdateEstateCommandRequest request, CancellationToken cancellationToken)
    {
        var estate = await _estateBusinessRule.EstateCheckById(request.EstateId);

        var entity = request.AsEntity(estate);
        _estateRepository.DbUpdate(entity);

        if (await _estateRepository.SaveChangesAsync(cancellationToken) > 0)
        {
            return Response<bool>.CreateResponse(true, 0, (int)HttpStatusCode.OK);
        }

        return Response<bool>.ErrorResponse("CreateEstate", "Registration Failed", StatusCodes.Status200OK);

    }
}