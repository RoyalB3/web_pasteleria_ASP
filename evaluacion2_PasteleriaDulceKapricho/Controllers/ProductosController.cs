using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace evaluacion2_PasteleriaDulceKapricho.Controllers
{
    public class ProductosController : Controller
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
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult CrearProducto(string nombreProducto, int idMateria, string categoria, int stock, int costo, int precio)
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
        con.Open();

        var sentencia = new SqlCommand();
        sentencia.CommandType = System.Data.CommandType.Text;
        sentencia.CommandText = "INSERT INTO PRODUCTOS (NOMBRE_PRODUCTO, ID_MATERIA, CATEGORIA, STOCK, COSTO, PRECIO) VALUES (@nombreProducto, @idMateria, @categoria, @stock, @costo, @precio)";
        sentencia.Parameters.Add(new SqlParameter("@nombreProducto", nombreProducto));
        sentencia.Parameters.Add(new SqlParameter("@idMateria", idMateria));
        sentencia.Parameters.Add(new SqlParameter("@categoria", categoria));
        sentencia.Parameters.Add(new SqlParameter("@stock", stock));
        sentencia.Parameters.Add(new SqlParameter("@costo", costo));
        sentencia.Parameters.Add(new SqlParameter("@precio", precio));
        sentencia.Connection = con;

        var result = sentencia.ExecuteNonQuery();
        var mensaje = "";

        if (result == 1)
        {
            mensaje = "Producto agregado correctamente";
        }
        else
        {
            mensaje = "Error al agregar el producto";
        }

        con.Close();

        ViewBag.mensaje = mensaje;
        ViewBag.nombreProducto = nombreProducto;
        ViewBag.idMateria = idMateria;
        ViewBag.categoria = categoria;
        ViewBag.stock = stock;
        ViewBag.costo = costo;
        ViewBag.precio = precio;

        return View("/Views/DulceKapricho/Productos/crearProducto.cshtml");
    }

    public IActionResult AumentarStock(int idProducto, int cantidad)
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
        con.Open();

        var sentencia = new SqlCommand();
        sentencia.CommandType = System.Data.CommandType.Text;
        sentencia.CommandText = "UPDATE PRODUCTOS SET STOCK = STOCK + @cantidad WHERE ID_PRODUCTO = @idProducto";
        sentencia.Parameters.Add(new SqlParameter("@cantidad", cantidad));
        sentencia.Parameters.Add(new SqlParameter("@idProducto", idProducto));
        sentencia.Connection = con;

        var result = sentencia.ExecuteNonQuery();
        var mensaje = "";

        if (result == 1)
        {
            mensaje = "Stock actualizado correctamente";
        }
        else
        {
            mensaje = "Error al actualizar el stock";
        }

        con.Close();

        ViewBag.mensaje = mensaje;
        ViewBag.idProducto = idProducto;
        ViewBag.cantidad = cantidad;

        return View("/Views/DulceKapricho/Productos/crearProducto.cshtml");
    }
    public IActionResult EliminarProducto(int idProducto)
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
        con.Open();

        var sentencia = new SqlCommand();
        sentencia.CommandType = System.Data.CommandType.Text;
        sentencia.CommandText = "DELETE FROM PRODUCTOS WHERE ID_PRODUCTO = @idProducto";
        sentencia.Parameters.Add(new SqlParameter("@idProducto", idProducto));
        sentencia.Connection = con;

        var result = sentencia.ExecuteNonQuery();
        var mensaje = "";

        if (result == 1)
        {
            mensaje = "Producto eliminado correctamente";
        }
        else
        {
            mensaje = "Error al eliminar el producto";
        }

        con.Close();

        ViewBag.mensaje = mensaje;
        ViewBag.idProducto = idProducto;

        return View("/Views/DulceKapricho/Productos/crearProductos.cshtml");
    }

}
}
