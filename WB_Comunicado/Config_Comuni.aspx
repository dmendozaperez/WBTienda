<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Config_Comuni.aspx.cs" Inherits="WB_ClienteBata.Configuracion_Comunicacion" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxFileManager ID="ASPxFileManager1" runat="server">
            <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
        </dx:ASPxFileManager>
    </div>
    </form>
</body>
</html>
