namespace Shop.Services.Category
{
    using Shop.Data;
    using Shop.Models.Category;
    using System.Collections.Generic;
    using System.Linq;
    public class CategoryService : ICategoryService
    {
        private readonly ShopDbContext context;
        public CategoryService(ShopDbContext context)
        {
            this.context = context;
        }
        public List<CategoryListingViewModel> GetCategories()
        => context.Categories
            .OrderByDescending(c => c.Name)
            .Select(c => new CategoryListingViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}
