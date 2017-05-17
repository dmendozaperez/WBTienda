<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Comunicado.aspx.cs" Inherits="WB_ClienteBata.Comunicado" EnableViewState="false" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>       
    
      <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" CustomFileSystemProviderTypeName="DateTimeSortedFileSystemProvider" OnCustomThumbnail="ASPxFileManager1_CustomThumbnail" Theme="Office2010Blue" >            
            <Settings RootFolder="" AllowedFileExtensions=".doc,.docs,.pdf,.xls,.xlsx,.jpg,.png" ThumbnailFolder="~\Thumb\" />
            <SettingsEditing AllowDelete="True" AllowCreate="true" AllowDownload="True" AllowMove="True" AllowRename="True" />
          <SettingsDataSource KeyFieldName="Id" ParentKeyFieldName="ParentID" NameFieldName="Name" IsFolderFieldName="IsFolder" 
            FileBinaryContentFieldName="Data" LastWriteTimeFieldName="LastWriteTime" />
        <SettingsFileList View="Details">
            <DetailsViewSettings AllowColumnResize="true" AllowColumnDragDrop="true" AllowColumnSort="true" ShowHeaderFilterButton="false" />
        </SettingsFileList>
        </dx:ASPxFileManager>
</asp:Content>
