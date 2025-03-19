using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.MapperProfiles
{
	public class TagProfile : Profile
	{
		public TagProfile()
		{
			CreateMap<Tag, SelectTagDTO>();
		}
	}
}
