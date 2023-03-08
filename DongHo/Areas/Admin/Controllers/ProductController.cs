
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using DongHo.Model;
using DongHo.Model.ViewModels;
using DongHo.DataAcess.IRepository;
using DongHo.Utility;

namespace WebDongHo.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public  IActionResult Index()
        {
            var data = _unitOfWork.Product.GetAll();
            return View(data);
        }
       public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new ProductVM();
            productVM.product = new Product();
            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            productVM.CoverTypeList = _unitOfWork.CoverType.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            productVM.BrandList = _unitOfWork.Brand.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            //if (id==null || id ==0)
            //{
               
            //    return View(productVM);
            //}
            //else
            //{
            //    productVM.product = _unitOfWork.Product.GetFirstOrDefault(a => a.Id == id);
            //}
            return View(productVM);
        }

        #region viewbag
        //public IActionResult Upsert(int? id)
        //{
        //    Product product = new Product();
        //    IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(a => new SelectListItem()
        //    {
        //        Text = a.Name,
        //        Value = a.Id.ToString()
        //    });

        //    IEnumerable<SelectListItem> coverTypeList = _unitOfWork.CoverType.GetAll().Select(a => new SelectListItem()
        //    {
        //        Text = a.Name,
        //        Value = a.Id.ToString()
        //    });
        //    IEnumerable<SelectListItem> brandList = _unitOfWork.Brand.GetAll().Select(a => new SelectListItem()
        //    {
        //        Text = a.Name,
        //        Value = a.Id.ToString()
        //    });
        //    if (id == null || id == 0)
        //    {
        //        ViewBag.CategoryList = categoryList;
        //        ViewBag.CoverTypeList = coverTypeList;
        //        ViewBag.BrandList = brandList;
        //        return View(product);
        //    }
        //    else
        //    {

        //    }
        //    return View(product);
        //}
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {

            if(ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;   
                _unitOfWork.Product.Add(productVM.product);
                
                //else
                //{
                //    _unitOfWork.Product.Update(productVM.product);
                //}
                _unitOfWork.Save();

                if (files.Count > 0)
                {
                    var uploads = Path.Combine(webRootPath, "images");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filesStream = new FileStream(Path.Combine(uploads, productVM.product.Id + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStream);
                    }
                    productVM.product.ImageUrl = @"\images\" + productVM.product.Id + extension;
                }
                else
                {
                    var uploads = Path.Combine(webRootPath, @"images\" + SD.test);
                    System.IO.File.Copy(uploads, webRootPath + @"\images\" + productVM.product.Id + ".png");
                    productVM.product.ImageUrl = @"\images\" + productVM.product.Id + ".png";

                }
                _unitOfWork.Save();
                TempData["Succes"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }


        public IActionResult Update(int? id)
        {

            ProductVM productVM = new ProductVM();
            //productVM.product = new Product();
            productVM.product = _unitOfWork.Product.GetFirstOrDefault(a => a.Id == id);
            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            productVM.CoverTypeList = _unitOfWork.CoverType.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            productVM.BrandList = _unitOfWork.Brand.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });          
          
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductVM productVM)
        {           
                string webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
            //_unitOfWork.Product.Update(productVM.product);

            //_unitOfWork.Save();
            var imageFromDb = _unitOfWork.Product.GetFirstOrDefault(a => a.Id == productVM.product.Id);
                if (files.Count > 0)
                {
                    var uploads = Path.Combine(webRootPath, "images");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, imageFromDb.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    using (var filesStream = new FileStream(Path.Combine(uploads, productVM.product.Id + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStream);
                    }
                    productVM.product.ImageUrl = @"\images\" + productVM.product.Id + extension;
                }
                else
                {
                    var uploads = Path.Combine(webRootPath, @"images\" + SD.test);
                    System.IO.File.Copy(uploads, webRootPath + @"\images\" + productVM.product.Id + ".png");
                    productVM.product.ImageUrl = @"\images\" + productVM.product.Id + ".png";

                }
                _unitOfWork.Product.Update(productVM.product);
                _unitOfWork.Save();
                TempData["Succes"] = "Cập nhập thành công";
                return RedirectToAction(nameof(Index));
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType,Brand");
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var data = _unitOfWork.Product.GetFirstOrDefault(a => a.Id == id);
            if(data ==null)
            {
                return NotFound();
            }
            else
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (data.ImageUrl!=null)
                {
                   
                    
                    var imagePath = Path.Combine(webRootPath, data.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _unitOfWork.Product.Remove(data);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Xoá thành công" });
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
