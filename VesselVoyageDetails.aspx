<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VesselVoyageDetails.aspx.cs" Inherits="VesselSharingAgreement.VesselVoyageDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" ClientIDMode="Static" runat="server">
        <div class="container banner-top-heading">
  <h3>Vessel Voyage Details (Ship Route)</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" style="text-align:center" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-12 form-horizontal">
        <h4>Vessel Voyage Details</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Id</label>
              <div class="col-md-8">
                <asp:DropDownList ID="ddlVesselId" TabIndex="1" class="form-control input-lg" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVesselId_SelectedIndexChanged"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="ddlVesselIdRequiredFieldValidator" runat="server" ControlToValidate="ddlVesselId" ErrorMessage="Please Select the Vessel ID" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Capacity in TEUs</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtVesselTEUs" type="text" class="form-control"  ReadOnly="True" />
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Registered Country</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtRegCountry" type="text" class="form-control"  ReadOnly="True" />
              </div>
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Name</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" type="text" ID="TxtVesselName" class="form-control"  ReadOnly="True" /><br />
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Flag Country</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtFlagCountry" type="text" class="form-control"  ReadOnly="True" />
              </div>
            </div>
              <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Operating tonnage</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtOperatingtonnage" type="text" class="form-control"  ReadOnly="True" />
              </div>
            </div>
          </div>
        </div>
          <div class="row">
          <div class="col-xs-12">
            <h5 style="font:bold"><a style="color:red">*</a> Indicates field is mandatory</h5>
            <h4>Allowable Vessel Container Types&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Label runat="server" ID="lblContainerTypeMsg" Font-Size="Large" ForeColor="Red"></asp:Label></h4>
            <div class="row">
              <div class="col-md-12 col-lg-11 col-lg-offset-1">
                <div class="checkbox padding-bottom-10 padding-right-50 inline-block" tabindex="2">
                  <label class="padding-left-0">
                  <input type="checkbox" tabindex="2" runat="server" id="ckContainerCargoVessel" value="" name="select">
                  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Container Cargo Vessel </label>
                </div>
                <div class="checkbox padding-bottom-10 padding-right-50 inline-block" tabindex="3">
                  <label class="padding-left-0">
                  <input type="checkbox" runat="server" tabindex="3" id="ckRefrigeratedContainerCargo" value="" name="select">
                  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Refrigerated Container Cargo </label>
                </div>
                <div class="checkbox padding-bottom-10 inline-block" tabindex="4">
                  <label class="padding-left-0">
                  <input type="checkbox" value="" tabindex="4" runat="server" id="ckHazardousMaterial" name="select">
                  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Hazardous Material Container Cargo</label>
                </div>
              </div>
            </div>
          </div>
		</div>
          <br />
        <div class="row">
          <div class="col-md-6">
              <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Voyage Id</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtVoyageId" MaxLength="12" TabIndex="5" type="text" class="form-control" placeholder="Voyage Id" /><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="VoyageIdRequiredFieldValidator" runat="server" ErrorMessage="Please enter the Voyage Id" ControlToValidate="TxtVoyageId" ForeColor="Red"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="form-group form-group-lg" runat="server" id="Destination">
              <label for="inputEmail3" class="col-md-4 control-label normal">Destination Port</label>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlDestination" TabIndex="7" class="form-control input-lg" runat="server"></asp:DropDownList><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="ddlDestinationRequiredFieldValidator" runat="server" ControlToValidate="ddlDestination" ErrorMessage="Please select Destination Port" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <!--<select class="form-control input-lg">
                  <option selected="selected">Select</option>
                  <option>ABC</option>
                  <option>XYZ</option>
                  <option>Etc</option>
                </select>-->
              </div>
            </div>
              <div class="form-group form-group-lg" runat="server" visible="false" id="extddlDestination">
              <label for="inputEmail3" class="col-md-4 control-label normal">Destination Port</label>
              <div class="col-md-8">
                  <asp:TextBox runat="server" Visible="false" type="text" ID="TxtddlDestination" class="form-control" placeholder="" />
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Gross Tonnage of Ship at Origin</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" value="" TabIndex="9" ID="TxtGrossTonnage" class="form-control" placeholder="Gross Tonnage" />
                  <asp:RequiredFieldValidator ID="GrossTonnageRequiredFieldValidator" runat="server" ControlToValidate="TxtGrossTonnage" ErrorMessage="Please enter the Gross Tonnage of Ship at Origin" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="TxtGrossTonnageValidator" ForeColor="Red" ControlToValidate="TxtGrossTonnage" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]*\.?[0-9]+$"></asp:RegularExpressionValidator>
              </div>
            </div>
          </div>
            <div class="col-md-6">
                <div class="form-group form-group-lg" runat="server" id="Origin">
              <label for="inputEmail3" class="col-md-4 control-label normal">Origin Port</label>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlOrigin" TabIndex="6" class="form-control input-lg" runat="server"></asp:DropDownList><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="ddlOriginRequiredFieldValidator" runat="server" ControlToValidate="ddlOrigin" ErrorMessage="Please select Origin Port" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <!--<select class="form-control input-lg">
                  <option selected="selected">Select</option>
                  <option>ABC</option>
                  <option>XYZ</option>
                  <option>Etc</option>
                </select>-->
              </div>
            </div>
                <div class="form-group form-group-lg" visible="false" runat="server" id="extddlOriginport">
              <label for="inputEmail3" class="col-md-4 control-label normal">Origin Port</label>
              <div class="col-md-8">
                  <asp:TextBox runat="server" Visible="false" type="text" ID="TxtddlOriginport" class="form-control" placeholder="" />
              </div>
            </div>
                <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Containers Loaded at Origin (In TEU)</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" value="" TabIndex="8" ID="TxtContainersTEUs" class="form-control" placeholder="TEUs" /><a style="color:red">*</a>
                  <asp:RequiredFieldValidator ID="ContainersTEUsRequiredFieldValidator" runat="server" ControlToValidate="TxtContainersTEUs" ErrorMessage="Please enter Containers Loaded at Origin " ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="TxtContainersTEUsValidator" ForeColor="Red" ControlToValidate="TxtContainersTEUs" runat="server" ErrorMessage="Please enter the numbers only" ValidationExpression="^[0-9]?[0-9]+$"></asp:RegularExpressionValidator>
              </div>
            </div>
                
            </div>
        </div>
          <div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10 text-center">
                <asp:Button ID="btncreate" runat="server" TabIndex="10" OnClientClick="ConfirmExpiry()" class="btn btn-primary btn-lg min-width-200" Text="Create Voyage" OnClick="btncreate_Click"  /> <asp:Button ID="btnCancel" TabIndex="11" class="btn btn-default btn-lg" runat="server" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click" /></div>
          </div>
        </div><br />
          <h4>Voyage Id</h4>
          <div class="row">
              <div class="col-md-6">
                <div class="form-group form-group-lg">
                    <%--<label for="inputEmail3" class="col-md-2 control-label normal">Voyage Id</label>--%>
                <div class="col-md-8">
                <asp:DropDownList ID="ddlVoyageId" TabIndex="12" class="form-control" DataTextField="Select VoyageID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVoyageId_SelectedIndexChanged" ></asp:DropDownList>
                <!--<input type="text" class="form-control" placeholder="Voyage Id" />-->
              </div>
            </div>
         </div>
       </div>
        <div class="row">
          <div class="col-md-12">
            <h4 class="margin-top-20">Ship Route Details Entry&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Label runat="server" ID="lblshiproutemsg" Font-Size="Large"></asp:Label></h4>
            <div class="table-responsive">
              <table class="table table-bordered">
                <thead>
                  <tr class="bg-primary">
                    <th>Voyage Sequence#</th>
                    <th>Origin / Transit port</th>
                    <th>Expected Start Date</th>
                    <th>Expected Start Time</th>
                    <th>Transit / Destination Port</th>
                    <th>Expected Arrival Date</th>
                    <th>Expected Arrival Time</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rpt"  onitemdatabound="rpt_ItemDataBound">
                        <ItemTemplate>
				                  <tr>
                    <th scope="row">
                        <asp:Label ID="lblVoyageSegmentSequenceNum" runat="server" Text='<%# Eval("VoyageSegmentSequenceNumber") %>'></asp:Label>
                        <asp:TextBox Text='<%# Eval("VoyageSegmentSequenceNumber") %>' ID="edtVoyageSegmentSequenceNum" ReadOnly="true" Visible="false" class="form-control" runat="server"></asp:TextBox></th>
                    <td><asp:Label ID="lblOriginTransitPortID" runat="server" Text='<%# Eval("OriginTransitPortID") %>'></asp:Label>
                        <asp:TextBox Text='<%# Eval("OriginTransitPortID") %>' ID="edtOriginTransitPortID" Visible="false" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:Label ID="lblExpectedStartDate" runat="server" Text='<%# Eval("ExpectedStartDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                        <asp:TextBox Text='<%# Eval("ExpectedStartDate","{0:dd/MM/yyyy}") %>' ID="edtExpectedStartDate" Visible="false" class="form-control date" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:Label ID="lblExpectedStartTime" runat="server" Text='<%# Eval("ExpectedStartTime","{0:hh:mm tt}") %>'></asp:Label>
                        <asp:TextBox Text='<%# Eval("ExpectedStartTime","{0:hh:mm tt}") %>' ID="edtExpectedStartTime" Visible="false" class="form-control time" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:Label ID="lblDestinationTransitPortID" runat="server" Text='<%# Eval("DestinationTransitPortID") %>'></asp:Label>
                        <asp:DropDownList ID="rptddldestintnportid" Visible="false" class="form-control" runat="server"></asp:DropDownList>
                        <!--<asp:TextBox Text='<%# Eval("DestinationTransitPortID") %>' ID="edtDestinationTransitPortID" Visible="false" class="form-control" runat="server"></asp:TextBox>-->
                    </td>
                    <td><asp:Label ID="lblExpectedArrivalDate" runat="server" Text='<%# Eval("ExpectedArrivalDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                        <asp:TextBox Text='<%# Eval("ExpectedArrivalDate","{0:dd/MM/yyyy}") %>' ID="edtExpectedArrivalDate" Visible="false" class="form-control date" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:Label ID="lblExpectedArrivalTime" runat="server" Text='<%# Eval("ExpectedArrivalTime","{0:hh:mm tt}") %>'></asp:Label>
                        <asp:TextBox Text='<%# Eval("ExpectedArrivalTime","{0:hh:mm tt}") %>' ID="edtExpectedArrivalTime" Visible="false" class="form-control time" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:LinkButton runat="server" ID="lnkedit" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' OnClick="btnedit_Click" class="btn btn-info"><span class="glyphicon glyphicon-pencil"></span> Edit</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkupdate" Visible="false" OnClick="btnupdate_Click" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' class="btn btn-success"><span class="glyphicon glyphicon-floppy-disk"></span> Save</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkecancel" Visible="false" OnClick="btnecancel_Click" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span> Cancel</asp:LinkButton>
                        <%--<asp:ImageButton ID="lnkedit" ImageUrl="~/images/Editicon.jpg" Height="34px" Width="32px" runat="server" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' OnClick="btnedit_Click" />--%>
                        <%--<asp:ImageButton ID="lnkupdate" Visible="false" ImageUrl="~/images/save.jpg" Height="34px" Width="32px" class="btn btn-xs btn-info" runat="server" OnClick="btnupdate_Click" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' />--%>
                        <%--<asp:ImageButton ID="lnkecancel" Visible="false" ImageUrl="~/images/cancel.jpg" Height="34px" Width="32px" class="btn btn-xs btn-info" runat="server" OnClick="btnecancel_Click" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' />--%>
                    </td>
                  </tr>
                            </ItemTemplate>
                    </asp:Repeater>
                  <tr runat="server" id="insrtrow" visible="false">
                    <th scope="row"><input type="number" min="1" visible="false" readonly="true" runat="server" id="TxtVoyageSequence" class="form-control max-width-70" /></th>
                    <td><asp:TextBox runat="server" Visible="false" ReadOnly="true" ID="TxtOriginportid" type="text" class="form-control" /></td>
                    <td><asp:TextBox runat="server" Visible="false" ID="TxtStartDate" type="text" class="form-control date" /></td>
                    <td><asp:TextBox runat="server" Visible="false" ID="TxtStartTime" type="text" class="form-control time" /></td>
                    <td><asp:DropDownList ID="ddldestintnportid" Visible="false" class="form-control" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox runat="server" Visible="false" ID="TxtArrivalDate" type="text" class="form-control date" /></td>
                    <td><asp:TextBox runat="server" Visible="false" ID="TxtArrivalTime" type="text" class="form-control time" /></td>
                    <td>
                        <asp:LinkButton runat="server" id="btninsert" TabIndex="25" class="btn btn-primary" Visible="false" OnClick="btninsert_Click" ><span class="glyphicon glyphicon-plus"></span> Add</asp:LinkButton>
                        <%--<asp:ImageButton ID="lnkedit" ImageUrl="~/images/Editicon.jpg" Height="34px" Width="32px" runat="server" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' OnClick="btnedit_Click" />--%>                        <%--<asp:ImageButton ID="lnkupdate" Visible="false" ImageUrl="~/images/save.jpg" Height="34px" Width="32px" class="btn btn-xs btn-info" runat="server" OnClick="btnupdate_Click" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' />--%>                        
                        <%--<asp:ImageButton ID="lnkecancel" Visible="false" ImageUrl="~/images/cancel.jpg" Height="34px" Width="32px" class="btn btn-xs btn-info" runat="server" OnClick="btnecancel_Click" CommandArgument='<%#Eval("VoyageSegmentSequenceNumber")%>' />--%><!--<a runat="server" id="btnplus" class="btn btn-xs btn-info"><span class="fa fa-plus"></span></a>-->

                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
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
<script src="js/jquery.datetimepicker.js"></script>

    <script>
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

    </script>

