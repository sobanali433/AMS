using AMS.Data;
using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace AMS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {


            //var p = _productRepository.GetAll();
            return View();
        }



        [HttpGet]
        public async Task<IActionResult>  _Details(int? id)
        {
            var categories = _productRepository.GetCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();

            if (id == null)
            {

                var model = new ProductModel
                {
                    CategoriesList = categories
                };

                model.IsEdit = false;
                return PartialView("_Details", model);
            }
            else
            {
                var product = await _productRepository.GetByIdAsync(id.Value);

                if (product == null)
                {
                    return NotFound();
                }
                var model = new ProductModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    IsActive = product.IsActive,
                    CategoriesList = categories,
                    ProductType = product.ProductType,
                    SKU = product.SKU,
                    IsEdit = true
                };

                return PartialView("_Details", model);
            }
        }



        [HttpPost]
        public JsonResult GetList()
        {
            //var data = _userRepository.GetAllUsersWithoutSuperAdmin();
            var user = _productRepository.GetProductList();
            
            var result = new
            {
                draw = Request.Form["draw"].FirstOrDefault(),
                recordsTotal = user.Count,
                recordsFiltered = user.Count,
                data = user
            };

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductModel model)
        {
            try
            {
                if (model == null)
                    return Json(new { isSuccess = false, message = "Invalid data" });

                if (model.ProductId == null || model.ProductId == 0)
                {
                    if (model.IsActive == false)
                    {
                        return Json(new { isSuccess = false, message = "Product is InActive" });
                    }

                    var product = new Product
                    {
                        ProductName = model.ProductName,
                        ProductType = model.ProductType,
                        Price = model.Price,
                        SKU = model.SKU,
                        IsActive = model.IsActive,
                        Categories = model.Categories,
                        CategoryId = model.CategoryId,
                        CreatedAt = DateTime.Now

                    };

                    var result = await _productRepository.AddproductAsync(product);

                    return Json(new
                    {
                        isSuccess = result,
                        message = result ? "Product added successfully" : "Failed to add product"
                    });
                }
                else
                {
                    var existingProduct = await _productRepository.GetByIdAsync(model.ProductId);

                    if (existingProduct == null)
                        return Json(new { isSuccess = false, message = "Product not found" });

                    existingProduct.ProductName = model.ProductName;
                    existingProduct.ProductType = model.ProductType;
                    existingProduct.Price = model.Price;
                    existingProduct.SKU = model.SKU;
                    existingProduct.IsActive = model.IsActive;
                    existingProduct.Categories = model.Categories;
                    existingProduct.CreatedAt = DateTime.Now;

                    var result = await _productRepository.UpdateAsync(existingProduct);

                    return Json(new
                    {
                        isSuccess = result,
                        message = result ? "Product updated successfully" : "Failed to update product"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            if (id == null)
                return Json(new { success = false, message = "Invalid ID" });

            var result = _productRepository.ProductDelete(id);

            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }



    }
}
