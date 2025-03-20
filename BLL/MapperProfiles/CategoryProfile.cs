using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using Shared.Enums;

namespace BLL.MapperProfiles
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, CategoryForSelectDTO>();
			CreateMap<Category, CategoryDTO>()
			.ForMember(dest => dest.CategoryStatus, opt => opt.MapFrom(src => src.IsActive ? CategoryStatus.Active : CategoryStatus.Inactive))
			.ForMember(dest => dest.ParentCategory, opt => opt.MapFrom(src => src.ParentCategory));
		}
	}
}
