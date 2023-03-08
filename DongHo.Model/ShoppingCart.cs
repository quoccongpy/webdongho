using DongHo.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DongHo.Model
{
    public class ShoppingCart
    {
		[Key]
		public int Id { get; set; }
		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		[ValidateNever]
		public Product Product { get; set; }
		[Range(1, 1000)]
		public int Count { get; set; }
		[Range(1, 10000, ErrorMessage = "Please enter a value between 1 and 10000")]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
		public double Price { get; set; }
	}
}
