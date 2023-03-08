using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DongHo.Model
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime ShipppingDate { get; set; }
        public double OrderTotal { get; set; }

        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }

        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }

        public string SessionId { get; set; }
        public string PaymentIntentId { get; set; }


        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}
