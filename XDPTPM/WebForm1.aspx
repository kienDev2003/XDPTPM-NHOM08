<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="XDPTPM.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="txtPrice" type="text" placeholder="Giá thanh toán" runat="server" />
            <input id="txtName" type="text" placeholder="Tên hóa đơn" runat="server" />
            <input id="txtIdBank" type="text" placeholder="Mã ngân hàng" runat="server" />
            <input id="txtSTK" type="text" placeholder="Số tài khoản" runat="server" />
            <input id="txtContentBank" type="text" placeholder="Nội dung chuyển khoản" runat="server" />
            <input id="txtNameCTK" type="text" placeholder="Tên chủ tài khoản" runat="server" />
            <input id="btnClick" type="button" value="Click-Me" onserverclick="Unnamed_ServerClick" runat="server"/>
        </div>
        <div>
            <img id="bank" runat="server"/>
        </div>
    </form>
</body>
</html>
