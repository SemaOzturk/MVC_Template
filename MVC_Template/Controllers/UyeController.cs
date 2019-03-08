using MVC_Template.Add_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_Template.Controllers
{
    [Authorize]
    public class UyeController : Controller
    {
        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Kullanici k, string Hatirla)
        {
            bool sonuc = Membership.ValidateUser(k.UserName, k.Password);
            if (sonuc)
            {
                // Bunu çalışması için web.config de bazı ayarların düzenlenmesi gerekir. Yani bu web sitesine üye giriş yapılabileceğine dair ayar yapmamız lazım
                if (Hatirla == "on")
                    FormsAuthentication.RedirectFromLoginPage(k.UserName, true);
                else
                    FormsAuthentication.RedirectFromLoginPage(k.UserName, false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı adı veya parola hatalı!";
                return View();
            }
        }

        public ActionResult CikisYap()
        {
            //Bır oncekı sayfa chrome tarafından cache'lendıgı ıcın normalde erısımımız olmayan bır sayfa cache'ten cagırılıyor. Bunun onune gecmek ıcın sayfayı Home'un Index'ıne yonlendırıyoruz
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        public ActionResult ParolamiUnuttum()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ParolamiUnuttum(Kullanici k)
        {
            MembershipUser mu = Membership.GetUser(k.UserName);
            if (mu.PasswordQuestion == k.PasswordQuestion)
            {
                string pwd = mu.ResetPassword(k.PasswordAnswer);
                mu.ChangePassword(pwd, k.Password);
                return RedirectToAction("GirisYap");
            }
            else
            {
                ViewBag.Mesaj = "Girilen bilgiler yanlistir!";
                return View();

            }
        }
    }
}