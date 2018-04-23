<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxGridView ID="Grid" runat="server" OnHtmlDataCellPrepared="Grid_HtmlDataCellPrepared" OnHtmlRowPrepared="Grid_HtmlRowPrepared">
        </dx:ASPxGridView>
    
    </div>
    </form>
</body>
</html>
