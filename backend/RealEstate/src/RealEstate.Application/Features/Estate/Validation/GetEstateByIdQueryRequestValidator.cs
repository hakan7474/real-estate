using FluentValidation;
using RealEstate.Application.Features.Estate.Queries.GetByIdEstate;

namespace RealEstate.Application.Features.Estate.Validation;

public class GetEstateByIdQueryRequestValidator : AbstractValidator<GetEstateByIdQueryRequest>
{
    public GetEstateByIdQueryRequestValidator()
    {
        RuleFor(x => x.EstateId)
            .NotEmpty()
            .WithName("Estate Id")
            .WithMessage("{PropertyName} is required.");
    }
}