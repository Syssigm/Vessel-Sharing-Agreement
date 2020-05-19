<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ViewModifyPort.aspx.cs" Inherits="VesselSharingAgreement.ViewModifyPort" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container banner-top-heading">
  <h3>View/Modify Port</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-6 col-md-7">
        <h4>Port Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlport" AutoPostBack="true" class="form-control input-lg" runat="server" OnSelectedIndexChanged="ddlport_SelectedIndexChanged"></asp:DropDownList>
                 <asp:TextBox runat="server" type="text" MaxLength="40" Visible="false" class="form-control input-lg" ID="TxtPortName" placeholder="Name of Port" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="PortnameValidator" runat="server" ErrorMessage="Please Enter Port Name" ControlToValidate="TxtPortName" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			</div>
			          <div class="col-md-6">
            <div class="padding-bottom-15">
                <%--<asp:TextBox ID="Txtportiddsbl" class="form-control input-lg" runat="server" ReadOnly="true"></asp:TextBox>--%>
                <asp:TextBox runat="server" type="text" MaxLength="5" class="form-control input-lg" ID="TxtPortId" placeholder="Port Id" ></asp:TextBox>
             <asp:RequiredFieldValidator ID="PortidRequiredFieldValidator" runat="server" ErrorMessage="Please Enter Port ID" ControlToValidate="TxtPortId" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			</div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:TextBox ID="TxtCountrydsbl" class="form-control input-lg" runat="server" placeholder="Country" ></asp:TextBox>
                <asp:DropDownList ID="ddlCountry" class="form-control input-lg" Visible="false"  runat="server" AutoPostBack="True" ></asp:DropDownList>
              <asp:RequiredFieldValidator ID="ddlCountryRequiredFieldValidator" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Please Select Country" ForeColor="Red" InitialValue="--Select country--"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:TextBox ID="TxtPortZonedsbl" class="form-control input-lg" runat="server" placeholder="Port Zone"></asp:TextBox>
                <asp:DropDownList ID="ddlPortZone" class="form-control input-lg" Visible="false"  runat="server"></asp:DropDownList>
              <asp:RequiredFieldValidator ID="ddlPortZoneRequiredFieldValidator" runat="server" ControlToValidate="ddlPortZone" ErrorMessage="Please enter the Port Zone" ForeColor="Red" InitialValue="--Select Port Zone--"></asp:RequiredFieldValidator>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12">
            <div class="padding-top-10 padding-bottom-10"><strong>Port Cargo Terminal Type</strong></div>
            <div class="radio inline-block padding-bottom-10 margin-right-20"">
              <label class="padding-left-0">
              <input type="radio" value="" runat="server" id="rbContainer1" name="select"  />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Container </label>
            </div>
            <div class="radio inline-block padding-bottom-10 margin-right-20"">
              <label class="padding-left-0">
              <input type="radio" value="" runat="server" id="rbNonContainer2" name="select" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Non Container </label>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-6">
              <h4>Port Terminal Number</h4>
            <div class="padding-bottom-15">
                <%--<asp:TextBox ID="TxtTerminalNumberdsbl" class="form-control input-lg" runat="server" ReadOnly="true"></asp:TextBox>--%>
              <asp:TextBox runat="server" class="form-control input-lg" MaxLength="3" ID="TxtTerminalNumber" placeholder="Cargo Terminal Number" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="TerminalNumberRequiredFieldValidator" runat="server" ControlToValidate="TxtTerminalNumber" ErrorMessage="Please enter the Terminal Number" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="TxtTerminalNumberValidator" ForeColor="Red" ControlToValidate="TxtTerminalNumber" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]?[0-9]+$"></asp:RegularExpressionValidator>
            </div>
          </div>
        </div>
        
		<h4>Port Address</h4>
		<div class="row">
          <%--<div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:TextBox ID="TxtPhoneNumberdsbl" class="form-control input-lg" runat="server" placeholder="Phone number"></asp:TextBox>
              <asp:TextBox runat="server" type="text" MaxLength="20" Visible="false" class="form-control input-lg phone" ID="TxtPhoneNumber" placeholder="Phone Number" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="PhoneNumberRequiredFieldValidator" runat="server" ControlToValidate="TxtPhoneNumber" ErrorMessage="Please Enter the Phone Number" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            </div>
			          <div class="col-md-6">

            <div class="padding-bottom-15">
                 <%--<asp:TextBox ID="TxtEmailiddsbl" class="form-control input-lg" runat="server" ReadOnly="true"></asp:TextBox>--%>
              <%--<asp:TextBox runat="server" type="text" MaxLength="50" class="form-control input-lg" ID="TxtEmailid" placeholder="Email Address" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailidRequiredFieldValidator" runat="server" ControlToValidate="TxtEmailid" ErrorMessage="Please Enter the Email ID" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			</div>--%>
			 <div class="col-md-6">
               <div class="padding-bottom-15">
                <%--<asp:TextBox ID="TxtBuildingNumberdsbl" class="form-control input-lg" runat="server" ReadOnly="true"></asp:TextBox>--%>
                <asp:TextBox runat="server" type="text" MaxLength="10" class="form-control input-lg" ID="TxtBuildingNumber" placeholder="Building Number" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="BuildingNumberRequiredFieldValidator" runat="server" ControlToValidate="TxtBuildingNumber" ErrorMessage="Please enter the Builing  Number" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
			 </div>
			 <div class="col-md-6">
            <div class="padding-bottom-15">
                <%--<asp:TextBox ID="TxtStreetdsbl" class="form-control input-lg" runat="server" ReadOnly="true"></asp:TextBox>--%>
              <asp:TextBox runat="server" type="text" MaxLength="100" class="form-control input-lg" ID="TxtStreet" placeholder="Street" />
                <asp:RequiredFieldValidator ID="StreetRequiredFieldValidator" runat="server" ControlToValidate="TxtStreet" ErrorMessage="Please enter the Street Name" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <%--<asp:TextBox ID="TxtCitydsbl" class="form-control input-lg" runat="server" ReadOnly="true"></asp:TextBox>--%>
              <asp:TextBox runat="server" type="text" MaxLength="40" class="form-control input-lg" ID="TxtCity" placeholder="City" />
                <asp:RequiredFieldValidator ID="CityRequiredFieldValidator" runat="server" ControlToValidate="TxtCity" ErrorMessage="Please enter the City Name " ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			</div>
			<div class="col-md-6">
            <div class="padding-bottom-15">
                <%--<asp:TextBox ID="TxtStatedsbl" class="form-control input-lg" runat="server" ReadOnly="true"></asp:TextBox>--%>
              <asp:TextBox runat="server" type="text" MaxLength="25" class="form-control input-lg" ID="TxtState" placeholder="State" />
                <asp:RequiredFieldValidator ID="StateRequiredFieldValidator" runat="server" ControlToValidate="TxtState" ErrorMessage="Please enter the State Name" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			</div>
			<div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:TextBox ID="TxtCountryadddsbl" class="form-control input-lg" runat="server" placeholder="Country"></asp:TextBox>
                <asp:DropDownList ID="ddlCountryadd" class="form-control input-lg" Visible="false" runat="server"></asp:DropDownList>
              <asp:RequiredFieldValidator ID="ddlCountryaddRequiredFieldValidator" runat="server" ControlToValidate="ddlCountryadd" ErrorMessage="Please select the Country" ForeColor="Red" InitialValue="--Select country--"></asp:RequiredFieldValidator>
            </div>
			</div>
			<div class="col-md-6">
            <div class="padding-bottom-15">
                <%--<asp:TextBox ID="TxtZipCodedsbl" class="form-control input-lg" runat="server" ReadOnly="true"></asp:TextBox>--%>
              <asp:TextBox runat="server" type="text" MaxLength="12" class="form-control input-lg" ID="TxtZipCode" placeholder="Postal Code/Zip Code" />
                <asp:RequiredFieldValidator ID="ZipCodeRequiredFieldValidator" runat="server" ControlToValidate="TxtZipCode" ErrorMessage="Please enter the Zip Code" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

          </div>
        </div>
		
        <div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10">
                <asp:Button ID="btnRegister" class="btn btn-primary btn-lg min-width-200" CausesValidation="false" runat="server" Text="Edit" OnClick="btnRegister_Click"  />
                <asp:Button ID="btnSave" Visible="false" class="btn btn-primary btn-lg min-width-200" runat="server" Text="Save" OnClick="btnSave_Click"  />
                <asp:Button ID="btnCancel" class="btn btn-default btn-lg" runat="server" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click"  />
                <!--<a href="#" class="btn btn-primary btn-lg min-width-200">Register</a> <a href="#" class="btn btn-default btn-lg">Cancel</a>--></div>
          </div>
        </div>
      </div>
     
    </div>
  </div>
</div>
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
</asp:Content>
