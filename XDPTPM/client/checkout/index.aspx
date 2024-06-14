<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="XDPTPM.client.checkout.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Thanh Toán</title>
    <link rel="stylesheet" href="./assets/css/main.css">
    <link rel="stylesheet" href="./assets/css/res.css">
</head>
<body>
    <div class="container">
        <div class="payment-method">
            <div class="transfer-form">
                <h2>Chuyển khoản qua mã QR</h2>
                <div class="qr-code">
                    <img id="img_qr_bank" runat="server" src="./assets/img/image.jpg" alt="QR Code">
                </div>
                <div class="btn-coppy">
                    <p>Quét mã QR để chuyển khoản.</p>

                </div>
            </div>
        </div>

        <div class="payment-method">
            <h2>Thanh Toán Thủ Công</h2>
            <form>
                <div class="form-group">
                    <div class="input-group">
                    </div>
                </div>
                <div class="form-group">
                    <label for="amount">Ngân hàng:</label>
                    <div class="input-group">
                        <input type="text" readonly id="bank" runat="server" name="id" required>
                        <button type="button" class="copy-btn" onclick="copyText('bank')">Sao chép</button>
                    </div>
                </div>
                <div class="form-group">
                    <label for="name">Chủ tài khoản:</label>
                    <div class="input-group">
                        <input type="text" readonly id="nameAccount" runat="server" name="name" required>
                        <button type="button" class="copy-btn" onclick="copyText('nameAccount')">Sao chép</button>
                    </div>
                </div>

                <div class="form-group">
                    <label for="account-number">Số tài khoản:</label>
                    <div class="input-group">
                        <input type="text" readonly id="account_number" runat="server" name="account-number" required>
                        <button type="button" class="copy-btn" onclick="copyText('account_number')">Sao chép</button>
                    </div>
                </div>

                <div class="form-group">
                    <label for="bank">Nội dung:</label>
                    <div class="input-group">
                        <input type="text" readonly id="bank_content" runat="server" name="bank" required>
                        <button type="button" class="copy-btn" onclick="copyText('bank_content')">Sao chép</button>
                    </div>
                </div>

                <div class="form-group">
                    <label for="amount">Số tiền:</label>
                    <div class="input-group">
                        <input type="text" id="amount" runat="server" name="amount" required>
                        <button type="button" class="copy-btn" onclick="copyText('amount')">Sao chép</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">
        var isPaymentProcessed = false;

        $(document).ready(function () {
            setInterval(function () {
                if (!isPaymentProcessed) {
                    $.ajax({
                        type: "POST",
                        url: "index.aspx/checkPayment",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {

                            var statusPayment = response.d;

                            if (!statusPayment) return;

                            sPaymentProcessed = true;

                            Swal.fire(
                                'Payment Successful',
                                '',
                                'success'
                            ).then((result) => {
                                if (result.isConfirmed) {
                                    window.location.href = '../thanks/index.aspx';
                                }
                            });

                            setTimeout(function () {
                                window.location.href = '../thanks/index.aspx';
                            }, 5000);
                        },
                        failure: function (response) {
                            alert("error");
                        }
                    });
                }
            }, 8000);
        });

    </script>
    <script>
        function copyText(id) {
            var copyText = document.getElementById(id);
            copyText.select();
            copyText.setSelectionRange(0, 99999);
            document.execCommand("copy");
            alert("Đã sao chép: " + copyText.value);
        }
    </script>
</body>
</html>
