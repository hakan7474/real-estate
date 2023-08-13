using FluentValidation;
using RealEstate.Application.Features.Estate.Commands.UpdateEstate;

namespace RealEstate.Application.Features.Estate.Validation;

public class UpdateEstateCommandRequestValidator : AbstractValidator<UpdateEstateCommandRequest>
{
    public UpdateEstateCommandRequestValidator()
    {
        RuleFor(x => x.EstateId)
            .NotEmpty()
            .WithName("Estate Id")
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.EstateName)
            .NotEmpty()
            .WithName("Estate Name")
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.FloorNumber)
            .NotEmpty()
            .WithName("Floor Number")
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.RoomCount)
            .NotEmpty()
            .WithName("Room Count")
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.EstateTypeId)
            .NotEmpty()
            .WithName("Estate Type")
            .WithMessage("{PropertyName} is required.");
    }
}