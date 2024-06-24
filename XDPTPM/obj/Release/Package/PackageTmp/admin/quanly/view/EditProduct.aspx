<%@ Page Title="" Language="C#" MasterPageFile="~/admin/quanly/_layout/_layout.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="XDPTPM.admin.quanly.view.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6" runat="server">
                    <h2><%# IsUpdate ? "Update Product" : "Add New Product" %></h2>
                </div>
            </div>
        </div>
    </section>
    <section class="content">
        <div class="card">
            <link href="../assets/css/EditProduct.css" rel="stylesheet" type="text/css" />
            <div class="card-header">
                <div class="form-horizontal">
                    <form id="form1" runat="server">
                        <div class="form-group">
                            <label class="control-label col-md-2" for="Name">Tên sản phẩm</label>
                            <asp:TextBox ID="txtProductName" runat="server" class="col-md-10"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2" for="UnitID">Đơn vị tính</label>
                            <asp:DropDownList ID="ddlUnit" runat="server" AppendDataBoundItems="true" class="col-md-10">
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2" for="Price">Giá bán</label>
                            <asp:TextBox ID="txtProductPrice" runat="server" CssClass="col-md-10" type="number"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2" for="CompletionTime">Thời gian hoàn thành</label>
                            <asp:TextBox ID="txtProductCompletionTime" runat="server" CssClass="col-md-10" type="number"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2" for="ImagePath">Hình ảnh</label><br />
                            <asp:FileUpload ID="fileUploadProductImage" runat="server" />
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2" for="Status">Trạng thái</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" class="col-md-10">
                                <asp:ListItem Text="-- Trạng thái --" Value="0" />
                                <asp:ListItem Text="Còn món" Value="true"></asp:ListItem>
                                <asp:ListItem Text="Đã hết" Value="false"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2" for="ClassifyID">Loại sản phẩm</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" class="col-md-10">
                            </asp:DropDownList>
                        </div>

                        <div>
                            <asp:Button ID="btnSave" runat="server" Text="Lưu" class="btn btn-primary" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-primary" OnClick="btnCancel_Click" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
