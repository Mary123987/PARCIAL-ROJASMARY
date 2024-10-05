using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PARCIAL_ROJASMARY.Models;
using PARCIAL_ROJASMARY.Data;

namespace PARCIAL_ROJASMARY.Controllers
{
    [Route("[controller]")]
    public class RemesaController : Controller
    {
        private readonly ILogger<RemesaController> _logger;
        private readonly ApplicationDbContext _context;

        public RemesaController(ILogger<RemesaController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

         [HttpPost]
         public IActionResult Registrar(Remesa objremesa)
        {
            if (ModelState.IsValid)
            {
                _context.DataRemesa.Add(objremesa);
                _context.SaveChanges();
                return RedirectToAction("Listado");
            }
            return View("Registrar");
        }


        [HttpGet]
        public IActionResult Listado()
        {
            var remesas = _context.DataRemesa.ToList(); // Fetch the list of Remesa
            return View(remesas); // Pass the list to the view
}










        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}