using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WB_ClienteBata.Bll;
using System.Web.Security;
namespace WB_ClienteBata.Control
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginUser.Focus();
        }
        protected void LoginUser_Authenticate(object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
        {
            try
            {
                //obtine el nombre del usuario que desea autenticarse
                string name = LoginUser.UserName;
                //Obtine el password
                string password = LoginUser.Password;
                //obtiene si el usuario desea o no almacenar una cookie
                bool checkcookie = LoginUser.RememberMeSet;
                Users user = new Users();
                user = loadUser(name);

                if (user != null)
                {
                    user._usu_est_id = "A";
                    if (user._usu_est_id.Equals(Constants.IdStatusActive))
                    {
                        //Desencripta la contraseña del usuario
                        string passUser = user._usu_contraseña;
                        //valida la contraseña contraseña que ingreso contra lad del usuario
                        if (password.Equals(passUser))
                        {
                            //userSign(ref user, name, password, Request.UserHostAddress);

                            Session[Constants.NameSessionUser] = user;
                            loadMenu(user._usu_id);
                            //insertar auditoria                            
                            //Autenticación
                            try
                            {
                                Session["pass"] = passUser;                                
                                FormsAuthentication.RedirectFromLoginPage(user._usu_id.ToString(), checkcookie);                                
                            }
                            catch (Exception ex) { LoginUser.FailureText = "Error de conexión: " + ex.Message.ToString(); }
                        }
                        else
                        {                            
                            System.Diagnostics.Trace.WriteLine("[ValidateUser] Usuario y/o contraseña invalidos.");
                        }
                    }
                    else if (user._usu_est_id.Equals(Constants.IdStatusPasswordExpiration))
                    {
                        //Desencripta la contraseña del usuario
                        string passUser =user._usu_contraseña;
                        //valida la contraseña contraseña que ingreso contra lad del usuario
                        if (password.Equals(passUser))
                        {                            
                            loadMenu(user._usu_id);
                            FormsAuthentication.SetAuthCookie(user._usu_id.ToString(), checkcookie);
                            Server.Transfer("changePassword.aspx?expiration=1");
                        }
                        else
                        {                           
                            System.Diagnostics.Trace.WriteLine("[ValidateUser] Usuario y/o contraseña invalidos.");
                        }
                    }
                    else
                    {
                        LoginUser.FailureText = "Error de conexión: El usuario no esta Activo";
                    }
                }
                else
                    System.Diagnostics.Trace.WriteLine("[ValidateUser] La validacion del usuario fallo.");

            }
            catch
            { }
        }
        private void loadMenu(decimal _usu_id)
        {
            //List<ApplicationFunctions> colappFunctions = new List<ApplicationFunctions>();
            //colappFunctions = ApplicationFunctions.getFunctions_tree(_usu_id);//  .getCoordinators(_user._usv_co, _user._usv_warehouse, _user._usv_area);
            //Session["_MENU"] = colappFunctions;
            //dwCustomers.Focus();
            //// Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            //dwCustomers.DataSource = dsCustomers;
            //dwCustomers.DataBind();
        }
        private Users loadUser(string userName)
        {
            DataTable dtUser = Users._leer_usuario(userName);

            if (dtUser == null || dtUser.Rows.Count <= 0)
            {
                return null;
            }

           
            DataRow dr = dtUser.Rows[0];


            Int32 v_usu_id; string v_usu_nombre; string v_usu_contraseña; string v_usu_est_id; string v_nombre; string v_usu_niv_id; string v_usu_niv_nombre; string v_cod_super;

            v_usu_id = Convert.ToInt32(dr["Usu_Id"].ToString());
            v_usu_nombre = dr["Usu_Nombre"].ToString();
            v_usu_contraseña = dr["Usu_Contraseña"].ToString();         
            v_nombre = dr["Nombre"].ToString();            
            Users u = new Users
            {
                _usu_id = Convert.ToInt32(dr["Usu_Id"].ToString()),
                _usu_nombre = dr["Usu_Nombre"].ToString(),
                _usu_contraseña = dr["Usu_Contraseña"].ToString(),              
                _nombre = dr["Nombre"].ToString(),                
                
            };

            return u;
        }
    }
}