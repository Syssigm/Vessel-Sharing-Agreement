<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ViewModifyCustomer.aspx.cs" Inherits="VesselSharingAgreement.ViewModifyCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" ClientIDMode="Static"  runat="server">
    
    <div class="container banner-top-heading">
  <h3>View/Modify Customer</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-6 col-md-8">
        <div class="row">
          <div class="col-md-6">
              <div>
                  <h4>Company Name</h4>
            <div class="padding-bottom-10">
              <asp:TextBox type="text" TabIndex="1" runat="server" id="TxtCompanyName" MaxLength="50" class="form-control input-lg" placeholder="Company Name" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="CompanyNameValidator" ControlToValidate="TxtCompanyName" runat="server" ForeColor="Red" ErrorMessage="Please Enter Company Name"></asp:RequiredFieldValidator>
            </div>
                  </div>
          </div>
            <div class="col-md-6">
              <div>
                  <h4>IMO Number</h4>
            <div class="padding-bottom-10">
              <asp:TextBox runat="server" type="text" MaxLength="7" class="form-control input-lg" ID="TxtIMOShipId" placeholder="IMO Ship Id ex:7474756" />
                <asp:RequiredFieldValidator ID="IMOShipIdRequiredFieldValidator" runat="server" ControlToValidate="TxtIMOShipId" ErrorMessage="Please enter IMO Ship Id" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
                  </div>
          </div>
          <div class="col-md-12">
            <h4>Customer Type  <asp:Label runat="server" ID="lblerrCustomerType" Font-Size="Large"></asp:Label></h4> 
            <div class="checkbox inline-block padding-bottom-10 margin-right-20" runat="server" tabindex="2">
                <label class="padding-left-0" >
                  <input type="checkbox" tabindex="2" runat="server" id="ckSSLine" value="" name="select" disabled="disabled">
                  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> SSLine </label>
                </div>
              <!--<label class="padding-left-0">
              <input type="checkbox" value="" runat="server"  name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> SSLine </label>-->
            
            <div class="checkbox padding-bottom-10 inline-block margin-right-20" runat="server" tabindex="3">
                <label class="padding-left-0">
                  <input type="checkbox" tabindex="3" runat="server" id="ckVeselOperator" value="" name="select" disabled="disabled">
                  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Vessel Operator </label>
              <!--<label class="padding-left-0"> <asp:CheckBox runat="server" />
              <input type="checkbox" value="" runat="server"  name="select">-->
              <!--<span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Vessel Operator </label>-->
            </div>
            <div class="checkbox inline-block margin-right-20" runat="server" tabindex="4">
                <label class="padding-left-0">
                  <input type="checkbox" tabindex="4" runat="server" id="ckCargOperator" value="" name="select" disabled="disabled">
                  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Cargo Operator </label>
              <!--<label class="padding-left-0">
              <input type="checkbox" value="" runat="server"  name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Cargo Operator </label>-->
            </div>
            <div class="checkbox inline-block margin-right-20" runat="server" tabindex="5">
                <label class="padding-left-0">
                  <input type="checkbox" tabindex="5" runat="server" id="ckAgent" value="" name="select" disabled="disabled">
                  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Agent </label>
              <!--<label class="padding-left-0">
              <input type="checkbox" value="" name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Agent</label>-->
            </div>
          </div>
        </div>
        <h4>ContContact Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox runat="server" TabIndex="6" ID="TxtFirstName" MaxLength="40" type="text" class="form-control input-lg" placeholder="First Name" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="FirstNameValidator" ControlToValidate="TxtFirstName" runat="server" ForeColor="Red" ErrorMessage="Please Enter First Name"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="FirstNameCharactersValidator" runat="server" ControlToValidate="TxtFirstName" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Valid characters: Alphabets and space." />
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-10">
              <asp:TextBox runat="server" TabIndex="7" ID="TxtLastName" MaxLength="40" type="text" class="form-control input-lg" placeholder="Last Name" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LastNameValidator" ControlToValidate="TxtLastName" runat="server" ForeColor="Red" ErrorMessage="Please Enter Last Name"></asp:RequiredFieldValidator><br />
    <asp:RegularExpressionValidator ID="LastNameCharacterValidator" runat="server" ControlToValidate="TxtLastName" ValidationExpression="[a-zA-Z ]*$" ForeColor="Red" ErrorMessage="*Valid characters: Alphabets and space." />
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" TabIndex="8" ID="TxtPhoneNumber" MaxLength="20" type="tel" class="form-control input-lg phone" placeholder="Phone Number" />
                <asp:RequiredFieldValidator ID="PhoneNumberNameValidator" ControlToValidate="TxtPhoneNumber" runat="server" ForeColor="Red" ErrorMessage="Please Enter the Phone Number" InitialValue="+1"></asp:RequiredFieldValidator><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtPhoneNumber" runat="server" ForeColor="Red" ErrorMessage="Please Enter the Phone Number" ></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ContactnumberValidator" runat="server" ValidationExpression="^\+?\d*$" ControlToValidate="TxtPhoneNumber" ForeColor="Red" ErrorMessage="Please Enter numbers only"></asp:RegularExpressionValidator>
            </div>
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" TabIndex="10" ID="TxtAltPhoneNumber" MaxLength="20" type="tel" class="form-control input-lg phone" placeholder="Alternate Phone Number" />
               <%--<asp:RegularExpressionValidator ID="ContactAltnumberValidator" runat="server" ValidationExpression="^\+?\d*$" ControlToValidate="TxtAltPhoneNumber" ForeColor="Red" ErrorMessage="Please Enter numbers only"></asp:RegularExpressionValidator>--%>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" ID="TxtEmailID" TabIndex="9" type="email" MaxLength="50" class="form-control input-lg" placeholder="Email ID" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailIDValidator" ControlToValidate="TxtEmailID" runat="server" ForeColor="Red" ErrorMessage="Please Enter Email ID"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="Emailvalidation"
              runat="server" ErrorMessage="Please Enter Valid Email ID"
                   ControlToValidate="TxtEmailID"
                  CssClass="requiredFieldValidateStyle"
                  ForeColor="Red"
                  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                  </asp:RegularExpressionValidator>
            </div>
            
          </div>
          
        </div>
        <div class="row">
          <div class="col-sm-12 padding-bottom-10">
            <h4>Customer Address</h4>
            <div class="checkbox inline-block margin-right-20">
              <%--<label class="padding-left-0">
              <input type="checkbox" value="" runat="server" class="bills" name="reg" checked="checked" id="reg" disabled="disabled" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Registered Address </label>--%>
            </div>
            <div class="link inline-block margin-right-20">
              <asp:LinkButton runat="server" TabIndex="11" ID="lnkaddbill" CommandArgument='<%#Eval("id")%>' OnClick="lnkaddbill_Click" ><span class="glyphicon"></span> Add Billing Address</asp:LinkButton>
            </div>
            <div class="link inline-block margin-right-20">
              <asp:LinkButton runat="server" TabIndex="12" ID="lnkadddba" OnClick="lnkadddba_Click" ><span class="glyphicon"></span> Add DBA Address</asp:LinkButton>
            </div>
          </div>
        </div>
        <div id="regaddress" class="gray-border padding-10 padding-bottom-0 margin-bottom-10 whiteBg img-rounded" runat="server">
          <div class="padding-bottom-15"><strong>Registered Address</strong></div>
          <div class="row">
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="13" ID="TxtBuildingNum" MaxLength="10" type="text" class="form-control input-lg" placeholder="Building Number" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="BuildingNumValidation" ForeColor="Red" runat="server" ControlToValidate="TxtBuildingNum" ErrorMessage="Please enter Building Number"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="14" ID="TxtStreet" MaxLength="100" type="text" class="form-control input-lg" placeholder="Street" />
                  <asp:RequiredFieldValidator ID="StreetValidation" ForeColor="Red" ControlToValidate="TxtStreet" runat="server" ErrorMessage="Please enter street Number/Name"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="15" ID="TxtCity" MaxLength="40" type="text" class="form-control input-lg" placeholder="City" />
                  <asp:RequiredFieldValidator ID="CityValidation" ForeColor="Red" ControlToValidate="TxtCity" runat="server" ErrorMessage="Please enter City"></asp:RequiredFieldValidator>
              </div>
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="17" ID="TxtState" MaxLength="25" type="text" class="form-control input-lg" placeholder="State" />
                  <asp:RequiredFieldValidator ID="StateValidation" ForeColor="Red" ControlToValidate="TxtState" runat="server" ErrorMessage="Please enter State"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="16" ID="TxtPostZip" MaxLength="12" type="text" class="form-control input-lg" placeholder="Postal Code/Zip Code" />
                  <asp:RequiredFieldValidator ID="PostZipValidation" ForeColor="Red" ControlToValidate="TxtPostZip" runat="server" ErrorMessage="Please enter Zip Code"></asp:RequiredFieldValidator>
              </div>
              <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlcountryReg" TabIndex="18" class="form-control input-lg" runat="server"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="countryRegValidation" ForeColor="Red" runat="server" ControlToValidate="ddlcountryReg"
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
        <div id="billaddress" class="gray-border padding-10 padding-bottom-0 padding-top-0 margin-bottom-10 whiteBg img-rounded" runat="server">
          <div class="padding-bottom-10"><strong>Billing Address</strong>
            <%--<div class="checkbox inline-block margin-left-20" id="sameBill" runat="server">
              <label class="padding-left-0">
                  <asp:CheckBox ID="sameBilladres" runat="server" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Same as Registered Address</label>
                
            </div>--%>
          </div>
          <div class="row">
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="19" ID="TxtBillBuildingNum" MaxLength="10" type="text" class="form-control input-lg" placeholder="Building Number" />
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="20" ID="TxtBillStreet" MaxLength="100" type="text" class="form-control input-lg" placeholder="Street" />
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="21" ID="TxtBillCity" MaxLength="40" type="text" class="form-control input-lg" placeholder="City" />
              </div>
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="23" ID="TxtBillState" MaxLength="25" type="text" class="form-control input-lg" placeholder="State" />
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="22" ID="TxtBillPostZip" MaxLength="12" type="text" class="form-control input-lg" placeholder="Postal Code/Zip Code" />
              </div>
              <div class="padding-bottom-15">
                  <asp:DropDownList ID="ddlcountryBill" TabIndex="24" class="form-control input-lg" runat="server"></asp:DropDownList>
                
              </div>
            </div>
          </div>
        </div>
        <div id="dbaaddress" class="gray-border padding-10 padding-bottom-0 padding-top-0 margin-bottom-10 whiteBg img-rounded" runat="server">
          <div class="padding-bottom-10"><strong>DBA</strong>
            <%--<div class="checkbox inline-block margin-left-20" id="samedba" runat="server">
              <label class="padding-left-0">
                  <asp:CheckBox ID="samedbaAddres"  runat="server" text="Same as Registered Address" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Same as Registered Address</label>
            </div>--%>
          </div>
          <div class="row">
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="25" ID="TxtDBABuildingNum" MaxLength="10" type="text" class="form-control input-lg" placeholder="Building Number" />
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="26" ID="TxtDBAStreet" MaxLength="100" type="text" class="form-control input-lg" placeholder="Street" />
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="27" ID="TxtDBACity" MaxLength="40" type="text" class="form-control input-lg" placeholder="City" />
              </div>
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="29" ID="TxtDBAState" MaxLength="25" type="text" class="form-control input-lg" placeholder="State" />
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="28" ID="TxtDBAPostZip" MaxLength="12" type="text" class="form-control input-lg" placeholder="Post Code/Zip Code" />
              </div>
              <div class="padding-bottom-15">
                  <asp:DropDownList ID="ddlCountrydba" TabIndex="30" class="form-control input-lg" runat="server"></asp:DropDownList>
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
                <asp:Button ID="btnEdit" runat="server" TabIndex="31" CausesValidation="false" class="btn btn-primary btn-lg min-width-200" Text="Edit" OnClick="btnEdit_Click" />&nbsp; 
                <asp:LinkButton ID="lnkSave" TabIndex="32" class="btn btn-primary btn-lg min-width-200" Visible="false" runat="server" CommandArgument='<%#Eval("CustomerID")%>' OnClick="lnkSave_Click"> Save </asp:LinkButton>
                <asp:Button class="btn btn-default btn-lg" TabIndex="33" Visible="false" ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" ></asp:Button>
                <asp:Button ID="Button1" runat="server" Text="Click" />
            </div>
          </div>
        </div>
      </div>
      <div class="col-sm-6  col-md-4 hidden-xs"> <%--<img src="" class="img-responsive img-thumbnail center-block" />--%> </div>
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
    <script type="text/javascript">
    

$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});


</script>
    
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
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
    $(function () {
        $("#Button1").bind("click", function () {
            var url = "http://localhost:50886/VesselApplication.aspx?FirstName=" + encodeURIComponent($("#TxtFirstName").val()) + "&LastName=" + encodeURIComponent($("#TxtLastName").val());
            window.location.href = url;
        });
    });
</script>
</asp:Content>
