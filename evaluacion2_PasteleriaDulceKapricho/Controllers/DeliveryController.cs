using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace evaluacion2_PasteleriaDulceKapricho.Controllers
{
    public class DeliveryController : Controller
{
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

        return View("/Views/DulceKapricho/Administrador/deliveryForm.cshtml");
    }
        public IActionResult AgregarDelivery(string rutDelivery, string nombreDelivery)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";
            string mensaje = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand sentencia = new SqlCommand();
                sentencia.CommandType = System.Data.CommandType.Text;
                sentencia.CommandText = "INSERT INTO DELIVERY (RUT_DELIVERY, NOMBRE_DELIVERY, FECHA_PEDIDO) VALUES (@rutDelivery, @nombreDelivery, GETDATE())";
                sentencia.Parameters.AddWithValue("@rutDelivery", rutDelivery);
                sentencia.Parameters.AddWithValue("@nombreDelivery", nombreDelivery);
                sentencia.Connection = con;

                int result = sentencia.ExecuteNonQuery();

                if (result == 1)
                {
                    mensaje = "Delivery agregado correctamente";
                }
                else
                {
                    mensaje = "Error al agregar el delivery";
                }

                con.Close();
            }

            ViewBag.mensaje = mensaje;
            ViewBag.rutDelivery = rutDelivery;
            ViewBag.nombreDelivery = nombreDelivery;

            return View("/Views/DulceKapricho/Administrador/deliveryForm.cshtml");
        }
    }
}
