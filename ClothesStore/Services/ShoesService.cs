using AutoMapper;
using ClothesStore.Authorization;
using ClothesStore.Entities;
using ClothesStore.Exceptions;
using ClothesStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;

namespace ClothesStore.Services
{
    public class ShoesService : IItemService<ShoesDto, AddShoesDto>
    {
        private readonly ClothesStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TrousersService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public ShoesService(ClothesStoreDbContext dbContext, IMapper mapper, ILogger<TrousersService> logger, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;

        }
        public int Add(AddShoesDto dto)
        {
            _logger.LogInformation("Shoes: CREATE action invoked!");

            var shoes = _mapper.Map<Shoe>(dto);
            shoes.CreatedById = _userContextService.GetUserId;
            _dbContext.Shoes.Add(shoes);
            _dbContext.SaveChanges();
            return shoes.Id;
        }

        public void DeleteById(int id)
        {
            _logger.LogInformation("Shoes: DELETE action invoked!");

            var shoes = _dbContext
                .Shoes
                .FirstOrDefault(t => t.Id == id);

            if (shoes is null)
                throw new NotFoundException($"Not found shoes with id: {id}!");

            var result = _authorizationService.AuthorizeAsync(_userContextService.User, shoes, new ResourceRequirement(ResourceOperation.Delete)).Result;

            if (!result.Succeeded)
                throw new ForbidExepction("Shoes: Unauthorized DELETE action!");

            _dbContext.Shoes.Remove(shoes);
            _dbContext.SaveChanges();
        }

        public PagesResult<ShoesDto> GetAll(AnnouncementQuery query)
        {
            _logger.LogInformation("Shoes: GET ALL action invoked!");

            var baseQuery = _dbContext
                .Shoes
                .Where(r => (query.SearchPhrase == null) || (r.Description.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Brand.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy) && (!(query.SortDirection is null)))
            {
                var sortByColumn = new Dictionary<string, Expression<Func<Shoe, object>>>()
                {
                    {"Price", r => r.Price},
                    {"PublicationDate", r => r.PublicationDate},
                    {"Brand", r => r.Brand},
                };

                var sortedByColumn = sortByColumn[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(sortedByColumn)
                    : baseQuery.OrderByDescending(sortedByColumn);
            }

            var list = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var countOfShoes = baseQuery.Count();

            var listDto = _mapper.Map<List<ShoesDto>>(list);
            var result = new PagesResult<ShoesDto>(listDto, countOfShoes, query.PageSize, query.PageNumber);

            return result;
        }

        public ShoesDto GetById(int id)
        {
            _logger.LogInformation("Shoes: GET BY ID action invoked!");

            var shoes = _dbContext
                .Shoes
                .FirstOrDefault(x => x.Id == id);

            if (shoes is null)
                throw new NotFoundException($"Not found shoes  with id: {id}!");

            var shoesDto = _mapper.Map<ShoesDto>(shoes);
            return shoesDto;
        }

        public void UpdateById(int id, AddShoesDto dto)
        {
            _logger.LogInformation("Shoes: UPDATE action invoked!");

            var shoes = _dbContext
                .Shoes
                .FirstOrDefault(b => b.Id == id);

            if (shoes is null)
                throw new NotFoundException($"Not found shoes  with id: {id}!");

            var result = _authorizationService.AuthorizeAsync(_userContextService.User, shoes, new ResourceRequirement(ResourceOperation.Delete)).Result;

            if (!result.Succeeded)
                throw new ForbidExepction("Shoes: Unauthorized DELETE action!");

            shoes.Description = dto.Description;
            shoes.Price = dto.Price;
            shoes.Title = dto.Title;
            shoes.Material = dto.Material;
            shoes.Colour = dto.Colour;
            shoes.Sex = dto.Sex;
            shoes.Type = dto.Type;
            shoes.Fastener = dto.Fastener;
            shoes.Heel = dto.Heel;
            shoes.Toecap = dto.Toecap;

            shoes.UpdateDate = DateTime.Now;

            _dbContext.Shoes.Update(shoes);
            _dbContext.SaveChanges();
        }
    }
}
