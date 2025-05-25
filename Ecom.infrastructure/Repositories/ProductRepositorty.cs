using AutoMapper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class ProductRepositorty : GenericRepository<Product>,IProductRepository
    { 
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        public ProductRepositorty(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return false;
            }

            var product = mapper.Map<Product>(productDTO);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var imagePaths = await imageManagementService.AddImageAsync(productDTO.Photo, productDTO.Name);

            var photo = imagePaths.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();

            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null)
            {
                return false;

            }
            var FindProduct = await context.Products.Include(n => n.Category).Include(n => n.Photos).FirstOrDefaultAsync(n => n.Id == updateProductDTO.Id);
            if (FindProduct is null) {
                return false;
            }
            mapper.Map(updateProductDTO,FindProduct);  
            var FindPhoto= await context.Photos.Where(n=>n.ProductId==updateProductDTO.Id).ToListAsync();
            foreach (var item in FindPhoto) {
                imageManagementService.DeleteImageAsync(item.ImageName);
            }
            context.Photos.RemoveRange(FindPhoto);
            var ImagePath = await imageManagementService.AddImageAsync(updateProductDTO.Photo,updateProductDTO.Name);
            var photo= ImagePath.Select(path=>new Photo
            {
                ImageName= path,
                ProductId=updateProductDTO.Id,
            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;

        }

        async Task<bool> IProductRepository.DeleteAsync(Product product)
        {
            var photo = await context.Photos.Where(n=>n.ProductId==product.Id).ToListAsync();
            foreach (var item in photo)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);

            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
 