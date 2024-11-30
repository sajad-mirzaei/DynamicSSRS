<%@ Page Title="SSRS Uploader" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UploadReportsSharedDataSource.aspx.cs" Inherits="UploadReportsSharedDataSource" %>

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
                    <label for="NewDataSourceName">New DataSource Name. (For DataSource Authentication)</label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="NewDataSourceName" style="min-width: 100%;" placeholder="Enter DataSource Name" required></asp:TextBox>
                </div>
                <div class="form-group col-12 mb-3">
                    <label for="NewDataSourceConnectionString">New DataSource ConnectionString. (For DataSource Authentication)</label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="NewDataSourceConnectionString" style="min-width: 100%;" placeholder="Enter DataSource ConnectionString" required></asp:TextBox>
                </div>
                <div class="form-group col-12 mb-3">
                    <label for="NewDataSourceUsername">New DataSource Username. (For DataSource Authentication)</label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="NewDataSourceUsername" style="min-width: 100%;" placeholder="Enter DataSource Username" required></asp:TextBox>
                </div>
                <div class="form-group col-12 mb-3">
                    <label for="NewDataSourcePassword">New DataSource Password. (For DataSource Authentication)</label>
                    <asp:TextBox runat="server" type="password" class="form-control" ID="NewDataSourcePassword" style="min-width: 100%;" placeholder="Enter DataSource Password" required></asp:TextBox>
                </div>
                <div class="form-group col-12 mb-3">
                    <label for="NewDataSourceParentFolder">New DataSource Parent Folder. (For DataSource Authentication)</label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="NewDataSourceParentFolder" style="min-width: 100%;" placeholder="Enter DataSource Parent Folder (/)" required></asp:TextBox>
                </div>

                <hr style="border: 1px solid">
                <div id="divAlertSuccess" class="alert alert-success" style="font-weight: bold;" runat="server" visible="false"></div>
                <div id="divAlertDanger" class="alert alert-danger" style="font-weight: bold;" runat="server" visible="false"></div>
                <asp:LinkButton ID="SubmitForm" runat="server" OnClick="SubmitForm_Click" class="btn btn-primary btn-block">Submit Form</asp:LinkButton>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
