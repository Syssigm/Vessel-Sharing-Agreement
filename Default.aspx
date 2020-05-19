<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" ClientIDMode="Static" Inherits="VesselSharingAgreement.Default" %>

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
    <form runat="server">
<div class="topHdr">
  <div class="container-fluid">
    <nav class="navbar navbar-default">
      <div class="container-fluid">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false"> <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </button>
          <a class="navbar-brand" href="index.html"> <img src="images/VSA-logo.png" class="img-responsive" /></a> </div>
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
          <ul class="nav navbar-nav navbar-right">
            <li><a href="Login.aspx">Login</a></li>
            <li><a href="CustomerRegistration.aspx">SignUp</a></li>
            <li><img src="images/social-icons.png" border="0" usemap="#Map">
              <map name="Map">
                <area shape="rect" coords="0,-1,35,33" href="#">
                <area shape="rect" coords="40,0,76,35" href="#">
                <area shape="rect" coords="82,0,116,38" href="#">
              </map>
            </li>
          </ul>
        </div>
        <!-- /.navbar-collapse -->
      </div>
      <!-- /.navbar-collapse -->
    </nav>
  </div>
  <!-- /.container-fluid -->
</div>
<div class="container banner-top-heading">
  <h3 style="text-transform:capitalize">OPTIMIZE YOUR FREIGHT BOOKING, REDUCE YOUR SHIP CARGO CHARGES </h3>
</div>
    <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="slider">
  <!-- Wrapper for slides -->
  <div class="carousel-inner banner-orange" role="listbox" style="">
    <div class="item active"> <img src="images/pexels-photo-93106.jpeg" class="img-responsive" alt="Slide 1">
      <div class="banner-top">
        <div class="container">
            <h3 id="TabShipRoute" class="banner-block-h3 active">By Ship Route</h3>
            <h3 id="TabByDate" class="banner-block-h3">By Date</h3>
		  <div class="banner-block banner-block-orange">
            <div class="row" id="Portddls">
              <div class="col-sm-6">
                <p class="padding-bottom-10">Origin Port</p>
                <div class="padding-bottom-15">
                    <asp:DropDownList ID="ddloriginport" class="form-control input-lg" runat="server"></asp:DropDownList>
                </div>
              </div>
              <div class="col-sm-6">
                <p class="padding-bottom-10">Destination Port</p>
                <div class="padding-bottom-15">
                    <asp:DropDownList ID="ddlDestinationPort" class="form-control input-lg" runat="server"></asp:DropDownList>
                </div>
              </div>
                </div>
                <div class="row" id="Datetextboxes">

                    <div class="row">
          <div class="col-md-4 padding-left-25">
            <div class="radio inline-block" tabindex="5">
              <label class="padding-left-0">
              <input type="radio" value="" tabindex="5" runat="server" id="ExpectedeparturedateRB" name="select"  />
              <span class="cr"><i class="cr-icon fa fa-circle brandcolor"></i></span> Departure Date </label>
            </div>
              </div>
            <div class="col-md-6">
            <div class="radio inline-block" tabindex="6">
              <label class="padding-left-0">
              <input type="radio" value="" tabindex="6" runat="server" id="ExpectedarrivaldateRB" name="select" />
              <span class="cr"><i class="cr-icon fa fa-circle brandcolor"></i></span> Arrival Date </label>
            </div>
          </div>
        </div>

              <div class="col-sm-6">
                <p class="padding-bottom-10">From Date</p>
                <div class="padding-bottom-15">
                    <asp:TextBox ID="TxtFromDate" class="form-control input-lg date" runat="server"></asp:TextBox>
                </div>
              </div>
              <div class="col-sm-6">
                <p class="padding-bottom-10">To Date</p>
                <div class="padding-bottom-15">
                    <asp:TextBox ID="TxtToDate" class="form-control input-lg date" runat="server"></asp:TextBox>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-sm-5 col-xs-12">
                <div class="padding-top-10">
                    <asp:Button ID="BtnCheckVSAAvailability" class="btn btn-primary btn-lg btn-block" runat="server" OnClick="BtnCheckVSAAvailability_click" Text="Check VSA Availability" />
                </div>
              </div>
            </div>
              <div class="row">
                  <div class="padding-top-10 padding-left-15">
                      <h4 style="font:bold" runat="server" id="Selection"> Selection Criteria: </h4>
                  </div>
                  <div class="col-sm-6">
                <asp:Label ID="lblValue1" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblResValue1" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
              </div>
              <div class="col-sm-6">
                <asp:Label ID="lblValue2" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblResValue2" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
              </div>


                  <%--<div class="col-sm-6">
                  
                      <div>
                          <asp:Label ID="lblOrigin" runat="server" Text="Origin Port : "></asp:Label>
                          <asp:Label ID="lblValue1" runat="server" Font-Bold="true" ForeColor="Blue" Text="Origin Port"></asp:Label>
                      
                          <asp:Label ID="lblDestination" runat="server" Text="Destination Port : "></asp:Label>
                          <asp:Label ID="lblvalue2" runat="server" Font-Bold="true" ForeColor="Blue" Text="Destination Port"></asp:Label>
                      </div>
                      </div>--%>
              </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<div class="container-fluid solutions-section">
