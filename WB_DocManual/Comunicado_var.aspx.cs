using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WB_ClienteBata
{
    public partial class Comunicado_var : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string _tienda = "50104";// this.Request.Params["empresa"].ToString() ;
            string _tienda = this.Request.Params["tienda"].ToString();
            Session["tienda"] = _tienda;

            if (_tienda.Length > 0)
            {
                Response.Redirect("Comunicado.aspx");
            }

        }
    }
}