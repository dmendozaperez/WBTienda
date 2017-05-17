using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WB_ClienteBata.Bll;
namespace WB_ClienteBata
{
    public partial class update_password : System.Web.UI.Page
    {
        static Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
             //if (!IsPostBack)
             //{
             //    LoginUser.UserName = _user._nombre;                 
             //    SetFocus(LoginUser.FindControl("Password"));                 
             //}
             //else
             //{
                 LoginUser.UserName = _user._nombre;
                 SetFocus(LoginUser.FindControl("Password"));   
             //}
           
        }
        protected void LoginUser_Authenticate(object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
        {
            try
            {
                string _new_pass = LoginUser.Password;

                TextBox txt = (TextBox)LoginUser.FindControl("NewPassword");

                if (_new_pass != txt.Text)
                {
                    LoginUser.FailureText = "Error de validacion: la contraseña de confirmacion es incorrecta";
                    SetFocus(LoginUser.FindControl("Password"));
                    return;
                }
                if (_new_pass == "123")
                {
                    LoginUser.FailureText = "Error de validacion: el password ingresado no puede ser la predeterminada";
                    SetFocus(LoginUser.FindControl("Password"));
                    return;
                }
                Boolean _valor = Users._actualiza_pass(_user._usu_id, _new_pass);
                if (_valor)
                {
                    Session[Constants.NameSessionUser] = null;
                    Response.Redirect("~/Comunicado/Control/LoginForm.aspx");
                }
                else
                {
                    LoginUser.FailureText = "Error de conexión con el servidor";
                    SetFocus(LoginUser.FindControl("Password"));
                }
            }
            catch
            {
            }
        }
       
    }
}