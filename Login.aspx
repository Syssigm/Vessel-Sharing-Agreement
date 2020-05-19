<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VesselSharingAgreement.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>VSA - Login</title>
<!-- Bootstrap Css -->
<link href="css/bootstrap.min.css" rel="stylesheet">
<link href="css/font-awesome.min.css" rel="stylesheet">
<link href="css/prettyCheckable.css" rel="stylesheet">
<link rel="stylesheet" href="css/intlTelInput.css">
<link rel="stylesheet" href="css/jquery.datetimepicker.css">
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
            <%--<li><a href="#">Home</a></li>
            <li><a href="#">Customer</a></li>
            <li><a href="#">Vessel Operator</a></li>
            <li><a href="#">EOI for VSA</a></li>
            <li><a href="#">Login</a></li>
            <li><a href="#">SignUp</a></li>--%>
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
  <h3>Login</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-6 col-md-8">
        <h4>Username</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox runat="server" ID="TxtUsername" type="text" class="form-control input-lg" placeholder="Enter your login Customer ID" autofocus />
            </div>
          </div>
        </div>
        <h4>Password </h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox runat="server" ID="TxtPassword" type="password" class="form-control input-lg" placeholder="Enter your password" />
            </div>
			<a href="#" class="" data-toggle="modal" data-target="#myModal">Forgot Password?</a>
          </div>
        </div>
        <div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10"><asp:LinkButton runat="server" ID="LnkLogin" class="btn btn-primary btn-lg min-width-200" OnClick="LnkLogin_Click">Login</asp:LinkButton> <asp:LinkButton runat="server" ID="LnkReset" class="btn btn-default btn-lg">Reset</asp:LinkButton></div>
		          <h4 class="padding-top-20">
                      <asp:LinkButton ID="LnkNewUser" href="CustomerRegistration.aspx" runat="server">New User?</asp:LinkButton> <small>Please click here below to register</small></h4>
            <%--<div class="padding-top-10"><a href="CustomerRegistration.aspx" class="btn btn-success btn-lg min-width-200">Register Now</a></div>--%>
          </div>

        </div>
      </div>
      <div class="col-sm-6  col-md-4 hidden-xs"> <img src="images/finger-pressing-a-virtual-lock_1112-490.jpg" class="img-responsive img-thumbnail center-block" /> </div>
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
        <p><i class="fa fa-map-marker"></i> Norcorss, GA 30022, USA</p>
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
    <p class="text-center"> Copyright © 2017 CargoVSA. All rights reserved. </p>
  </div>
</div>

<div class="modal fade" tabindex="-1" role="dialog" id="myModal">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Forgot Password?</h4>
      </div>
      <div class="modal-body">
			<h4>Registered Email ID</h4>
              <asp:TextBox runat="server" ID="TxtForgotpasswordemailid" type="text" class="form-control input-lg" placeholder="Enter your login id"/>
          <h4>User Name</h4>
              <asp:TextBox runat="server" ID="TxtForgotpasswordUsername" type="text" class="form-control input-lg" placeholder="Enter your login id"/>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <asp:Button runat="server" ID="Btnforgotpasswordsubmit" OnClick="Btnforgotpasswordsubmit_Click" type="button" class="btn btn-primary" Text="Submit"></asp:Button>
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

<!-- jQuery Js -->
<script src="js/jquery.min.js"></script>
<!-- Bootstrap Js -->
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.bootstrap-responsive-tabs.min.js"></script>
<script src="js/intlTelInput.js"></script>
<script src="js/jquery.datetimepicker.js"></script>

<script>
$('.time').datetimepicker({
	datepicker:false,
	format:'H:i',
	step:5
});
$('.date').datetimepicker({
	timepicker:false,
	format:'m/d/Y',
	formatDate:'Y/m/d',
});

</script>

  <script>
    $(".phone").intlTelInput({
      allowDropdown: true,
      autoHideDialCode: false,
      autoPlaceholder: "on",
      // dropdownContainer: "body",
      // excludeCountries: ["us"],
      formatOnDisplay: true,
      // geoIpLookup: function(callback) {
      //   $.get("http://ipinfo.io", function() {}, "jsonp").always(function(resp) {
      //     var countryCode = (resp && resp.country) ? resp.country : "";
      //     callback(countryCode);
      //   });
      // },
      // hiddenInput: "full_number",
      initialCountry: "auto",
      // nationalMode: false,
      // onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
      placeholderNumberType: "MOBILE",
      // preferredCountries: ['cn', 'jp'],
      separateDialCode: true,
      utilsScript: "js/utils.js"
    });
  </script>

<script>
$("#bill-address").hide();
$("#dba-address").hide();
$("#bill").click(function() {
    if($(this).is(":checked")) {
        $("#bill-address").show();
    } else {
        $("#bill-address").hide();
    }
});
$("#dba").click(function() {
    if($(this).is(":checked")) {
        $("#dba-address").show();
    } else {
        $("#dba-address").hide();
    }
});

$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});

        </script>
    </div>
    </form>
</body>
</html>
