using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EnterwellBills.Models;
using EnterwellBills.Extensions;

namespace EnterwellBills.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Bills()
        {
            ViewBag.Message = "Your application description page.";
            var fakture = new List<Faktura>();

            using (var db = new ApplicationDbContext())
            {
                if (db.Fakture.Count() > 0)
                    fakture = db.Fakture.OrderByDescending(f => f.DatumStvaranja).ToList();
            }

            return View(fakture);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Message = "Your contact page.";

            ViewBag.Taxes = TaxCalculator.Instance.GetAllTaxes();
                       
            var faktura = new Faktura()
            {
                Stvaratelj = User.Identity.Name,
                DatumStvaranja = DateTime.Now,
            };

            return View(faktura);
        }

        [HttpPost]
        public ActionResult Create(Faktura faktura)
        {
            return View();
        }
    }
}