using eCommerce_Backend.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_xUniTest.DummyData
{
    public class ProductInCategoryFakeData
    {
        public static List<ProductInCategory> ListProductInCategory()
        {
            return new List<ProductInCategory>()
            {
                new ProductInCategory(){
                    CategoriesId = 10000,
                    ProductsId = 1
                },
                new ProductInCategory(){
                    CategoriesId = 10001,
                    ProductsId = 2
                }
            };
        }
    }
}
