<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerRegistration.aspx.cs" Inherits="VesselSharingAgreement.CustomerRegistration" %>

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
<link rel="stylesheet" href="css/intlTelInput.css">
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
            <li><a href="Login.aspx">Login</a></li>
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
  <h3>Customer Registration</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-6 col-md-8">
          <h5 style="font:bold"><a style="color:red">*</a> Indicates field is mandatory</h5>
        <h4>Customer Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox type="text" TabIndex="1" runat="server" MaxLength="20" id="TxtCustomerId" class="form-control input-lg" placeholder="Customer Id" ></asp:TextBox><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="CustomerIdValidator" ValidationGroup="A" ControlToValidate="TxtCustomerId" runat="server" ForeColor="Red" ErrorMessage="Please Enter Customer Id"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox type="text" TabIndex="2" runat="server" id="TxtCompanyName" MaxLength="50" class="form-control input-lg" placeholder="Company Name" ></asp:TextBox><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="CompanyNameValidator" ValidationGroup="A" ControlToValidate="TxtCompanyName" runat="server" ForeColor="Red" ErrorMessage="Please Enter Company Name"></asp:RequiredFieldValidator>
            </div>
          </div>
            <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox runat="server" type="text" MaxLength="7" TabIndex="3" class="form-control input-lg" ID="TxtIMOShipId" placeholder="IMO Ship Id ex:7474756" /><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="IMOShipIdRequiredFieldValidator" ValidationGroup="A" runat="server" ControlToValidate="TxtIMOShipId" ErrorMessage="Please enter IMO Ship Id" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="TxtIMOShipIdValidator" ValidationGroup="A" ForeColor="Red" ControlToValidate="TxtIMOShipId" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]?[0-9]+$"></asp:RegularExpressionValidator>
            </div>
          </div>
          <div class="col-md-12">
            <h4>Customer Type<a style="color:red">*</a>  <asp:Label runat="server" ID="lblerrCustomerType" Font-Size="Large"></asp:Label></h4>
            <div class="checkbox inline-block padding-bottom-10 margin-right-20" tabindex="4" aria-checked="false" ><img src="./images/checkbox-unchecked-black.png" alt="">
               <label class="padding-left-0"> <asp:CheckBox ID="ckSSLine" runat="server" TabIndex="4" /><span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> SSLine </label>
                </div>
              <!--<label class="padding-left-0">
              <input type="checkbox" value="" runat="server"  name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> SSLine </label>-->
            
            <div class="checkbox padding-bottom-10 inline-block margin-right-20" tabindex="5">
                <label class="padding-left-0"> <asp:CheckBox ID="ckVeselOperator" TabIndex="5" runat="server" /><span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Vessel Operator </label>
              <!--<label class="padding-left-0"> <asp:CheckBox runat="server" />
              <input type="checkbox" value="" runat="server"  name="select">-->
              <!--<span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Vessel Operator </label>-->
            </div>
            <div class="checkbox inline-block margin-right-20" tabindex="6">
                <label class="padding-left-0"> <asp:CheckBox ID="ckCargOperator" TabIndex="6" runat="server" /><span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Cargo Operator </label>
              <!--<label class="padding-left-0">
              <input type="checkbox" value="" runat="server"  name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Cargo Operator </label>-->
            </div>
            <div class="checkbox inline-block margin-right-20" tabindex="7">
                <label class="padding-left-0"> <asp:CheckBox ID="ckAgent" runat="server" TabIndex="7" /><span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Agent </label>
              <!--<label class="padding-left-0">
              <input type="checkbox" value="" name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Agent</label>-->
            </div>
          </div>
        </div>
        <h4>Contact Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox runat="server" ID="TxtFirstName" TabIndex="8" MaxLength="40" type="text" class="form-control input-lg" placeholder="First Name" ></asp:TextBox><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="FirstNameValidator" ValidationGroup="A" ControlToValidate="TxtFirstName" runat="server" ForeColor="Red" ErrorMessage="Please Enter First Name"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="FirstNameCharactersValidator" ValidationGroup="A" runat="server" ControlToValidate="TxtFirstName" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Please enter only Alphabets." />
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox runat="server" ID="TxtLastName" TabIndex="9" MaxLength="40" type="text" class="form-control input-lg" placeholder="Last Name" ></asp:TextBox><a style="color:red">*</a>
              <asp:RequiredFieldValidator ID="LastNameValidator" ValidationGroup="A" ControlToValidate="TxtLastName" runat="server" ForeColor="Red" ErrorMessage="Please Enter Last Name"></asp:RequiredFieldValidator><br />
              <asp:RegularExpressionValidator ID="LastNameCharacterValidator" ValidationGroup="A" runat="server" ControlToValidate="TxtLastName" ValidationExpression="[a-zA-Z ]*$" ForeColor="Red" ErrorMessage="Please enter only Alphabets." />
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" ID="TxtPhoneNumber" TabIndex="10" MaxLength="20" type="tel" class="form-control input-lg phone" placeholder="Phone Number" /><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="PhoneNumberNameValidator" ValidationGroup="A" ControlToValidate="TxtPhoneNumber" runat="server" ForeColor="Red" ErrorMessage="Please Enter the Phone Number" InitialValue="+1"></asp:RequiredFieldValidator><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" ControlToValidate="TxtPhoneNumber" runat="server" ForeColor="Red" ErrorMessage="Please Enter the Phone Number" ></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ContactnumberValidator" ValidationGroup="A" runat="server" ValidationExpression="^\+?\d*$" ControlToValidate="TxtPhoneNumber" ForeColor="Red" ErrorMessage="Please Enter numbers only"></asp:RegularExpressionValidator>
            </div>
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" ID="TxtAltPhoneNumber" TabIndex="12" MaxLength="20" type="tel" class="form-control input-lg phone" placeholder="Alternate Phone Number" />
                <asp:RegularExpressionValidator ID="ContactAltnumberValidator" runat="server" ValidationExpression="^\+?\d*$" ControlToValidate="TxtAltPhoneNumber" ForeColor="Red" ErrorMessage="Please Enter numbers only"></asp:RegularExpressionValidator>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" ID="TxtEmailID" type="email" TabIndex="11" MaxLength="50" class="form-control input-lg" placeholder="Email ID" ></asp:TextBox><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="EmailIDValidator" ValidationGroup="A" ControlToValidate="TxtEmailID" runat="server" ForeColor="Red" ErrorMessage="Please Enter Email ID"></asp:RequiredFieldValidator>
            </div>
            
          </div>
          
        </div>
        <div class="row">
          <div class="col-sm-12 padding-bottom-10">
            <h4>Customer Address</h4>
            <div class="checkbox inline-block margin-right-20" tabindex="13">
              <label class="padding-left-0">
              <input type="checkbox" value="" runat="server" tabindex="13" class="bills" name="reg" checked="checked" id="reg" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Registered Address </label><a style="color:red">*</a>
            </div>
            <div class="checkbox inline-block margin-right-20" tabindex="14">
              <label class="padding-left-0">
              <input type="checkbox" value="" name="bill" tabindex="14" runat="server" id="bill" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Billing Address </label>
            </div>
            <div class="checkbox inline-block margin-right-20" tabindex="15">
              <label class="padding-left-0">
              <input type="checkbox" value="" name="dba" tabindex="15" runat="server" id="dba" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> DBA</label>
            </div>
          </div>
        </div>
        <div id="reg-address" class="gray-border padding-10 padding-bottom-0 margin-bottom-10 whiteBg img-rounded">
          <div class="padding-bottom-15"><strong>Registered Address</strong></div>
          <div class="row">
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtBuildingNum" TabIndex="16" MaxLength="10" type="text" class="form-control input-lg" placeholder="Building Number" ></asp:TextBox><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="BuildingNumValidation" ValidationGroup="A" ForeColor="Red" runat="server" ControlToValidate="TxtBuildingNum" ErrorMessage="Please enter Building Number"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtStreet" TabIndex="17" MaxLength="100" type="text" class="form-control input-lg" placeholder="Street" /><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="StreetValidation" ValidationGroup="A" ForeColor="Red" ControlToValidate="TxtStreet" runat="server" ErrorMessage="Please enter street Number/Name"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtCity" MaxLength="40" TabIndex="18" type="text" class="form-control input-lg" placeholder="City" /><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="CityValidation" ValidationGroup="A" ForeColor="Red" ControlToValidate="TxtCity" runat="server" ErrorMessage="Please enter City"></asp:RequiredFieldValidator>
              </div>
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtPostZip" MaxLength="12" TabIndex="20" type="text" class="form-control input-lg" placeholder="Postal Code/Zip Code" /><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="PostZipValidation" ValidationGroup="A" ForeColor="Red" ControlToValidate="TxtPostZip" runat="server" ErrorMessage="Please enter Zip Code"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
                <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtState" MaxLength="25" TabIndex="19" type="text" class="form-control input-lg" placeholder="State" /><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="StateValidation" ValidationGroup="A" ForeColor="Red" ControlToValidate="TxtState" runat="server" ErrorMessage="Please enter State"></asp:RequiredFieldValidator>
              </div>
              <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlcountryReg" class="form-control input-lg"  TabIndex="21" runat="server"></asp:DropDownList><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="countryRegValidation" ValidationGroup="A" ForeColor="Red" runat="server" ControlToValidate="ddlcountryReg"
                ErrorMessage="Please select country" InitialValue="0"></asp:RequiredFieldValidator>
                  <%--<select class="form-control input-lg">
                  <option selected="selected">Country</option>
                  <option>India</option>
                  <option>Qatar</option>
                  <option>USA</option>
                  <option>UK</option>
                </select>--%>
              </div>
            </div>
          </div>
        </div>
        <div id="bill-address" class="gray-border padding-10 padding-bottom-0 padding-top-0 margin-bottom-10 whiteBg img-rounded">
          <div class="padding-bottom-10"><strong>Billing Address</strong>
            <div class="checkbox inline-block margin-left-20" tabindex="22">
              <label class="padding-left-0">
                  <asp:CheckBox ID="sameBilladres" TabIndex="22" runat="server" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Same as Registered Address</label>
                
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="23" ID="TxtBillBuildingNum" MaxLength="10" type="text" class="form-control input-lg" placeholder="Building Number" /><a style="color:red">*</a>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="24" ID="TxtBillStreet" MaxLength="100" type="text" class="form-control input-lg" placeholder="Street" /><a style="color:red">*</a>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="25" ID="TxtBillCity" MaxLength="40" type="text" class="form-control input-lg" placeholder="City" /><a style="color:red">*</a>
              </div>
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtBillState" TabIndex="27" MaxLength="25" type="text" class="form-control input-lg" placeholder="State" /><a style="color:red">*</a>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtBillPostZip" TabIndex="26" MaxLength="12" onkeypress="return isNumber(event)" type="text" class="form-control input-lg" placeholder="Postal Code/Zip Code" /><a style="color:red">*</a>
              </div>
              <div class="padding-bottom-15">
                  <asp:DropDownList ID="ddlcountryBill" TabIndex="28" class="form-control input-lg" runat="server"></asp:DropDownList><a style="color:red">*</a>
                <%--<select class="form-control input-lg">
                  <option selected="selected">Country</option>
                  <option>India</option>
                  <option>Qatar</option>
                  <option>USA</option>
                  <option>UK</option>
                </select>--%>
              </div>
            </div>
          </div>
        </div>
        <div id="dba-address" class="gray-border padding-10 padding-bottom-0 padding-top-0 margin-bottom-10 whiteBg img-rounded">
          <div class="padding-bottom-10"><strong>DBA</strong>
            <div class="checkbox inline-block margin-left-20" tabindex="29">
              <label class="padding-left-0">
                  <asp:CheckBox ID="samedbaAddres" TabIndex="29" runat="server"  />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Same as Registered Address</label>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="30" ID="TxtDBABuildingNum" MaxLength="10" type="text" class="form-control input-lg" placeholder="Building Number" /><a style="color:red">*</a>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="31" ID="TxtDBAStreet" MaxLength="100" type="text" class="form-control input-lg" placeholder="Street" /><a style="color:red">*</a>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="32" ID="TxtDBACity" MaxLength="40" type="text" class="form-control input-lg" placeholder="City" /><a style="color:red">*</a>
              </div>
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtDBAState" TabIndex="34" MaxLength="25" type="text" class="form-control input-lg" placeholder="State" /><a style="color:red">*</a>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtDBAPostZip" TabIndex="33" MaxLength="12" type="text" onkeypress="return isNumber(event)" class="form-control input-lg" placeholder="Post Code/Zip Code" /><a style="color:red">*</a>
              </div>
              <div class="padding-bottom-15">
                  <asp:DropDownList ID="ddlCountrydba" TabIndex="35" class="form-control input-lg" runat="server"></asp:DropDownList><a style="color:red">*</a>
                <%--<select class="form-control input-lg">
                  <option selected="selected">Country</option>
                  <option>India</option>
                  <option>Qatar</option>
                  <option>USA</option>
                  <option>UK</option>
                </select>--%>
              </div>
            </div>
          </div>
        </div>
        <div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10">
                <asp:Button ID="btnRegister" TabIndex="36" runat="server" ValidationGroup="A" class="btn btn-primary btn-lg min-width-200" Text="Register" OnClick="btnRegister_Click" />&nbsp; 
                <asp:Button class="btn btn-default btn-lg" TabIndex="37" ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click"></asp:Button>
            </div>
          </div>
        </div>
      </div>
      <div class="col-sm-6  col-md-4 hidden-xs"> <%--<img src="" class="img-responsive img-thumbnail center-block" />--%> </div>
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

      
    </div>
    </form>
    
