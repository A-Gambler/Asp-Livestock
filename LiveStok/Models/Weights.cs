using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace livestock.Models
{
    public class Weight
    {
        [Key]
        public Guid ID { get; set; }

        public Guid WeightSheetID { get; set; }
        public Decimal weight { get; set; }

        public WeightSheet weightSheet { get; set; }
    }
}