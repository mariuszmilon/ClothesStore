using ClothesStore.Entities;
using ClothesStore.Models;

namespace ClothesStore.Services
{
    public interface IItemService
    {
        int Add(AddTrousersDto dto);
        TrousersDto GetById(int id);
        void DeleteById(int id);
        void UpdateById(int id, AddTrousersDto dto);
        PagesResult<TrousersDto> GetAll(AnnouncementQuery query);
    }
}
