using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace WB_ClienteBata.Bll
{
    public class Conexion
    {
        //static string strconexion = ConfigurationManager.ConnectionStrings["MyConexionSql"].ConnectionString;
        static string strconexion = "Server=10.10.10.208;Database=BdTienda;User ID=sa;Password=Bata2013;Trusted_Connection=False;";
        public static string myconexion()
        {
            return strconexion;
        }
    }
}