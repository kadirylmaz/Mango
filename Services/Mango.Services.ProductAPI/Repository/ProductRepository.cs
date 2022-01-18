using AutoMapper;
using Mango.Services.ProductAPI.Data.DbContexts;
using Mango.Services.ProductAPI.Domain;
using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto product)
        {
            var entity = _mapper.Map<ProductDto,Product>(product);
            if (entity.ProductId > 0)
            {
                _db.Products.Update(entity);
            }
            else
            {
                _db.Products.Add(entity);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(entity);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(x=>x.ProductId == id);
                if (product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _db.Products.Where(x=>x.ProductId == id).FirstOrDefaultAsync(); 
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> products = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
