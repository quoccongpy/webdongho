using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DongHo.Model;
using DongHo.DataAcess.IRepository;

namespace WebDongHo.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  IActionResult Index()
        {
            var data = _unitOfWork.Brand.GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand model)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Brand.Add(model);
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
            var data =  _unitOfWork.Brand.GetFirstOrDefault(a => a.Id == id);
            if(data==null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(Brand model)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Brand.Update(model);
                _unitOfWork.Save();
                TempData["Succes"] = "Cập nhập thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var data = _unitOfWork.Brand.GetFirstOrDefault(a => a.Id == id);
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
            var data = _unitOfWork.Brand.GetFirstOrDefault(a => a.Id == id);
            if(data==null)
            {
                return NotFound();
            }
           else
            {
                _unitOfWork.Brand.Remove(data);
                _unitOfWork.Save();
                TempData["Succes"] = "Xoá thành công";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
