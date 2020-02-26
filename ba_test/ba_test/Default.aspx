<%@ Page Title="Главная" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>bizApps test #2</h1>
    <div class="row" style="color: grey;">
        Тестовых записей сервис предоставляет только 12. Учитывайте при выборе параметров
    </div>
    <hr/ />
    <div class="row">
        <table id="conf">
            <tbody>
                <tr>
                    <th style="width: 300px">Кол-во элементов на странице</th>
                    <th style="width: 300px">№ страницы</th>
                    <th></th>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="edParam1" name="edParam1" runat="server" CssClass="form-control" type="number" min="1" step="1" placeholder="Укажите кол-во элементов" />
                    </td>
                    <td>
                        <asp:TextBox ID="edParam2" runat="server" CssClass="form-control" type="number" min="1" step="1" placeholder="Укажите № страницы" />
                    </td>
                    <td>
                        <asp:Button ID="load" runat="server" Text="Загрузить" CssClass="btn" OnClick="load_Click" />
                        <asp:Button ID="save" runat="server" Text="Сохранить" CssClass="btn" Enabled="false" OnClick="save_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="row" style="padding-left: 15px;">
        <asp:CustomValidator runat="server" ID="MyValidator" Display="None" EnableClientScript="false" />
        <asp:ValidationSummary ID="ErrorSummary" runat="server" HeaderText="Ошибки валидации" CssClass="validationError" />
    </div>

    <hr/ />

    <div class="row">
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="selected" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Id">
                    <ItemTemplate>
                        <asp:Label ID="labelID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                        <asp:Label ID="labelfirstname" runat="server" Text='<%# Bind("first_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <asp:Label ID="labellastname" runat="server" Text='<%# Bind("last_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User email">
                    <ItemTemplate>
                        <asp:Label ID="labelemail" runat="server" Text='<%# Bind("email") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Avatar">
                    <ItemTemplate>
                        <asp:Label ID="labelavatar" runat="server" Text='<%# Bind("avatar") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>

</asp:Content>
