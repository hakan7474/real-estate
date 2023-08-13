using MediatR;
using RealEstate.Core;

namespace RealEstate.Application.Features.Estate.Commands.DeleteEstate;

public class DeleteEstateCommandRequest : IRequest<Response<bool>>
{
    public Guid EstateId { get; set; }
}