﻿using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.EF;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using eCommerce_SharedViewModels.Common;
using static eCommerce_SharedViewModels.Utilities.Constants.SystemConstants;
using eCommerce_SharedViewModels.Enums;
using Microsoft.EntityFrameworkCore;
using eCommerce_Backend.Application.Common;
using System.Net.Http.Headers;
using eCommerce_SharedViewModels.EntitiesDto.Product;

namespace eCommerce_Backend.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly eCommerceDbContext _dbContext;
        private readonly IFileStorage _fileStorage;
        public CategoryService(eCommerceDbContext dbContext,
            IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
            _dbContext = dbContext;
        }
        public async Task<ApiResult<bool>> Create(CategoryCreateDto request)
        {
            var category = new Categories()
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
                Status = Status.Available,
            };

            // Save image
            if (request.ThumbnailImage != null)
            {
                category.CategoryImages = new List<CategoryImages>()
                {
                    new CategoryImages()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                    }
                };
            }

            using (_dbContext)
            {
                _dbContext.Add(category);
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<CategoryReadDto>> GetById(int Id)
        {
            var data = await _dbContext.Categories.FindAsync(Id);
            if (data == null)
                return new ApiErrorResult<CategoryReadDto>(ErrorMessage.CategoryNotFound);
            var result = new CategoryReadDto()
            {
                Id = data.Id,
                CategoryName = data.CategoryName,
                Description = data.Description,
                Status = data.Status
                
            };
            return new ApiSuccessResult<CategoryReadDto>(result);
        }

        public async Task<List<CategoryReadDto>> GetList()
        {
            using (_dbContext)
            {
                var data = await _dbContext.Categories.Where(x => x.Status == Status.Available).Select(x => new CategoryReadDto()
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                }).ToListAsync();
                return data;
            }
        }

        public async Task<List<ProductReadDto>> GetListProductById(int categoryId)
        {
            using (_dbContext)
            {
                var data = await _dbContext.Products.Where(x => x.Status == Status.Available && x.CategoiesId == categoryId)
                    .Select(x => new ProductReadDto()
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    Price = x.Price,
                    CategoiesId = x.CategoiesId,
                    UpdatedDate = x.UpdatedDate,
                }).ToListAsync();
                return data;
            }
        }

        public async Task<PagedResult<CategoryReadDto>> GetPaging(CategoryPagingDto request)
        {
            using (_dbContext)
            {
                var query = _dbContext.Categories.Where(x => x.Status == Status.Available).AsQueryable();
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.CategoryName.Contains(request.Keyword));
                }
                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new CategoryReadDto()
                    {
                        Id = x.Id,
                        CategoryName = x.CategoryName,
                        Description = x.Description,
                    }).ToListAsync();
                var pagedResult = new PagedResult<CategoryReadDto>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedResult;
            }
        }

        public async Task<ApiResult<bool>> SoftDelete(int Id)
        {
            using (_dbContext)
            {
                var data = await _dbContext.Categories.FindAsync(Id);
                if (data == null)
                {
                    return new ApiErrorResult<bool>(ErrorMessage.CategoryNotFound);
                }
                data.Status = Status.Disable;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<bool>> Update(int Id, CategoryUpdateDto request)
        {
            using (_dbContext)
            {
                var data = await _dbContext.Categories.FindAsync(Id);
                if (data == null)
                    return new ApiErrorResult<bool>(ErrorMessage.ProductNotFound);
                if (await _dbContext.Categories.AnyAsync(x => x.CategoryName == request.CategoryName && x.Id != Id))
                {
                    return new ApiErrorResult<bool>(ErrorMessage.ProductNameExists);
                }
                data.CategoryName = request.CategoryName;
                data.Description = request.Description;
                //Save image
                if (request.ThumbnailImage != null)
                {
                    var thumbnailImage = await _dbContext.CategoryImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.CategoriesId == Id);
                    if (thumbnailImage != null)
                    {
                        thumbnailImage.FileSize = request.ThumbnailImage.Length;
                        thumbnailImage.ImagePath = await SaveFile(request.ThumbnailImage);
                        _dbContext.CategoryImages.Update(thumbnailImage);
                    }
                }
                await _dbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorage.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + RESOURCES + "/" + USER_IMAGES_FOLDER_NAME + "/" + fileName;
        }
    }
}
