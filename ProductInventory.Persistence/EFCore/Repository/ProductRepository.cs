﻿using ProductInventory.Application.Repository;
using ProductInventory.Domain.Entities;
using ProductInventory.Persistence.EFCore.Context;

namespace ProductInventory.Persistence.EFCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(Guid id) 
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product) 
        { 
            _context.Products.Add(product);
        }

        public async Task UpdateAsync(Product product) 
        { 
            _context.Products.Update(product); 
        }

        public async Task DeleteAsync(Product product) 
        { 
            _context.Products.Remove(product);
        }
    }
}
