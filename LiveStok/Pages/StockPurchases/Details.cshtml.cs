using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace livestock.Pages.StockPurchasesPage
{
    public class DetailsModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;


        public List<FileAttached> AttachedFiles { get; set; }

        public LiveStok.Models.NonDBModels.PartialStockPurchaseBuyModel PartialStockPurchaseBuyModel { get; set; }

        [BindProperty]
        public List<IFormFile> files { get; set; }

        public DetailsModel(livestock.Data.ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public StockPurchase StockPurchase { get; set; }

        public string lblStatusUploadImagePreview { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StockPurchase = await _context.StockPurchase
                .Include(s => s.Agent)
                .Include(s => s.Buyer)
                .Include(s => s.Location)
                .Include(s => s.Transport)
                .Include(s => s.TypeOfAnimal)
                .Include(s => s.WeightSheet)
                //.Include(s => s.Vendor)
                .Include(s => s.MarketBuySummaries)
                .Include(s=> s.BuyType).FirstOrDefaultAsync(m => m.ID == id);

            if (StockPurchase == null)
            {
                return NotFound();
            }

            PartialStockPurchaseBuyModel = new LiveStok.Models.NonDBModels.PartialStockPurchaseBuyModel()
            {
                StockPurchase = StockPurchase,
                PricePerHeadBuys = await _context.PricePerHeadBuy.Where(x=> x.StockPurchaseID == StockPurchase.ID).Include(m=> m.TypeOfAnimal).ToListAsync(),
                TypeOfAnimals_SelectList = new SelectList(_context.TypeOfAnimals, "ID", "Name"),
                Agents_SelectList = new SelectList(_context.Agent, "ID", "Name"),
                MarketBuys = await _context.MarketBuys.Where(x => x.StockPurchaseID == StockPurchase.ID).Include(m => m.TypeOfAnimal).ToListAsync()
            };

            /////Load Attached Files///
            AttachedFiles = new List<FileAttached>();
            DirectoryInfo dir = new DirectoryInfo(_environment.WebRootPath + @"\Attached\");
            if(dir.Exists == false)
            {
                dir.Create();
            }
            var files = dir.GetFiles(id.ToString() + "_*.*");

            foreach(var file in files)
            {
                var newFileModel = new FileAttached()
                {
                    NameCompleteWithGuid = file.Name,
                    Name = file.Name.Substring(37),
                    FileRelativePath = @"../Attached/" + file.Name,
                    FilePhysicalPath = file.FullName

                };

                switch(file.Extension.ToUpper())
                {
                    case ".PDF":
                        newFileModel.ImageThumbnailRelativePath = @"../images/adobe-pdf-icon.png";
                        break;
                    case ".XLS":
                        newFileModel.ImageThumbnailRelativePath = @"../images/ExcelIcon.png";
                        break;
                    case ".XLSZ":
                        newFileModel.ImageThumbnailRelativePath = @"../images/ExcelIcon.png";
                        break;
                    default:
                        newFileModel.ImageThumbnailRelativePath = @"../Attached/" + file.Name;
                        break;
                }

                AttachedFiles.Add(newFileModel);
                }

            return Page();
        }

            

        //[HttpPost("UploadFiles")]
        public async Task<IActionResult> OnPostAsync(Guid StockPurchaseID)
        {
            long size = files.Sum(f => f.Length);

           
            foreach (var formFile in files)
            {
         
            try
            {

                if (size < 10000000) //10MB 
                {


                    //string filename = System.IO.Path.GetFileName(FileUploadImagePreview.FileName);
                    //string extensionWDot = ".png"; //System.IO.Path.GetExtension(FileUploadImagePreview.FileName);
                    string filePath = "";

                        //filePath = Path.Combine(_environment.WebRootPath, @"\Images\Attached\" + StockPurchaseID.ToString() + "_" + files[0].Name + extensionWDot);
                        filePath = _environment.WebRootPath + @"\Attached\" + StockPurchaseID.ToString() + "_" + formFile.FileName;


                        if (formFile.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }
                        }

                        
                    lblStatusUploadImagePreview = "Upload status: File uploaded!";
                }
                else
                    lblStatusUploadImagePreview = "Upload status: The file has to be less than 10 MB!";

                    //Else
                    //    this.lblStatusUploadImagePreview.Text = "Upload status: Only JPEG files are accepted!"
                    //End If
                                      
            }

            catch (Exception ex)
            {
                lblStatusUploadImagePreview = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }

            }


       
            return await OnGetAsync(StockPurchaseID); //Para que recarge las variables

            //return Ok(new { count = files.Count, size, filePath });
            
        }

        public async Task<IActionResult> OnPostDeleteAttachedFileAsync(Guid StockPurchaseID, string FilePhysicalPath)
        {
            FileInfo FileToDelete = new FileInfo(FilePhysicalPath);
            if(FileToDelete.Exists == true)
            {
                FileToDelete.Delete();
            }


            return await OnGetAsync(StockPurchaseID);
        }

    }
}
