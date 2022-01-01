namespace Shop.Services.Category
{
    using Shop.Models.Category;
    using System.Collections.Generic;
    using Data.Models;
    public interface ICategoryService
    {
        List<CategoryListingViewModel> GetCategories();
        Category GetCategory(int Id);
    }
}
