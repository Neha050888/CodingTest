using CodingTest.Entities.Interface;
using CodingTest.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace CodingTest.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _customer;
        private readonly IWebHostEnvironment _environment;

        public CustomerController(IRepository<Customer> customer,IWebHostEnvironment environment)
        {
            _customer = customer;
            _environment = environment;

        }
        public IActionResult Index()
        {
            if (TempData["FileName"]!=null)
                ViewBag.FileName = TempData["FileName"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCustomer(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index");
                }

                var filePath = Path.Combine(_environment.WebRootPath, "Files");
                var savedCust = await _customer.SaveToFile(customer, filePath);
                if (!string.IsNullOrEmpty(savedCust.FileName)){
                    TempData["FileName"] = customer.FileName;
                }
            }
           
            catch (Exception)
            {
               
                throw;
                
            }
          
            return RedirectToAction("Index","Customer");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int code)
        {
            string errorMessage = "";
            if (code == 404)
            {
                errorMessage = "The requested page not found.";
            }
            else if (code == 500)
            {
                errorMessage = "500 internal server error.";
            }
            else
            {
                errorMessage = "An error occurred while processing your request.";
            }
            ViewBag.ErrorStatusCode = code;
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }


        //[HttpPost]  This also saves customer to file
        //public FileContentResult SaveCustomer(Customer customer)
        //{

        //    string jsonString = JsonSerializer.Serialize(customer);
        //    var fileName = "test.txt";
        //    var mimeType = "text/plain";
        //    var fileBytes = Encoding.ASCII.GetBytes(jsonString);
        //    return new FileContentResult(fileBytes, mimeType)
        //    {
        //        FileDownloadName = fileName
        //    };
        //}
    }
}
