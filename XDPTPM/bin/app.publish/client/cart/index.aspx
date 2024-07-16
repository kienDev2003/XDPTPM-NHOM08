<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="XDPTPM.client.cart.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Giỏ Hàng</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">
    <style>
        body {
            margin-top: 20px;
            background: #eee;
        }

        .ui-w-40 {
            width: 40px !important;
            height: auto;
        }

        .card {
            box-shadow: 0 1px 15px 1px rgba(52,40,104,.08);
        }

        .ui-product-color {
            display: inline-block;
            overflow: hidden;
            margin: .144em;
            width: .875rem;
            height: .875rem;
            border-radius: 10rem;
            -webkit-box-shadow: 0 0 0 1px rgba(0,0,0,0.15) inset;
            box-shadow: 0 0 0 1px rgba(0,0,0,0.15) inset;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <div class="container px-3 my-5 clearfix">
        <!-- Shopping cart table -->
        <div class="card">
            <div class="card-header">
                <h2>Giỏ Hàng</h2>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-bordered m-0">
                        <thead>
                            <tr>
                                <!-- Set columns width -->
                                <th class="text-center py-3 px-4" style="min-width: 400px;">Product Name</th>
                                <th class="text-right py-3 px-4" style="width: 100px;">Price</th>
                                <th class="text-center py-3 px-4" style="width: 120px;">Quantity</th>
                                <th class="text-right py-3 px-4" style="width: 100px;">Total</th>
                                <th class="text-center align-middle py-3 px-0" style="width: 40px;"><a href="#" class="shop-tooltip float-none text-light" title="" data-original-title="Clear cart"><i class="ino ion-md-trash"></i></a></th>
                            </tr>
                        </thead>
                        <tbody id="table_Content" runat="server">
                        </tbody>
                    </table>
                </div>
                <!-- / Shopping cart table -->

                <div class="d-flex flex-wrap justify-content-between align-items-center pb-4">
                    <div class="d-flex">
                        <div class="text-right mt-4">
                            <label class="text-muted font-weight-normal m-0">Total price</label>
                            <div class="text-large"><strong id="txtTotalPrice" runat="server"></strong></div>
                        </div>
                    </div>
                </div>
                <form runat="server" id="form1">
                    <div class="float-right">
                        <button type="button" id="btn_checkout" runat="server" onserverclick="btn_checkout_ServerClick" class="btn btn-lg btn-primary mt-2">Check out</button>
                    </div>
                </form>


            </div>
        </div>
    </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
        $(document).on('change', 'input[type="number"]', function () {

            var productId = $(this).attr('id');
            var quantity = $(this).val();

            $.ajax({
                type: "POST",
                url: "index.aspx/changeQuantityDish",
                data: JSON.stringify({ productID: productId, quantityNew: quantity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);

                    $("#table_Content").html(result.ContentHtml);

                    $("#txtTotalPrice").text(result.TotalAll);
                },
                error: function (response) {
                    alert("error");
                }
            });
        });

        $(document).on('click', 'input[type="button"]', function () {

            var productId = $(this).attr('id');

            $.ajax({
                type: "POST",
                url: "index.aspx/deleteDish",
                data: JSON.stringify({ productID: productId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    location.reload();
                },
                error: function (response) {
                    alert("error");
                }
            });
        });
    </script>
</body>
</html>
