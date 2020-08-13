using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OxuAz.DAL;
using OxuAz.Extension;
using OxuAz.Models;
using static OxuAz.Utilities.Utilities;

namespace OxuAz.Areas.NewsAdmin.Controllers
{
    [Area("NewsAdmin")]
    public class NewsController : Controller
    {

        private readonly AppDbContext _data;
        private readonly IHostingEnvironment _hosting;


        public NewsController(AppDbContext data,IHostingEnvironment hosting)
        {
            _data = data;
            _hosting = hosting;
        }
        public IActionResult Index()
        {
            var data = _data.News.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(News news)
        {
            if (!ModelState.IsValid)
            {
                return View(news);
            }
            if (news.Photo == null)
            {
                ModelState.AddModelError("Photo", "Sekil Yukleyin!");
                return View(news);
            }

            if (!news.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Sekil YUkleyin(jpg,gif)");
                return View(news);
            }

            news.Image = news.Photo.Save(_hosting.WebRootPath, "newsphoto");
            _data.News.Add(news);
            _data.SaveChanges();
            return RedirectToAction("Index");


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
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var des = _data.News.Find(Id);
            if (des == null)
            {
                return NotFound();
            }

            return View(des);
        }

        [HttpPost]
        public IActionResult Edit(int? Id, News news)
        {

            if (!ModelState.IsValid)
            {
                return View(news);
            }

            var newsfromdb = _data.News.Find(news.Id);
            if (news.Photo != null)
            {
                if (!news.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Duzgun fayl secin");
                    return View(news);
                }
          
                RemoveImage(_hosting.WebRootPath, newsfromdb.Image);
                newsfromdb.Image = news.Photo.Save(_hosting.WebRootPath, "newsphoto");
            }
            newsfromdb.Title = news.Title;
            newsfromdb.Content = news.Content;
            newsfromdb.CreateAt = news.CreateAt;
            _data.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
