using Microsoft.Extensions.Logging;
using RealEstate.Application.Interfaces.Repositories;
using RealEstate.Core.GenericRepositories;
using RealEstate.Domain.Entities;
using RealEstate.Persistence.Context;

namespace RealEstate.Persistence.Repositories;

public class EstateRepository : Repository<Estate, RealEstateContext>, IEstateRepository
{
    public EstateRepository(RealEstateContext dbContext, ILogger<Estate> logger) : base(dbContext, logger)
    {
    }
}