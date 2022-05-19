using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleManagementSystem.Classes.proxy;
using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Controllers
{
    public class StartUpController : Controller
    {


        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: StartUp
        [HttpPost]
        public ActionResult Login(account model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           AccountsEntities db=new AccountsEntities();
   
            var accounts=(from a in db.accounts
                          where a.email==model.email
            
                         select a).First();

            if(accounts.password==model.password)
            {
                Session["account"]=accounts;
                Session["email"] = accounts.email;

                proxy_vms newz = new proxy_vms();
                bool isValid= newz.openLink(accounts, "Index", "Home");
                if(isValid)
                {
                    return RedirectToLocal(returnUrl);
                }
             
                

            }






            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Remove("account");
            Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }
    }
}