using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Template.Controllers
{
    using Models;
    using Add_Classes;

    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.AktifKullanici = HttpContext.Application["AktifKullanici"];
            ViewBag.ToplamZiyaretci = HttpContext.Application["ToplamZiyaretci"];
            return View();
        }
        public ActionResult CookieAta()
        {
            //Cookie'ye deger atamayı saglayacagız
            return View();
        }

        [HttpPost]
        public ActionResult CookieAta(string CookieAdi, string CookieDegeri)
        {
            HttpCookie ck = new HttpCookie(CookieAdi);
            ck.Value = CookieDegeri;
            ck.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(ck);

            return View();
        }

        public ActionResult CookieOku()
        {
            string deger = Request.Cookies["SC201"].Value;
            ViewBag.Cerez = deger;
            return View();
        }

        public ActionResult Sepetim()
        {
            List<Product> urunler = new List<Product>();
            if (Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)Session["AktifSepet"];
                urunler = s.Urunler;
            }
            return View(urunler);
        }

        [HttpPost]
        public void SepettenCikart(int id)
        {
            //List<Product> urunler = new List<Product>();
            if (Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)Session["AktifSepet"];

                Product product = s.Urunler.Find(x => x.ProductID == id);
                if (product == null)
                    return;

                s.Urunler.Remove(product);

                //urunler = s.Urunler;                
            }
        }
    }
}