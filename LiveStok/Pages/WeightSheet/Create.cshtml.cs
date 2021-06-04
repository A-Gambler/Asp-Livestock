using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using livestock.Data;
using livestock.Models;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using LiveStok.Models.NonDBModels;

namespace LiveStok.Pages.WeightSheetPage
{
    [BindProperties]
    public class CreateModel : PageModel
    {

        

        private readonly livestock.Data.ApplicationDbContext _context;

        public WeightSheet WeightSheet { get; set; }
        public StockPurchase StockPurchase { get; set; }
        
        public WeightSheetCalculation WeightSheetCalculation { get; set; }


        public string WeightTextList { get; set; }
        

        public CreateModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(Guid StockPurchaseID)
        {
                        
                this.WeightSheet = new WeightSheet()
                {
                    ID = Guid.NewGuid(),
                    Date = DateTime.Now,
                    StockPurchaseID = StockPurchaseID
                };
            

            ViewData["StockPurchaseID"] = new SelectList(_context.StockPurchase, "ID", "ID");
            //ViewData["TypesOfAnimals"] = new SelectList(new string[] { "LAMBS", "MUTTON", "RAMS" });
            ViewData["TypesOfAnimals"] = new SelectList(_context.TypeOfAnimals, "Id", "Name");

            StockPurchase = _context.StockPurchase.Include(m=> m.TypeOfAnimal).Where(x => x.ID == StockPurchaseID).FirstOrDefault();


            WeightSheetCalculation = new WeightSheetCalculation();
            WeightSheetCalculation.weightRanges = new List<WeightRange>();  //To avoid null exception

            return Page();
        }

     

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WeightSheet.Add(WeightSheet);

            List<Weight> newWeights = new List<Weight>();

            var Weights = CalculateWeightSheet.ListOfWeightsFromText(this.WeightTextList, this.WeightSheet.WeightOff);

            foreach (decimal weight in Weights)
            {
                var newRow = new Weight()
                {
                    ID = Guid.NewGuid(),
                    WeightSheetID = WeightSheet.ID,
                    weight = weight
                };

                newWeights.Add(newRow);
            }

            _context.Weights.AddRange(newWeights);

            await _context.SaveChangesAsync();

            return RedirectToPage("../StockPurchases/Details", new { id = WeightSheet.StockPurchaseID });
        }

      

        public async Task<IActionResult> OnPostCalculateAsync()
        {

            this.WeightSheetCalculation = CalculateWeightSheet.Calculate(_context,this.WeightSheet.StockPurchaseID, this.WeightTextList, this.WeightSheet.WeightOff, this.WeightSheet.Skin);
            

            return Page();
        }
        }
}

