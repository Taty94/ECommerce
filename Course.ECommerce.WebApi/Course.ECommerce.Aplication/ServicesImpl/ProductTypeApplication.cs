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
    public class ProductTypeApplication : IProductTypeApplication
    {
        private readonly IGenericRepository<ProductType> typeRepository;

        public ProductTypeApplication(IGenericRepository<ProductType> repository)
        {
            this.typeRepository = repository;
        }

        public async Task<ICollection<ProductTypeDto>> GetAsync()
        {
            var query = await typeRepository.GetAllAsync();
            query = query.Where(pt => !pt.IsDeleted).ToList();
            return query.Select(pt => new ProductTypeDto
            {
                Id = pt.Id,
                Description = pt.Description,
                CreationDate = pt.CreationDate
            }).ToList();
        }

        public async Task<ProductTypeDto> GetByIdAsync(string id)
        {
            var prodType = await typeRepository.GetByIdAsync(id);
            return new ProductTypeDto
            {
                Id = prodType.Id,
                Description = prodType.Description,
                CreationDate = prodType.CreationDate
            };
        }

        public async Task<ProductTypeDto> PostAsync(CreateProductTypeDto productTypeDto)
        {
            var productType = new ProductType()
            {
                Id = productTypeDto.Id,
                Description = productTypeDto.Description,
                IsDeleted = false,
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            var result = await typeRepository.PostAsync(productType);
            return await GetByIdAsync(result.Id);
        }

        public async Task<ProductTypeDto> PutAsync(string id, CreateProductTypeDto productTypeDto)
        {
            var productType = await typeRepository.GetByIdAsync(id);
            productType.Description = productTypeDto.Description;
            productType.ModifiedDate = DateTime.Now;

            var result = await typeRepository.PutAsync(productType);
            return await GetByIdAsync(result.Id);

        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await typeRepository.DeleteAsync(id);
        }

        public async Task<ResultPagination<ProductTypeDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Description", string order = "asc")
        {
            var query = typeRepository.GetQueryable();

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


            var items = await query.Select(pb => new ProductTypeDto
            {
                Id = pb.Id,
                Description = pb.Description,
                CreationDate = pb.CreationDate
            }).ToListAsync();

            var resultQuery = new ResultPagination<ProductTypeDto>();
            resultQuery.Total = total;
            resultQuery.Items = items;

            return resultQuery;
        }
    }
}
