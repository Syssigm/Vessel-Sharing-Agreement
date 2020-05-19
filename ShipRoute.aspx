<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShipRoute.aspx.cs" Inherits="VesselSharingAgreement.ShipRoute" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Vessel Sharing Agreement</title>
<!-- Bootstrap Css -->
<link href="css/bootstrap.min.css" rel="stylesheet">
<link href="css/font-awesome.min.css" rel="stylesheet">
<link href="css/prettyCheckable.css" rel="stylesheet">
<link rel="stylesheet" href="css/jquery.datetimepicker.css">
<script src="js/jquery-3.2.1.js"></script>
<!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
<link rel="stylesheet" type="text/css" href="css/custom.css">
<link href="https://fonts.googleapis.com/css?family=Muli:200,300,400,600,700,800,900" rel="stylesheet">
<!--<link href="https://fonts.googleapis.com/css?family=Roboto:100,300,400,700,900" rel="stylesheet">
<link href="https://fonts.googleapis.com/css?family=Lato:100,300,400,700,900" rel="stylesheet">-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div align="center">
            <div class="padding-bottom-10">
                <asp:LinkButton ID="lnkBack" Font-Bold="true" Font-Size="Larger" runat="server" OnClick="lnkBack_Click">Close</asp:LinkButton>
                </div>
        </div>
    
        <div class="table-responsive">
              <table class="table table-bordered">
                <thead>
                  <tr class="bg-primary">
                    <th class="">Route No.</th>
                    <th class="">Origin Port</th>
                      <th class="">Destination Port</th>
                    <th class="max-width-70 text-center">Origin Departure Time</th>
                    <th class="max-width-70 text-center">Destination Arrival Time</th>
                  </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptshiproute">
                        <ItemTemplate>
                <tr>
                    <td><asp:Label ID="lblvoyagesequence" runat="server" Text='<%# Eval("voyagesequence") %>'/></td>
                    <td><asp:Label ID="lblOriginport" runat="server" Text='<%# Eval("OriginPort") %>'/></td>
                    <td><asp:Label ID="lblDestinationport" runat="server" Text='<%# Eval("DestinationPort") %>'></asp:Label></td>
                    <td><asp:Label ID="lblStartdate" runat="server" Text='<%# Eval("Startdate","{0:d-MMM-yyyy hh:mm tt}") %>'></asp:Label></td>
                    <td><asp:Label ID="lblEnddate" runat="server" Text='<%# Eval("Enddate","{0:d-MMM-yyyy hh:mm tt}") %>'></asp:Label></td>
                  </tr>
                            </ItemTemplate>
                    </asp:Repeater>
                </tbody>
              </table>
            </div>
    
    </div>
    </form>
</body>
</html>
