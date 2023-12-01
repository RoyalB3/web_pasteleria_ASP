using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace evaluacion2_PasteleriaDulceKapricho.Controllers
{
    public class ProcedimientosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CrearMateria(int id, string rut, int cantidad, DateTime fecha, string nombre)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "insert into MATERIA_PRIMA(RUT_PROVEEDOR, CANTIDAD, FECHA, NOMBRE_MATERIAL) values (@prut, @pcantidad, GETDATE(), @pnombre_material)";
            sentencia.Parameters.Add(new SqlParameter("prut", rut));
            sentencia.Parameters.Add(new SqlParameter("pcantidad", cantidad));
            sentencia.Parameters.Add(new SqlParameter("pnombre_material", nombre));
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

            ViewBag.id = id;
            ViewBag.rut = rut;
            ViewBag.cantidad = cantidad;
            ViewBag.nombre = nombre;
            ViewBag.fecha = fecha;
            ViewBag.mensaje = mensaje;

            return View("/Views/DulceKapricho/Stock/agregarMateria.cshtml");
        }

        public IActionResult AgregarStock(int id, int cantidad)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "UPDATE MATERIA_PRIMA SET CANTIDAD = CANTIDAD + @pcantidad WHERE ID_MATERIA = @pid";
            sentencia.Parameters.Add(new SqlParameter("pid", id));
            sentencia.Parameters.Add(new SqlParameter("pcantidad", cantidad));
            sentencia.Connection = con;
            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";
            if (result == 1)
            {
                mensaje = "REGISTRO GRABADO";
            }
            else
            {
                mensaje = "ERROR NO SE ENCONTRO UN ARTICULO CON ESTE ID";
            }

            con.Close();

            ViewBag.mensaje = mensaje;

            ViewBag.id = id;
            ViewBag.cantidad = cantidad;
            ViewBag.mensaje = mensaje;

            return View("/Views/DulceKapricho/Stock/agregarMateria.cshtml");
        }
        public IActionResult BorrarMateria(int id)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "DELETE FROM MATERIA_PRIMA WHERE ID_MATERIA = @pid";
            sentencia.Parameters.Add(new SqlParameter("@pid", id));
            sentencia.Connection = con;

            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";

            if (result == 1)
            {
                mensaje = "Registro eliminado correctamente";
            }
            else
            {
                mensaje = "Error al eliminar el registro de la materia prima";
            }

            con.Close();

            ViewBag.mensaje = mensaje;
            ViewBag.id = id;

            return View("/Views/DulceKapricho/Stock/borrarMateria.cshtml");
        }
        public IActionResult CrearProveedor(string rutProveedor, string nombreProveedor, string correoProveedor, int telefonoProveedor)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "INSERT INTO PROVEEDORES (RUT_PROVEEDOR, NOMBRE_PROVEEDOR, CORREO, TELEFONO) VALUES (@prut, @pnombre, @pcorreo, @ptelefono)";
            sentencia.Parameters.Add(new SqlParameter("@prut", rutProveedor));
            sentencia.Parameters.Add(new SqlParameter("@pnombre", nombreProveedor));
            sentencia.Parameters.Add(new SqlParameter("@pcorreo", correoProveedor));
            sentencia.Parameters.Add(new SqlParameter("@ptelefono", telefonoProveedor));
            sentencia.Connection = con;

            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";

            if (result == 1)
            {
                mensaje = "Registro grabado";
            }
            else
            {
                mensaje = "Error al insertar";
            }

            con.Close();

            ViewBag.mensaje = mensaje;
            ViewBag.rut = rutProveedor;
            ViewBag.nombre = nombreProveedor;
            ViewBag.correo = correoProveedor;
            ViewBag.telefono = telefonoProveedor;

            return View("/Views/DulceKapricho/Stock/proveedores.cshtml");
        }
        public IActionResult BorrarProveedor(string Rut)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "delete from PROVEEDORES where RUT_PROVEEDOR = @p_rut";
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
            return View("/Views/DulceKapricho/Stock/proveedores.cshtml");
        }
        public IActionResult ActualizarProveedor(string rut, string nuevoCorreo, int nuevoTelefono)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "UPDATE PROVEEDORES SET CORREO = @nuevoCorreo, TELEFONO = @nuevoTelefono WHERE RUT_PROVEEDOR = @rut";
            sentencia.Parameters.Add(new SqlParameter("@rut", rut));
            sentencia.Parameters.Add(new SqlParameter("@nuevoCorreo", nuevoCorreo));
            sentencia.Parameters.Add(new SqlParameter("@nuevoTelefono", nuevoTelefono));
            sentencia.Connection = con;

            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";

            if (result == 1)
            {
                mensaje = "Datos actualizados correctamente";
            }
            else
            {
                mensaje = "Error al actualizar los datos del proveedor";
            }

            con.Close();

            ViewBag.mensaje = mensaje;
            ViewBag.rut = rut;
            ViewBag.nuevoCorreo = nuevoCorreo;
            ViewBag.nuevoTelefono = nuevoTelefono;

            return View("/Views/DulceKapricho/Stock/proveedores.cshtml");
        }
        //Para mostrar los datos
        /*public IActionResult ListaProveedores()
        {
            List<Dictionary<string, object>> proveedores = new List<Dictionary<string, object>>();

            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT RUT_PROVEEDOR, NOMBRE_PROVEEDOR, CORREO, TELEFONO FROM PROVEEDORES", con);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dictionary<string, object> proveedor = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        proveedor[reader.GetName(i)] = reader.GetValue(i);
                    }
                    proveedores.Add(proveedor);
                }

                reader.Close();
            }

            ViewBag.Proveedores = proveedores;
            return View("/Views/DulceKapricho/Stock/proveedores.cshtml");
        }*/

    } 
} 

