﻿using System.ComponentModel.DataAnnotations;

namespace Drink_Wholesale.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
    }
}