<!-- jQuery Js -->
<script src="js/jquery.min.js"></script>
<!-- Bootstrap Js -->
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.bootstrap-responsive-tabs.min.js"></script>
<script src="js/intlTelInput.js"></script>
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
      // initialCountry: "auto",
      // nationalMode: false,
      // onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
      placeholderNumberType: "MOBILE",
       
      // preferredCountries: ['cn', 'jp'],
      separateDialCode: true,
      utilsScript: "build/js/utils.js"
    });
</script>
    <script type="text/javascript">
    $("#reg").click(function () {
        if($(this).is(":checked")) {
            $("#reg-address").show();
        } else {
            $("#reg-address").hide();
        }
    });

$("#bill-address").hide();
$("#dba-address").hide();
$("#bill").click(function () {
    if($(this).is(":checked")) {
        $("#bill-address").show();
    } else {
        $("#bill-address").hide();

        $("#<%= TxtBillBuildingNum.ClientID %>").val("");

            $("#<%= TxtBillCity.ClientID %>").val("");

            $("#<%= TxtBillPostZip.ClientID %>").val("");
            
            $("#<%= TxtBillState.ClientID %>").val("");

        $("#<%= TxtBillStreet.ClientID %>").val("");

        $('#sameBilladres').prop('checked', false); // Unchecks it

        $('#ddlcountryBill').val("");
    }
});
$("#dba").click(function() {
    if($(this).is(":checked")) {
        $("#dba-address").show();
    } else {
        $("#dba-address").hide();

        $("#<%= TxtDBABuildingNum.ClientID %>").val("");

            $("#<%= TxtDBACity.ClientID %>").val("");

            $("#<%= TxtDBAPostZip.ClientID %>").val("");
            
            $("#<%= TxtDBAState.ClientID %>").val("");

        $("#<%= TxtDBAStreet.ClientID %>").val("");

        $('#samedbaAddres').prop('checked', false); // Unchecks it

        $('#ddlCountrydba').val("");
    }
});

