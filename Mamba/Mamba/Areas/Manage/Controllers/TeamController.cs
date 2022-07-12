using Mamba.DAL;
using Mamba.Helper;
using Mamba.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mamba.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context,IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            var team = _context.Teams.ToList();
            return View(team);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team member)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (member.ImageFile != null)
            {
                if (member.ImageFile.ContentType != "image/jpeg" && member.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Image content must be jpeg/png");
                    return View();
                }
                if (member.ImageFile.Length>2500000)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less 2.5mb");
                    return View();
                }
                member.Image = FileManager.Save(_env.WebRootPath, "upload/team",member.ImageFile);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image is required");
                return View();
            }
            _context.Teams.Add(member);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int Id)
        {
            var isExists = _context.Teams.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(isExists);
        }
        [HttpPost]
        public IActionResult Edit(Team member)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExists = _context.Teams.FirstOrDefault(x => x.Id == member.Id);
            if (isExists == null)
            {
                return RedirectToAction("index", "error");
            }

            if (member.ImageFile != null)
            {
                if (member.ImageFile.ContentType != "image/jpeg" && member.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Image content must be jpeg/png");
                    return View();
                }
                if (member.ImageFile.Length > 2500000)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less 2.5mb");
                    return View();
                }
                var newImageFile = FileManager.Save(_env.WebRootPath, "upload/team", member.ImageFile);
                FileManager.Delete(_env.WebRootPath, "upload/team", isExists.Image);
                isExists.Image = newImageFile;
            }
            isExists.Fullname = member.Fullname;
            isExists.Position = member.Position;
            isExists.FaceUrl = member.FaceUrl;
            isExists.TwitUrl = member.TwitUrl;
            isExists.LikdnUrl = member.LikdnUrl;
            isExists.InstaUrl = member.InstaUrl;
            _context.SaveChanges();
            return RedirectToAction("index");
            
        }
        
        public IActionResult Delete(int Id)
        {
            var isExists = _context.Teams.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(isExists);
        }
        [HttpPost]
        public IActionResult Delete(Team member)
        {
            var isExists = _context.Teams.FirstOrDefault(x => x.Id == member.Id);
            if (isExists == null)
            {
                return RedirectToAction("index", "error");
            }

            FileManager.Delete(_env.WebRootPath, "upload/team", isExists.Image);
            _context.Teams.Remove(isExists);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
