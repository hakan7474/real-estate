using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core.Exceptions;

namespace RealEstate.Application.Features.Estate.Rules;

public class EstateBusinessRule
{
    private readonly IEstateRepository _estateRepository;

    public EstateBusinessRule(IEstateRepository estateRepository)
    {
        _estateRepository = estateRepository;
    }

    public async Task EstateCodeCanNotBeDuplicatedWhenInserted(string estateCode)
    {
        var result = await _estateRepository.GetAsync(b => b.EstateCode == estateCode);
        if (!result.IsNull()) throw new BusinessException("EstateCode", "Estate Code exists.");
    }

    public async Task<Domain.Entities.Estate> EstateCheckById(Guid estateId)
    {
        var result = await _estateRepository.GetByIdAsync(estateId);
        if (result.IsNull()) throw new BusinessException("EstateNotFound", "Estate not found.");
        return result;
    }
}