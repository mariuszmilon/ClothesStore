using ClothesStore.Models;
using ClothesStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStore.Controllers
{
    [Route("api/trousers")]
    [ApiController]
    [Authorize]
    public class TrousersController : ControllerBase
    {
        private readonly IItemService<TrousersDto, AddTrousersDto> _itemService;

        public TrousersController(IItemService<TrousersDto, AddTrousersDto> itemService)
        {
            _itemService = itemService;
        }


        [HttpPost]
        public ActionResult Post([FromBody] AddTrousersDto dto)
        {
            var id = _itemService.Add(dto);
            return Created($"api/trousers/{id}", null);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<TrousersDto> GetById([FromRoute] int id)
        {
            var trousersDto = _itemService.GetById(id);
            return Ok(trousersDto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _itemService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] AddTrousersDto dto)
        {
            _itemService.UpdateById(id, dto);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<PagesResult<TrousersDto>> GetAll([FromQuery] AnnouncementQuery query)
        {
            var listOfTrousers = _itemService.GetAll(query);
            return Ok(listOfTrousers);
        }
    }
}
