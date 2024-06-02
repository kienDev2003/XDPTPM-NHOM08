<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="XDPTPM.thucdon.index" %>


<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="./assets/css/style.css" type="text/css" />
    <link rel="stylesheet" href="./assect/css/all.css" type="text/css" />
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <title>Thực Đơn</title>
    <style>
        .thongbao {
            margin: 5px;
            padding: 5px 0;
            background-color: #333333;
            line-height: 1.5em;
            font-size: 13px;
            border-radius: 2px;
        }

        @media only screen and (min-width: 960px) {
            .quangcaomobile {
                display: none;
            }
        }

        .mobi-pl {
            display: block;
        }

        @media screen and (min-width: 780px) {
            .mobi-pl {
                display: none;
            }
        }

        @media only screen and (max-width: 959px) {
            .quangcaopc {
                display: none;
            }
        }

        .navbar-brand {
            margin-right: 0 !important;
            font-size: 1.5rem !important;
        }

        .text-danger {
            color: #33ccff !important;
        }

        #btnNext {
            padding: 10px 20px;
            background-color: #15d8da;
            color: black;
            border: none;
            cursor: pointer;
            margin-left: 5px;
            margin-top: 3px;
        }

            #btnNext:hover {
                cursor: pointer;
            }
    </style>
</head>

<body>
    <form runat="server">
        <header>
            <div class="menu1">
                <div class="logo">
                    <a class="navbar-brand" href="">
                        <span class="text-danger">Thực Đơn</span>
                    </a>
                </div>
                <div class="button-menu">
                    <span><i></i><i></i><i></i></span>
                </div>
                <nav class="list-menu">
                    <ul>
                        <li><a href="">Cơm</a></li>
                        <li><a href="">Bún</a></li>
                        <li><a href="">Bánh</a></li>
                        <li><a href="">Nhậu</a></li>
                        <li><a href="">Gỏi</a></li>
                        <li><a href="">Đồ Cuốn</a></li>
                        <li><a href="">Lẩu - Nướng</a></li>
                        <li><a href="">Lẩu - Nướng</a></li>
                    </ul>
                </nav>
                <div class="button-search"></div>
                <div class="form-search">
                    <input
                        type="text"
                        id="txt_search"
                        autocomplete="off"
                        placeholder="Tìm kiếm món ăn" />
                    <button id="btn_search" type="submit"></button>
                </div>
            </div>
        </header>
        <main>
            <div class="body row">
                <div class="tab-dish">
                    <h1 id="title_dish" class="title-dish">Món Ăn Nổi Bật</h1>
                    <ul id="list_dish" runat="server" class="list-dish">
                    </ul>
                </div>
            </div>
        </main>

        <div class="cart-icon ">
            <a href="../cart/index.aspx">

                <i class='cart-icon-i bx bx-cart'></i>
                <span id="cart_notice" class="cart-notice">0</span>

            </a>
        </div>

        <script type="text/javascript" src="./assets/js/jQuery.js"></script>
        <script type="text/javascript" src="./assets/js/main.js"></script>
        <script>
            $(document).on('click', 'input[type="button"]', function () {
                var buttonId = $(this).attr('id');

                $.ajax({
                    type: "POST",
                    url: "index.aspx/addDishToOrder",
                    data: JSON.stringify({ ProductID: buttonId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (respone) {
                        var cart_notice = document.getElementById("cart_notice");
                        cart_notice.innerText = respone.d;
                        alert("Đã thêm món ăn");
                    },
                    error: function (xhr, status, error) {
                        console.error(xhr.responseText);
                    }
                });
            });
        </script>
    </form>
</body>
</html>