$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});
</script>
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $("#<%= sameBilladres.ClientID %>").change(function () { 
        if ($(this).is(":checked")){
         var TxtBuildingNum = $("#<%= TxtBuildingNum.ClientID %>").val();
         $("#<%= TxtBillBuildingNum.ClientID %>").val(TxtBuildingNum);

         var TxtCity = $("#<%= TxtCity.ClientID %>").val();
         $("#<%= TxtBillCity.ClientID %>").val(TxtCity);

         var TxtPostZip = $("#<%= TxtPostZip.ClientID %>").val();
         $("#<%= TxtBillPostZip.ClientID %>").val(TxtPostZip);

         var TxtState = $("#<%= TxtState.ClientID %>").val();
         $("#<%= TxtBillState.ClientID %>").val(TxtState);

         var TxtStreet = $("#<%= TxtStreet.ClientID %>").val();
            $("#<%= TxtBillStreet.ClientID %>").val(TxtStreet);

           var ddlBill = $('#ddlcountryReg').val();
           $('#ddlcountryBill').val(ddlBill);
            

        }else
        {
            $("#<%= TxtBillBuildingNum.ClientID %>").val("");

            $("#<%= TxtBillCity.ClientID %>").val("");

            $("#<%= TxtBillPostZip.ClientID %>").val("");
            
            $("#<%= TxtBillState.ClientID %>").val("");

            $("#<%= TxtBillStreet.ClientID %>").val("");

            $('#ddlcountryBill').val("");
        }
        
    
    });
    </script>
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $("#<%= samedbaAddres.ClientID %>").change(function () { 
        if ($(this).is(":checked")){
         var TxtBuildingNum = $("#<%= TxtBuildingNum.ClientID %>").val();
         $("#<%= TxtDBABuildingNum.ClientID %>").val(TxtBuildingNum);

         var TxtCity = $("#<%= TxtCity.ClientID %>").val();
         $("#<%= TxtDBACity.ClientID %>").val(TxtCity);

         var TxtPostZip = $("#<%= TxtPostZip.ClientID %>").val();
         $("#<%= TxtDBAPostZip.ClientID %>").val(TxtPostZip);

         var TxtState = $("#<%= TxtState.ClientID %>").val();
         $("#<%= TxtDBAState.ClientID %>").val(TxtState);

         var TxtStreet = $("#<%= TxtStreet.ClientID %>").val();
            $("#<%= TxtDBAStreet.ClientID %>").val(TxtStreet);

            var ddlDba = $('#ddlcountryReg').val();
            $('#ddlCountrydba').val(ddlDba);

        }else
        {
            $("#<%= TxtDBABuildingNum.ClientID %>").val("");

            $("#<%= TxtDBACity.ClientID %>").val("");

            $("#<%= TxtDBAPostZip.ClientID %>").val("");
            
            $("#<%= TxtDBAState.ClientID %>").val("");

            $("#<%= TxtDBAStreet.ClientID %>").val("");

            $('#ddlCountrydba').val("");
        }
        
    
    });
