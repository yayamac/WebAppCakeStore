using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCakeShopeBoutiqe.Data;
using WebAppCakeShopeBoutiqe.Models;

namespace WebAppCakeShopeBoutiqe.Controllers
{
    public class ClientsController : Controller
    {
        private readonly WebAppCakeShopeBoutiqeContext _context;

        public ClientsController(WebAppCakeShopeBoutiqeContext context)
        {
            _context = context;
        }

        /*********************************  LOGIN  ****************************************/


        // GET: Clients/Login
        public IActionResult Login()
        {
            return View();
        }


        // POST: Clients/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("ClientId,ClientName,Password,PasswordConfirm,EmailAddress")] Client client, string ReturnUrl)
        {
            if (ModelState.IsValid)
            { 
                var q = from c in _context.Client
                        where c.ClientName == client.ClientName && c.Password == client.Password
                        select c;

                if (q.Count() > 0 )
                {
                    //HttpContext.Session.SetString("clientname", q.First().ClientName);
                    Signin(q.First());
                    //return RedirectToAction(nameof(Index), "Home");
                }

                else
                {
                    ViewData["Error"] = "Client name and/or Password are incorrect. ";
                }

                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(Index), "Home");
                }


            }

            return View(client);
        }
        //Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index), "Home");
        }


        //Signin
        /**
         * Sign in function that starting 10 minutes session and saving name,id,cartId and mail as claims.
         */
        private async void Signin(Client account)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, account.EmailAddress),
            new Claim("FullName", account.ClientName),
            new Claim("UserId", account.ClientId.ToString()),
            //new Claim("cartId", account..ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }






        /*********************************  REGISTER  ****************************************/


        // GET: Clients/Register
        public IActionResult Register()
        {
            return View();
        }


        // POST: Clients/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("ClientId,ClientName,Password,PasswordConfirm,EmailAddress")] Client client)
        {
            if (ModelState.IsValid)
            {
                //check if name client already registered
                var q = _context.Client.FirstOrDefault(c => c.ClientName == client.ClientName);
                if (q==null)
                {
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), "Home");
                }

                else
                {
                    ViewData["Error"] = "Client name already exist., cannot register this client. ";
                }

               
            }
            return View(client);
        }








        //// GET: Clients
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Client.ToListAsync());
        //}

        //// GET: Clients/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Client
        //        .FirstOrDefaultAsync(m => m.ClientId == id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(client);
        //}

        //// GET: Clients/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Client.FindAsync(id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(client);
        //}

        //// POST: Clients/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientName,Password,PasswordConfirm,EmailAddress")] Client client)
        //{
        //    if (id != client.ClientId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(client);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ClientExists(client.ClientId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(client);
        //}

        //// GET: Clients/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Client
        //        .FirstOrDefaultAsync(m => m.ClientId == id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(client);
        //}

        //// POST: Clients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var client = await _context.Client.FindAsync(id);
        //    _context.Client.Remove(client);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ClientExists(int id)
        //{
        //    return _context.Client.Any(e => e.ClientId == id);
        //}
    }
}
