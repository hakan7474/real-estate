using Microsoft.Extensions.Logging;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core.GenericRepositories;
using RealEstate.Domain.Entities;
using RealEstate.Persistence.Context;

namespace RealEstate.Persistence.Repositories;

public class TypeRepository : Repository<Types, RealEstateContext>, ITypeRepository
{
    public TypeRepository(RealEstateContext dbContext, ILogger<Types> logger) : base(dbContext, logger)
    {
    }
}