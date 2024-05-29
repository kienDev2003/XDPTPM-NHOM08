<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderDetails.aspx.cs" Inherits="XDPTPM.bep.orderDetails" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="./assets/css/reset.css" />
    <link rel="stylesheet" href="./assets/css/global.css" />
    <link rel="stylesheet" href="./assets/css/orderDetails.css" />
    <title>Chi tiết đơn</title>
</head>
<body>
    <form runat="server" id="form">
        <div class="container">
            <header class="header">
                <h1 class="tittle" id="nameTable" runat="server">Chi tiết đơn bàn 1</h1>
            </header>
            <main class="main">
                <table>
                    <thead>
                        <th>Name</th>
                        <th>Quantity</th>
                        <th>Unit</th>
                        <th>Completion time</th>
                    </thead>
                    <tbody id="content_table" runat="server">
                    </tbody>
                </table>
                <center>
                    <input id="btnSuccess" type="button" value="Hoàn Thành Đơn" runat="server" onserverclick="btnSuccess_ServerClick" />
                </center>
            </main>
            <footer class="footer">
                <p>Copyright © 2023 | By Nhóm 8 | Hotline: 098.****.***</p>
            </footer>
        </div>
    </form>
</body>
</html>

