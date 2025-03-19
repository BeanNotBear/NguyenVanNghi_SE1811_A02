using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.MapperProfiles
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, CategoryForSelectDTO>();
		}
	}
}
