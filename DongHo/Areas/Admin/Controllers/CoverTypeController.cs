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
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  IActionResult Index()
        {
            var data = _unitOfWork.CoverType.GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType model)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(model);
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
            var data =  _unitOfWork.CoverType.GetFirstOrDefault(a => a.Id == id);
            if(data==null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(CoverType model)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(model);
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
            var data = _unitOfWork.CoverType.GetFirstOrDefault(a => a.Id == id);
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
            var data = _unitOfWork.CoverType.GetFirstOrDefault(a => a.Id == id);
            if(data==null)
            {
                return NotFound();
            }
           else
            {
                _unitOfWork.CoverType.Remove(data);
                _unitOfWork.Save();
                TempData["Succes"] = "Xoá thành công";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
