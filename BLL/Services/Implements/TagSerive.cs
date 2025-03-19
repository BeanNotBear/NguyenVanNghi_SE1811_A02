using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implements
{
	public class TagSerive(IUnitOfWork<Tag> unitOfWork, IMapper mapper) : ITagService
	{
		public async Task<Tag> GetById(int id)
		{
			return await unitOfWork.GenericRepository.Get(x => x.TagId == id);
		}

		public async Task<IEnumerable<SelectTagDTO>> GetSelectTagList()
		{
			return mapper.Map<IEnumerable<SelectTagDTO>>(await unitOfWork.GenericRepository.GetAll());
		}
	}
}
