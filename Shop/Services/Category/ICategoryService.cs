namespace Shop.Services.Category
{
    using Shop.Models.Category;
    using System.Collections.Generic;
    public interface ICategoryService
    {
        List<CategoryListingViewModel> GetCategories();
    }
}
