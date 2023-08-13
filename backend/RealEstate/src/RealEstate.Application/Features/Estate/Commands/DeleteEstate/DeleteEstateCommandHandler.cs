using MediatR;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Features.Estate.Rules;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core;
using RealEstate.Core.GenericUnitOfWork;
using System.Net;

namespace RealEstate.Application.Features.Estate.Commands.DeleteEstate;

public class DeleteEstateCommandRequestHandler : IRequestHandler<DeleteEstateCommandRequest, Response<bool>>
{
    private readonly IEstateRepository _estateRepository;
    private readonly EstateBusinessRule _estateBusinessRule;
    public DeleteEstateCommandRequestHandler(IUnitOfWork unitOfWork, EstateBusinessRule estateBusinessRule)
    {
        _estateRepository = unitOfWork.Repository<IEstateRepository>();
        _estateBusinessRule = estateBusinessRule;
    }

    public async Task<Response<bool>> Handle(DeleteEstateCommandRequest request, CancellationToken cancellationToken)
    {
        var estate = await _estateBusinessRule.EstateCheckById(request.EstateId);

        _estateRepository.Delete(estate);

        if (await _estateRepository.SaveChangesAsync(cancellationToken) > 0)
        {
            return Response<bool>.CreateResponse(true, 0, (int)HttpStatusCode.OK);
        }

        return Response<bool>.ErrorResponse("CreateEstate", "Registration Failed", StatusCodes.Status200OK);

    }
}