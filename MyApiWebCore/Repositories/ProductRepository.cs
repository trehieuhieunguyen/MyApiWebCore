﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductStoreContext _context;
        private IMapper _mapper;

        public ProductRepository(ProductStoreContext context, IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddProductAsync(ProductModel model)
        {
            var newProduct = _mapper.Map<Product>(model);
            _context.Products!.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct.Id;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = _context.Products!.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _context.Products!.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductModel>> GetAllProductAsync()
        {
            var product =await _context.Products!.ToListAsync();
            return _mapper.Map<List<ProductModel>>(product);
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            var product =await _context.Products!.FindAsync(id);
            return _mapper.Map<ProductModel>(product);
        }

        public async Task<IEnumerable<Product>> GetProductFilter(int page, int pageSize, string filter)
        {
            var query = _context.Products!.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(p => p.Description!.Contains(filter));
            }
            var products = await query.Skip((page -1 )*pageSize).Take(pageSize).ToListAsync();
            return products;
        }

        public async Task UpdateProductAsync(int id, ProductModel model)
        {
            if(id == model.Id)
            {
                var updateProduct = _mapper.Map<Product>(model);
                _context.Products!.Update(updateProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}
