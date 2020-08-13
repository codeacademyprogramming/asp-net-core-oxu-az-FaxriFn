using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OxuAz.DAL;
using OxuAz.Models;

namespace OxuAz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _data;
        public HomeController(ILogger<HomeController> logger,AppDbContext data)
        {
            _logger = logger;
            _data = data;
        }

        public IActionResult Index()
        {

            var data = _data.News.ToList();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var data = _data.News.Find(Id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        public IActionResult Like(int? Id)
        {
            News like = _data.News.ToList().Find(u=>u.Id==Id);

           like.Like+=1;
            _data.SaveChanges();
            

            return RedirectToAction(nameof(Index));
        }
        public IActionResult DisLike(int? Id)
        {
            News like = _data.News.ToList().Find(u => u.Id == Id);

            like.DisLike += 1;
            _data.SaveChanges();


            return RedirectToAction(nameof(Index));

        }
    }
}
