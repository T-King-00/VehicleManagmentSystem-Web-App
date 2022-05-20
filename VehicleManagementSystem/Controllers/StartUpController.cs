using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleManagementSystem.Classes;
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

           AccountsEntities db=Classes.SingleDbObject.getInstanceAccounts();

            bool alreadyInDb = (from a in db.accounts
                                where a.email == model.email

                                select a).Count() > 0;
            if(alreadyInDb)
            { 
                 var accounts=(from a in db.accounts
                          where a.email==model.email
            
                         select a).First();

                if(accounts.password==model.password)
                {
                    Session["account"]=accounts;
                    Session["email"] = accounts.email;
                    Session["role"] = accounts.role;

                   
                    return RedirectToLocal(returnUrl);
                    
             
                }
            }

            return View();
        }


        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: StartUp
        [HttpPost]
        public ActionResult Register(account model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AccountsEntities db = Classes.SingleDbObject.getInstanceAccounts();
            

            bool alreadyInDb = (from a in db.accounts
                            where a.email == model.email

                             select a).Count() > 0;
        

            if (alreadyInDb==true)
            {
                ViewBag.msg = "Account is already registered";
                return View();
            }
            else if(alreadyInDb != true)
            {
                Random random = new Random();

                model.id=random.Next();
                model.role ="client";
                Session["role"] = "client";
                Session["account"] = model;
                Session["email"] = model.email;
                db.accounts.Add(model);
                static_functions.saveChangesInAccountDb(db);
                proxy_vms newz = new proxy_vms();
                bool isValid = newz.openLink(model, "Index", "Home");
                if (isValid!=true)
                {
                    return RedirectToAction("AccessDenied","vehicle");
                }
                else
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