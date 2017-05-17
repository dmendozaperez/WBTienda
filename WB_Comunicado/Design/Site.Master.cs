using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WB_ClienteBata.Bll;
namespace WB_ClienteBata.Design
{
    public partial class Site : System.Web.UI.MasterPage
    {
        string _menu;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
            if (!IsPostBack)
            {
                Users user = (Users)Session[Constants.NameSessionUser];
                _menu = (string)(Session["Menu"]) != null ? (string)(Session["Menu"]) : "";

                if ( user==null)
                {
                    this.HeadLoginView.Visible = false;
                }

                if (user!=null)
                {
                    lblusuario.Text = user._nombre;
                    lblnivel.Text = user._usu_nivel;
                    LoadMenu();
                }
                if (!_menu.Equals(string.Empty))
                 {
                     lblusuario.Text = user._nombre;
                     lblnivel.Text = user._usu_nivel;
                     LoadMenu();
                 }
            }
            else
            {
                Users user = (Users)Session[Constants.NameSessionUser];
                _menu = (string)(Session["Menu"]) != null ? (string)(Session["Menu"]) : "";
                if (user != null)
                {
                    lblusuario.Text = user._nombre;
                    lblnivel.Text = user._usu_nivel;
                    LoadMenu();
                }
                if (!_menu.Equals(string.Empty))
                {
                    lblusuario.Text = user._nombre;
                    lblnivel.Text = user._usu_nivel;
                    LoadMenu();
                }
            }

        }
        protected void HeadLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            
            ///
            Session.Clear();
            ///
            Session.Abandon();
        }
        //protected void AddChildItem(ref MenuItem miMenuItem, string _padre, ref Syrinx2.MenuItem mimenu_2)
        //{
        //    //List<ApplicationFunctions> colappfunctions = new List<ApplicationFunctions>();
        //    //colappfunctions = (List<ApplicationFunctions>)Session["_MENU"];
        //    //foreach (ApplicationFunctions app in colappfunctions)
        //    //{
        //    //    if (app._idpadre.ToString().Equals(_padre) && !app._id.ToString().Equals(app._idpadre.ToString()))
        //    //    {
        //    //        MenuItem miMenuItemChild = new MenuItem(app._nombre, app._id.ToString(), "", app._url, "");

        //    //        Syrinx2.MenuItem miMenuItemChild2 = null;// new Syrinx2.MenuItem(app._nombre, app._url);
        //    //        //MenuItem miMenuItemChild = new MenuItem(app._nombre, app._id.ToString());    
        //    //        if (app._verifica_submenu)
        //    //        {
        //    //            miMenuItemChild2 = new Syrinx2.MenuItem();
        //    //            miMenuItemChild.Selectable = false;
        //    //            miMenuItemChild2.Text = app._nombre;
                        
        //    //        }
        //    //        else
        //    //        {
        //    //            miMenuItemChild2 = new Syrinx2.MenuItem(app._nombre, app._url);
        //    //        }
        //    //        miMenuItem.ChildItems.Add(miMenuItemChild);
        //    //        mimenu_2.Items.Add(miMenuItemChild2);
        //    //        AddChildItem(ref miMenuItemChild, app._id.ToString(),ref miMenuItemChild2);
        //    //    }
        //    //}
        //}
        private void LoadMenu()
        {
            //List<ApplicationFunctions> colappfunctions = new List<ApplicationFunctions>();
            //colappfunctions = (List<ApplicationFunctions>)Session["_MENU"];
            //foreach (ApplicationFunctions app in colappfunctions)
            //{

            //    //esta condicion indica q son elementos padre.
            //    if (app._id == app._idpadre)
            //    {
            //        //menu2.Visible = true;
            //        //vamos a ver si es padre y creamos el menu
            //        MenuItem miMenuItem = new MenuItem(app._nombre,app._id.ToString(),"",app._url,"");
            //        miMenuItem.Selectable = false;
            //        Syrinx2.MenuItem mimenu_2 = new Syrinx2.MenuItem();
            //        mimenu_2.Text = app._nombre;
            //        //this.menu2.Items.Add(mimenu_2);
            //        this.MenuPrin.Items.Add(miMenuItem);
            //        AddChildItem(ref miMenuItem, app._idpadre.ToString(), ref mimenu_2);
                    


                  
            //    }
            //}
        }
        //public bool AddMenuItem(ref TreeNode mnuMenuItem, List<ApplicationFunctions> colappfunctions)
        //{
        //    bool bChilds = false;
        //    string[] strValue = mnuMenuItem.Value.Split('¬');
        //    //recorremos cada elemento del datatable para poder determinar cuales son elementos hijos
        //    //del menuitem dado pasado como parametro 
        //    foreach (ApplicationFunctions app in colappfunctions)
        //    {
        //        //if (row[3].ToString().Equals(mnuMenuItem.Value) && !row[0].ToString().Equals(row[3].ToString()))
        //        if (app._idpadre.ToString().Equals(strValue[0]) && !app._id.ToString().Equals(app._idpadre.ToString()))
        //        {
        //            bChilds = true;
        //            TreeNode mnuNewMenuItem = new TreeNode();
        //            mnuNewMenuItem.Value = app._id.ToString() + "¬" + app._url;
        //            mnuNewMenuItem.Text = " " + app._nombre;
        //            //mnuNewMenuItem.ImageUrl = row[4].ToString();                    
        //            //mnuNewMenuItem.NavigateUrl = app._url;
        //            //mnuNewMenuItem.Target = "_self";                      
        //            //llamada recursiva para ver si el nuevo menú ítem aun tiene elementos hijos.
        //            bool bSubChilds = this.AddMenuItem(ref mnuNewMenuItem, colappfunctions);

        //            //Agregamos el Nuevo MenuItem al MenuItem que viene de un nivel superior.
        //            if (bSubChilds || !app._url.Equals(""))
        //                mnuMenuItem.ChildNodes.Add(mnuNewMenuItem);
        //        }
        //    }

        //    return bChilds;
        //}

         
    }
}