using AutoMapper;
using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Repositories;
using Course.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    /// <summary>
    /// Servicio de aplicacion, para los catalogos de productos
    /// </summary>
    public class ProductApplication : IProductApplication
    {
        private readonly IGenericRepository<Product> productRepository;

        public ProductApplication(IGenericRepository<Product> repository, IMapper mapper)
        {
            this.productRepository = repository;
        }

        //public async Task<ICollection<ProductDto>> GetProductsAsync()
        //{
        //    var query = productRepository.GetQueryable();

        //    #region mappear Dto
        //    query = query.Where(p => !p.IsDeleted);
        //    var resultQuery = await query.Select(p=> new ProductDto
        //                        {
        //                            Id = p.Id,
        //                            Name = p.Name,
        //                            Price = p.Price,
        //                            Description = p.Description,
        //                            ProductBrand = p.ProductBrand.Description,
        //                            ProductType = p.ProductType.Description,
        //                            CreationDate = p.CreationDate,
        //                            ModifiedDate = p.ModifiedDate
        //                        }).ToListAsync();
        //    #endregion

        //    return resultQuery;
        //}

        public async Task<ProductDto> GetProductByIdAsync(Guid Id)
        {
            var query = productRepository.GetQueryable();
            query = query.Where(p => p.Id == Id);

            #region mappear
            var resultQuery = await query.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ProductBrand = p.ProductBrand.Description,
                ProductType = p.ProductType.Description,
                CreationDate = p.CreationDate,
                ModifiedDate = p.ModifiedDate
            }).SingleOrDefaultAsync();
            #endregion
            return resultQuery;
        }

        public async Task<ProductDto> PostAsync(CreateProductDto productDto)
        {
            #region mappear
            var product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                ProductBrandId = productDto.ProductBrandId,
                ProductTypeId = productDto.ProductTypeId,
                CreationDate = DateTime.Now
            };
            #endregion
            var result = await productRepository.PostAsync(product);
            return await GetProductByIdAsync(result.Id);
        }

        public async Task<ProductDto> PutAsync(Guid id, CreateProductDto productDto)
        {
            var product = await productRepository.GetByIdAsync(id);
            
            #region mappear
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.ProductBrandId = productDto.ProductBrandId;
            product.ProductTypeId = productDto.ProductTypeId;
            product.ModifiedDate = DateTime.Now;
            #endregion

            var result = await productRepository.PutAsync(product);
            return await GetProductByIdAsync(result.Id);
             
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            return await productRepository.DeleteAsync(Id);
        }

        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {
            var query = productRepository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(p => !p.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                        p => p.Name.ToUpper().Contains(search));
                        //|| p.Code.ToUpper().StarsWith(search)
            }

            //1.Total
            var total = await query.CountAsync();

            //3.Ordenamiento
            if (!string.IsNullOrEmpty(sort))
            {
                //Soportar Campos
                //sort => name or price. Other trwo exception
                switch (sort.ToUpper())
                {
                    case "NAME":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "PRICE":
                        query = query.OrderBy(p => p.Price);
                        break;
                    default:
                        throw new ArgumentException($"The parameter sort {sort} not support");
                }
            }

            //2.Pagination
            query = query.Skip(offset).Take(limit);


            var items = await query.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ProductBrand = p.ProductBrand.Description,
                ProductType = p.ProductType.Description,
                CreationDate = p.CreationDate,
                ModifiedDate = p.ModifiedDate
            }).ToListAsync();

            var resultQuery = new ResultPagination<ProductDto>();
            resultQuery.Total = total;
            resultQuery.Items = items;

            return resultQuery;


        }
    }


}