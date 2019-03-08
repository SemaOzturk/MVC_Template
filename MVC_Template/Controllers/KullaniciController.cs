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
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }

       [AllowAnonymous]
        public ActionResult KullaniciEkle()
        {
            return View();
        }

       [AllowAnonymous]
        [HttpPost]
        public ActionResult KullaniciEkle(Kullanici k)
        {
            Membership.CreateUser(k.UserName, k.Password, k.Email, k.PasswordQuestion, k.PasswordAnswer, true, out MembershipCreateStatus durum);

            string mesaj = "";
            switch (durum)
            {
                case MembershipCreateStatus.InvalidUserName:
                    mesaj = "Geçersiz kullanıcı adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj = "Geçersiz parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj = "Geçersiz gizli soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj = "Geçersiz gizli cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj = "Geçersiz email adresi";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj = "Kullanılmış kullanıcı adı";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj = "Kullanılmış email adresi girildi";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj = "Kullanıcı engel hatası";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj = "Geçersiz kullanıcı key hatası";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj = " Kullanılmış kullanıcı key hatası";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj = "Üye yönetimi sağlayıcı hatası";
                    break;
                case MembershipCreateStatus.Success:
                    break;
                default:
                    break;
            }
            ViewBag.Mesaj = mesaj;

            if (durum == MembershipCreateStatus.Success)
                return RedirectToAction("Index");
            else
                return View();
        }

        [Authorize(Roles="Admin")]
        public ActionResult RolAta(string Id)
        {
            ViewBag.UserName = Id;
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            return View();
            // return View(id);
        }

       [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RolAta(string UserName, string RoleName)
        {
            // Seçilen kullanıcıya seçilen rolü atayacağız
            Roles.AddUserToRole(UserName, RoleName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string UyeRolleri(string UserName)
        {
            List<string> roller = Roles.GetRolesForUser(UserName).ToList();
            string rol = "";
            foreach (string r in roller)
            {
                rol += r + "-";
            }
            if (rol.Length > 1)
                rol = rol.Remove(rol.Length - 1, 1);
            return rol;
        }

        [HttpPost]
        public void Sil(string id)
        {
            Membership.DeleteUser(id, deleteAllRelatedData: true);
        }
    }
}