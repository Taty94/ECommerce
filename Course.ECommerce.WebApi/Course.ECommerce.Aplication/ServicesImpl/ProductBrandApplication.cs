using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    public class ProductBrandApplication : IProductBrandApplication
    {
        private readonly IGenericRepository<ProductBrand> brandRepository;

        public ProductBrandApplication(IGenericRepository<ProductBrand> repository)
        {
            this.brandRepository = repository;
        }

        public async Task<ICollection<ProductBrandDto>> GetAsync()
        {
            var query = await brandRepository.GetAllAsync();
            query = query.Where(pb => !pb.IsDeleted).ToList();
            return query.Select(pb => new ProductBrandDto
            {
                Id = pb.Id,
                Description = pb.Description,
                CreationDate = pb.CreationDate
            }).ToList();
        }

        public async Task<ProductBrandDto> GetByIdAsync(string id)
        {
            var productBrand = await brandRepository.GetByIdAsync(id);
            return new ProductBrandDto
            {
                Id = productBrand.Id,
                Description = productBrand.Description,
                CreationDate = productBrand.CreationDate
            };
        }

        public async Task<ProductBrandDto> PostAsync(CreateProductBrandDto prodBrandDto)
        {
            var productBrand = new ProductBrand()
            {
                Id = prodBrandDto.Id,
                Description = prodBrandDto.Description,
                IsDeleted = false,
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            var result = await brandRepository.PostAsync(productBrand);
            return await GetByIdAsync(result.Id);
        }

        public async Task<ProductBrandDto> PutAsync(string id, CreateProductBrandDto prodBrandDto)
        {
            var productBrand = await brandRepository.GetByIdAsync(id);

            productBrand.Description = prodBrandDto.Description;
            productBrand.ModifiedDate = DateTime.Now;

            var result = await brandRepository.PutAsync(productBrand);
            return await GetByIdAsync(result.Id);
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            return await brandRepository.DeleteAsync(Id);
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


            var items = await query.Select(pb => new ProductBrandDto
            {
                Id = pb.Id,
                Description = pb.Description,
                CreationDate = pb.CreationDate
            }).ToListAsync();

            var resultQuery = new ResultPagination<ProductBrandDto>();
            resultQuery.Total = total;
            resultQuery.Items = items;

            return resultQuery;
        }
    }
}
