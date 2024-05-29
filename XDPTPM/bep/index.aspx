<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="XDPTPM.bep.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width='device-width', initial-scale=1.0" />
    <link rel="stylesheet" href="./assets/css/reset.css" />
    <link rel="stylesheet" href="./assets/css/global.css" />
    <link rel="stylesheet" href="./assets/css/main.css" />
    <title>Khu Vực Bếp</title>
</head>
<body>
    <div class="container">
        <header class="header">
            <h1 class="tittle">Khu Vực Bếp</h1>
        </header>
        <main class="main">
            <ul class="list-item" id="list_order" runat="server">
            </ul>
        </main>
        <footer class="footer">
            <p>Copyright © 2023 | By Nhóm 8 | Hotline: 098.****.***</p>
        </footer>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setInterval(function () {
                $.ajax({
                    type: "POST",
                    url: "index.aspx/getListOrder",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var orderListHTML = response.d;

                        $("#list_order").html(orderListHTML);
                    },
                    failure: function (response) {
                        alert("error");
                    }
                });
            }, 3000);
        });

    </script>
</body>
</html>
