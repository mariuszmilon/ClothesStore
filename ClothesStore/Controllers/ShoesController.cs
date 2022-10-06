using ClothesStore.Models;
using ClothesStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStore.Controllers
{
    [Route("api/shoes")]
    [ApiController]
    [Authorize]
    public class ShoesController : ControllerBase
    {
        private readonly IItemService<ShoesDto, AddShoesDto> _itemService;

        public ShoesController(IItemService<ShoesDto, AddShoesDto> itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] AddShoesDto dto)
        {
            var id = _itemService.Add(dto);
            return Created($"api/shoes/{id}", null);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<ShoesDto> GetById([FromRoute] int id)
        {
            var shoesDto = _itemService.GetById(id);
            return Ok(shoesDto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _itemService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] AddShoesDto dto)
        {
            _itemService.UpdateById(id, dto);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<PagesResult<ShoesDto>> GetAll([FromQuery] AnnouncementQuery query)
        {
            var listOfShoes = _itemService.GetAll(query);
            return Ok(listOfShoes);
        }
    }
}
