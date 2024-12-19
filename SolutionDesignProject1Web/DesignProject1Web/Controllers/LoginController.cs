using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;
using System.Text.Json;

namespace DesignProject1Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            //var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            //{
            //    claim.Issuer,
            //    claim.OriginalIssuer,
            //    claim.Type,
            //    claim.Value
            //});

            //return Json(claims);

            ////return RedirectToAction("Listado", "Conversor", new { area = "" });




            // ------------ Solucion con la imgen del perfil ---------------------------------------------------------
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Principal == null)
            {
                return Json(new { Error = "No se encontró la información del usuario." });
            }

            var claims = result.Principal.Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            var pictureClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == "picture");
            string pictureURL = pictureClaim.Value;

            //TempData["Image"] = pictureURL;

            HttpContext.Session.SetString("UserImage", pictureURL);

            return RedirectToAction("Listado", "Conversor");

            //var pictureUrl = result.Principal?.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;
            //ViewData["PictureUrl"] = pictureUrl;
            ////return RedirectToAction("Listado", "Conversor", pictureUrl);
            //return RedirectToAction("Listado", "Conversor");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            //return View("Inicio");
            return RedirectToAction("Inicio", "Login");
        }

    }
}
