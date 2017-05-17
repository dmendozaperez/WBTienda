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
           // string _emp = "Emcomer";// this.Request.Params["empresa"].ToString() ;
            string _emp = this.Request.Params["empresa"].ToString() ;
            string _tda= this.Request.Params["tda"];
            Session["empresa"] = _emp;
            Session["tda"] = _tda;
            if (_emp.Length > 0)
            {
                Response.Redirect("Comunicado.aspx");
            }

        }
    }
}