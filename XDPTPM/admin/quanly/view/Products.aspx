<%@ Page Title="" Language="C#" MasterPageFile="~/admin/quanly/_layout/_layout.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="XDPTPM.admin.quanly.view.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
    <div class="container-fluid">
        <div class="row mb-2">
            <div class="col-sm-6">
                <h1>Danh sách món ăn</h1>
            </div>

        </div>
    </div><!-- /.container-fluid -->
</section>


<!-- Main content -->
<section class="content">

    <!-- Default box -->
    <div class="card">
        <link href="../assets/css/table.css" rel="stylesheet" type="text/css" />
        <div class="card-header">
            <h3 class="card-title">Danh sách</h3>

            <div class="card-tools">
                <asp:HyperLink ID="hlAddProduct" runat="server" CssClass="btn btn-primary" NavigateUrl="../view/EditProduct.aspx"> Thêm mới </asp:HyperLink>
            
            </div>

        </div>
        <div class="card-body">
            <form id="form1" runat="server">
                <div>
                    <div class="row mb-2" >
                        <div class="col-sm-6" >
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Hãy nhập tên sản phẩm..."></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click"  CssClass="btn btn-primary" />
                        </div>
                    </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" OnRowCommand="gvProducts_RowCommand" AllowPaging="True" PageSize="6" OnPageIndexChanging="GridView1_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="Name" HeaderText="Tên món ăn" />
                        <asp:BoundField DataField="UnitValue" HeaderText="Đơn vị tính" />
                        <asp:BoundField DataField="Price" HeaderText="Giá bán" DataFormatString="{0:N0}" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="Hình ảnh">
                            <ItemTemplate>
                                <img src='<%# Eval("ImagePath") %>' style="width:150px;height:100px;" alt="Không có hình ảnh" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng thái">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("Status")) ? "Còn món" : "Hết món" %>
                            </ItemTemplate>                         
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Loại">
                            <ItemTemplate>
                                <%# Eval("ClassifyValue") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                          
                        <asp:TemplateField HeaderText="Chức năng">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Sửa"  CommandName="EditProduct" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary" />
                            <asp:Button ID="btnDelete" runat="server" Text="Xóa" CommandName="DeleteProduct" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </form>
         </div>
    </div>
    <!-- /.card -->

</section>
</asp:Content>
