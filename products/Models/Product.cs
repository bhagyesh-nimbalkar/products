﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace products.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }


        public string ProductName { get; set; }

        public string Status { get; set; }

        public DateTime Timestamp { get; set; }

        public int Mrp { get; set; }

    }
}
