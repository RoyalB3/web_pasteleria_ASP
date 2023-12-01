using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace evaluacion2_PasteleriaDulceKapricho.Controllers
{
    public class ProveedorController : Controller
{
    public IActionResult ObtenerCorreoPorRut(string rut)
    {
        string correoProveedor = ObtenerCorreoElectronico(rut);

        return Json(new { correo = correoProveedor });
    }

    // Método para obtener el correo electrónico del proveedor desde la base de datos
    private string ObtenerCorreoElectronico(string rut)
    {
        string correoProveedor = "";

        using (SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = bddEva3; Integrated Security = True; Connect Timeout = 30;"))
        {
            con.Open();
            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            string query = $"SELECT CORREO FROM PROVEEDORES WHERE RUT = '{rut}'"; // Ajusta la consulta según tu esquema de base de datos
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                correoProveedor = reader["CORREO"].ToString();
            }

            con.Close();
        }

        return correoProveedor;
    }
}
}
