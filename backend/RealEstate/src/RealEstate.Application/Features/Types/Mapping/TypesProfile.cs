using AutoMapper;
using RealEstate.Application.Features.Types.Queries.GetByCodeTypeDetail;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Types.Mapping;

public class TypesProfile : Profile
{
    public TypesProfile()
    {
        CreateMap<TypeDetail, GetTypeDetailByTypeCodeQueryResponse>()
            .ForMember(dest => dest.TypeItemId, opt => opt.MapFrom(src => src.Id));
    }
}