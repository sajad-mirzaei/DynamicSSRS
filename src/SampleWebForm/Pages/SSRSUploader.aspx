<%@ Page Title="SSRS Uploader" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SSRSUploader.aspx.cs" Inherits="SSRSUploader" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="upForm" RenderMode="block" UpdateMode="Always">
        <ContentTemplate>
            <div class="container mt-5">
                <h2 class="text-center mb-4">SSRS RDL Uploader</h2>
                <!-- Folder Path -->
                <div class="form-group col-12 mb-3">
                    <label for="RdlFolderPath">Server Folder Path. (Example: "/Reports" enter "/" for root of your server)</label>
                    <asp:TextBox runat="server" ID="ReportsFolderPath" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter RDL Sever Folder Path" required></asp:TextBox>
                </div>

                <div class="form-group col-12 mb-3">
                    <label for="RdlFolderPath">RDL Folder Path. (Example: "C:\Reports")</label>
                    <asp:TextBox runat="server" ID="RdlFolderPath" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter RDL Folder Path" required></asp:TextBox>
                </div>

                <hr style="border: 1px solid">

                <!-- Server Information -->
                <div class="form-group col-12 mb-3">
                    <label for="ServerUrl">Server URL. (Example: http://myserver/ReportServer/ReportService2010.asmx) </label>
                    <asp:TextBox runat="server" ID="ServerUrl" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter Server URL" required></asp:TextBox>
                </div>
                <div class="form-group col-12 mb-3">
                    <label for="ServerUsername">Server Username (For Server Authentication)</label>
                    <asp:TextBox runat="server" ID="ServerUsername" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter Server Username" required></asp:TextBox>
                </div>
                <div class="form-group col-12 mb-3">
                    <label for="ServerPassword">Server Password. (For Server Authentication)</label>
                    <asp:TextBox runat="server" ID="ServerPassword" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter Server Password" required></asp:TextBox>
                </div>

                <hr style="border: 1px solid">

                <div class="form-group col-12 mb-3">
                    <label for="DataSourceUsername">Data Source Username. (For Data Source Authentication)</label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="DataSourceUsername" placeholder="Enter Data Source Username" required></asp:TextBox>
                </div>
                <div class="form-group col-12 mb-3">
                    <label for="DataSourcePassword">Data Source Password. (For Data Source Authentication)</label>
                    <asp:TextBox runat="server" type="password" class="form-control" ID="DataSourcePassword" placeholder="Enter Data Source Password" required></asp:TextBox>
                </div>

                <hr style="border: 1px solid">
                <asp:LinkButton ID="SubmitForm" runat="server" OnClick="SubmitForm_Click" class="btn btn-primary btn-block">Submit Form</asp:LinkButton>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
