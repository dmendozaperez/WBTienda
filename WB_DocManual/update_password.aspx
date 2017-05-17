<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="update_password.aspx.cs" Inherits="WB_ClienteBata.update_password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
  <%--  <script language="Javascript" type="text/javascript" src="http://api.easyjquery.com/easyjquery.js"></script>
    <script src="../../Scripts/Js/logInJs.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .auto-style1 {
            width: 7px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Actualizar Password
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
   !Por seguridad, debe de cambiar la contraseña predeterminado para poder acceder al sistema 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <asp:UpdatePanel ID="upPanelMsg" runat="server">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <center>
                <div style="position: absolute; left: 0; background: #f5f5f5; filter: alpha(opacity=85);
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 100%; font-size: medium;">
                    <img src="Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
                    Cargando información...
                </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <table width="100%" cellpadding="4" cellspacing="4">
        <tr>
            <td align="center">     
                 <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
                   OnAuthenticate="LoginUser_Authenticate">
                    <LayoutTemplate> 
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
                                            <td align="left" valign="top">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"><h3>Nombre de usuario:</h3></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server" ReadOnly="true" CssClass="campo1" Width="200px" MaxLength="200" BackColor="Khaki" Font-Size="10" Font-Bold="true"></asp:TextBox>
                                            </td>
                                            <td class="auto-style1">
                                                
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
                                                &nbsp;</td>
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
                                                    ValidationGroup="Login1"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                         </LayoutTemplate>
                </asp:Login>
                </td>
    </tr>
    </table>
</asp:Content>
