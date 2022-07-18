using AutoMapper;
using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Repositories;
using FluentValidation;
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
        private readonly IValidator<CreateProductTypeDto> validator;

        public ProductTypeApplication(IGenericRepository<ProductType> repository, IMapper mapper, IValidator<CreateProductTypeDto> validator)
        {
            this.typeRepository = repository;
            this.mapper = mapper;
            this.validator = validator;
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

            #region NotFoundException
            if (prodType == null)
            {
                throw new NotFoundException($"Tipo de producto con Id:{id} no existe");
            }
            #endregion

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
            #region validator
            await validator.ValidateAndThrowAsync(productTypeDto);
            #endregion

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
            productType.ModifiedDate = DateTime.Now;
            #endregion

            var result = await typeRepository.InsertAsync(productType);
            return await GetByIdAsync(result.Id);
        }

        public async Task<ProductTypeDto> UpdateAsync(string id, CreateProductTypeDto productTypeDto)
        {
            #region validator
            await validator.ValidateAndThrowAsync(productTypeDto);
            #endregion

            var productType = await typeRepository.GetByIdAsync(id);

            #region NotFoundException
            if (productType == null)
            {
                throw new NotFoundException($"Tipo de producto con Id:{id} no existe");
            }
            #endregion

            //productType.Description = productTypeDto.Description;
            //productType.ModifiedDate = DateTime.Now;

            #region automapper
            productType = mapper.Map(productTypeDto, productType);
            productType.ModifiedDate = DateTime.Now;
            #endregion

            var result = await typeRepository.UpdateAsync(productType);
            return await GetByIdAsync(result.Id);

        }

        public async Task<bool> DeleteAsync(string id)
        {
            var isFound = await typeRepository.DeleteAsync(id);
            if (!isFound) throw new NotFoundException($"Tipo de producto con Id:{id} no se elimino, no existe");
            return isFound;
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