<script>
$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});
</script>
    <script src="js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type = "text/javascript">
        
           
        function ConfirmExpiry() {
            if ($('input[id$=ckHazardousMaterial]').is(':checked')) {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("Vessel voyage contains Hazardous Material Do you want to continue ?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";

                    }
                    document.forms[0].appendChild(confirm_value);
                }
            }
    </script>


    <%--<script type="text/javascript">
        var inputTxtContainersTEUs = document.getElementById('TxtContainersTEUs');
        var inputTxtGrossTonnage = document.getElementById('TxtGrossTonnage');
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

        inputTxtContainersTEUs.addEventListener('input', checkInputTel);
        inputTxtContainersTEUs.addEventListener('keypress', res);

        inputTxtGrossTonnage.addEventListener('input', checkInputTel);
        inputTxtGrossTonnage.addEventListener('keypress', res);
    </script>--%>
    <script>
        $("#TxtVoyageId").click("click", function () {
            if (($('#ckContainerCargoVessel').prop('checked') == false) && ($('#ckRefrigeratedContainerCargo').prop('checked') == false) && ($('#ckHazardousMaterial').prop('checked') == false))
            {
               $('#lblContainerTypeMsg').text("Please Select atleast one container type").css("color", "red")
           }
           else
           {
               $('#lblContainerTypeMsg').text("");
           }
            
        })
    </script>
</asp:Content>
