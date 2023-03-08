using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace DongHo.Model.ViewModels
{
    public class ProductVM
    {
        public Product product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
    }
}
