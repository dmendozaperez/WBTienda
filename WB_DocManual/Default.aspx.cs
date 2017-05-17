using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using WB_ClienteBata.Bll;

namespace WB_ClienteBata
{
    public partial class Default : System.Web.UI.Page
    {
        static Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
            if (!IsPostBack)
            {
                if (Session["pass"].ToString() != "123")
                {
                    ASPxFileManager1.SettingsFileList.DetailsViewSettings.AllowColumnSort = true;
                }
                else
                {
                    Response.Redirect("update_password.aspx");
                }
            }
           
        }

        protected void ASPxFileManager1_CustomThumbnail(object source, DevExpress.Web.FileManagerThumbnailCreateEventArgs e)
        {
            switch (((FileManagerFile)e.Item).Extension)
            {
                case ".pdf":
                    e.ThumbnailImage.Url = "Images/pdf-viewer.png";
                    break;              
            }
        }

      

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                FailureText.Text = "";
                string _new_pass =Password.Text;
                
                
                if (_new_pass != NewPassword.Text)
                {
                   FailureText.Text = "Error de validacion: la contraseña de confirmacion es incorrecta";
                   Password.Focus();                    
                    return;
                }
                if (_new_pass == "123")
                {
                    FailureText.Text = "Error de validacion: el password ingresado no puede ser la predeterminada";
                    Password.Focus();  
                    return;
                }
                Boolean _valor = Users._actualiza_pass(_user._usu_id, _new_pass);
                if (_valor)
                {
                    //Session[Constants.NameSessionUser] = null;
                    //Response.Redirect("Default.aspx");
                }
                else
                {
                    FailureText.Text = "Error de conexión con el servidor";
                    Password.Focus(); 
                }
            }
            catch
            {
            }
        }
    }
}