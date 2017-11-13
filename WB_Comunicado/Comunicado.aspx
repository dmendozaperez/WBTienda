<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Comunicado.aspx.cs" Inherits="WB_ClienteBata.Comunicado" EnableViewState="false" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" CustomFileSystemProviderTypeName="DateTimeSortedFileSystemProvider"  OnCustomThumbnail="ASPxFileManager1_CustomThumbnail" Theme="Office2010Blue" SettingsLoadingPanel-Text="Cargando&hellip;sss">            
            <ClientSideEvents SelectedFileOpened="function(s, e) {
	            e.file.Download();
	            e.processOnServer = false;
            }" />
            <Settings RootFolder="" AllowedFileExtensions=".doc,.docs,.pdf,.xls,.xlsx,.mp4" ThumbnailFolder="~\Thumb\" />
            <SettingsEditing AllowDownload="True" />
            <SettingsUpload Enabled="False">
            </SettingsUpload>        
<SettingsLoadingPanel Text="Cargando…"></SettingsLoadingPanel>

          <SettingsDataSource KeyFieldName="Id" ParentKeyFieldName="ParentID" NameFieldName="Name" IsFolderFieldName="IsFolder" 
            FileBinaryContentFieldName="Data" LastWriteTimeFieldName="LastWriteTime" />
        <SettingsFileList View="Details">
            <DetailsViewSettings AllowColumnResize="true" AllowColumnDragDrop="true" AllowColumnSort="true" ShowHeaderFilterButton="false" />
        </SettingsFileList>
          <SettingsPermissions>
            <AccessRules>
                <dx:FileManagerFolderAccessRule Path="" Edit="Deny" />
                <dx:FileManagerFileAccessRule Path="*.xml" Edit="Deny" />
                <dx:FileManagerFolderAccessRule Path="Tiendas Calientes" Browse="Deny" />
                <dx:FileManagerFolderAccessRule Path="Tiendas Frias" Browse="Deny" />
                <dx:FileManagerFolderAccessRule Path="BG" Browse="Deny" />
                <dx:FileManagerFolderAccessRule Path="WB" Browse="Deny" />
                <dx:FileManagerFolderAccessRule Path="Documents" Role="DocumentManager" EditContents="Allow" />

                <dx:FileManagerFolderAccessRule Path="" Role="MediaModerator" Upload="Deny" />
                <dx:FileManagerFolderAccessRule Path="Music" Role="MediaModerator" EditContents="Allow" Upload="Allow" />
                <dx:FileManagerFolderAccessRule Path="Video" Role="MediaModerator" EditContents="Allow" Upload="Allow" />

                <dx:FileManagerFolderAccessRule Role="Administrator" Edit="Allow" Browse="Allow" />
            </AccessRules>
        </SettingsPermissions>
        </dx:ASPxFileManager>
</asp:Content>
