using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            MyModel new_model_item = new MyModel();

            new_model_item.Id = 1;
            new_model_item.Data = "Example data.";

            dbContext.Add(new_model_item);
            dbContext.SaveChanges();

            MyModel saved_item = dbContext.MyModels.FirstOrDefault();

            Console.WriteLine("////////////////////////////////");
            Console.WriteLine($"New item Id: {saved_item.Id}");
            Console.WriteLine($"New item Data: {saved_item.Data}");
            Console.WriteLine($"New item CreatedAt: {saved_item.CreatedAt}");

            return View();
        }

    }
}
