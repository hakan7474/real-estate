using FluentValidation;
using RealEstate.Application.Features.Types.Queries.GetByCodeTypeDetail;
namespace RealEstate.Application.Features.Types.Validation;

public class GetTypeDetailByTypeCodeQueryRequestValidator : AbstractValidator<GetTypeDetailByTypeCodeQueryRequest>
{
    public GetTypeDetailByTypeCodeQueryRequestValidator()
    {
        RuleFor(x => x.TypeCode)
            .NotEmpty()
            .WithName("Type Code")
            .WithMessage("{PropertyName} is required.");
    }
}