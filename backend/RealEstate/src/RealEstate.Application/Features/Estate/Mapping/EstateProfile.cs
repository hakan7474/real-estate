using AutoMapper;
using RealEstate.Application.Features.Estate.Commands.CreateEstate;
using RealEstate.Application.Features.Estate.Commands.UpdateEstate;
using RealEstate.Application.Features.Estate.Queries.GetByIdEstate;
using RealEstate.Application.Features.Estate.Queries.GetListEstate;

namespace RealEstate.Application.Features.Estate.Mapping;

public class EstateProfile : Profile
{
    public EstateProfile()
    {
        CreateMap<CreateEstateCommandRequest, Domain.Entities.Estate>()
            .ForPath(dest => dest.Contact.Country, opt => opt.MapFrom(src => src.Country))
            .ForPath(dest => dest.Contact.City, opt => opt.MapFrom(src => src.City))
            .ForPath(dest => dest.Contact.State, opt => opt.MapFrom(src => src.State))
            .ForPath(dest => dest.Contact.District, opt => opt.MapFrom(src => src.District))
            .ForPath(dest => dest.Contact.Location, opt => opt.MapFrom(src => src.Location))
            .ForPath(dest => dest.Contact.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<UpdateEstateCommandRequest, Domain.Entities.Estate>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EstateId))
            .ForPath(dest => dest.Contact.Country, opt => opt.MapFrom(src => src.Country))
            .ForPath(dest => dest.Contact.City, opt => opt.MapFrom(src => src.City))
            .ForPath(dest => dest.Contact.State, opt => opt.MapFrom(src => src.State))
            .ForPath(dest => dest.Contact.District, opt => opt.MapFrom(src => src.District))
            .ForPath(dest => dest.Contact.Location, opt => opt.MapFrom(src => src.Location))
            .ForPath(dest => dest.Contact.Address, opt => opt.MapFrom(src => src.Address));


        CreateMap<Domain.Entities.Estate, GetEstateByIdQueryResponse>()
            .ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.Id))
            .ForPath(dest => dest.Country, opt => opt.MapFrom(src => src.Contact.Country))
            .ForPath(dest => dest.City, opt => opt.MapFrom(src => src.Contact.City))
            .ForPath(dest => dest.State, opt => opt.MapFrom(src => src.Contact.State))
            .ForPath(dest => dest.District, opt => opt.MapFrom(src => src.Contact.District))
            .ForPath(dest => dest.Location, opt => opt.MapFrom(src => src.Contact.Location))
            .ForPath(dest => dest.Address, opt => opt.MapFrom(src => src.Contact.Address));

        CreateMap<Domain.Entities.Estate, GetListEstateQueryResponse>()
            .ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.Id))
            .ForPath(dest => dest.Country, opt => opt.MapFrom(src => src.Contact.Country))
            .ForPath(dest => dest.City, opt => opt.MapFrom(src => src.Contact.City))
            .ForPath(dest => dest.State, opt => opt.MapFrom(src => src.Contact.State))
            .ForPath(dest => dest.District, opt => opt.MapFrom(src => src.Contact.District))
            .ForPath(dest => dest.Location, opt => opt.MapFrom(src => src.Contact.Location))
            .ForPath(dest => dest.Address, opt => opt.MapFrom(src => src.Contact.Address))
            .ForMember(dest => dest.EstateTypeCode, opt => opt.MapFrom(src => src.EstateType.ItemCode))
            .ForMember(dest => dest.EstateTypeName, opt => opt.MapFrom(src => src.EstateType.ItemName));
    }
}