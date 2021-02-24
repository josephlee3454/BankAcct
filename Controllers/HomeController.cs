
using System.Transactions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using bankAccounts.Models;

namespace bankAccounts.Controllers
{
    public class HomeController : Controller
    {
          private MyContext _Context;
    // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _Context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("RegisterView");
        }


        [HttpPost("Register")]
        public IActionResult Register(UserRegistration creUser)
        {
            if(ModelState.IsValid)
            {
                if(_Context.Users.Any(us=>us.Email==creUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already use!");
                    return View("RegisterView");
                }
                else
                {
                    PasswordHasher<UserRegistration> Hasher = new PasswordHasher<UserRegistration>();
                    creUser.Password = Hasher.HashPassword(creUser, creUser.Password);
                    UserModel CreUser = new UserModel
                    {
                        first_name = creUser.first_name,
                        last_name = creUser.last_name,
                        Email = creUser.Email,
                        Pass = creUser.Password,
                    };
                    _Context.Users.Add(CreUser);
                    _Context.SaveChanges();


                    int usId = CreUser.UserId;
                    HttpContext.Session.SetInt32("usId",usId);

                    return RedirectToAction("account");
                }
            }
            else {
                return View("RegisterView");
            }


                }

            [HttpGet("login")]
            public IActionResult LoginPage()
            {
                return View("Login");
            }
            [HttpPost("loguser")]
            public IActionResult Login(UserLog curUser)
            {
                if (ModelState.IsValid)
                {
                    UserModel usDb = _Context.Users.FirstOrDefault(us => us.Email == curUser.LogEm);
                    if (usDb == null)
                    {
                       ModelState.AddModelError("LogEm", "Invalid Email/Password");
                        return View("Login");
                    }
                    var hasher = new PasswordHasher<UserLog>();//make hasher 

                    var result = hasher.VerifyHashedPassword(curUser, usDb.Pass,curUser.LogPass);


                    if (result == 0)
                    {
                        ModelState.AddModelError("LoginPass", "Invalid Email/Password");
                        return View("Login");
                    }

                    int uid = usDb.UserId;
                    HttpContext.Session.SetInt32("uid",uid);
                    return RedirectToAction("Account");

                    }
                    else
                    {
                        return View("Login");
                    }
                }

            [HttpGet("account")]
            public IActionResult Account()
              {
                int? uid = HttpContext.Session.GetInt32("uid");
                if (uid == null)
                {
                    return RedirectToAction("RegisterView");
                }
                else
                {
                    UserModel theUser = _Context.Users.FirstOrDefault(us => us.UserId == uid);
                    ViewBag.theUser = theUser;
                    List<TransacM> transWithUser = _Context.Transactions
                    .Where(transaction => transaction.UserId == uid)
                    .OrderByDescending(item => item.CreatedAt)
                    .ToList(); 


                    decimal sum = _Context.Transactions
                    .Where(transaction => transaction.UserId == uid)
                    .Sum(item => item.TotalAmt);
                    ViewBag.Total = sum;
                    return View(new Account {allTransaction = transWithUser});
                }
              }

              [HttpPost("account/new")]
              public IActionResult NewTransaction(Account NewTransaction)
              {
                  if(ModelState.IsValid)
                  {
                    int? uid= HttpContext.Session.GetInt32("uid");
                    UserModel theUser= _Context.Users.FirstOrDefault(us=>us.UserId==uid);

                    NewTransaction.userTransaction.UserId = (int)uid;
                    NewTransaction.userTransaction.AcctOwner = theUser;
                    
                    _Context.Transactions.Add(NewTransaction.userTransaction);
                    _Context.SaveChanges();
                    return RedirectToAction("Account");
                  }
                  else{
                      return View("Account");
                  }
              }

              [HttpGet("logiut")]
              public IActionResult Logout()
              {
                  HttpContext.Session.Clear();
                  return RedirectToAction("RegisterView");
              }


            }
          

        }





















        // public IActionResult Privacy()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }

