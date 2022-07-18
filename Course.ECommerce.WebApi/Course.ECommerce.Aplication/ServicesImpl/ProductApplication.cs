using AutoMapper;
using AutoMapper.QueryableExtensions;
using Course.ECommerce.Aplication.Classes;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Aplication.Services;
using Course.ECommerce.Domain.Entities;
using Course.ECommerce.Domain.Repositories;
using Course.ECommerce.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Course.ECommerce.Aplication.ServicesImpl
{
    /// <summary>
    /// Servicio de aplicacion, para los catalogos de productos
    /// </summary>
    public class ProductApplication : IProductApplication
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateProductDto> validator;

        public ProductApplication(IGenericRepository<Product> repository, IMapper mapper, IValidator<CreateProductDto> validator)
        {
            this.productRepository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            var products = productRepository.GetQueryable();
            #region automapper
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Product, ProductDto>()
                                .ForMember(p => p.ProductBrand, x => x.MapFrom(org => org.ProductBrand.Description))
                                .ForMember(p => p.ProductType, x => x.MapFrom(org => org.ProductType.Description)));
            var resultQuery = products.ProjectTo<ProductDto>(configuration);
            #endregion

            return await resultQuery.ToListAsync();
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var query = productRepository.GetQueryable();
            query = query.Where(p => p.Id == id);
            
            #region NotFoundException
            if (query.SingleOrDefaultAsync().Result == null)
            {
                throw  new NotFoundException($"Producto con Id:{id} no existe");
            }
            #endregion

            #region mappear
            //var resultQuery = await query.Select(p => new ProductDto
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Price = p.Price,
            //    Description = p.Description,
            //    ProductBrand = p.ProductBrand.Description,
            //    ProductType = p.ProductType.Description,
            //    CreationDate = p.CreationDate,
            //    ModifiedDate = p.ModifiedDate
            //}).SingleOrDefaultAsync();
            #endregion

            #region automapper
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Product, ProductDto>()
                                .ForMember(p => p.ProductBrand, x => x.MapFrom(org => org.ProductBrand.Description))
                                .ForMember(p => p.ProductType, x => x.MapFrom(org => org.ProductType.Description)));
            var resultQuery = query.ProjectTo<ProductDto>(configuration);
            #endregion
            return await resultQuery.SingleOrDefaultAsync();
        }

        public async Task<ProductDto> InsertAsync(CreateProductDto productDto)
        {
            #region validator
            //var isValid = await validator.ValidateAsync(productDto, options =>
            //{
            //    options.ThrowOnFailures();
            //    options.IncludeRuleSets("ProductInfo", "ProductRelation").IncludeRulesNotInRuleSet();
            //});
            await validator.ValidateAndThrowAsync(productDto);
            #endregion

            #region mappear
            //var product = new Product()
            //{
            ////    Id = Guid.NewGuid(),
            //    Name = productDto.Name,
            //    Price = productDto.Price,
            //    Description = productDto.Description,
            //    ProductBrandId = productDto.ProductBrandId,
            //    ProductTypeId = productDto.ProductTypeId,
            //    CreationDate = DateTime.Now
            //};
            #endregion

            #region automapper
            var product = mapper.Map<Product>(productDto);
            product.ModifiedDate = DateTime.Now;
            #endregion

            var result = await productRepository.InsertAsync(product);
            return await GetByIdAsync(result.Id);
        }

        public async Task<ProductDto> UpdateAsync(Guid id, CreateProductDto productDto)
        {
            #region validator
            //var isValid = await validator.ValidateAsync(productDto, options =>
            //{
            //    options.ThrowOnFailures();
            //    options.IncludeRuleSets("ProductInfo", "ProductRelation").IncludeRulesNotInRuleSet();
            //});
            await validator.ValidateAndThrowAsync(productDto);
            #endregion

            var product = await productRepository.GetByIdAsync(id);

            #region NotFoundException
            if (product == null)
            {
                throw new NotFoundException($"Producto con Id:{id} no se modificó, no existe");
            }
            #endregion

            #region mappear
            //product.Name = productDto.Name;
            //product.Price = productDto.Price;
            //product.Description = productDto.Description;
            //product.ProductBrandId = productDto.ProductBrandId;
            //product.ProductTypeId = productDto.ProductTypeId;
            //product.ModifiedDate = DateTime.Now;
            #endregion

            #region automapper
            //product = mapper.Map<Product>(productDto);
            product = mapper.Map(productDto,product);
            product.ModifiedDate = DateTime.Now;
            #endregion

            var result = await productRepository.UpdateAsync(product);
            return await GetByIdAsync(result.Id);
             
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var isFound = await productRepository.DeleteAsync(id);
            if(!isFound) throw new NotFoundException($"Producto con Id:{id} , no se eliminó, no existe");
            return isFound;
        }

        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "", int offset = 0, int limit = 3, string sort = "Name", string order = "asc")
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

            #region mapper
            //var items = await query.Select(p => new ProductDto
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Price = p.Price,
            //    Description = p.Description,
            //    ProductBrand = p.ProductBrand.Description,
            //    ProductType = p.ProductType.Description,
            //    CreationDate = p.CreationDate,
            //    ModifiedDate = p.ModifiedDate
            //}).ToListAsync();
            #endregion

            #region automapper
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Product, ProductDto>()
                                .ForMember(p => p.ProductBrand, x => x.MapFrom(org => org.ProductBrand.Description))
                                .ForMember(p => p.ProductType, x => x.MapFrom(org => org.ProductType.Description)));
            var items = await query.ProjectTo<ProductDto>(configuration).ToListAsync(); ;
            #endregion

            var resultQuery = new ResultPagination<ProductDto>();
            resultQuery.Total = total;
            resultQuery.Items = items;

            return resultQuery;


        }
    }


}