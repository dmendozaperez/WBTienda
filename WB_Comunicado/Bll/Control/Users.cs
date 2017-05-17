using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace WB_ClienteBata.Bll
{
    public class Users
    {
        #region<ATRIBUTOS>
        public Int32 _usu_id { set; get; }
        public string _usu_nombre { set; get; }
        public string _usu_contraseña { set; get; }
        
        public string _usu_est_id { set; get; }
        public string _nombre { set; get; }
        public string _usu_niv_id { set; get; }
        public string _usu_nivel { set; get; }
        public DateTime _usu_creacion { set; get; }
        public string _cod_super { set; get; }
        public string _user_responsable { set; get; }

        #endregion

        

        #region<Metodos Estaticos>    

        public static Boolean _existe_bg_bw(string _tda,ref string _cadena)
        {
            Boolean _valida = false;
            string sqlquery = "USP_Existe_Tda_BGWB";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion_tda());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cod_entid", _tda);

                cmd.Parameters.Add("@existe", SqlDbType.Bit);
                cmd.Parameters["@existe"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@cadena", SqlDbType.VarChar, 5);
                cmd.Parameters["@cadena"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                _valida =(Boolean) cmd.Parameters["@existe"].Value;
                _cadena=(string)cmd.Parameters["@cadena"].Value;
            }
            catch(Exception exc)
            {
                _valida = false;
            }
            return _valida;
        }

        public static string _estado_acceso(string _tda)
        {
            //DataTable dt = null;
            SqlConnection cn = null;
            SqlCommand cmd = null;
            //SqlDataAdapter da = null;
            string sqlcommand = "USP_VefificaTda";
            string _estado = "";
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlcommand, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@cod_tda", _tda);
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 5);
                cmd.Parameters["@estado"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _estado = cmd.Parameters["@estado"].Value.ToString();
            }
            catch
            {
                if (cn!=null)
                    if (cn.State == ConnectionState.Open) cn.Close();
                _estado = "0";
            }
            if (cn != null)
                if (cn.State == ConnectionState.Open) cn.Close();
            return _estado;
        }
        public static Boolean _actualiza_pass(Int32 _usu_id, string _pass)
        {
            string sqlquery= "USP_Modificar_UsuarioWeb";
            SqlConnection cn=null;
            SqlCommand cmd=null;
            Boolean _valida = false;
            try
            {
                cn=new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_id", _usu_id);
                cmd.Parameters.AddWithValue("@pass", _pass);
                cmd.ExecuteNonQuery();
                _valida = true;
            }
            catch
            {
                _valida = false;
                if (cn.State==ConnectionState.Open) cn.Close();
            }
            return _valida;
        }
        public static DataTable _leer_usuario(string _usv_username)
        {
            DataTable dt = null;
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            string sqlcommand = "USP_Leer_Usuario_Acceso";
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlcommand, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Usu_Nombre", _usv_username);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception)
            {
                dt = null;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return dt;
        }
     
        #endregion
    }
    
}