using ClothesStore.Models;
using ClothesStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStore.Controllers
{
    [Route("api/pullover_sweatshirt")]
    [ApiController]
    [Authorize]
    public class PulloverAndSweatshirtController : ControllerBase
    {
        private readonly IItemService<PulloverAndSweatshirtDto, AddPulloverAndSweatshirtDto> _itemService;

        public PulloverAndSweatshirtController(IItemService<PulloverAndSweatshirtDto, AddPulloverAndSweatshirtDto> itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] AddPulloverAndSweatshirtDto dto)
        {
            var id = _itemService.Add(dto);
            return Created($"api/pullover_sweatshirt/{id}", null);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<PulloverAndSweatshirtDto> GetById([FromRoute] int id)
        {
            var itemDto = _itemService.GetById(id);
            return Ok(itemDto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _itemService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] AddPulloverAndSweatshirtDto dto)
        {
            _itemService.UpdateById(id, dto);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<PagesResult<PulloverAndSweatshirtDto>> GetAll([FromQuery] AnnouncementQuery query)
        {
            var listOfClothes = _itemService.GetAll(query);
            return Ok(listOfClothes);
        }
    }
}
