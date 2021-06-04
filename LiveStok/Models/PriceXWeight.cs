using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace livestock.Models
{
    public class PriceXWeight
    {
        [Key]
        public Guid ID { get; set; }
        [Display(Name ="Weight Class")]
        public int WgtCl { get; set; }
        public Guid TypeOfAnimalID { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public decimal Price { get; set; }


        public TypeOfAnimal TypeOfAnimal { get; set; }


    }
}

