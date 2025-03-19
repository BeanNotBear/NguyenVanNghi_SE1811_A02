using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using Shared.Enums;


namespace BLL.MapperProfiles
{
	public class AccountProfile : Profile
	{
		public AccountProfile()
		{
			CreateMap<SystemAccount, SystemAccountDTO>()
				.ForMember(dest => dest.AccountRole, opt => opt.MapFrom(src => (AccountRole)(src.AccountRole)));
			CreateMap<CreateAccountDTO, SystemAccount>().ForMember(dest => dest.AccountRole, opt => opt.MapFrom(src => (int)(src.AccountRole)));
			CreateMap<SystemAccount, SystemAccountDetailDTO>().ForMember(dest => dest.AccountRole, opt => opt.MapFrom(src => (int)(src.AccountRole)));
		}
	}
}
