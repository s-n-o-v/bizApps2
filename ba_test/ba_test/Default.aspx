<%@ Page Title="Главная" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="bizApps._Default" %>

<%@ Register Assembly="CoreLibrary" Namespace="CoreLibrary" TagPrefix="ololo" %>

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
                        <asp:TextBox ID="edParam1" name="edParam1" runat="server" CssClass="form-control" type="number" min="1" step="1" placeholder="Укажите кол-во элементов" ValidationGroup="1" />
                    </td>
                    <td>
                        <asp:TextBox ID="edParam2" runat="server" CssClass="form-control" type="number" min="1" step="1" placeholder="Укажите № страницы" ValidationGroup="1" />
                    </td>
                    <td>
                        <asp:Button ID="load" runat="server" Text="Загрузить" CssClass="btn" OnClick="load_Click" ValidationGroup="1" />
                        <asp:Button ID="save" runat="server" Text="Сохранить" CssClass="btn" Enabled="false" OnClick="save_Click" ValidateRequestMode="Enabled" ValidationGroup="2" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="row" style="padding-left: 15px;">
        <asp:CustomValidator runat="server" ID="MyValidator" Display="None" 
            EnableClientScript="true" 
            OnServerValidate="MyValidator_ServerValidate"
            ClientValidationFunction="ClientValidate" ErrorMessage="Необходимо заполнить хотя бы одно поле" ValidationGroup="1" />
        <asp:CustomValidator runat="server" ID="selectedValidator" EnableClientScript="false" OnServerValidate="selectedValidator_ServerValidate" ValidationGroup="2" />
        <asp:ValidationSummary ID="ErrorSummary" runat="server" HeaderText="Ошибки валидации" CssClass="validationError" />
    </div>

    <hr/ />

    <div class="row">
        <ololo:MyGridView ID="mygv" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="selected" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="User Id" DataField="id" />
                <asp:BoundField HeaderText="First Name" DataField="first_name" />
                <asp:BoundField HeaderText="Last Name" DataField="last_name" />
                <asp:BoundField HeaderText="User email" DataField="email" />
                <asp:BoundField HeaderText="Le Avatar" DataField="avatar" />
            </Columns>
        </ololo:MyGridView>
    </div>

    <script type="text/javascript">
        $(function() {
            console.log( "ready!" );
        });

        function ClientValidate(source, arguments)
        {
            console.log('source', source);
            var isValid = false;

            $('.form-control').each(function (item) {
                if ($(this).val() !== '') {
                    isValid = true;
                }
                console.info('item ' + item, $( this ).val());
            });
            console.info('isValid', isValid);
            arguments.IsValid = isValid;
        }
    </script>
</asp:Content>
