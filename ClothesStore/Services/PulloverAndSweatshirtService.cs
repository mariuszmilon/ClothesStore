using AutoMapper;
using ClothesStore.Authorization;
using ClothesStore.Entities;
using ClothesStore.Exceptions;
using ClothesStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;

namespace ClothesStore.Services
{
    public class PulloverAndSweatshirtService : IItemService<PulloverAndSweatshirtDto, AddPulloverAndSweatshirtDto>
    {
        private readonly ClothesStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TrousersService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public PulloverAndSweatshirtService(ClothesStoreDbContext dbContext, IMapper mapper, ILogger<TrousersService> logger, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;

        }
        public int Add(AddPulloverAndSweatshirtDto dto)
        {
            _logger.LogInformation("PulloverAndSweatshirt: CREATE action invoked!");

            var clothes = _mapper.Map<PulloverAndSweatshirt>(dto);
            clothes.CreatedById = _userContextService.GetUserId;
            _dbContext.PulloversAndSweats.Add(clothes);
            _dbContext.SaveChanges();
            return clothes.Id;
        }

        public void DeleteById(int id)
        {
            _logger.LogInformation("PulloverAndSweatshirt: DELETE action invoked!");

            var clothes = _dbContext
                .PulloversAndSweats
                .FirstOrDefault(t => t.Id == id);

            if (clothes is null)
                throw new NotFoundException($"Item with id: {id} not found!");

            var result = _authorizationService.AuthorizeAsync(_userContextService.User, clothes, new ResourceRequirement(ResourceOperation.Delete)).Result;

            if (!result.Succeeded)
                throw new ForbidExepction("PulloverAndSweatshirt: Unauthorized DELETE action!");

            _dbContext.PulloversAndSweats.Remove(clothes);
            _dbContext.SaveChanges();
        }

        public PagesResult<PulloverAndSweatshirtDto> GetAll(AnnouncementQuery query)
        {
            _logger.LogInformation("PulloverAndSweatshirt: GET ALL action invoked!");

            var baseQuery = _dbContext
                .PulloversAndSweats
                .Where(r => (query.SearchPhrase == null) || (r.Description.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Brand.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy) && (!(query.SortDirection is null)))
            {
                var sortByColumn = new Dictionary<string, Expression<Func<PulloverAndSweatshirt, object>>>()
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

            var countOfClothes = baseQuery.Count();

            var listDto = _mapper.Map<List<PulloverAndSweatshirtDto>>(list);
            var result = new PagesResult<PulloverAndSweatshirtDto>(listDto, countOfClothes, query.PageSize, query.PageNumber);

            return result;
        }

        public PulloverAndSweatshirtDto GetById(int id)
        {
            _logger.LogInformation("PulloverAndSweatshirt: GET BY ID action invoked!");

            var clothes = _dbContext
                .PulloversAndSweats
                .FirstOrDefault(x => x.Id == id);

            if (clothes is null)
                throw new NotFoundException($"Item with id: {id} not found!");

            var clothesDto = _mapper.Map<PulloverAndSweatshirtDto>(clothes);
            return clothesDto;
        }

        public void UpdateById(int id, AddPulloverAndSweatshirtDto dto)
        {
            _logger.LogInformation("PulloverAndSweatshirt: UPDATE action invoked!");

            var clothes = _dbContext
                .PulloversAndSweats
                .FirstOrDefault(b => b.Id == id);

            if (clothes is null)
                throw new NotFoundException($"Item with id: {id} not found!");

            var result = _authorizationService.AuthorizeAsync(_userContextService.User, clothes, new ResourceRequirement(ResourceOperation.Delete)).Result;

            if (!result.Succeeded)
                throw new ForbidExepction("PulloverAndSweatshirt: Unauthorized DELETE action!");

            clothes.Description = dto.Description;
            clothes.Price = dto.Price;
            clothes.Title = dto.Title;
            clothes.Material = dto.Material;
            clothes.Colour = dto.Colour;
            clothes.Sex = dto.Sex;
            clothes.Type = dto.Type;
            clothes.Neckline = dto.Neckline;
            clothes.Hood = dto.Hood;
            clothes.Decolletage = dto.Decolletage;

            clothes.UpdateDate = DateTime.Now;

            _dbContext.PulloversAndSweats.Update(clothes);
            _dbContext.SaveChanges();
        }
    }
}
