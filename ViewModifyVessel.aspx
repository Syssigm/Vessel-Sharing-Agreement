<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ViewModifyVessel.aspx.cs" Inherits="VesselSharingAgreement.ViewModifyVessel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" ClientIDMode="Static" runat="server">
        <div class="container banner-top-heading">
  <h3>View/Modify Vessel</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-6 col-md-8">
          
	          <h4>Vessel Name</h4>
        <div class="row">
          <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:DropDownList ID="ddlvesselName" AutoPostBack="true" TabIndex="1" class="form-control input-lg" runat="server" OnSelectedIndexChanged="ddlvesselName_SelectedIndexChanged" ></asp:DropDownList>
                  <asp:TextBox runat="server" type="text" Visible="false" TabIndex="2" MaxLength="50" class="form-control input-lg" ID="TxtVesselName" placeholder="Name of Vessel" />
                  <asp:RequiredFieldValidator ID="VesselNameRequiredFieldValidator" runat="server" ControlToValidate="TxtVesselName" ErrorMessage="Please enter Vessel Name" ForeColor="Red"></asp:RequiredFieldValidator>
			</div>
              </div>
            <div class="col-md-6">
              
              <div class="padding-bottom-15">
                <asp:LinkButton ID="lnkSave" runat="server" Visible="false" TabIndex="16" class="btn btn-primary btn-lg min-width-100" Text="Save" OnClick="lnkSave_Click"></asp:LinkButton>
                <asp:Button ID="btnCancel" runat="server" TabIndex="17" class="btn btn-default btn-lg" Visible="false" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click" />
                <asp:Button ID="btnEdit" TabIndex="15" CausesValidation="false" class="btn btn-primary btn-lg min-width-200" runat="server" Text="Edit" OnClick="btnEdit_Click" />
			</div>
              </div>
            </div>
          
          <div class="row">
              <div class="col-md-6">
                  <h4 runat="server">IMO Number</h4>
            <div class="padding-bottom-15">
                <asp:TextBox runat="server" type="text" MaxLength="7" ReadOnly="true" TabIndex="3" class="form-control input-lg" ID="TxtIMOId" />
              <asp:TextBox runat="server" type="text" MaxLength="7" Visible="false" TabIndex="3" class="form-control input-lg" ID="TxtIMOShipId" />
                <asp:RequiredFieldValidator ID="IMOShipIdRequiredFieldValidator" runat="server" ControlToValidate="TxtIMOShipId" ErrorMessage="Please enter IMO Ship Id" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="TxtIMOShipIdValidator" ForeColor="Red" ControlToValidate="TxtIMOShipId" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]?[0-9]+$"></asp:RegularExpressionValidator>
            </div>
          </div>
              <div class="col-md-6">
                  <h4 runat="server">Operator Name</h4>
          <div class="padding-bottom-15">
              <asp:TextBox runat="server" type="text" TabIndex="4" ReadOnly="true" class="form-control input-lg" ID="TxtOperatorName" />
                <asp:DropDownList ID="ddlVesselOperator" Visible="false" TabIndex="4" class="form-control input-lg" runat="server"></asp:DropDownList>
			<asp:RequiredFieldValidator ID="ddlVesselOperatorRequiredFieldValidator" runat="server" ControlToValidate="ddlVesselOperator" ErrorMessage="Please select Vessel Operator" ForeColor="Red" InitialValue="Vessel Operators"></asp:RequiredFieldValidator>
            </div>

          </div>
        </div>
        <h4>Vessel Location Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:TextBox runat="server" type="text" TabIndex="5" ReadOnly="true" class="form-control input-lg" ID="Txtregcountry" />
                <asp:DropDownList ID="ddlregcountry" TabIndex="5" Visible="false" class="form-control input-lg" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="ddlregcountryRequiredFieldValidator" runat="server" ControlToValidate="ddlregcountry" ErrorMessage="Please select the Registered Country" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-md-6">
            <div class="padding-bottom-15">
                <asp:TextBox runat="server" type="text" TabIndex="6" ReadOnly="true" class="form-control input-lg" ID="TxtVesFlagCountry" />
                <asp:DropDownList ID="ddlVesFlagCountry" Visible="false" TabIndex="6" class="form-control input-lg" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="ddlVesFlagCountryRequiredFieldValidator" runat="server" ControlToValidate="ddlVesFlagCountry" ErrorMessage="Please select the Vessel Flag Country" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
          </div>
          <div class="col-xs-12">
		  <h4>Vessel Type  <asp:Label runat="server" ID="lblerrVesselType" Font-Size="Large"></asp:Label></h4>
		  <div class="row">
		  <div class="col-lg-4 col-md-5">
            <div class="checkbox inline-block padding-bottom-0" tabindex="7">
              <label class="padding-left-0">
              <input type="checkbox" tabindex="7" runat="server" id="ckContainerCargoVessel" value="" name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Container Cargo Vessel </label>
            </div>
			</div>
					  <div class="col-lg-7 col-md-7">

            <div class="checkbox inline-block padding-bottom-0" tabindex="8">
              <label class="padding-left-0">
              <input type="checkbox" tabindex="8" runat="server" id="ckRefrigeratedContainerCargo" value="" name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Refrigerated Container Cargo </label>
            &nbsp;</div>
			</div>
					  <div class="col-lg-4 col-md-5">
            <div class="checkbox inline-block padding-bottom-0" tabindex="9">
              <label class="padding-left-0">
              <input type="checkbox" tabindex="9" runat="server" id="ckCanCarryRORO" value="" name="select">
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Can Carry RORO</label>
            </div>
			</div>
					  <div class="col-lg-7 col-md-7">
            <div class="checkbox inline-block padding-bottom-0" tabindex="10">
              <label class="padding-left-0">
              <input type="checkbox" tabindex="10" runat="server" id="ckHazardousMaterialContainerCargo" name="select">
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
      <asp:TextBox runat="server" TabIndex="11" ReadOnly="true" class="form-control" ID="TxtTEUs" />
        <asp:RequiredFieldValidator ID="TEUsRequiredFieldValidator" runat="server" ControlToValidate="TxtTEUs" ErrorMessage="Please enter the TEUs" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="TxtTEUsvalidator" ControlToValidate="TxtTEUs" ForeColor="Red" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]?[0-9]+$"></asp:RegularExpressionValidator>
    </div>
  </div>
			  <div class="form-group form-group-lg">
    <label for="inputEmail3" class="col-md-6 control-label normal">Operating Tonnage Limit</label>
    <div class="col-md-6">
              <asp:TextBox runat="server" TabIndex="12" ReadOnly="true" class="form-control input-lg" ID="TxtOperatingTonnageLimit" />
              <asp:RequiredFieldValidator ID="OperatingTonnageLimitRequiredFieldValidator" runat="server" ControlToValidate="TxtOperatingTonnageLimit" ErrorMessage="Please enter the Operating Tonnage Limit" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="TxtOperatingTonnageLimitValidator" ForeColor="Red" ControlToValidate="TxtOperatingTonnageLimit" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]*\.?[0-9]+$"></asp:RegularExpressionValidator>
    </div>
  </div>
			  <div class="form-group form-group-lg">
    <label for="inputEmail3" class="col-md-6 control-label normal">Gross Tonnage Limit</label>
    <div class="col-md-6">
        <asp:TextBox runat="server" TabIndex="13" ReadOnly="true" class="form-control input-lg" ID="TxtGrossTonnageLimit" />
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
       var inputTxtGrossTonnageLimit = document.getElementById('TxtGrossTonnageLimit');
       var inputTxtOperatingTonnageLimit = document.getElementById('TxtOperatingTonnageLimit');
       var inputTxtIMOShipId = document.getElementById('TxtIMOShipId');

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

        inputTxtTEUs.addEventListener('input', checkInputTel);
        inputTxtTEUs.addEventListener('keypress', res);

        inputTxtGrossTonnageLimit.addEventListener('input', checkInputTel);
        inputTxtGrossTonnageLimit.addEventListener('keypress', res);

        inputTxtOperatingTonnageLimit.addEventListener('input', checkInputTel);
        inputTxtOperatingTonnageLimit.addEventListener('keypress', res);

        inputTxtIMOShipId.addEventListener('input', checkInputTel);
        inputTxtIMOShipId.addEventListener('keypress', res);
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