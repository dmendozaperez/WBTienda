using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace WB_ClienteBata
{
    // public class FileSystemData {
    //    public int? Id { get; set; }
    //    public int? ParentId { get; set; }
    //    public string Name { get; set; }
    //    public bool IsFolder { get; set; }
    //    public Byte[] Data { get; set; }
    //    public DateTime? LastWriteTime { get; set; }
    //}
    //public class MyProvider : PhysicalFileSystemProvider
    //{

    //    public MyProvider()
    //        : base("~\\Publicado\\Emcomer")
    //    { }

    //    public override IEnumerable<FileManagerFolder> GetFolders(FileManagerFolder parentFolder)
    //    {



    //        IEnumerable<FileManagerFolder> folders = base.GetFolders(parentFolder);

    //        return folders.Where(f => f.FullName.Contains("Invierno")).ToList().OrderByDescending(f =>f.LastWriteTime).ToArray();
    //    }
    //}
    public partial class Comunicado : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Session["Opcion"] = "Emcomer";

            string _emp = (string)Session["empresa"];// "Emcomer";//(string)Session["empresa"];//"Emcomer";//(string)Session["empresa"];//this.Request.Params["Opcion"].ToString() ;
            string _tda = (string)Session["tda"];//"142"; //(string)Session["tda"];//"540";// (string)Session["tda"];

            //string _emp = "Emcomer";// "Emcomer";//(string)Session["empresa"];//"Emcomer";//(string)Session["empresa"];//this.Request.Params["Opcion"].ToString() ;
            //string _tda = "283";

            if (_emp != null)
            {

                /*en este codigo vemos si pertenece a bg o wg*/
                string _cadena = "";
                Boolean _BG_WB = WB_ClienteBata.Bll.Users._existe_bg_bw(_tda,ref _cadena);

                if (_BG_WB)
                {
                    _emp = "Tiendas Especializadas";
                    ASPxFileManager1.Settings.RootFolder = "~\\Publicado\\" + _emp;

                    if (_cadena=="BG")
                    {
                        ASPxFileManager1.SettingsPermissions.AccessRules.Add(new FileManagerFolderAccessRule("WB", Rights.Deny));
                        ASPxFileManager1.SettingsPermissions.Role = "Administrator";
                    }
                    else
                    {
                        ASPxFileManager1.SettingsPermissions.AccessRules.Add(new FileManagerFolderAccessRule("BG", Rights.Deny));
                        ASPxFileManager1.SettingsPermissions.Role = "Administrator";
                    }



                }
                else
                {

                    //ASPxFileManager1.CustomFileSystemProvider = new MyProvider();                
                    ASPxFileManager1.Settings.RootFolder = "~\\Publicado\\" + _emp;
                    if (_emp.ToUpper() == "Emcomer".ToUpper())
                    {
                        string _estado_acceso = WB_ClienteBata.Bll.Users._estado_acceso(_tda);
                        //ASPxFileManager1.SettingsPermissions.Role = "";

                        if (_estado_acceso == "0")
                        {
                            ASPxFileManager1.SettingsPermissions.AccessRules.Add(new FileManagerFolderAccessRule("Tiendas Calientes", Rights.Deny));
                            ASPxFileManager1.SettingsPermissions.Role = "Administrator";
                        }
                        else
                        {
                            ASPxFileManager1.SettingsPermissions.AccessRules.Add(new FileManagerFolderAccessRule("Tiendas Frias", Rights.Deny));
                            ASPxFileManager1.SettingsPermissions.Role = "Administrator";
                        }
                    }
                }
                //FileManagerFolder[] FileManagerFolders;
                //FileManagerFolders = ASPxFileManager1.SelectedFolder.GetFolders();

                //PhysicalFileSystemProvider get=new PhysicalFileSystemProvider("~\\Publicado\\Emcomer");

                //IEnumerable<FileManagerFolder> folders = get.GetFolders(FileManagerFolders[0]);

                //folders.Where(f => !f.FullName.Contains("Invierno"));

                //FileManagerFolders= folders[0];



                //FileManagerFolder FileFolder;// As DevExpress.Web.ASPxFileManager.FileManagerFolder

                //IEnumerable<FileManagerFolder> folders = PhysicalFileSystemProvider.GetFolders(ASPxFileManager1);

                //folders.Where(f => !f.FullName.Contains("Invierno"));

                //PhysicalFileSystemProvider p;


                //var file = new FileManagerFile(this, new FileManagerFolder(this, "Oxford Innovation", "oxin1"), "My file");

                //IEnumerable<FileManagerFolder> folders = PhysicalFileSystemProvider.GetFolders(FileManagerFolder.); 
                //ASPxFileManager1.JSProperties["cpLabelText"] = "The " + ASPxFileManager1.SelectedFolder.Name + " folder contains " + ItemsNumberToString(FolderCount, "folder") + " and " + ItemsNumberToString(FileCount, "file");

                //IEnumerable<FileManagerFolder> folders = PhysicalFileSystemProvider.GetFolders(parentFolder);
                //IEnumerable<FileManagerFolder> folders = ASPxFileManager1 .GetFolders(parentFolder);

                //ASPxFileManager1.Settings.RootFolder = "~\\Publicado\\";// + _emp;

                //ASPxFileManager1.Settings.RootFolder = "~\\Publicado\\" + "Especial : " + "~\\Publicado\\" + _emp;
            }
            else
            {
                ASPxFileManager1.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            //// Session["Opcion"] = "Emcomer";

            ////string _emp = (string)Session["empresa"];// "Emcomer";//(string)Session["empresa"];//"Emcomer";//(string)Session["empresa"];//this.Request.Params["Opcion"].ToString() ;
            ////string _tda = (string)Session["tda"];//"142"; //(string)Session["tda"];//"540";// (string)Session["tda"];

            //string _emp = "Emcomer";// "Emcomer";//(string)Session["empresa"];//"Emcomer";//(string)Session["empresa"];//this.Request.Params["Opcion"].ToString() ;
            //string _tda = "123";

            //if (_emp != null)
            //{

            //    /*en este codigo vemos si pertenece a bg o wg*/

            //    Boolean _BG_WB = WB_ClienteBata.Bll.Users._existe_bg_bw(_tda);

            //    if (_BG_WB)
            //    {
            //        _emp = "Tiendas Especializadas";
            //        ASPxFileManager1.Settings.RootFolder = "~\\Publicado\\" + _emp;
            //    }
            //    else
            //    { 

            //        //ASPxFileManager1.CustomFileSystemProvider = new MyProvider();                
            //        ASPxFileManager1.Settings.RootFolder = "~\\Publicado\\" + _emp;
            //        if (_emp.ToUpper()== "Emcomer".ToUpper())
            //        {
            //            string _estado_acceso = WB_ClienteBata.Bll.Users._estado_acceso(_tda);
            //            //ASPxFileManager1.SettingsPermissions.Role = "";

            //              if (_estado_acceso=="0")
            //                {
            //                ASPxFileManager1.SettingsPermissions.AccessRules.Add(new FileManagerFolderAccessRule("Tiendas Calientes", Rights.Deny));
            //                ASPxFileManager1.SettingsPermissions.Role = "Administrator";
            //            }
            //                else
            //                {
            //                ASPxFileManager1.SettingsPermissions.AccessRules.Add(new FileManagerFolderAccessRule("Tiendas Frias", Rights.Deny));
            //                ASPxFileManager1.SettingsPermissions.Role = "Administrator";
            //                }
            //        }
            //    }
            //    //FileManagerFolder[] FileManagerFolders;
            //    //FileManagerFolders = ASPxFileManager1.SelectedFolder.GetFolders();

            //    //PhysicalFileSystemProvider get=new PhysicalFileSystemProvider("~\\Publicado\\Emcomer");

            //    //IEnumerable<FileManagerFolder> folders = get.GetFolders(FileManagerFolders[0]);

            //    //folders.Where(f => !f.FullName.Contains("Invierno"));

            //    //FileManagerFolders= folders[0];



            //    //FileManagerFolder FileFolder;// As DevExpress.Web.ASPxFileManager.FileManagerFolder

            //    //IEnumerable<FileManagerFolder> folders = PhysicalFileSystemProvider.GetFolders(ASPxFileManager1);

            //    //folders.Where(f => !f.FullName.Contains("Invierno"));

            //    //PhysicalFileSystemProvider p;


            //    //var file = new FileManagerFile(this, new FileManagerFolder(this, "Oxford Innovation", "oxin1"), "My file");

            //    //IEnumerable<FileManagerFolder> folders = PhysicalFileSystemProvider.GetFolders(FileManagerFolder.); 
            //    //ASPxFileManager1.JSProperties["cpLabelText"] = "The " + ASPxFileManager1.SelectedFolder.Name + " folder contains " + ItemsNumberToString(FolderCount, "folder") + " and " + ItemsNumberToString(FileCount, "file");

            //    //IEnumerable<FileManagerFolder> folders = PhysicalFileSystemProvider.GetFolders(parentFolder);
            //    //IEnumerable<FileManagerFolder> folders = ASPxFileManager1 .GetFolders(parentFolder);

            //    //ASPxFileManager1.Settings.RootFolder = "~\\Publicado\\";// + _emp;

            //    //ASPxFileManager1.Settings.RootFolder = "~\\Publicado\\" + "Especial : " + "~\\Publicado\\" + _emp;
            //}
            //else
            //{
            //    ASPxFileManager1.Visible = false;
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