<%@ Page Title="" Language="C#" MasterPageFile="~/admin/quanly/_layout/_layout.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="XDPTPM.admin.quanly.view.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="../assets/css/report.css">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <form id="form1" runat="server">
        <div class="header">
            <div>
                <span>Từ ngày: </span>
                <input id="lastDate" runat="server" class="date" type="date">
            </div>
            <div>
                <span>Đến ngày: </span>
                <input id="date" runat="server" class="date" type="date">
            </div>
            <div>
                <button id="btnSreach" runat="server" onserverclick="btnSreach_ServerClick" class="search">Tìm kiếm</button>
            </div>
        </div>
        <br />
        <br />

        <div class="content" style="text-align: center">
            <asp:GridView ID="gvReport" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="10">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Bàn">
                        <ItemTemplate>
                            <%# Eval("TableID") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Thời gian đặt">
                        <ItemTemplate>
                            <%# Eval("OrderTime") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Số tiền">
                        <ItemTemplate>
                            <%# Eval("AmountOfMoney") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
        <p runat="server" style="color:green;margin-top:20px;margin-right:20%;float:right;font-size:20px" id="txtAllMoney">Doanh thu</p>
    </form>
    <br />
    <br />
    
</asp:Content>
