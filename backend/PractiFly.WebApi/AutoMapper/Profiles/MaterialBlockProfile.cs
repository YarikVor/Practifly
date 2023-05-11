using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.MaterialBlocks;

namespace PractiFly.WebApi.AutoMapper.Profiles;

public class MaterialBlockProfile : Profile
{
    public MaterialBlockProfile(IPractiflyContext _context)
    {
        CreateMap<CreateMaterialDto, Material>();
        CreateMap<EditMaterialDto, Material>();
        CreateMap<Material, MaterialDto>();
        CreateProjection<HeadingMaterial, MaterialsHeadingItemDto>()
            //TODO: Maybe use Material
            .ForMember(dto => dto.Id, opt => opt.MapFrom(e => e.Material.Id))
            .ForMember(dto => dto.Name, opt => opt.MapFrom(e => e.Material.Name))
            .ForMember(dto => dto.IsPractical, opt => opt.MapFrom(e => e.Material.IsPractical));
        CreateProjection<Material, MaterialItemDto>();
    }
}