using AutoMapper;
using ClothesStore.Entities;
using ClothesStore.Models;
using ClothesStore.Exceptions;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using ClothesStore.Authorization;

namespace ClothesStore.Services
{
    public class TrousersService : IItemService<TrousersDto, AddTrousersDto>
    {
        private readonly ClothesStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TrousersService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public TrousersService(ClothesStoreDbContext dbContext, IMapper mapper, ILogger<TrousersService> logger, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;

        }

        public int Add(AddTrousersDto dto)
        {
            _logger.LogInformation("Trousers: CREATE action invoked!");

            var trousers = _mapper.Map<Trousers>(dto);
            trousers.CreatedById = _userContextService.GetUserId;
            _dbContext.Trousers.Add(trousers);
            _dbContext.SaveChanges();
            return trousers.Id;
        }

        public void DeleteById(int id)
        {
            _logger.LogInformation("Trousers: DELETE action invoked!");

            var trousers = _dbContext
                .Trousers
                .FirstOrDefault(t => t.Id == id);

            if (trousers is null)
                throw new NotFoundException($"Item with id: {id} not found!");

            var result = _authorizationService.AuthorizeAsync(_userContextService.User, trousers, new ResourceRequirement(ResourceOperation.Delete)).Result;

            if (!result.Succeeded)
                throw new ForbidExepction("Trousers: Unauthorized DELETE action!");

            _dbContext.Trousers.Remove(trousers);
            _dbContext.SaveChanges();
        }

        public PagesResult<TrousersDto> GetAll(AnnouncementQuery query)
        {
            _logger.LogInformation("Trousers: GET ALL action invoked!");

            var baseQuery = _dbContext
                .Trousers
                .Where(r => (query.SearchPhrase == null) || (r.Description.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())
                    || r.Brand.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy) && (!(query.SortDirection is null)))
            {
                var sortByColumn = new Dictionary<string, Expression<Func<Trousers, object>>>()
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

            var countOfTrousers = baseQuery.Count();

            var listDto = _mapper.Map<List<TrousersDto>>(list);
            var result = new PagesResult<TrousersDto>(listDto, countOfTrousers, query.PageSize, query.PageNumber);

            return result;
        }

        public TrousersDto GetById(int id)
        {
            _logger.LogInformation("Trousers: GET BY ID action invoked!");

            var trousers = _dbContext
                .Trousers
                .FirstOrDefault(x => x.Id == id);

                if (trousers is null)
                    throw new NotFoundException($"Item with id: {id} not found!");

            var trousersDto = _mapper.Map<TrousersDto>(trousers);
            return trousersDto;
        }

        public void UpdateById(int id, AddTrousersDto dto)
        {
            _logger.LogInformation("Trousers: UPDATE action invoked!");

            var trousers = _dbContext
                .Trousers
                .FirstOrDefault(b => b.Id == id);

            if (trousers is null)
                throw new NotFoundException($"Item with id: {id} not found!");

            var result = _authorizationService.AuthorizeAsync(_userContextService.User, trousers, new ResourceRequirement(ResourceOperation.Delete)).Result;

            if (!result.Succeeded)
                throw new ForbidExepction("Trousers: Unauthorized DELETE action!");

            trousers.Description = dto.Description;
            trousers.Price = dto.Price;
            trousers.Title = dto.Title;
            trousers.Material = dto.Material;
            trousers.Colour = dto.Colour;
            trousers.Sex = dto.Sex;
            trousers.Type = dto.Type;
            trousers.Fastener = dto.Fastener;
            trousers.Waisted = dto.Waisted;
            trousers.CountOfPocket = dto.CountOfPocket;
            trousers.TrouserLeg = dto.TrouserLeg;

            trousers.UpdateDate = DateTime.Now;

            _dbContext.Trousers.Update(trousers);
            _dbContext.SaveChanges();
        }


    }
}
