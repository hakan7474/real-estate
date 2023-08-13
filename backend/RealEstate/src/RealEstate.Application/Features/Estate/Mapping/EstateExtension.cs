using AutoMapper;
using RealEstate.Application.Features.Estate.Commands.CreateEstate;
using RealEstate.Application.Features.Estate.Commands.UpdateEstate;
using RealEstate.Application.Features.Estate.Queries.GetByIdEstate;
using RealEstate.Application.Features.Estate.Queries.GetListEstate;

namespace RealEstate.Application.Features.Estate.Mapping;

public static class EstateExtension
{
    internal static IMapper Mapper;

    static EstateExtension()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EstateProfile>()).CreateMapper();
    }

    public static Domain.Entities.Estate AsEntity(this CreateEstateCommandRequest entity)
    {
        return Mapper.Map<Domain.Entities.Estate>(entity);
    }

    public static Domain.Entities.Estate AsEntity(this UpdateEstateCommandRequest request, Domain.Entities.Estate entity)
    {
        return Mapper.Map(request, entity);
    }

    public static GetEstateByIdQueryResponse AsResponse(this Domain.Entities.Estate entity)
    {
        return Mapper.Map<GetEstateByIdQueryResponse>(entity);
    }

    public static List<GetListEstateQueryResponse> AsResponse(this List<Domain.Entities.Estate> entity)
    {
        return Mapper.Map<List<GetListEstateQueryResponse>>(entity);
    }
}