using KFC_Clone.Models;
using SaltingAndHashing.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KFC_Clone.Lib;
using System.Reflection;
using System.Web.Security;
using System.Runtime;
using System.Web.UI.WebControls;
using KFC_Clone.Models.DBModels;

namespace KFC_Clone.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService service)
        {
            _accountService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
          
            return View(new UserContextModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserContextModel user)
        {
            string _message = "";
            string _username = "";

            try
            {
                if (!ModelState.IsValid)
                {
                    _message = "Invalid Username/Password";
                }
                else
                {
                    var loginUser = _accountService.LogIn(user.Email, user.Password);

                    if (loginUser != null)
                    {
                        _message = "Login Success";
                        _username = loginUser.Name;

                        var firstName = _username.Contains(" ") 
                        ? _username.Substring(0, _username.IndexOf(" "))
                        : _username;

                        var ticket = new FormsAuthenticationTicket(1, _username, DateTime.Now, DateTime.Now.AddDays(7), user.RememberMe, $"userId:{user.Id}");
                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                        var authCookie = new HttpCookie(_username, encryptedTicket);

                        var identityCookie = new HttpCookie("FirstName", firstName);
                        
                        HttpContext.Response.Cookies.Add(authCookie);
                        HttpContext.Response.Cookies.Add(identityCookie);

                        FormsAuthentication.RedirectFromLoginPage(_username, user.RememberMe);

                        return RedirectToAction("UserProfile");
                    }
                    else
                    {
                        _message = "User not Found";
                      
                    }
                }
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }

            ViewBag.Message = _message;
            

            return RedirectToAction("UserForShareView", "Home");
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UserForShareView", "Home");
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            User user = new User();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult CreateAccount(User user)
        {
            string message = "";

            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.PhoneNumber) || 
                String.IsNullOrEmpty(user.Name)  || String.IsNullOrEmpty(user.Password))
            {
                message = "Please enter all fields.";
            }
            else
            {
                user = _accountService.CreateAccount(user.Email, user.Name, user.PhoneNumber, user.Password);

                message = "Account created for " + user.Name;

                user = new User();
            }

            ViewBag.Message = message;

            return View(user);
        }

        [HttpGet]
        [Authorize]
        public ActionResult UserProfile()
        {
            return View();
        }
    }
}