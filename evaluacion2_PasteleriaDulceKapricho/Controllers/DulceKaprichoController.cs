using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace evaluacion2_PasteleriaDulceKapricho.Controllers
{
    public class DulceKaprichoController : Controller
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";

        public class Persona
        {
            public string RUT { get; set; }
            public string NOMBRE { get; set; }
            public string APELLIDO { get; set; }
            public string CORREO { get; set; }
            public string CLAVE { get; set; }
            public DateTime F_NACIMIENTO { get; set; }
            public string DIRECCION { get; set; }
            public int TELEFONO { get; set; }
            public int NIVEL { get; set; }
        }
        public class Producto
        {
            public int ID_PRODUCTO { get; set; }
            public string NOMBRE_PRODUCTO { get; set; }
            public int ID_MATERIA { get; set; }
            public string CATEGORIA { get; set; }
            public int STOCK { get; set; }
            public int COSTO { get; set; }
            public int PRECIO { get; set; }
        }
        public class Compra
        {
            public int N_COMPRA { get; set; }
            public string RUT_PROVEEDOR { get; set; }
            public int ID_MATERIA { get; set; }
            public int CANTIDAD { get; set; }
            public int PRECIO { get; set; }
            public DateTime FECHA_COMPRA { get; set; }
        }
        public class Delivery
        {
            public int IdDelivery { get; set; }
            public string RutDelivery { get; set; }
            public string NombreDelivery { get; set; }
            public DateTime FechaPedido { get; set; }
        }
        public IActionResult LoginTrabajador()
        {
            return View("/Views/DulceKapricho/Trabajador/login.cshtml");
        }
        public IActionResult LoginCliente()
        {
            return View("/Views/DulceKapricho/Clientes/LoginCliente.cshtml");
        }
        public IActionResult LoginAdmin()
        {
            return View("/Views/DulceKapricho/Trabajador/admin.cshtml");
        }
        public IActionResult PortalAdmin()
        {
            return View("/Views/DulceKapricho/Trabajador/portalAdministrador.cshtml");
        }
        public IActionResult CrearTrabajador()
        {
            return View("/Views/DulceKapricho/Trabajador/crearTrabajador.cshtml");
        }
        public IActionResult RegistrarCliente()
        {
            return View("/Views/DulceKapricho/Clientes/registrarCliente.cshtml");
        }
        public IActionResult Contacto()
        {
            return View("/Views/DulceKapricho/Clientes/contacto.cshtml");
        }
        public IActionResult CrearProducto()
        {
            List<Producto> productos = new List<Producto>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var sentencia = new SqlCommand();
                sentencia.CommandType = System.Data.CommandType.Text;
                string query = "SELECT * FROM PRODUCTOS";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                string htmlProductos = "";
                while (reader.Read())
                {
                    htmlProductos += $"<tr><td>{reader["ID_PRODUCTO"]}</td><td>{reader["NOMBRE_PRODUCTO"]}</td><td>{reader["ID_MATERIA"]}</td><td>{reader["CATEGORIA"]}</td><td>{reader["STOCK"]}</td><td>{reader["COSTO"]}</td><td>{reader["PRECIO"]}</td></tr>";
                }
                ViewBag.htmlProductos = htmlProductos;
                con.Close();
            }
            return View("/Views/DulceKapricho/Productos/crearProducto.cshtml");
        }
        public IActionResult VerProductos()
        {
            return View("/Views/DulceKapricho/Productos/Productos.cshtml");
        }

        public IActionResult PortalTrabajador()
        {
            return View("/Views/DulceKapricho/Trabajador/portalTrabajador.cshtml");
        }
        public IActionResult PortalAdministrador()
        {
            return View("/Views/DulceKapricho/Trabajador/portalAdministrador.cshtml");
        }
        public IActionResult PortalClientes()
        {
            return View("/Views/Home/Index.cshtml");
        }
        public IActionResult Despacho()
        {
            return View("/Views/DulceKapricho/Despacho/despacho.cshtml");
        }
        public IActionResult PedidosPendientes()
        {
            return View("/Views/DulceKapricho/Despacho/pedidosPendientes.cshtml");
        }
        public IActionResult Portales()
        {
            return View("/Views/DulceKapricho/portales.cshtml");
        }
        public IActionResult Tipo()
        {
            return View("/Views/DulceKapricho/bifurcacion.cshtml");
        }
        public IActionResult Stock()
        {
            return View("/Views/DulceKapricho/Stock/portalStock.cshtml");
        }
        public IActionResult StockTrabajador()
        {
            return View("/Views/DulceKapricho/Stock/portalStockTrabajador.cshtml");
        }
        public IActionResult AddMaterial()
        {
            List<string> materiaPrima = new List<string>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var sentencia = new SqlCommand();
                sentencia.CommandType = System.Data.CommandType.Text;
                string query = "SELECT * FROM MATERIA_PRIMA";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                string htmlMateriaPrima = "";
                while (reader.Read())
                {
                    htmlMateriaPrima += $"<tr><td>{reader["ID_MATERIA"]}</td><td>{reader["RUT_PROVEEDOR"]}</td><td>{reader["CANTIDAD"]}</td><td>{reader["FECHA"]}</td><td>{reader["NOMBRE_MATERIAL"]}</td></tr>";
                }
                ViewBag.htmlMateriaPrima = htmlMateriaPrima;
                con.Close();
            }
            return View("/Views/DulceKapricho/Stock/agregarMateria.cshtml");
        }
        public IActionResult SolicitarMateria()
        {

            return View("/Views/DulceKapricho/Stock/solicitarMateria.cshtml");
        }
        public IActionResult Carrito()
        {
            return View("/Views/DulceKapricho/Productos/Carrito.cshtml");
        }
        public IActionResult Compras()
        {
            List<Compra> compras = new List<Compra>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var sentencia = new SqlCommand();
                sentencia.CommandType = System.Data.CommandType.Text;
                string query = "SELECT * FROM COMPRAS";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                string htmlCompras = "";
                while (reader.Read())
                {
                    htmlCompras += $"<tr><td>{reader["N_COMPRA"]}</td><td>{reader["RUT_PROVEEDOR"]}</td><td>{reader["ID_MATERIA"]}</td><td>{reader["CANTIDAD"]}</td><td>{reader["PRECIO"]}</td><td>{reader["FECHA_COMPRA"]}</td></tr>";
                }
                ViewBag.htmlCompras = htmlCompras;
                con.Close();
            }
            return View("/Views/DulceKapricho/Administrador/compras.cshtml");
        }
        public IActionResult delivery()
        {
            {
                List<string> personas = new List<string>();

                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var sentencia = new SqlCommand();
                    sentencia.CommandType = System.Data.CommandType.Text;
                    string query = "SELECT * FROM PERSONA WHERE NIVEL = 4";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    string htmlPersona = "";
                    while (reader.Read())
                    {
                        htmlPersona += $"<tr><td>{reader["RUT"]}</td><td>{reader["NOMBRE"]}</td><td>{reader["APELLIDO"]}</td><td>{reader["CORREO"]}</td><td>{reader["CLAVE"]}</td><td>{reader["F_NACIMIENTO"]}</td><td>{reader["DIRECCION"]}</td><td>{reader["TELEFONO"]}</td><td>{reader["NIVEL"]}</td></tr>";
                    }
                    ViewBag.htmlPersona = htmlPersona;
                    con.Close();
                }

                return View("/Views/DulceKapricho/Administrador/delivery.cshtml");
            }
        }

        public IActionResult DeliveryForm()
        {
            List<string> deliveries = new List<string>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var sentencia = new SqlCommand();
                sentencia.CommandType = System.Data.CommandType.Text;
                string query = "SELECT * FROM DELIVERY";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                string htmlDelivery = "";
                while (reader.Read())
                {
                    htmlDelivery += $"<tr><td>{reader["ID_DELIVERY"]}</td><td>{reader["RUT_DELIVERY"]}</td><td>{reader["NOMBRE_DELIVERY"]}</td><td>{reader["FECHA_PEDIDO"]}</td></tr>";
                }
                ViewBag.htmlDelivery = htmlDelivery;
                con.Close();
            }

            return View("/Views/DulceKapricho/Administrador/deliveryForm.cshtml");
        }
 
        public IActionResult Gestion()
        {
            List<Persona> personas = new List<Persona>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var sentencia = new SqlCommand();
                sentencia.CommandType = System.Data.CommandType.Text;
                string query = "SELECT * FROM PERSONA";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                string htmlPersona = "";
                while (reader.Read())
                {
                    htmlPersona += $"<tr><td>{reader["RUT"]}</td><td>{reader["NOMBRE"]}</td><td>{reader["APELLIDO"]}</td><td>{reader["CORREO"]}</td><td>{reader["CLAVE"]}</td><td>{reader["F_NACIMIENTO"]}</td><td>{reader["DIRECCION"]}</td><td>{reader["TELEFONO"]}</td><td>{reader["NIVEL"]}</td></tr>";
                }
                ViewBag.htmlPersona = htmlPersona;
                con.Close();
            }
            return View("/Views/DulceKapricho/Administrador/gestion.cshtml");

        }
        public IActionResult Proveedores()
        {
            List<Persona> personas = new List<Persona>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                var sentencia = new SqlCommand();
                sentencia.CommandType = System.Data.CommandType.Text;
                string query = "SELECT * FROM PROVEEDORES";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                string htmlPersona = "";
                while (reader.Read())
                {
                    htmlPersona += $"<tr><td>{reader["RUT_PROVEEDOR"]}</td><td>{reader["NOMBRE_PROVEEDOR"]}</td><td>{reader["CORREO"]}</td><td>{reader["TELEFONO"]}</td></tr>";
                }
                ViewBag.htmlPersona = htmlPersona;
                con.Close();
            }
            return View("/Views/DulceKapricho/Stock/proveedores.cshtml");
        }

        public IActionResult CrearPersona(string rut, string nombre, string apellido, string mail, string clave, DateTime f_nacimiento, string direccion, int telefono, int nivel)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "insert into PERSONA(RUT, NOMBRE, APELLIDO, CORREO, CLAVE, F_NACIMIENTO, DIRECCION, TELEFONO, NIVEL) values (@prut, @pnom, @papellido, @pmail, @pclave, @pfecha, @pdireccion, @ptelefono, @pnivel)";
            sentencia.Parameters.Add(new SqlParameter("prut", rut));
            sentencia.Parameters.Add(new SqlParameter("pnom", nombre));
            sentencia.Parameters.Add(new SqlParameter("papellido", apellido));
            sentencia.Parameters.Add(new SqlParameter("pmail", mail));
            sentencia.Parameters.Add(new SqlParameter("pclave", clave));
            sentencia.Parameters.Add(new SqlParameter("pfecha", f_nacimiento));
            sentencia.Parameters.Add(new SqlParameter("pdireccion", direccion));
            sentencia.Parameters.Add(new SqlParameter("ptelefono", telefono));
            sentencia.Parameters.Add(new SqlParameter("pnivel", nivel));
            sentencia.Connection = con;
            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";
            if (result == 1)
            {
                mensaje = "REGISTRO GRABADO";
            }
            else
            {
                mensaje = "ERROR";
            }

            con.Close();

            ViewBag.mensaje = mensaje;

            ViewBag.rut = rut;
            ViewBag.nombre = nombre;
            ViewBag.apellido = apellido;
            ViewBag.correo = mail;
            ViewBag.clave = clave;
            ViewBag.f_nacimiento = f_nacimiento;
            ViewBag.direccion = direccion;
            ViewBag.telefono = telefono;
            ViewBag.nivel = nivel;
            ViewBag.mensaje = mensaje;

            return View("/Views/DulceKapricho/Administrador/gestion.cshtml");
        }

        public IActionResult EditarPersona(string rut, string nombre, string apellido, string mail, string direccion, int telefono, int nivel)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "UPDATE PERSONA SET CORREO = @pmail, DIRECCION = @pdireccion, TELEFONO = @ptelefono, NIVEL = @pnivel WHERE RUT = @prut";
            sentencia.Parameters.Add(new SqlParameter("@prut", rut));
            sentencia.Parameters.Add(new SqlParameter("@pmail", mail));
            sentencia.Parameters.Add(new SqlParameter("@pdireccion", direccion));
            sentencia.Parameters.Add(new SqlParameter("@ptelefono", telefono));
            sentencia.Parameters.Add(new SqlParameter("@pnivel", nivel));
            sentencia.Connection = con;

            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";

            if (result == 1)
            {
                mensaje = "Datos actualizados correctamente";
            }
            else
            {
                mensaje = "Error al actualizar los datos";
            }

            con.Close();

            ViewBag.mensaje = mensaje;
            ViewBag.rut = rut;
            ViewBag.correo = mail;
            ViewBag.direccion = direccion;
            ViewBag.telefono = telefono;
            ViewBag.nivel = nivel;

            return View("/Views/DulceKapricho/Trabajador/crearTrabajador.cshtml");
        }

        public IActionResult BorrarPersona(string Rut)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "delete from PERSONA where RUT = @p_rut";
            sentencia.Parameters.Add(new SqlParameter("p_rut", Rut));
            sentencia.Connection = con;
            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";
            if (result == 1)
            {
                mensaje = "Usuario borrado";
            }
            else
            {
                mensaje = "ERROR";
            }

            con.Close();
            ViewBag.mensaje = mensaje;
            ViewBag.Rut = Rut;
            return View("/Views/DulceKapricho/Administrador/gestion.cshtml");
        }

        /*public ActionResult Index(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id FROM Usuarios WHERE NombreUsuario = @username AND Clave = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    // Iniciar sesión exitosa, redirigir a la página de inicio
                    Session["UsuarioId"] = result.ToString(); // Guardar información del usuario en la sesión si es necesario
                    return RedirectToAction("Inicio", "Home"); // Reemplaza "Inicio" y "Home" con tu ruta de inicio
                }
                else
                {
                    // Credenciales inválidas, mostrar mensaje de error
                    ViewBag.ErrorMessage = "Nombre de usuario o contraseña incorrectos";
                    return View();
                }
            }*/
        }
    }

    