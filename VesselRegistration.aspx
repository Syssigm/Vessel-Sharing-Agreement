<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VesselRegistration.aspx.cs" Inherits="VesselSharingAgreement.VesselRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" ClientIDMode="Static" runat="server">
        <div class="container banner-top-heading">
  <h3>Vessel Registration</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" Font-Size="Larger" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-6 col-md-8">
       <h5 style="font:bold"><a style="color:red">*</a> Indicates field is mandatory</h5>
	          <h4>Vessel Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" TabIndex="1" type="text" MaxLength="12" class="form-control input-lg" ID="TxtVesselId" placeholder="Vessel Id" autofocus /><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="VesselIdRequiredFieldValidator" runat="server" ControlToValidate="TxtVesselId" ErrorMessage="Please enter the Vessel ID" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
              </div>
            <div class="col-md-6">
                <div class="padding-bottom-15">
                <asp:TextBox runat="server" type="text" TabIndex="2" MaxLength="50" class="form-control input-lg" ID="TxtVesselName" placeholder="Name of Vessel" /><a style="color:red">*</a>
                            <asp:RequiredFieldValidator ID="VesselNameRequiredFieldValidator" runat="server" ControlToValidate="TxtVesselName" ErrorMessage="Please enter Vessel Name" ForeColor="Red"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="FirstNameCharactersValidator" runat="server" ControlToValidate="TxtVesselName" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Please enter only Alphabets." />
                </div>
			            
            </div>
            </div>
          <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-15">
              <asp:TextBox runat="server" type="text" MaxLength="7" TabIndex="3" class="form-control input-lg" ID="TxtIMOShipId" placeholder="IMO Ship Id ex:7475776" /><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="IMOShipIdRequiredFieldValidator" runat="server" ControlToValidate="TxtIMOShipId" ErrorMessage="Please enter IMO Ship Id" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="TxtIMOShipIdValidator" ForeColor="Red" ControlToValidate="TxtIMOShipId" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]?[0-9]+$"></asp:RegularExpressionValidator>
            </div>
              </div>
              <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlVesselOperator" TabIndex="4" class="form-control input-lg" runat="server"></asp:DropDownList><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="ddlVesselOperatorRequiredFieldValidator" runat="server" ControlToValidate="ddlVesselOperator" ErrorMessage="Please select Vessel Operator" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
			</div>
        </div>
        <h4>Vessel Registration Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlregcountry" TabIndex="6" class="form-control input-lg" runat="server"></asp:DropDownList><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="ddlregcountryRequiredFieldValidator" runat="server" ControlToValidate="ddlregcountry" ErrorMessage="Please select the Registered Country" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlVesFlagCountry" TabIndex="7" class="form-control input-lg" runat="server"></asp:DropDownList><a style="color:red">*</a>
                <asp:RequiredFieldValidator ID="ddlVesFlagCountryRequiredFieldValidator" runat="server" ControlToValidate="ddlVesFlagCountry" ErrorMessage="Please select the Vessel Flag Country" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-xs-12">
		  <h4>Vessel Type<a style="color:red">*</a>  <asp:Label runat="server" ID="lblerrVesselType" Font-Size="Large"></asp:Label></h4>
		  <div class="row">
		  <div class="col-lg-4 col-md-5">
            <div class="checkbox inline-block padding-bottom-0" tabindex="8">
              <label class="padding-left-0">
              <input type="checkbox" tabindex="8" runat="server" id="ckContainerCargoVessel" value="1" name="select" validate="required:true, minlength:1">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Container Cargo Vessel </label>
            </div>
			</div>
					  <div class="col-lg-7 col-md-7">

            <div class="checkbox inline-block padding-bottom-0" tabindex="9">
              <label class="padding-left-0">
              <input type="checkbox" tabindex="9" runat="server" id="ckRefrigeratedContainerCargo" value="2" name="select" validate="required:true, minlength:1" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Refrigerated Container Cargo </label>
            </div>
			</div>
					  <div class="col-lg-4 col-md-5">
            <div class="checkbox inline-block padding-bottom-0" tabindex="10">
              <label class="padding-left-0">
              <input type="checkbox" tabindex="10" runat="server" id="ckCanCarryRORO" value="3" name="select" validate="required:true, minlength:1">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Can Carry RORO</label>
            </div>
			</div>
					  <div class="col-lg-7 col-md-7">
            <div class="checkbox inline-block padding-bottom-0" tabindex="11">
              <label class="padding-left-0">
              <input type="checkbox" tabindex="11" value="4" runat="server" id="ckHazardousMaterialContainerCargo" name="select" validate="required:true, minlength:1">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Hazardous Material Container Cargo</label>
            </div>
            </div>
            </div>
          </div>
		            <div class="col-md-12">
					
            <div class="form-horizontal">
					  <h4 class="margin-top-20">Vessel Capacity</h4>

			  <div class="form-group form-group-lg">
    <label for="inputEmail3" class="col-md-6 control-label normal">TEUs (Twenty Foot Equivalent Units)</label>
    <div class="col-md-6">
      <asp:TextBox runat="server" TabIndex="12" value="" class="form-control" ID="TxtTEUs" placeholder="TEUs" /><a style="color:red">*</a>
        <asp:RequiredFieldValidator ID="TEUsRequiredFieldValidator" runat="server" ControlToValidate="TxtTEUs" ErrorMessage="Please enter the TEUs" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="TxtTEUsvalidator" ControlToValidate="TxtTEUs" ForeColor="Red" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]?[0-9]+$"></asp:RegularExpressionValidator>
    </div>
  </div>
                <div class="form-group form-group-lg">
    <label for="inputEmail3" class="col-md-6 control-label normal">Operating Tonnage Limit</label>
    <div class="col-md-6">
              <asp:TextBox runat="server" TabIndex="13" value="" class="form-control input-lg" ID="TxtOperatingTonnageLimit" placeholder="Operating Tonnage Limit" /><a style="color:red">*</a>
              <asp:RequiredFieldValidator ID="OperatingTonnageLimitRequiredFieldValidator" runat="server" ControlToValidate="TxtOperatingTonnageLimit" ErrorMessage="Please enter the Operating Tonnage Limit" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="TxtOperatingTonnageLimitValidator" ForeColor="Red" ControlToValidate="TxtOperatingTonnageLimit" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]*\.?[0-9]+$"></asp:RegularExpressionValidator>
    </div>
  </div>
			  <div class="form-group form-group-lg">
    <label for="inputEmail3" class="col-md-6 control-label normal">Gross Tonnage Limit</label>
    <div class="col-md-6">
              <asp:TextBox runat="server" TabIndex="14" value="" class="form-control input-lg" ID="TxtGrossTonnageLimit" placeholder="Gross Tonnage Limit" /><a style="color:red">*</a>
              <asp:RequiredFieldValidator ID="GrossTonnageLimitRequiredFieldValidator" runat="server" ControlToValidate="TxtGrossTonnageLimit" ErrorMessage="Please enter the Gross Tonnage Limit" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="TxtGrossTonnageLimitValidator" ForeColor="Red" ControlToValidate="TxtGrossTonnageLimit" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]*\.?[0-9]+$"></asp:RegularExpressionValidator>
    </div>
  </div>
			  

              
            </div>

          </div>


        </div>
        <div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10">
                <asp:Button ID="btnRegister" TabIndex="15" class="btn btn-primary btn-lg min-width-200" runat="server" Text="Create Vessel" OnClick="btnRegister_Click" />
                <asp:Button ID="btnCancel" runat="server" TabIndex="16" class="btn btn-default btn-lg" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click" />
                  </div>
          </div>
        </div>
      </div>
      <div class="col-sm-6  col-md-4 hidden-xs"> <%--<img src="" class="img-responsive img-rounded img-thumbnail center-block" />--%> </div>
    </div>
  </div>
