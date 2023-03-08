using DongHo.DataAcess.IRepository;
using DongHo.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongHo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var datalist = _unitOfWork.Company.GetAll();
            return Json(new { data = datalist });
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Add(model);
                _unitOfWork.Save();
                TempData["Succes"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Update(int? id)
        {
            var data = _unitOfWork.Company.GetFirstOrDefault(a => a.Id == id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Company company)
        {
           if(ModelState.IsValid)
            {
                _unitOfWork.Company.Update(company);
                _unitOfWork.Save();
                TempData["Succes"] = "Cập nhập thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var data = _unitOfWork.Company.GetFirstOrDefault(a => a.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Company.Remove(data);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Xoá thành công" });
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
