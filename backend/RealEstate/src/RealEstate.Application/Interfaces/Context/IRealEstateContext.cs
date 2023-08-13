using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces.Context;

public interface IRealEstateContext
{
    DbSet<Types> Types { get; set; }
    DbSet<TypeDetail> TypeDetails { get; set; }
    DbSet<Estate> Estates { get; set; }
}