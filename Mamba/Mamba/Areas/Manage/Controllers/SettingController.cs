using Mamba.DAL;
using Mamba.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mamba.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Settings.ToList();
            return View(data);
        }
        public IActionResult Edit(int Id)
        {
            var isExists = _context.Settings.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(isExists);
        }
        [HttpPost]
        public IActionResult Edit(Setting setting)
        {
            var isExists = _context.Settings.FirstOrDefault(x => x.Id == setting.Id);
            if (isExists == null)
            {
                return RedirectToAction("index", "error");
            }
            isExists.Value = setting.Value;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
