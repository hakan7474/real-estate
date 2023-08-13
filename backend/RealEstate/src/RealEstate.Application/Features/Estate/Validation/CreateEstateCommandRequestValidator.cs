using FluentValidation;
using RealEstate.Application.Features.Estate.Commands.CreateEstate;

namespace RealEstate.Application.Features.Estate.Validation;

public class CreateEstateCommandRequestValidator : AbstractValidator<CreateEstateCommandRequest>
{
    public CreateEstateCommandRequestValidator()
    {
        RuleFor(x => x.EstateCode)
            .NotEmpty()
            .WithName("Estate Code")
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