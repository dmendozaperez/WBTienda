<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WB_ClienteBata.Default" EnableViewState="false" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server" EnableViewState="False">
     <link media="screen" rel="stylesheet" href="Scripts/Colorbox/colorbox.css" />
      <script src="Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>      
      <script src="Scripts/Js/Evaluacion.js" type="text/javascript"></script>
    <style type="text/css">
        .ColumnaOculta {display:none;}
        .special.ui-state-default{
        background-color: #3173a5;
        background-image: none;
   }
    </style>  
    <script type="text/javascript">
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
            }
        }

        $(document).ready(function () {
            $(".iframe").colorbox({ width: "35%", height: "50%", iframe: true });
            $("#tabs").tabs({
                collapsible: true
            });
            $('#tabs').tabs('select', '#tab-1'); // Para seleccionar el tab 1 y que este colapsado el panel de insercion de rol 
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server" EnableViewState="False">   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server" EnableViewState="False">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" EnableViewState="False">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Cambiar Password</a></li>
        </ul>
        <div id="tabs-1">
            <table width="100%" cellpadding="4" cellspacing="4">
        <tr>
            <td align="left">     
                
                 
   <table cellpadding="4" cellspacing="4" style="border-collapse: collapse; border: 1px solid silver;">
                            <tr>
                                <td>
                                    <table cellpadding="4" cellspacing="4">
                                        <tr>
                                            <td colspan="3" align="left" style="color: White; background-color: #b42c2c; font-weight: bold; font-size: 9px;">
                                                <h2 style="font-size: 14px">
                                                    Actualizar Contraseña</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                            </td>
                                        </tr>                                       
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password"><h3>Nueva Contraseña:</h3></asp:Label>
                                            </td>
                                            <td>                                                  
                                                        <asp:TextBox ID="Password" runat="server" TabIndex="0" CssClass="campo1" Width="200px" TextMode="Password"></asp:TextBox>                                                  
                                            </td>
                                            <td class="auto-style1">
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="La contraseña es obligatoria." CssClass="error" ToolTip="La contraseña es obligatoria."
                                                    ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                             <td align="left">
                                                <asp:Label ID="Label1" runat="server" AssociatedControlID="NewPassword"><h3>Confirmar Contraseña:</h3></asp:Label>
                                            </td>
                                            <td>                                              
                                                <asp:TextBox ID="NewPassword" runat="server" CssClass="campo1" Width="200px" TextMode="Password"></asp:TextBox>                                                
                                            </td>
                                            <td class="auto-style1">
                                                <asp:RequiredFieldValidator ID="ConfirmationRequired" runat="server" ControlToValidate="NewPassword"
                                                    ErrorMessage="El ingreso de la contraseña es obligatorio." CssClass="error" ToolTip="La contraseña es obligatoria."
                                                    ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td colspan="2" align="left">                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="color: Red;">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">                                                                                                 
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Actualizar"
                                                    ValidationGroup="Login1" OnClick="LoginButton_Click"/>                                                 
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                         
               
                </td>
    </tr>
    </table>
        </div>
    </div>
     <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" CustomFileSystemProviderTypeName="DateTimeSortedFileSystemProvider" OnCustomThumbnail="ASPxFileManager1_CustomThumbnail" Theme="Office2010Blue" >            

              <ClientSideEvents SelectedFileOpened="function(s, e) {
	            e.file.Download();
	            e.processOnServer = false;
            }" />
            <Settings RootFolder="~\Publicado" AllowedFileExtensions=".doc,.docs,.pdf,.xls,.xlsx,.mp4" ThumbnailFolder="~\Thumb\" />
            <SettingsEditing AllowCreate="True" AllowDelete="True" AllowDownload="True" AllowMove="True" AllowRename="True" />
          <SettingsDataSource KeyFieldName="Id" ParentKeyFieldName="ParentID" NameFieldName="Name" IsFolderFieldName="IsFolder" 
            FileBinaryContentFieldName="Data" LastWriteTimeFieldName="LastWriteTime" />
        <SettingsFileList View="Details">
            <DetailsViewSettings AllowColumnResize="true" AllowColumnDragDrop="true" AllowColumnSort="true" ShowHeaderFilterButton="false" />
        </SettingsFileList>
        </dx:ASPxFileManager>
</asp:Content>
