using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace evaluacion2_PasteleriaDulceKapricho.Controllers
{
    public class PedidosController : Controller
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
        public class Pedido
        {
            public int ID_PEDIDO { get; set; }
            public string RUT_CLIENTE { get; set; }
            public DateTime FECHA_PEDIDO { get; set; }
            public int TOTAL_PRECIO { get; set; }

        }
        public IActionResult Index()
    {
        return View();
    }
        public decimal ObtenerPrecioVentaPorIdProducto(int idProducto)
        {
            decimal precioVenta = 0;
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;"))
            {
                con.Open();
                string query = "SELECT PRECIO FROM PRODUCTOS WHERE ID_PRODUCTO = @idProducto";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    precioVenta = Convert.ToDecimal(result);
                }
                con.Close();
            }

            return precioVenta;
        }
        private int ObtenerUltimoIDPedido(SqlConnection con)
        {
            string query = "SELECT IDENT_CURRENT('PEDIDOS')";
            SqlCommand cmd = new SqlCommand(query, con);
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }
        private Pedido ObtenerDetallesPedidoPorId(int idPedido, SqlConnection con)
        {
            // Aquí realizas la lógica para obtener los detalles del pedido según su ID desde la base de datos
            Pedido pedido = new Pedido();
            string query = "SELECT * FROM PEDIDOS WHERE ID_PEDIDO = @idPedido";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idPedido", idPedido);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                pedido.ID_PEDIDO = Convert.ToInt32(reader["ID_PEDIDO"]);
                pedido.RUT_CLIENTE = reader["RUT"].ToString();
                pedido.FECHA_PEDIDO = Convert.ToDateTime(reader["FECHA_PEDIDO"]);
                pedido.TOTAL_PRECIO = Convert.ToInt32(reader["TOTAL_PRECIO"]);
            }
            reader.Close();

            return pedido;
        }
        public IActionResult CrearPedido(string rutCliente, int tipoEntrega, int idDelivery, int cantidad, int idProducto)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bddEva3;Integrated Security=True;Connect Timeout=30;");
            con.Open();

            decimal precioVenta = ObtenerPrecioVentaPorIdProducto(idProducto);
            int totalPrecio = Convert.ToInt32(precioVenta * cantidad);

            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "INSERT INTO PEDIDOS (RUT_CLIENTE, FECHA_PEDIDO, TOTAL_PRECIO, TIPO_ENTREGA, ID_DELIVERY, CANTIDAD, ID_PRODUCTO) VALUES (@rutCliente, GETDATE(), @totalPrecio, @tipoEntrega, @idDelivery, @cantidad, @idProducto)";
            sentencia.Parameters.Add(new SqlParameter("@rutCliente", rutCliente));
            sentencia.Parameters.Add(new SqlParameter("@totalPrecio", totalPrecio)); // Usar el cálculo del precio total
            sentencia.Parameters.Add(new SqlParameter("@tipoEntrega", tipoEntrega));
            sentencia.Parameters.Add(new SqlParameter("@idDelivery", idDelivery));
            sentencia.Parameters.Add(new SqlParameter("@cantidad", cantidad));
            sentencia.Parameters.Add(new SqlParameter("@idProducto", idProducto));

            sentencia.Connection = con;
            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";

            if (result == 1)
            {
                mensaje = "Pedido creado correctamente";
            }
            else
            {
                mensaje = "Error al crear el pedido";
            }

            int idPedidoCreado = ObtenerUltimoIDPedido(con);

            // Obtener los detalles del pedido recién creado desde la base de datos
            Pedido pedido = ObtenerDetallesPedidoPorId(idPedidoCreado, con);

            con.Close();

            ViewBag.pedido = pedido;
            ViewBag.mensaje = mensaje;
            ViewBag.rutCliente = rutCliente;
            ViewBag.tipoEntrega = tipoEntrega;
            ViewBag.idDelivery = idDelivery;
            ViewBag.cantidad = cantidad;
            ViewBag.idProducto = idProducto;

            return View("/Views/Productos/Carrito.cshtml");
        }
    }
}
