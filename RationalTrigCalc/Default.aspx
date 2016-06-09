<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rational Trigonometry Calculator</title>
    <style type="text/css">
        .font_style
        {
            font-family:Arial;
            font-size:small;
        }
        .width_style
        {
            width:250px;
        }
        .margin_style
        {
            margin-top:4px;
            margin-bottom:4px;
            margin-left:0px;
            margin-right:0px;
        }
    </style>
</head>
<body class="font_style">
    <form id="form1" runat="server">
    <div>
                
        <strong>
        <span style="font-size:x-large;color:blue">Rational Trigonometry Calculator</span>
        <br />
        <span style="color:gray">For Right Triangles</span>
        </strong>

        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="triangle.png" CssClass="width_style" />
        <br />        

        <p class="margin_style">
        <asp:Label ID="s1Label" runat="server" Text="Spread, s1" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="s1TextBox" runat="server" AutoCompleteType="Disabled" CssClass="width_style"></asp:TextBox>
        <br />
        <asp:Label ID="s1ClassicTrigLabel" runat="server" Font-Size="Smaller" ForeColor="Gray" Text="Angle (Deg):"></asp:Label>
        </p>
        
        <p class="margin_style">
        <asp:Label ID="s2Label" runat="server" Text="Spread, s2" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="s2TextBox" runat="server" AutoCompleteType="Disabled" CssClass="width_style"></asp:TextBox>
        <br />
        <asp:Label ID="s2ClassicTrigLabel" runat="server" Font-Size="Smaller" ForeColor="Gray" Text="Angle (Deg):"></asp:Label>
        </p>
        
        <p class="margin_style">
        <asp:Label ID="Q1Label" runat="server" Text="Quadrance, Q1" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="Q1TextBox" runat="server" AutoCompleteType="Disabled" CssClass="width_style"></asp:TextBox>
        <br />
        <asp:Label ID="Q1ClassicTrigLabel" runat="server" Font-Size="Smaller" ForeColor="Gray" Text="Length:"></asp:Label>
        </p>
                 
        <p class="margin_style">
        <asp:Label ID="Q2Label" runat="server" Text="Quadrance, Q2" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="Q2TextBox" runat="server" AutoCompleteType="Disabled" CssClass="width_style"></asp:TextBox>
        <br />
        <asp:Label ID="Q2ClassicTrigLabel" runat="server" Font-Size="Smaller" ForeColor="Gray" Text="Length:"></asp:Label>
        </p>

        <p class="margin_style">
        <asp:Label ID="Q3Label" runat="server" Text="Quadrance, Q3" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="Q3TextBox" runat="server" AutoCompleteType="Disabled" CssClass="width_style"></asp:TextBox>
        <br />
        <asp:Label ID="Q3ClassicTrigLabel" runat="server" Font-Size="Smaller" ForeColor="Gray" Text="Length:"></asp:Label>
        </p>
        
        <p class="margin_style">
        <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Font-Bold="False"></asp:Label>         
        </p>
        
        <asp:Button ID="CalculateButton" runat="server" OnClick="CalculateButton_Click" Text="Calculate" />
        <asp:Button ID="ClearButton" runat="server" OnClick="ClearButton_Click" Text="Clear" />

        <br />
        <br />
        Rational trigonometry is an alternative to the classical trigonometry taught in schools.
        It is developed by Norman J. Wildberger and described in his 2005 book <em>DIVINE PROPORTIONS: Rational Trigonometry to Universal Geometry</em>.
        <br />
        <br />
        Rational trigonometry uses the concepts of spread and quadrance instead of angle and length:
        <br />
        - The spread between two lines is a number between 0 and 1. Parallel lines have a spread of 0 and perpendicular lines have a spread of 1.
        <br />
        - The quadrance between two points is the squared length between the points. 
        <br />        
        <br />
        Read more about rational trigonometry at <a href="https://en.wikipedia.org/wiki/Rational_trigonometry" target="_blank">Wikipedia</a>.<br />
        <br />
        This calculator works for right triangles and uses rational numbers (i.e. fractions like 2/3) as input and output.
        <br />        
        <br />        
        <br />
        By Peter Bollhorn, 2016</div>
    
    </form>
</body>
</html>