</div>
    <!-- jQuery Js -->
<script src="js/jquery.min.js"></script>
<!-- Bootstrap Js -->
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.bootstrap-responsive-tabs.min.js"></script>
<script>
$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});


</script>

    <%--<script type="text/javascript">
        var inputTxtTEUs = document.getElementById('TxtTEUs');
        var inputTxtIMOShipId = document.getElementById('TxtIMOShipId');

        
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

        inputTxtTEUs.addEventListener('input', checkInputTel);
        inputTxtTEUs.addEventListener('keypress', res);

        inputTxtIMOShipId.addEventListener('input', checkInputTel);
        inputTxtIMOShipId.addEventListener('keypress', res);

        
                
        </script>
    <script type="text/javascript">
        

        var inputTxtGrossTonnageLimit = document.getElementById('TxtGrossTonnageLimit');
        var inputTxtOperatingTonnageLimit = document.getElementById('TxtOperatingTonnageLimit');

        var goodKey = '0123456789.';
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

        inputTxtGrossTonnageLimit.addEventListener('input', checkInputTel);
        inputTxtGrossTonnageLimit.addEventListener('keypress', res);

        inputTxtOperatingTonnageLimit.addEventListener('input', checkInputTel);
        inputTxtOperatingTonnageLimit.addEventListener('keypress', res);

        

    </script>--%>
    <script>
        $("#TxtTEUs").click("click", function () {
           if (($('#ckContainerCargoVessel').prop('checked') == false) && ($('#ckRefrigeratedContainerCargo').prop('checked') == false) && ($('#ckCanCarryRORO').prop('checked') == false) && ($('#ckHazardousMaterialContainerCargo').prop('checked') == false))
            {
               $('#lblerrVesselType').text("Please Select atleast one container type").css("color", "red")
           }
           else
           {
               $('#lblerrVesselType').text("");
           }
            
        })
    </script>
    
</asp:Content>
