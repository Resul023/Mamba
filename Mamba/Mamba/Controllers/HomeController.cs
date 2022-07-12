using Mamba.DAL;
using Mamba.Models;
using Mamba.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mamba.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVW = new HomeViewModel {
                Teams = _context.Teams.ToList(),
            };
            return View(homeVW);
        }

       
    }
}
