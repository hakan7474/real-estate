using MediatR;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Features.Estate.Mapping;
using RealEstate.Application.Features.Estate.Rules;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core;
using RealEstate.Core.GenericUnitOfWork;
using System.Net;

namespace RealEstate.Application.Features.Estate.Commands.CreateEstate;

public class CreateEstateCommandHandler : IRequestHandler<CreateEstateCommandRequest, Response<bool>>
{
    private readonly IEstateRepository _estateRepository;
    private readonly EstateBusinessRule _estateBusinessRule;
    public CreateEstateCommandHandler(IUnitOfWork unitOfWork, EstateBusinessRule estateBusinessRule)
    {
        _estateRepository = unitOfWork.Repository<IEstateRepository>();
        _estateBusinessRule = estateBusinessRule;
    }

    public async Task<Response<bool>> Handle(CreateEstateCommandRequest request, CancellationToken cancellationToken)
    {
        await _estateBusinessRule.EstateCodeCanNotBeDuplicatedWhenInserted(request.EstateCode);

        var entity = request.AsEntity();

        await _estateRepository.AddAsync(entity, cancellationToken);

        if (await _estateRepository.SaveChangesAsync(cancellationToken) > 0)
        {
            return Response<bool>.CreateResponse(true, 0, (int)HttpStatusCode.OK);
        }

        return Response<bool>.ErrorResponse("CreateEstate", "Registration Failed", StatusCodes.Status200OK);

    }
}