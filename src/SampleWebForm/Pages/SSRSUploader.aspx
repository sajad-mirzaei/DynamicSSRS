<%@ Page Title="SSRS Uploader" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SSRSUploader.aspx.cs" Inherits="SSRSUploader" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="upForm" RenderMode="block" UpdateMode="Always">
        <ContentTemplate>

            <asp:LinkButton ID="ddddd" runat="server" OnClick="ddddd_Click">dddddd</asp:LinkButton>

            <div class="container mt-5">
                <h2 class="text-center mb-4">SSRS RDL Uploader</h2>
                <form id="uploadRdlForm" class="needs-validation" novalidate>
                    <!-- Folder Path -->
                    <div class="form-group col-12 mb-3">
                        <p class="text-muted">Example: /Reports (enter / for root of your server)</p>
                        <label for="RdlFolderPath">Server Folder Path</label>
                        <asp:TextBox runat="server" ID="ReportsFolderPath" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter RDL Sever Folder Path" required></asp:TextBox>
                        <div class="invalid-feedback">Please provide the RDL folder path in your server.</div>
                    </div>
                    
                    <div class="form-group col-12 mb-3">
                        <p class="text-muted">Example: C:\Reports</p>
                        <label for="RdlFolderPath">RDL Folder Path</label>
                        <asp:TextBox runat="server" ID="RdlFolderPath" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter RDL Folder Path" required></asp:TextBox>
                        <div class="invalid-feedback">Please provide the RDL folder path.</div>
                    </div>

                    <hr style="border: 1px solid">

                    <!-- Server Information -->
                    <div class="form-group col-12 mb-3">
                        <p class="text-muted">Example: http://myserver/ReportServer</p>
                        <label for="ServerUrl">Server URL</label>
                        <asp:TextBox runat="server" ID="ServerUrl" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter Server URL" required></asp:TextBox>
                        <div class="invalid-feedback">Please provide a valid server URL.</div>
                    </div>
                    <div class="form-group col-12 mb-3">
                        <p class="text-muted">For Server Authentication</p>
                        <label for="ServerUsername">Server Username</label>
                        <asp:TextBox runat="server" ID="ServerUsername" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter Server Username" required></asp:TextBox>
                    </div>
                    <div class="form-group col-12 mb-3">
                        <p class="text-muted">For Server Authentication</p>
                        <label for="ServerPassword">Server Password</label>
                        <asp:TextBox runat="server" ID="ServerPassword" CssClass="form-control" Style="min-width: 100%;" placeholder="Enter Server Password" required></asp:TextBox>
                    </div>

                    <hr style="border: 1px solid">

                    <div class="form-group col-12 mb-3">
                        <p class="text-muted">For Data Source Authentication</p>
                        <label for="DataSourceUsername">Data Source Username</label>
                        <asp:TextBox runat="server" type="text" class="form-control" id="DataSourceUsername" placeholder="Enter Data Source Username" required></asp:TextBox>
                    </div>
                    <div class="form-group col-12 mb-3">
                        <p class="text-muted">For Data Source Authentication</p>
                        <label for="DataSourcePassword">Data Source Password</label>
                        <asp:TextBox runat="server" type="password" class="form-control" id="DataSourcePassword" placeholder="Enter Data Source Password" required></asp:TextBox>
                    </div>
                    
                    <hr style="border: 1px solid">

                    <button type="button" id="submitForm" class="btn btn-primary btn-block">Submit</button>
                </form>

                <div id="progressBar" class="progress mt-4" style="display: none;">
                    <div class="progress-bar" role="progressbar" style="width: 0%;">0%</div>
                </div>

                <div id="responseMessage" class="mt-4"></div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
