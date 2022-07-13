using AutoMapper;
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
        private readonly IMapper mapper;

        public ProductTypeApplication(IGenericRepository<ProductType> repository, IMapper mapper)
        {
            this.typeRepository = repository;
            this.mapper = mapper;
        }

        public async Task<ICollection<ProductTypeDto>> GetAsync()
        {
            var query = await typeRepository.GetAllAsync();
            //query = query.Where(pt => !pt.IsDeleted).ToList();

            #region mapper
            //return query.Select(pt => new ProductTypeDto
            //{
            //    Id = pt.Id,
            //    Description = pt.Description,
            //    CreationDate = pt.CreationDate
            //}).ToList();
            #endregion

            #region automapper
            return mapper.Map<ICollection<ProductTypeDto>>(query);
            #endregion


        }

        public async Task<ProductTypeDto> GetByIdAsync(string id)
        {
            var prodType = await typeRepository.GetByIdAsync(id);

            #region mapper
            //return new ProductTypeDto
            //{
            //    Id = prodType.Id,
            //    Description = prodType.Description,
            //    CreationDate = prodType.CreationDate
            //};
            #endregion

            #region automapper
            return mapper.Map<ProductTypeDto>(prodType);
            #endregion
        }

        public async Task<ProductTypeDto> InsertAsync(CreateProductTypeDto productTypeDto)
        {
            #region mapper
            //var productType = new ProductType()
            //{
            //    Id = productTypeDto.Id,
            //    Description = productTypeDto.Description,
            //    IsDeleted = false,
            //    CreationDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now
            //};
            #endregion

            #region automapper
            var productType = mapper.Map<ProductType>(productTypeDto);
            productType.CreationDate = DateTime.Now;
            productType.ModifiedDate = DateTime.Now;
            #endregion

            var result = await typeRepository.InsertAsync(productType);
            return await GetByIdAsync(result.Id);
        }

        public async Task<ProductTypeDto> UpdateAsync(string id, CreateProductTypeDto productTypeDto)
        {
            var productType = await typeRepository.GetByIdAsync(id);
            
            //productType.Description = productTypeDto.Description;
            //productType.ModifiedDate = DateTime.Now;

            #region automapper
            productType = mapper.Map<ProductType>(productTypeDto);
            productType.ModifiedDate = DateTime.Now;
            #endregion

            var result = await typeRepository.UpdateAsync(productType);
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

            //var items = await query.Select(pb => new ProductTypeDto
            //{
            //    Id = pb.Id,
            //    Description = pb.Description,
            //    CreationDate = pb.CreationDate
            //}).ToListAsync();

            var items = mapper.Map<List<ProductTypeDto>>(query);

            var resultQuery = new ResultPagination<ProductTypeDto>();
            resultQuery.Total = total;
            resultQuery.Items = items;

            return resultQuery;
        }
    }
}
