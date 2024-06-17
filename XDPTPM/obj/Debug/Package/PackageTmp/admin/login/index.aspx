<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="XDPTPM.admin.login.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="./assets/css/reset.css" />
    <link rel="stylesheet" href="./assets/css/main.css" />
    <title>Login Administrator</title>
</head>
<body>
    <div id="login">
        <form runat="server">
            <h1>Sign In</h1>
            <input id="txtUsername" runat="server" type="text" placeholder="Username" />
            <input id="txtPassword" runat="server" type="password" placeholder="Password" />
            <button runat="server" id="btn_login" onserverclick="btn_login_ServerClick">Sign in</button>
        </form>
    </div>
</body>
</html>

