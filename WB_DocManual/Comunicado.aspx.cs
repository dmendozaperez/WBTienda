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
    public partial class Comunicado : System.Web.UI.Page
    {
        static Users _user;
        protected void Page_Init(object sender, EventArgs e)
        {
            //if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            //else
            //    _user = (Users)Session[Constants.NameSessionUser];
            if (!IsPostBack)
            {
                //if (Session["pass"].ToString() != "123")
                //{
                //    ASPxFileManager1.SettingsFileList.DetailsViewSettings.AllowColumnSort = true;
                //}
                //else
                //{
                //    //Response.Redirect("update_password.aspx");
                //}
            }

            // Session["Opcion"] = "Emcomer";
            //if (!IsPostBack)
            //{
            string _tienda = (string)Session["tienda"];//this.Request.Params["Opcion"].ToString() ;

            // _tienda = "50111";

            string _ruta = "~\\Documento\\" + _tienda;
            //string _ruta = "~\\" + _tienda;

            // _ruta = @"D:\David\Sist_ComunicacionTDA\WB_Comunicado\WB_DocManual\Documento\" + _tienda;

            string _path_Formularios = _ruta + "\\Formularios";

            bool exists = System.IO.Directory.Exists(Server.MapPath(_ruta));

            bool exists_form = System.IO.Directory.Exists(Server.MapPath(_path_Formularios));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(_ruta));

            if (!exists_form)
                System.IO.Directory.CreateDirectory(Server.MapPath(_path_Formularios));

            if (_tienda != null)
            {
                //ASPxFileManager1.Settings.RootFolder = "~\\Documento\\" + _emp;
                ASPxFileManager1.Settings.RootFolder = _ruta;

            }
            else
            {
                ASPxFileManager1.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            // //if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            // //else
            // //    _user = (Users)Session[Constants.NameSessionUser];
            // if (!IsPostBack)
            // {
            //     //if (Session["pass"].ToString() != "123")
            //     //{
            //     //    ASPxFileManager1.SettingsFileList.DetailsViewSettings.AllowColumnSort = true;
            //     //}
            //     //else
            //     //{
            //     //    //Response.Redirect("update_password.aspx");
            //     //}
            // }

            //// Session["Opcion"] = "Emcomer";
            // //if (!IsPostBack)
            // //{
            // string _tienda = (string)Session["tienda"];//this.Request.Params["Opcion"].ToString() ;

            // _tienda = "50111";

            // string _ruta = "~\\Documento\\" + _tienda;
            // //string _ruta = "~\\" + _tienda;

            // // _ruta = @"D:\David\Sist_ComunicacionTDA\WB_Comunicado\WB_DocManual\Documento\" + _tienda;

            // string _path_Formularios = _ruta + "\\Formularios";

            // bool exists = System.IO.Directory.Exists(Server.MapPath(_ruta));

            // bool exists_form= System.IO.Directory.Exists(Server.MapPath(_path_Formularios));

            // if (!exists)
            //     System.IO.Directory.CreateDirectory(Server.MapPath(_ruta));

            // if (!exists_form)
            //     System.IO.Directory.CreateDirectory(Server.MapPath(_path_Formularios));

            // if (_tienda != null)
            // {
            //     //ASPxFileManager1.Settings.RootFolder = "~\\Documento\\" + _emp;
            //     ASPxFileManager1.Settings.RootFolder = _ruta;

            // }
            // else
            // {
            //     ASPxFileManager1.Visible = false;
            // }
            //}

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
    }
}