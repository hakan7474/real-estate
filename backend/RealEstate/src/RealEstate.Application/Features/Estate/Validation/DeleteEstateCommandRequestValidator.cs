using FluentValidation;
using RealEstate.Application.Features.Estate.Commands.DeleteEstate;

namespace RealEstate.Application.Features.Estate.Validation;

public class DeleteEstateCommandRequestValidator : AbstractValidator<DeleteEstateCommandRequest>
{
    public DeleteEstateCommandRequestValidator()
    {
        RuleFor(x => x.EstateId)
            .NotEmpty()
            .WithName("Estate Id")
            .WithMessage("{PropertyName} is required.");
    }
}