<div class="container">
            <h4 class="margin-top-20">VSA Availability Details <asp:Label ID="lblroutemsg" runat="server" Text=""></asp:Label></h4>

<div class="table-responsive">
              <table class="table table-bordered">
                <thead>
                  <tr class="bg-primary">
                    <th class="">Route</th>
                    <th class="">Vessel Name</th>
                    <th class="max-width-70 text-center">Start Date</th>
                    <th class="max-width-70 text-center">Arrival Date</th>
                    <th class="text-center">VSA Available</th>
                  </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptVSAAvailability">
                        <ItemTemplate>
                <tr>
                    <td><asp:LinkButton runat="server" ID="lnkRoute" OnClientClick="return NewWindow();" OnClick="lnkRoute_Click" CommandArgument='<%# Eval("VoyageID") %>'><asp:Label ID="lblDestinationport" runat="server" Text='<%# Eval("Port") %>'></asp:Label></asp:LinkButton></td>
                    <td><asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("VesselName") %>'></asp:Label></td>
                    <td><asp:Label ID="lblStartdate" runat="server" Text='<%# Eval("Startdate","{0:d-MMM-yyyy hh:mm tt}") %>'></asp:Label></td>
                    <td><asp:Label ID="lblEnddate" runat="server" Text='<%# Eval("Enddate","{0:d-MMM-yyyy hh:mm tt}") %>'></asp:Label></td>
                    <td>Yes</td>
                  </tr>
                            </ItemTemplate>
                    </asp:Repeater>
                </tbody>
              </table>
            </div>

</div>

</div>

<div class="container-fluid footer-section">
  <div class="container">
    <div class="row">
      <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
        <ul class="center-block">
          <li><a href="index.html"><i class="fa fa-home"></i>Home</a></li>
          <li><a href="#"><i class="fa fa-file-text"></i> About Us</a></li>
          <li><a href="#"><i class="fa fa-users"></i>Customers</a></li>
          <%--<li><a href="#"><i class="fa fa-ship"></i>Voyages Available for VSA</a></li>
          <li><a href="#"><i class="fa fa-calendar"></i>Vessel Movement Schedule</a></li>
          <li><a href="#"><i class="fa fa-handshake-o"></i>VSA Partners</a></li>--%>
        </ul>
      </div>
      <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
        <h4>Address</h4>
        <p><i class="fa fa-map-marker"></i><a  href="#"> Norcorss, GA 30022, USA</a></p>
        <p><i class="fa fa-phone"></i> +1 (678) 682-9234 </p>
        <p><i class="fa fa-phone"></i> +1 (678) 658-8626</p>
        <p><i class="fa fa-envelope-o"></i> info@cargovsa.com</p>
      </div>
      <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 connect">
        <h4 class="">Connect Us</h4>
        <p class="social"> <a href="#"><img src="images/facebook.png" border="0" class="gray-border"></a> <a href="#"><img src="images/twitter.png" border="0" class="gray-border"></a> <a href="#"><img src="images/linkedin.png" border="0" class="gray-border"></a> </p>
        <p class=""><a href="#" class="b-btn">Write to us </a> </p>
        <%--<p class=""><a href="#" class="b-btn">Login/Signup</a> </p>--%>
      </div>
    </div>
  </div>
</div>
<div class="container-fluid copyrights-section">
  <div class="container">
      <asp:Label ID="lblVisitor" runat="server"></asp:Label>
    <p class="text-center"> Copyright © 2017 CargoVSA. All rights reserved. </p>
  </div>
</div>
        </form>

<!-- jQuery Js -->
<script src="js/jquery.min.js"></script>
<!-- Bootstrap Js -->
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.bootstrap-responsive-tabs.min.js"></script>
<script src="js/jquery.datetimepicker.js"></script>
<script>
$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});
</script>
    <script>
        $("#Portddls").show();
        $("#Datetextboxes").hide();
        $("#TabByDate").click(function () {
            $("#Datetextboxes").show();
            $("#Portddls").hide();
            $(this).addClass('active');
            $("#TabShipRoute").removeClass('active');
        });

        $("#TabShipRoute").click(function () {
            $("#Datetextboxes").hide();
            $("#Portddls").show();
            $(this).addClass('active');
            $("#TabByDate").removeClass('active');
        });

        $('.time').datetimepicker({
            datepicker: false,
            format: 'H:i',
            step: 5
        });
        $('.date').datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            formatDate: 'Y/m/d',
        });

        function datetab() {
            alert("date tab")
            $("#Datetextboxes").show();
            $("#Portddls").hide();
        }
    </script>
    <script type="text/javascript">
function NewWindow() {
    document.forms[0].target = '_blank';
}
</script>
    
</body>
</html>
