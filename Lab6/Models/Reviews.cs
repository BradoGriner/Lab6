using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Models
{
    public class Reviews
    {
        
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(100000, MinimumLength = 10)]
        [Required]
        public string MovieReview { get; set; }

        public int MovieID { get; set; }

    }
}
