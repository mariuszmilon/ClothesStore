using ClothesStore.Entities;
using ClothesStore.Models;

namespace ClothesStore.Services
{
    public interface IItemService<TDto, TAddDto>
    {
        int Add(TAddDto dto);
        TDto GetById(int id);
        void DeleteById(int id);
        void UpdateById(int id, TAddDto dto);
        PagesResult<TDto> GetAll(AnnouncementQuery query);
    }
}
