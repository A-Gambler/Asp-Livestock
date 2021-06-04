using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using livestock.Models;
using LiveStok.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace livestock.Controllers
{
    public class StockPurchasesAPIController : Controller
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public StockPurchasesAPIController(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FindAll()
        {
            //title: 'Renovación Cliente 2',
            //start: '2018-09-04',
            //end: '2018-09-04'

            var x = this._context.StockPurchase.Include(y => y.Buyer).Include(m => m.TypeOfAnimal).Include(k => k.Agent).ToList();

            return Json(x.Select(e => new
            {
                id = e.ID.ToString(),
                title = e.TypeOfAnimal.Name + " Buyer: " + e.Buyer.Code + " No " + e.BuyerNo,
                description = "<br>&nbsp&nbsp&nbsp&nbspEst Weight: " + e.EstimatedWeight + "<br>&nbsp&nbsp&nbsp&nbspAgent: " + e.Agent.Name + "<br>&nbsp&nbsp&nbsp&nbspContact: " + e.ContactName + "<br>&nbsp&nbsp&nbsp&nbspPhone:" + e.ContactPhone,
                //start = e.IntendedDeliveryDate.ToString("yyyy-MM-dd HH:mm")
                start = e.IntendedDeliveryDate.ToString("yyyy-MM-dd")

            }).ToList());
        }

        //public ActionResult Search()
        //{
        //    MemoryStream stream = new MemoryStream();
        //    List<StockPurchase> ListStockPurchase = new List<StockPurchase>();


        //    Request.Body.CopyTo(stream);
        //    stream.Position = 0;

        //    try
        //    {
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            string requestBody = reader.ReadToEnd();
        //            if (requestBody.Length > 0)
        //            {
        //                var SchValues = requestBody;
        //                if (SchValues != null || SchValues != "")
        //                {

        //                    ListStockPurchase = (from SP in _context.StockPurchase.Include(a => a.Buyer).Include(c => c.Agent).Include(t => t.Transport).Include(l => l.Location).Include(v => v.Vendor).Include(g => g.TypeOfAnimal)
        //                                         join B in _context.Buyer on SP.BuyerID equals B.ID
        //                                         join A in _context.Agent on SP.AgentID equals A.ID
        //                                         where EF.Functions.Like(SP.Buyer.Code + SP.Agent.Name + SP.BuyerNo, "%" + requestBody + "%")
        //                                         select SP).ToList();
        //                }
        //            }
        //            else
        //            {
        //                ListStockPurchase = (from SP in _context.StockPurchase.Include(a => a.Buyer).Include(c => c.Agent).Include(t => t.Transport).Include(l => l.Location).Include(v => v.Vendor).Include(g => g.TypeOfAnimal)
        //                                     join B in _context.Buyer on SP.BuyerID equals B.ID
        //                                     join A in _context.Agent on SP.AgentID equals A.ID
        //                                     select SP).ToList();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return PartialView("~/Pages/StockPurchases/_ListStockPurchase.cshtml", ListStockPurchase);
        //}

        public async Task<PaginatedList<StockPurchase>> GetStockPurchaseGridRowsAsync(string searchString, int? pageNumber)
        {

            int pageSize = 11;

            PaginatedList<StockPurchase> ListStockPurchase = new PaginatedList<StockPurchase>(new List<StockPurchase>(), 0, 0, pageSize);



            try
            {
                if (string.IsNullOrWhiteSpace(searchString) == false)
                {

                    ListStockPurchase = await PaginatedList<StockPurchase>.CreateAsync((from SP in _context.StockPurchase.Include(a => a.Buyer).Include(c => c.Agent).Include(t => t.Transport).Include(l => l.Location).Include(g => g.TypeOfAnimal)
                                                                                        join B in _context.Buyer on SP.BuyerID equals B.ID
                                                                                        join A in _context.Agent on SP.AgentID equals A.ID
                                                                                        where EF.Functions.Like(SP.Buyer.Code + SP.Agent.Name + SP.BuyerNo, "%" + searchString + "%")
                                                                                        orderby SP.Date, SP.Buyer
                                                                                        select SP).AsNoTracking(), pageNumber ?? 1, pageSize); ;
                }

                else
                {
                    ListStockPurchase = await PaginatedList<StockPurchase>.CreateAsync((from SP in _context.StockPurchase.Include(a => a.Buyer).Include(c => c.Agent).Include(t => t.Transport).Include(l => l.Location).Include(g => g.TypeOfAnimal)
                                                                                        join B in _context.Buyer on SP.BuyerID equals B.ID
                                                                                        join A in _context.Agent on SP.AgentID equals A.ID
                                                                                        orderby SP.Date, SP.Buyer
                                                                                        select SP).AsNoTracking(), pageNumber ?? 1, pageSize); ;

                }
            }
            catch (Exception ex)
            {

            }

            return ListStockPurchase;
        }

        public async Task<ActionResult> RefreshStockPurchaseGrid(string searchString, int? pageNumber)
        {
                     
            return PartialView("~/Pages/StockPurchases/_ListStockPurchase.cshtml", await GetStockPurchaseGridRowsAsync(searchString,pageNumber));
        }

        public ActionResult SummaryTotals([FromBody] List<PricePerHeadBuy> ListPricePerHeadBuy)
        {
            //List<PricePerHeadBuy> ListPricePerHeadBuy = new List<PricePerHeadBuy>();

            try
            {
                foreach (var item in ListPricePerHeadBuy)
                {


                    item.TypeOfAnimal = _context.TypeOfAnimals.Where(x => x.ID == item.TypeOfAnimalID).FirstOrDefault();
                    //PricePerHeadBuy pricePerHeadBuy = new PricePerHeadBuy();
                    //pricePerHeadBuy.TypeOfAnimal = _context.TypeOfAnimals.Where(x => x.ID == item.TypeOfAnimalID).FirstOrDefault();
                    //pricePerHeadBuy.TypeOfAnimalID = item.TypeOfAnimalID;
                    //pricePerHeadBuy.HeadsBought = item.HeadsBought;
                    //pricePerHeadBuy.Price = item.Price;
                    //pricePerHeadBuy.Skin = item.Skin;
                    //pricePerHeadBuy.Weight = item.Weight;
                    //pricePerHeadBuy.Freight = item.Freight;

                    //ListPricePerHeadBuy.Add(pricePerHeadBuy);
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Pages/StockPurchases/_SummaryTotalsPrivateBuy.cshtml", ListPricePerHeadBuy);
        }
    }
}