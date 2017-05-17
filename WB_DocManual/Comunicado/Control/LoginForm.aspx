﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="WB_ClienteBata.Control.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
  <%--   <script language="Javascript" type="text/javascript" src="http://api.easyjquery.com/easyjquery.js"></script>--%>
    <script src="../../Scripts/Js/logInJs.js" type="text/javascript"></script>
    Inicio de sesión en el sistema
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Ingrese el nombre de usuario y contraseña para iniciar sesión en el Sistema.&nbsp;
    El nombre del usuario y contraseña es responsabilidad del usuario.&nbsp; Recuerde que el sistema diferencia
    la contraseña entre minusculas y mayusculas.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                                    Iniciar sesión</h2>
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
                                                <asp:TextBox ID="UserName" runat="server" CssClass="campo1" Width="200px" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="El nombre de usuario es obligatorio." CssClass="error" ToolTip="El nombre de usuario es obligatorio."
                                                    ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password"><h3>Contraseña:</h3></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" CssClass="campo1" Width="200px" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="La contraseña es obligatoria." CssClass="error" ToolTip="La contraseña es obligatoria."
                                                    ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:CheckBox ID="RememberMe" runat="server" />
                                            </td>
                                            <td colspan="2" align="left">
                                                Recordármelo la próxima vez.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="color: Red;">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:HyperLink ID="hlinkrecovery" NavigateUrl="~/Aquarella/Control/recoveryPassword.aspx" runat="server" ForeColor="#0000CC">¿Ha olvidado la contraseña?</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Iniciar sesión"
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