</script>
  <script>
$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});
</script>

<%--    <script type="text/javascript">
        var inputEl = document.getElementById('TxtPhoneNumber');
        var inputALT = document.getElementById('TxtAltPhoneNumber');
        var inputTxtIMOShipId = document.getElementById('TxtIMOShipId');
        var goodKey = '0123456789+ ';
        var key = null;

        var checkInputTel = function() {
            var start = this.selectionStart,
              end = this.selectionEnd;

            var filtered = this.value.split('').filter(filterInput);
            this.value = filtered.join("");

            /* Prevents moving the pointer for a bad character */
            var move = (filterInput(String.fromCharCode(key)) || (key == 0 || key == 8)) ? 0 : 1;
            this.setSelectionRange(start - move, end - move);
        }

        var filterInput = function(val) {
            return (goodKey.indexOf(val) > -1);
        }

        /* This function save the character typed */
        var res = function(e) {
            key = (typeof e.which == "number") ? e.which : e.keyCode;
        }

        inputEl.addEventListener('input', checkInputTel);
        inputEl.addEventListener('keypress', res);

        inputALT.addEventListener('input', checkInputTel);
        inputALT.addEventListener('keypress', res);

        inputTxtIMOShipId.addEventListener('input', checkInputTel);
        inputTxtIMOShipId.addEventListener('keypress', res);
    </script>--%>

    <script>
        $("#TxtFirstName").click("click", function () {
            if (($('#ckSSLine').prop('checked') == false) && ($('#ckVeselOperator').prop('checked') == false) && ($('#ckCargOperator').prop('checked') == false) && ($('#ckAgent').prop('checked') == false))
            {
                $('#lblerrCustomerType').text("Please Select atleast one Customer type").css("color", "red")
           }
           else
           {
                $('#lblerrCustomerType').text("");
           }
            
        })
    </script>

        <script type="text/javascript">
    var queryString = new Array();
    $(function () {
        if (queryString.length == 0) {
            if (window.location.search.split('?').length > 1) {
                var params = window.location.search.split('?')[1].split('&');
                for (var i = 0; i < params.length; i++) {
                    var key = params[i].split('=')[0];
                    var value = decodeURIComponent(params[i].split('=')[1]);
                    queryString[key] = value;
                }
            }
        }
        if (queryString["FirstName"] != null && queryString["LastName"] != null) {
            var data = "<u>Values from QueryString</u><br /><br />";
            data += "<b>FirstName:</b> " + queryString["FirstName"] + " <b>LastName:</b> " + queryString["LastName"];
            $("#lblmsg").html(data);
        }
    });
</script>
 
</body>
</html>

