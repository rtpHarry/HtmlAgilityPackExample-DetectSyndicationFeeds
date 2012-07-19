<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Html Agility Pack Example - Extract All Syndication Feeds From A Web Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Html Agility Pack Example - Extract All Syndication Feeds From A Web Page</h1>
        <p>
            This example shows you how to use Html Agility Pack to load a remote url and parse
            out the urls of all syndication feeds it contains.</p>
        <div>
            <asp:Label ID="Label1" runat="server" Text="URL:" />
            <asp:TextBox ID="Url" runat="server" Text="http://www.cnn.com/"></asp:TextBox>
            <asp:Button ID="RetrieveButton" runat="server" Text="Get Feeds"/>
        </div>
        <asp:GridView ID="ResultsGrid" runat="server" DataSourceID="ObjectDataSource1">
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="Select" 
            TypeName="SyndicationFeedsDataObject">
            <SelectParameters>
                <asp:ControlParameter ControlID="Url" Name="WebsiteUrl" PropertyName="Text" 
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
