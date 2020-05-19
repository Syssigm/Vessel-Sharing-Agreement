<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PortRegistration.aspx.cs" Inherits="VesselSharingAgreement.PortRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" ClientIDMode="Static" runat="server">
    <div class="container banner-top-heading">
  <h3>Create Port</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-6 col-md-7">
        <h5 style="font:bold"><a style="color:red">*</a> Indicates field is mandatory</h5>
        <h4>Port Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" TabIndex="1" type="text" MaxLength="5" class="form-control input-lg" ID="TxtPortId" placeholder="Port Id" ></asp:TextBox><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="PortidRequiredFieldValidator" runat="server" ErrorMessage="Please Enter Port ID" ControlToValidate="TxtPortId" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			</div>
			          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" TabIndex="2" type="text" MaxLength="40" class="form-control input-lg" ID="TxtPortName" placeholder="Name of Port" ></asp:TextBox><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="PortNameRequiredFieldValidator" runat="server" ControlToValidate="TxtPortName" ErrorMessage="Please Enter the Port Name" ForeColor="Red"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="PortNameCharactersValidator" runat="server" ControlToValidate="TxtPortName" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Please enter only Alphabets." />
            </div>
			</div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlCountry" TabIndex="3" class="form-control input-lg"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList><a style="color:red">*</a>
              <!--<select class="form-control input-lg">
                <option selected="selected">Port Country</option>
                <option>India</option>
                <option>Qatar</option>
                <option>USA</option>
                <option>UK</option>
              </select>-->
                <asp:RequiredFieldValidator ID="ddlCountryRequiredFieldValidator" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Please Select Country" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlPortZone" TabIndex="4" class="form-control input-lg"  runat="server"></asp:DropDownList><a style="color:red">*</a>
              <!--<select class="form-control input-lg">
                <option selected="selected">Port Zone</option>
                <option>India</option>
                <option>Qatar</option>
                <option>USA</option>
                <option>UK</option>
              </select>-->
                <asp:RequiredFieldValidator ID="ddlPortZoneRequiredFieldValidator" runat="server" ControlToValidate="ddlPortZone" ErrorMessage="Please enter the Port Zone" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12">
            <div class="padding-top-10 padding-bottom-10"><strong>Port Cargo Terminal Type<a style="color:red">*</a></strong></div>
            <div class="radio inline-block padding-bottom-10 margin-right-20" tabindex="5">
              <label class="padding-left-0">
              <input type="radio" value="" tabindex="5" runat="server" id="rbContainer1" name="select"  />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Container </label>
            </div>
            <div class="radio inline-block padding-bottom-10 margin-right-20" tabindex="6">
              <label class="padding-left-0">
              <input type="radio" value="" tabindex="6" runat="server" id="rbNonContainer2" name="select" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Non Container </label>
            </div>
          </div>
        </div>
        <div class="row">
            <h4>Port Terminal Number</h4>
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" TabIndex="7" MaxLength="3" class="form-control input-lg" ID="TxtTerminalNumber" placeholder="Cargo Terminal Number ex:0xx" ></asp:TextBox><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="TerminalNumberRequiredFieldValidator" runat="server" ControlToValidate="TxtTerminalNumber" ErrorMessage="Please enter the Terminal Number" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="TxtTerminalNumberValidator" ForeColor="Red" ControlToValidate="TxtTerminalNumber" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]?[0-9]+$"></asp:RegularExpressionValidator>
            </div>
          </div>
        </div>
        
		<h4>Port Address</h4>
		<div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" TabIndex="8" type="text" MaxLength="20" class="form-control input-lg phone" ID="TxtPhoneNumber" placeholder="Phone Number" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="PhoneNumberRequiredFieldValidator" runat="server" ControlToValidate="TxtPhoneNumber" ErrorMessage="Please Enter the Phone Number" ForeColor="Red" InitialValue="+1"></asp:RequiredFieldValidator>
                <%--<asp:RegularExpressionValidator ID="ContactnumberValidator" runat="server" ValidationExpression="^\+?\d*$" ControlToValidate="TxtPhoneNumber" ForeColor="Red" ErrorMessage="Please Enter numbers only"></asp:RegularExpressionValidator>--%>
            </div>
            </div>
			          <div class="col-md-6">

            <div class="padding-bottom-15">
              <asp:TextBox runat="server" type="text" TabIndex="9" MaxLength="50" class="form-control input-lg" ID="TxtEmailid" placeholder="Email Address" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailidRequiredFieldValidator" runat="server" ControlToValidate="TxtEmailid" ErrorMessage="Please Enter the Email ID" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			            </div>
			          <div class="col-md-6">

					              <div class="padding-bottom-15">
              <asp:TextBox runat="server" type="text" MaxLength="10" TabIndex="10" class="form-control input-lg" ID="TxtBuildingNumber" placeholder="Building Number" ></asp:TextBox><a style="color:red">*</a>
                                      <asp:RequiredFieldValidator ID="BuildingNumberRequiredFieldValidator" runat="server" ControlToValidate="TxtBuildingNumber" ErrorMessage="Please enter the Builing  Number" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			            </div>
			          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" type="text" MaxLength="100" TabIndex="11" class="form-control input-lg" ID="TxtStreet" placeholder="Street" /><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="StreetRequiredFieldValidator" runat="server" ControlToValidate="TxtStreet" ErrorMessage="Please enter the Street Name" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" type="text" MaxLength="40" TabIndex="12" class="form-control input-lg" ID="TxtCity" placeholder="City" /><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="CityRequiredFieldValidator" runat="server" ControlToValidate="TxtCity" ErrorMessage="Please enter the City Name " ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			            </div>
			          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" type="text" MaxLength="25" TabIndex="13" class="form-control input-lg" ID="TxtState" placeholder="State" /><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="StateRequiredFieldValidator" runat="server" ControlToValidate="TxtState" ErrorMessage="Please enter the State Name" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
			            </div>
			          <div class="col-md-6">

            <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlCountryadd" class="form-control input-lg" TabIndex="14" runat="server"></asp:DropDownList><a style="color:red">*</a>

              <!--<select class="form-control input-lg">
                <option selected="selected">Country</option>
                <option>India</option>
                <option>Qatar</option>
                <option>USA</option>
                <option>UK</option>
              </select>-->
                <asp:RequiredFieldValidator ID="ddlCountryaddRequiredFieldValidator" runat="server" ControlToValidate="ddlCountryadd" ErrorMessage="Please select the Country" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
						            </div>
			          <div class="col-md-6">

            <div class="padding-bottom-15">
              <asp:TextBox runat="server" type="text" MaxLength="12" TabIndex="15" class="form-control input-lg" ID="TxtZipCode" placeholder="Postal Code/Zip Code" /><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="ZipCodeRequiredFieldValidator" runat="server" ControlToValidate="TxtZipCode" ErrorMessage="Please enter the Zip Code" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

          </div>
        </div>
		
        <div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10">
                <asp:Button ID="btnRegister" TabIndex="16" class="btn btn-primary btn-lg min-width-200" runat="server" Text="Create Port" OnClick="btnRegister_Click" />
                <asp:Button ID="btnCancel" TabIndex="17" class="btn btn-default btn-lg" runat="server" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click" />
                <!--<a href="#" class="btn btn-primary btn-lg min-width-200">Register</a> <a href="#" class="btn btn-default btn-lg">Cancel</a>--></div>
          </div>
        </div>
      </div>
      <div class="col-sm-6 col-md-5 hidden-xs"> <%--<img src="" class="img-responsive img-rounded img-thumbnail center-block" />--%> </div>
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

    <%--<script type="text/javascript">

        
        var inputALT = document.getElementById('TxtPhoneNumber');
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

        

        inputALT.addEventListener('input', checkInputTel);
        inputALT.addEventListener('keypress', res);
    </script>--%>


    <%--<script type="text/javascript">

        
        var TxtTerminalNumber = document.getElementById('TxtTerminalNumber');
        var goodKey = '0123456789';
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

        

        TxtTerminalNumber.addEventListener('input', checkInputTel);
        TxtTerminalNumber.addEventListener('keypress', res);
    </script>--%>
    
</asp:Content>
