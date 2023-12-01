using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace evaluacion2_PasteleriaDulceKapricho.Controllers
{
    public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
        private bool IsValidUser(string rut, string clave)
        {

            string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = bddEva3; Integrated Security = True; Connect Timeout = 30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT COUNT(*) FROM PERSONA WHERE RUT = @pRut AND Clave = @pClave AND NIVEL = 1";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@pRut", rut);
                    cmd.Parameters.AddWithValue("@pClave", clave);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    public async Task <IActionResult> LoginAdmin(string rut, string clave)
    {
        if (ModelState.IsValid)
        {
            if (IsValidUser(rut, clave))
            {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim("pRut", rut),
                        new Claim(ClaimTypes.Role,"administrador")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return View("/Views/DulceKapricho/Trabajador/portalAdministrador.cshtml");
                }
            else
            {
                ModelState.AddModelError(string.Empty, "Rut o clave invalida");
            }
        }

        return View("/Views/DulceKapricho/Trabajador/admin.cshtml");
    }

        private bool IsValidUser2(string rut, string clave)
        {

            string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = bddEva3; Integrated Security = True; Connect Timeout = 30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT COUNT(*) FROM PERSONA WHERE RUT = @pRut AND Clave = @pClave AND NIVEL = 2";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@pRut", rut);
                    cmd.Parameters.AddWithValue("@pClave", clave);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public IActionResult LoginTrabajador(string rut, string clave)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser2(rut, clave))
                {
                    return View("/Views/DulceKapricho/Trabajador/portalTrabajador.cshtml");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Rut o clave invalida");
                }
            }

            return View("/Views/DulceKapricho/Trabajador/login.cshtml");
        }
        private bool IsValidUser3(string rut, string clave)
        {

            string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = bddEva3; Integrated Security = True; Connect Timeout = 30;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT COUNT(*) FROM PERSONA WHERE RUT = @pRut AND Clave = @pClave AND NIVEL = 3";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@pRut", rut);
                    cmd.Parameters.AddWithValue("@pClave", clave);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public IActionResult LoginCliente(string rut, string clave)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser3(rut, clave))
                {
                    return View("/Views/DulceKapricho/Clientes/portalClientes.cshtml");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Rut o clave invalida");
                }
            }

            return View("/Views/DulceKapricho/Clientes/loginCliente.cshtml");
        }

        public async Task<IActionResult> Cerrar()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
