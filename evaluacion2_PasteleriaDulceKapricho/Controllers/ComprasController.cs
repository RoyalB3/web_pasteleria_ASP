using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Data.SqlClient;

namespace evaluacion2_PasteleriaDulceKapricho.Controllers
{
    public class ComprasController : Controller
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;";

        public class Compra
        {
            public int N_COMPRA { get; set; }
            public string RUT_PROVEEDOR { get; set; }
            public int ID_MATERIA { get; set; }
            public int CANTIDAD { get; set; }
            public int PRECIO { get; set; }
            public DateTime FECHA_COMPRA { get; set; }
        }
        [HttpGet]
        public IActionResult CrearCompra()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCompra(string rutProveedor, int idMateria, int cantidad, int precio, DateTime fechaCompra)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "INSERT INTO COMPRAS (RUT_PROVEEDOR, ID_MATERIA, CANTIDAD, PRECIO, FECHA_COMPRA) VALUES (@rutProveedor, @idMateria, @cantidad, @precio, GETDATE())";
            sentencia.Parameters.Add(new SqlParameter("@rutProveedor", rutProveedor));
            sentencia.Parameters.Add(new SqlParameter("@idMateria", idMateria));
            sentencia.Parameters.Add(new SqlParameter("@cantidad", cantidad));
            sentencia.Parameters.Add(new SqlParameter("@precio", precio));
            sentencia.Connection = con;

            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";

            if (result == 1)
            {
                mensaje = "Compra agregada correctamente";
            }
            else
            {
                mensaje = "Error al agregar la compra";
            }

            con.Close();

            ViewBag.mensaje = mensaje;
            ViewBag.rutProveedor = rutProveedor;
            ViewBag.idMateria = idMateria;
            ViewBag.cantidad = cantidad;
            ViewBag.precio = precio;


            return View("/Views/DulceKapricho/Administrador/compras.cshtml);");
        }

        public IActionResult EliminarCompra(int idCompra)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TuBaseDeDatos;Integrated Security=True;Connect Timeout=30;"))
            {
                con.Open();

                var sentencia = new SqlCommand();
                sentencia.CommandType = System.Data.CommandType.Text;
                sentencia.CommandText = "DELETE FROM COMPRAS WHERE N_COMPRA = @idCompra";
                sentencia.Parameters.AddWithValue("@idCompra", idCompra);
                sentencia.Connection = con;

                var result = sentencia.ExecuteNonQuery();
                var mensaje = "";

                if (result == 1)
                {
                    mensaje = "Compra eliminada correctamente";
                }
                else
                {
                    mensaje = "Error al eliminar la compra";
                }

                con.Close();

                ViewBag.mensaje = mensaje;
                ViewBag.idCompra = idCompra;

                return View("/Views/DulceKapricho/Administrador/compras.cshtml);");
            }
        }


    }
}
