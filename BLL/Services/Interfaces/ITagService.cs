using BLL.DTOs;
using DAL.Entities;

namespace BLL.Services.Interfaces
{
	public interface ITagService
	{
		Task<IEnumerable<SelectTagDTO>> GetSelectTagList();
		Task<Tag> GetById(int id);
	}
}
