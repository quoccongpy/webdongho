using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DongHo.Model;
using DongHo.DataAcess.IRepository;

namespace DongHo.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  IActionResult Index()
        {
            var data = _unitOfWork.Category.GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Category.Add(model);
                _unitOfWork.Save();
                TempData["Succes"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
           
        }

        public  IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var data =  _unitOfWork.Category.GetFirstOrDefault(a => a.Id == id);
            if(data==null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(Category model)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Category.Update(model);
                _unitOfWork.Save();
                TempData["Succes"] = "Cập nhập thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var data = _unitOfWork.Category.GetFirstOrDefault(a => a.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var data = _unitOfWork.Category.GetFirstOrDefault(a => a.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Category.Remove(data);
                _unitOfWork.Save();
                TempData["Succes"] = "Xoá thành công";
                return RedirectToAction(nameof(Index));
            }
        }

        //[HttpDelete]
        //public IActionResult Delete(int? id)
        //{
        //    var data = _unitOfWork.Category.GetFirstOrDefault(a => a.Id == id);
        //    if(data==null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        _unitOfWork.Category.Remove(data);
        //        _unitOfWork.Save();
        //        return Json(new { success = true, message = "Xoá thành công" });
        //    }    

        //}
    }
}
