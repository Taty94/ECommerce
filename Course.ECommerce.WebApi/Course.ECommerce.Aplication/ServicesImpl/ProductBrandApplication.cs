using AutoMapper;
using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    public class ProductBrandApplication : IProductBrandApplication
    {
        private readonly IGenericRepository<ProductBrand> brandRepository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateProductBrandDto> validator;

        public ProductBrandApplication(IGenericRepository<ProductBrand> repository, IMapper mapper, IValidator<CreateProductBrandDto> validator)
        {
            this.brandRepository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<ICollection<ProductBrandDto>> GetAsync()
        {
            var query = await brandRepository.GetAllAsync();
            //query = query.Where(pb => !pb.IsDeleted).ToList();

            #region mapper
            //return query.Select(pb => new ProductBrandDto
            //{
            //    Id = pb.Id,
            //    Description = pb.Description,
            //    CreationDate = pb.CreationDate
            //}).ToList();
            #endregion

            #region automapper
            var result = mapper.Map<ICollection<ProductBrandDto>>(query);
            #endregion

            return result;
        }

        public async Task<ProductBrandDto> GetByIdAsync(string id)
        {
            var productBrand = await brandRepository.GetByIdAsync(id);

            #region NotFoundException
            if (productBrand == null)
            {
                throw new NotFoundException($"Marca con Id:{id} no existe");
            }
            #endregion

            #region mapper
            //return new ProductBrandDto
            //{
            //    Id = productBrand.Id,
            //    Description = productBrand.Description,
            //    CreationDate = productBrand.CreationDate
            //};
            #endregion 

            #region automapper
            return mapper.Map<ProductBrandDto>(productBrand);
            #endregion
        }

        public async Task<ProductBrandDto> InsertAsync(CreateProductBrandDto prodBrandDto)
        {
            #region validator
            await validator.ValidateAndThrowAsync(prodBrandDto);
            #endregion

            #region mapper
            //var productBrand = new ProductBrand()
            //{
            //    Id = prodBrandDto.Id,
            //    Description = prodBrandDto.Description,
            //    IsDeleted = false,
            //    CreationDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now
            //};
            #endregion

            #region automapper
            var productBrand = mapper.Map<ProductBrand>(prodBrandDto);
            productBrand.ModifiedDate = DateTime.Now;
            #endregion

            var result = await brandRepository.InsertAsync(productBrand);
            return await GetByIdAsync(result.Id);
        }

        public async Task<ProductBrandDto> UpdateAsync(string id, CreateProductBrandDto prodBrandDto)
        {
            #region validator
            await validator.ValidateAndThrowAsync(prodBrandDto);
            #endregion

            var productBrand = await brandRepository.GetByIdAsync(id);

            #region NotFoundException
            if (productBrand == null)
            {
                throw new NotFoundException($"Marca con Id:{id} no existe");
            }
            #endregion

            //productBrand.Description = prodBrandDto.Description;
            //productBrand.ModifiedDate = DateTime.Now;

            #region automapper
            //productBrand = mapper.Map<ProductBrand>(prodBrandDto);
            productBrand = mapper.Map(prodBrandDto,productBrand);
            productBrand.ModifiedDate = DateTime.Now;
            #endregion

            var result = await brandRepository.UpdateAsync(productBrand);
            return await GetByIdAsync(result.Id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var isFound = await brandRepository.DeleteAsync(id);
            if (!isFound) throw new NotFoundException($"Marca con Id:{id}, no se elimino, no existe");
            return isFound;
        }

        public async Task<ResultPagination<ProductBrandDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc")
        {
            var query = brandRepository.GetQueryable();

            //Filtra los no eliminados
            query = query.Where(pb => !pb.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                        pb => pb.Description.ToUpper().Contains(search));
            }

            //1.Total
            var total = await query.CountAsync();

            //3.Ordenamiento
            if (!string.IsNullOrEmpty(sort))
            {
                //Soportar Campos
                //sort => description. Other trwo exception
                switch (sort.ToUpper())
                {
                    case "DESCRIPTION":
                        query = query.OrderBy(p => p.Description);
                        break;
                    default:
                        throw new ArgumentException($"The parameter sort {sort} not support");
                }
            }

            //2.Pagination
            query = query.Skip(offset).Take(limit);

            #region mapper
            //var items = await query.Select(pb => new ProductBrandDto
            //{
            //    Id = pb.Id,
            //    Description = pb.Description,
            //    CreationDate = pb.CreationDate
            //}).ToListAsync();
            #endregion

            #region automapper
            var items = mapper.Map<List<ProductBrandDto>>(query);
            #endregion


            var resultQuery = new ResultPagination<ProductBrandDto>();
            resultQuery.Total = total;
            resultQuery.Items = items;

            return resultQuery;
        }
    }
}
