using Microsoft.Extensions.Logging;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core.GenericRepositories;
using RealEstate.Domain.Entities;
using RealEstate.Persistence.Context;

namespace RealEstate.Persistence.Repositories;

public class TypeDetailRepository : Repository<TypeDetail, RealEstateContext>, ITypeDetailRepository
{
    public TypeDetailRepository(RealEstateContext dbContext, ILogger<TypeDetail> logger) : base(dbContext, logger)
    {
    }
}