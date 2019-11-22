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

            return View(GetFactureList());
        }
        private  List<Faktura> GetFactureList()
        {
            var result = new List<Faktura>();

            using (var db = new ApplicationDbContext())
            {
                if (db.Fakture.Count() > 0)
                    result = db.Fakture.Where(f => f.Stvaratelj == User.Identity.Name).OrderByDescending(f => f.DatumStvaranja).ToList();
            }

            return result;
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
                Stavke = new List<Stavka>()
            };

            return View(faktura);
        }

        [HttpPost]
        public ActionResult Create(Faktura faktura)
        {
            faktura.Stvaratelj = User.Identity.Name;
            faktura.DatumStvaranja = DateTime.Now;

            faktura.Cijena = 0d;
            for (int i = 0; i < faktura.Stavke.Count; i++)
            {
                faktura.Stavke[i].UkupnaCijena = faktura.Stavke[i].JedinicnaCijena * faktura.Stavke[i].Kolicina;
                faktura.Cijena += faktura.Stavke[i].UkupnaCijena;
            }

            var helper = faktura.PDV.Split(':'); // helper[0] je naziv, a helper[1] je postotak
            faktura.CijenaSaPDV = TaxCalculator.Instance.Calculate(faktura.Cijena, helper[0]);

            using (var db = new ApplicationDbContext())
            {
                db.Fakture.Add(faktura);
                db.SaveChanges();
            }

            return View("Bills", GetFactureList());
        }

        public ActionResult Details(int id)
        {
            Faktura faktura = new Faktura();

            using (var db = new ApplicationDbContext())
            {
                faktura = db.Fakture.First(f => f.Id == id);
            }

            return View(faktura);
        }
    }
}