using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiveStok.Models.NonDBModels
{
    public class WeightSheetCalculation
    {
        public List<WeightRange> weightRanges { get; set; }

        [DisplayName("Total Count")]
        public int TotalCount { get; set; }
        [DisplayName("Total Value + Skin")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal TotalValue_SkinPlusValue { get; set; }
        [DisplayName("Total Value")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal TotalValue_WithoutSkin { get; set; }
        [DisplayName("Average $")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal AveragePrice { get; set; }
        [DisplayName("Average $/Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal AveragePriceXKilo { get; set; }
        [DisplayName("Total Weight")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal TotalWeight { get; set; }
        [DisplayName("Average Weight")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal AverageWeight { get; set; }
        [DisplayName("Average Fat")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal AverageFat { get; set; } = 1m;
        [DisplayName("Skin Value")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal SkinValue { get; set; }

        public bool ShowCreateButton { get; set; }
    }

    public class WeightRange
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public int Count { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Weight { get; set; }

        [DisplayName("%")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal WeightPercentageOfTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Value { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [DisplayName("Class")]
        public int WeightClass { get; set; }

        [DisplayName("Range (Kgs)")]
        public string Range
        {
            get
            {
                return Min + " - " + Max;
            }
        }

    }
}
