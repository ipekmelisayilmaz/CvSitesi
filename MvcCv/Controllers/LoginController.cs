using MvcCv.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcCv.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {


            return View();
        }
        //    [HttpPost]  
        //    public ActionResult Index(TblAdmin p)
        //    {

        //        DbCvEntities db = new DbCvEntities();   
        //        var bilgi = db.TblAdmin.FirstOrDefault(x => 
        //        x.KullaniciAdi == p.KullaniciAdi && x.Sifre 
        //        == p.Sifre);
        //        if (bilgi != null)
        //        {
        //            FormsAuthentication.SetAuthCookie(bilgi.KullaniciAdi,false);
        //            Session["KullaniciAdi"] = bilgi.KullaniciAdi.ToString();
        //            return RedirectToAction("Index","Deneyim");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index","Login");
        //        }

        //    }
        //}

        [HttpPost]
        public ActionResult Index(TblAdmin p)
        {
            DbCvEntities db = new DbCvEntities();
            var bilgi = db.TblAdmin.FirstOrDefault(x =>
                x.KullaniciAdi == p.KullaniciAdi && x.Sifre == p.Sifre);

            if (bilgi != null)
            {
                // FormsAuthenticationTicket oluştur
                var authTicket = new FormsAuthenticationTicket(
                    1, // Versiyon
                    bilgi.KullaniciAdi, // Kullanıcı Adı
                    DateTime.Now, // Başlangıç Zamanı
                    DateTime.Now.AddMinutes(30), // Geçerlilik Süresi
                    false, // Kalıcı Çerez?
                    "RolBilgisi" // Kullanıcı rolleri gibi ek bilgiler
                );

                // Çerezi şifreli bir şekilde oluştur
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    HttpOnly = true, // Çerezi JavaScript erişimine kapatır
                    Secure = FormsAuthentication.RequireSSL // SSL kullanıyorsanız true olmalı
                };

                Response.Cookies.Add(authCookie); // Çerezi istemciye gönder

                // Kullanıcı bilgilerini session'a kaydet
                Session["KullaniciAdi"] = bilgi.KullaniciAdi.ToString();

                // Yönlendirme
                return RedirectToAction("Index", "Deneyim");
            }
            else
            {
                // Kullanıcı adı veya şifre yanlış
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult LogOut()
        {
            // Kimlik doğrulama çerezini sil
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
                {
                    Expires = DateTime.Now.AddDays(-1), // Çerezin süresini geçmişe ayarla
                    Value = null
                };
                Response.Cookies.Add(cookie);
            }

            // Oturum bilgilerini temizle
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            // Kullanıcıdan kaynaklı olası güvenlik sorunlarını azaltmak için tarayıcı önbelleğini temizle
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            return RedirectToAction("Index", "Login");




            //FormsAuthentication.SignOut();
            //Session.Abandon();
            //return RedirectToAction("Index", "Login");

        }
          
    }
    }