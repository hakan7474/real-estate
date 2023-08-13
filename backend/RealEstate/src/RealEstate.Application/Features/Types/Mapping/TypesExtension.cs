using AutoMapper;
using RealEstate.Application.Features.Types.Queries.GetByCodeTypeDetail;

namespace RealEstate.Application.Features.Types.Mapping;

public static class TypesExtension
{
    internal static IMapper Mapper;

    static TypesExtension()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<TypesProfile>()).CreateMapper();
    }


    public static List<GetTypeDetailByTypeCodeQueryResponse> AsResponse(this ICollection<Domain.Entities.TypeDetail> entity)
    {
        return Mapper.Map<List<GetTypeDetailByTypeCodeQueryResponse>>(entity);
    }
}