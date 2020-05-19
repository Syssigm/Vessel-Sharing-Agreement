<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VesselApplication.aspx.cs" Inherits="VesselSharingAgreement.VesselApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container banner-top-heading">
  <h3>Application for Vessel Sharing</h3>
</div>
    <div align="center">
        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
    </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-12 form-horizontal">
        <h4>Apply for Vessel Sharing to Transport Container Cargo</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Id</label>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlvesselid" class="form-control" AutoPostBack="true" DataTextField="Vessel Id" runat="server" OnSelectedIndexChanged="ddlvesselid_SelectedIndexChanged"></asp:DropDownList>
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Name</label>
              <div class="col-md-8">
                <asp:TextBox type="text" runat="server" id="TxtVeselName" class="form-control"  disabled="disabled" />
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Origin Port</label>
              <div class="col-md-8">
                <asp:TextBox type="text" runat="server" id="TxtOrigiPort" class="form-control"  disabled="disabled" />
              </div>
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Voyage Id</label>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlvoyageid" class="form-control" runat="server" OnSelectedIndexChanged="ddlvoyageid_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Capacity in TEUs</label>
              <div class="col-md-8">
                <asp:TextBox type="text" runat="server" id="TxtCapacityTEUs" class="form-control" disabled="disabled" />
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Destination Port</label>
              <div class="col-md-8">
                <asp:TextBox type="text" class="form-control" runat="server" id="TxtDestinatioPort" disabled="disabled" />
              </div>
            </div>
          </div>
        </div>
        
		<hr />
          <%--<h4>Retrieve Applications By Status</h4>
          <div class="col-md-6">
              <div class="form-group form-group-lg">
              
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlRoutesapplied" class="form-control" runat="server" OnSelectedIndexChanged="ddlRoutesapplied_SelectedIndexChanged" AutoPostBack="True">
                      <asp:ListItem Selected="True">Select</asp:ListItem>
                      <asp:ListItem>Available Routes for VSA App</asp:ListItem>
                      <asp:ListItem>Applied VSA Routes</asp:ListItem>
                  </asp:DropDownList>
              </div>
            </div>
              </div>--%>
              <%--<div class="col-md-6">
              <div class="form-group form-group-lg">
              
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlapplyRoute" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlapplyRoute_SelectedIndexChanged"></asp:DropDownList>
              </div>
            </div>
          </div>--%>
        <div class="row">
          <div class="col-md-12">
			<h4>Application for Vessel Sharing Participation&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Label ID="lblapplyMsg" runat="server" Font-Size="Large" Text=""></asp:Label></h4>
			
			<div class="padding-top-10">
  <!-- Nav tabs -->

                <style>
                    .mytbl table tr td, .mytbl table tr th {
                        padding: 0 5px!important;
                        font-size: 16px;
                    }

                </style>
              <ul class="nav nav-tabs responsive-tabs inner-tabs">
				<li role="presentation" class="active"><a href="#voyage" class="bold"><span class="label label-success">1</span> <img src="images/Voyage-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> Voyage Invitation Details</a></li> 
				<li role="presentation"><a href="#teus" class="bold">
                    
                   <div class="inline-block" style="vertical-align: top;"> <span class="label label-success">2</span> <img src="images/TEUs-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> TEUs </div>
                   <div class="inline-block mytbl"><table class="table table-bordered table-condensed margin-bottom-0">
                       <tr>
                           <th></th>
                            <th style="color:black" >Net Load</th>
                            <th style="color:black" >Net Dis</th>
                            <th style="color:black" >Avl Dis</th>
                            <th style="color:black" >On Ship</th>
                       </tr>
                        <tr>
                           <td style="color:black" >40'</td>
                            <td><asp:Label ID="lblLoad40sum" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblDisc40sum" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblAvlDischarge40" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblOnship40" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                       </tr>
                        <tr>
                           <td style="color:black" >20'</td>
                            <td><asp:Label ID="lblLoad20sum" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblDisc20sum" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblAvlDischarge20" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblOnship20" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                       </tr>


                   </table>
                       </div>

				                        </a></li>
				<li role="presentation"><a href="#charges" class="bold"><span class="label label-success">3</span> <img src="images/Price-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> Charges</a></li>
              </ul>

  <!-- Tab panes -->
  <div class="tab-content">
  <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10 active" id="voyage">
	<div class="table-responsive">
              <table class="table table-bordered table-striped" id="tblVoyageinvitation">
                      <thead>
                        <tr class="bg-primary">
                          <th class="max-width-90">Voyage Sequence#</th>
                          <th>Origin / Transit port</th>
                          <th class="max-width-100">Inviting vessel sharing Participants? <i class="fa fa-info-circle" title="Cargo Operators, Agents, Vessel Operators etc."></i></th>
                          <th class="max-width-90 text-center">Total Number of VSA Participants Allowed</th>
                          <th>VSA Notes</th>
                        </tr>
                      </thead>
                      <tbody>
                          <asp:Repeater runat="server" ID="apprpt"><ItemTemplate>
                        <tr>
                          <th scope="row">
                              <asp:Label ID="lblVoyageSegmentSequenceNum" runat="server" Text='<%# Eval("VoyageSegmentSequencenumber") %>'></asp:Label></th>
                          <td>
                              <asp:Label ID="lblshiporign" runat="server" Text='<%# Eval("shiporign") %>'></asp:Label>
                              
                          </td>
                          <td><asp:Label ID="lblinvitation" runat="server" Text='<%# Eval("InviteVSAParticipantsFlag") %>'></asp:Label></td>
                          <td class="text-center"><%# Eval("MaxNumberofParticipants") %></td>
                          <td><asp:TextBox ID="TxtVSAnotestodisplay" TextMode="MultiLine" Height="80px" Width="440px" ReadOnly="true" runat="server" Text='<%# Eval("VSANotes") %>'></asp:TextBox></td>
                        </tr>
                    </ItemTemplate></asp:Repeater>
                      </tbody>
                    </table>
            </div>	
	</div>
    <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10" id="teus">
	<div class="table-responsive">
              <table class="table table-bordered table-striped" id="tblApplyTEUS">
                <thead>
                  <tr class="bg-primary">
                    <th class="max-width-90">Voyage Sequence#</th>
                    <th class="min-width-250">Origin / Transit port</th>
                    <th align="center" class="max-width-90">Available free space in TEUs</th>
                    <%--<th colspan="3" align="center" class="max-width-160"><div class="text-center">Containers Cargo Operator wants to Load</div></th>
                    <th colspan="3" align="center" class="max-width-160"><div class="text-center">Containers Cargo Operator wants to Discharge</div></th>--%>
                  <%--</tr>--%>
                  <%--<tr class="bg-primary no-border">--%>
                    <th align="center" class="max-width-120"> Operator wants to Load 40'</th>
                    <th align="center" class="max-width-120"> Operator wants to Load 20'</th>
                    <th align="center">NetLoad TEUs</th>
                    <th align="center" class="max-width-120"> Operator wants to Discharge 40'</th>
                    <th align="center" class="max-width-120"> Operator wants to Discharge 20'</th>
                    <th align="center">NetDischarge TEUs</th>
                    <%--<th align="center" class="brandcolorBg">TEUs</th>--%>
                    <th align="center" class="max-width-90 brandcolorBg">Net TEUs applied for Voyage Segment</th>
                  </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptTuesApply" ><ItemTemplate>
                  <tr>
                    <th scope="row"><asp:Label ID="lblVoyageSegmentSequenceNum" runat="server" Text='<%# Eval("VoyageSegmentSequenceNumber") %>'></asp:Label></th>
                    <td><asp:Label ID="lblshiporignport" runat="server" Text='<%# Eval("shiporign") %>'></asp:Label>
                        <asp:Label ID="lblshiporign" Visible="false" runat="server" Text='<%# Eval("shiporignport") %>'></asp:Label>
                    </td>
                    <td align="right"><asp:Label ID="lblAvailableSpaceTEU" runat="server" Text='<%# Eval("AvailableSpaceTEU") %>'></asp:Label></td>
                    <td align="right"><asp:TextBox runat="server" onchange="newload40(this)" onfocus="newload40(this)" ID="Txtload40inch" Width="200" class="form-control inline-block max-width-70 text-right" />
                        <asp:Label ID="lblload40inch" runat="server" Visible="false" Text='<%# Eval("lblload40inch") %>'></asp:Label>
                        <asp:RegularExpressionValidator  Runat="server" ID="valload40inch" ControlToValidate="Txtload40inch" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                        <%--<asp:RequiredFieldValidator ID="load40inchRequiredField" ControlToValidate="Txtload40inch" runat="server" ForeColor="Red" ValidationGroup = '<%# "Group_" + Container.ItemIndex %>' ErrorMessage="Please enter 40inch load"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td align="right"><asp:TextBox runat="server" onchange="newload40(this)" onfocus="newload40(this)" ID="Txtload20inch" Width="200" class="form-control inline-block max-width-70 text-right" />
                        <asp:Label ID="lblload20inch" runat="server" Visible="false" Text='<%# Eval("lblload20inch") %>'></asp:Label>
                        <asp:RegularExpressionValidator  Runat="server" ID="valload20inch" ControlToValidate="Txtload20inch" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                        <%--<asp:RequiredFieldValidator ID="load20inchRequiredField" ControlToValidate="Txtload20inch" runat="server" ForeColor="Red" ValidationGroup = '<%# "Group_" + Container.ItemIndex %>' ErrorMessage="Please enter 20inch load"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td align="right"><asp:Label ID="lblTEUsLoaded" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbllblTEUsLoaded" runat="server" Visible="false" Text='<%# Eval("lbllblTEUsLoaded") %>'></asp:Label>
                    </td>
                    <td align="right"><asp:TextBox runat="server" onchange="newload40(this)" onfocus="newload40(this)" ID="Txtdis40inch" Width="200" class="form-control inline-block max-width-70 text-right" />
                        <asp:Label ID="lbldis40inch" runat="server" Visible="false" Text='<%# Eval("lbldis40inch") %>'></asp:Label>
                        <asp:RegularExpressionValidator  Runat="server" ID="valdis40inch" ControlToValidate="Txtdis40inch" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                        <%--<asp:RequiredFieldValidator ID="dis40inchRequiredField" ControlToValidate="Txtdis40inch" runat="server" ValidationGroup = '<%# "Group_" + Container.ItemIndex %>' ForeColor="Red" ErrorMessage="Please enter 40inch Discharge"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td align="right"><asp:TextBox runat="server" onchange="newload40(this)" onfocus="newload40(this)" ID="Txtdis20inch" Width="200" class="form-control inline-block max-width-70 text-right" />
                        <asp:Label ID="lbldis20inch" runat="server" Visible="false" Text='<%# Eval("lbldis20inch") %>'></asp:Label>
                        <asp:RegularExpressionValidator  Runat="server" ID="valdis20inch" ControlToValidate="Txtdis20inch" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                        <%--<asp:RequiredFieldValidator ID="dis20inchRequiredField" ControlToValidate="Txtdis20inch" runat="server" ValidationGroup = '<%# "Group_" + Container.ItemIndex %>' ForeColor="Red" ErrorMessage="Please enter 20inch Discharge"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td align="right"><asp:Label ID="lblTEUsDischarged" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbllblTEUsDischarged" runat="server" Visible="false" Text='<%# Eval("lbllblTEUsDischarged") %>'></asp:Label>
                    </td>
                    <td align="right"><asp:Label ID="lblTEUsTotal" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbllblTEUsTotal" runat="server" Visible="false" Text='<%# Eval("lbllblTEUsTotal") %>'></asp:Label>
                    </td>
                      
                  </tr>
                        </ItemTemplate></asp:Repeater>
                </tbody>
              </table>
            </div>	
	</div>

    <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10" id="charges">
	
	<div class="table-responsive">
              
			  <table class="table table-bordered table-striped" id="tblCharges">
                <thead>
                  <tr class="bg-primary">
                    <th rowspan="2" class="max-width-90">Voyage Sequence#</th>
                    <th rowspan="2" class="min-width-220">Origin / Transit port</th>
                    <th rowspan="2" class="max-width-90">Charge by TEU or Container Size?</th>
                    <th rowspan="2" class="max-width-90">Price per TEU (USD)</th>
                    <th colspan="2" align="center" class="max-width-180"><div class="text-center">Price per Container  (USD)</div></th>
                    <th rowspan="2" class="max-width-100">Agreeing for Vessel Arrangement Fees? (Additional charge)</th>
                    <th rowspan="2" class="max-width-100">VSA Arrangement Fees for each TEU (USD)</th>
                      <th rowspan="2" class="max-width-100">VSA Remarks</th>
                    <th rowspan="2" class="max-width-100">Status</th>
                  </tr>
                  <tr class="bg-primary no-border">
                    <td align="center">40'</td>
                    <td align="center">20'</td>
                  </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="RptCharges"><ItemTemplate>
                  <tr>
                    <th scope="row"><%# Eval("VoyageSegmentSequenceNumber") %></th>
                    <td><asp:Label ID="lblshiporignport" runat="server" Text='<%# Eval("shiporign") %>'></asp:Label></td>
                    <td align="right">
                        <asp:Label ID="lblIsPricingbyTEU" runat="server" Text='<%# Eval("IsPricingbyTEU") %>'></asp:Label></td>
                    <td align="right">
                        <asp:Label ID="lblPricePerTEU" runat="server" Text='<%# Eval("PricePerTEU") %>'></asp:Label></td>
                    <td align="right">
                        <asp:Label ID="lblPricePer40FeetContainer" runat="server" Text='<%# Eval("PricePer40FeetContainer") %>'></asp:Label></td>
                    <td align="right">
                        <asp:Label ID="lblPricePer20FeetContainer" runat="server" Text='<%# Eval("PricePer20FeetContainer") %>'></asp:Label></td>
                    <td><asp:CheckBox ID="CkVSAArrangementFeeAgreed" Checked="true" runat="server"  />
                        <%--<div class="checkbox">
              <label class="padding-left-0">
              <input type="checkbox" value="" runat="server" name="reg" id="CkVSAArrangementFeeAgreed" checked="checked" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span>  </label>
            </div>--%></td>
                    <td align="right"><asp:Label ID="lblVSAArrangementFeePerTEU_VO" runat="server" Text='<%# Eval("VSAArrangementFeePerTEU_VO") %>'></asp:Label></td>
                      <td align="right">
                          <asp:TextBox ID="TxtVSARemarks" MaxLength="500" TextMode="MultiLine" class="form-control min-width-200" placeholder="VSA Remarks" runat="server"></asp:TextBox>
                          <asp:TextBox ID="TxtVSAnotestodisplay" Visible="false" TextMode="MultiLine" Height="80px" Width="440px" ReadOnly="true" runat="server" Text='<%# Eval("VSANotes") %>'></asp:TextBox>
                      </td>
                      <td align="right"><asp:Label ID="lblAppStatus" runat="server" Text='<%# Eval("AppStatus") %>'>></asp:Label> 
                          <asp:Label ID="lblStatus" Visible="false" runat="server" Text='<%# Eval("Status") %>'>></asp:Label>
                      </td>
                  </tr>
                  </ItemTemplate></asp:Repeater>
                </tbody>
              </table>
			  
            </div>
	
	</div>


  </div>

</div>
</div>
</div>
          <div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10 text-center"><%--<a href="#" class="btn btn-primary btn-lg min-width-200" accesskey="i"><span class="underline">I</span>nvite</a>--%> <%--<a href="#" class="btn btn-info btn-lg" accesskey="e"><span class="underline">E</span>dit / Modify</a>--%> <%--<a href="#" class="btn btn-default btn-lg" accesskey="c"><span class="underline">C</span>ancel</a>--%>
                <asp:Button ID="btnApply" OnClick="lnkapply_Click" runat="server" disabled="true" TabIndex="3" class="btn btn-primary btn-lg min-width-200" Text="Apply" />
                <asp:Button ID="Btncancel" runat="server" TabIndex="4" class="btn btn-default btn-lg" Text="Cancel" />
            </div>
          </div>
        </div>
        <!--<div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10 text-center"> <a href="#" class="btn btn-info btn-lg" accesskey="e"><span class="underline">E</span>dit / Modify</a> <a href="#" class="btn btn-default btn-lg" accesskey="h"><span class="underline">H</span>ome</a></div>
          </div>
        </div>-->
		
      </div>
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

    <script>
       
        
        $("[id*=CkVSAArrangementFeeAgreed]").click(function () {
            
            if ($(this).is(':checked')) {
                var row = $(this).closest("tr");
                $("[id*=lnkapply]", row).removeAttr("disabled");
                $("[id*=lblapplyMsg]").text("");
            } else {
                var row = $(this).closest("tr");
                var rowindex = parseInt(row.index()) + 1;
                $("[id*=lnkapply]", row).attr("disabled", true);
                $("[id*=lblapplyMsg]").text("You have not agreed for vessel arrangement of Fee Port no. " + rowindex + ", Please agree for the same").css("color", "red");
            }

        });
    </script> 

    
    
   

   <script>
       //Declarations:
       //Declaration of Available free space and Invitation flag.
       var Availablefreespc,
           invitationflg,
           validation_error_flag,

           //Declaration of variables to get the values of load and discharge
           TEUsload40, TEUsload20,
           TEUsdis40, TEUsdis20,

           //Declaration of Deficit free space
           deficitfreespace,

           NetfirstTEUload40, NetfirstTEUload20,
           prevNetTEU = 0, prevNetload40,
           prevNetload20, prevNetDisc40,
           prevNetDisc20,
           Previousrow = 0,
           validation_error_flag = false;
           validation_apply_flag = false;
           

           netload, netdis,

           Netteus,
           netteufirstrow = 0,

           intNetteus = 0,
           intermednettotalfirstrow,

           intTeusLoad40 = 0,
           intTeusLoad20 = 0,
           intTeusDisc40 = 0,
           intTeusDisc20 = 0,

           prenetteus, prenetteuload40, prenetteuload20,
           prenetteudis40, prenetteudis20,
           nthrow, origin, destination,
           InvtNOnetteu = 0, Firstrowdisctot = 0, currntrow;

        function newload40(r) {

            // Get the column count of the table

            function getColumnCount() {
                return document.getElementById('tblApplyTEUS').getElementsByTagName('tr')[0].getElementsByTagName('th').length;
            }

            // Get the row count of the table

            function getRowCount() {
                return document.getElementById('tblApplyTEUS').rows.length;
            }

            // Get the tables related to TEU and Voyage Invitation table

            var table = document.getElementById("tblApplyTEUS");

            var tableVoyageInvitation = document.getElementById("tblVoyageinvitation");

            var lastCol = getColumnCount() - 1;
            var lastRow = getRowCount() - 1;
            var row = table.rows[r.parentNode.parentNode.rowIndex];

            var currentrow = row.rowIndex;

            // The message should be blank to begin with

            $("[id*=lblapplyMsg]").text(" ");
            validation_error_flag = false;

            // Initialise the dynamic cumulative Current Net TEU values to zero

             prevNetTEU = 0;
             prevNetload40 = 0;
             prevNetload20 = 0;
             prevNetDisc40 = 0;
             prevNetDisc20 = 0;

             
            // Loop through currently entered row to the last row

             for (var i = 1 ; ((i <= lastRow) && (validation_error_flag == false)) ; i++)
             {
                currntrow = table.rows[i];
                var flag = tableVoyageInvitation.rows[i];
                Previousrow = table.rows[i - 1];

                // Initialise the dynamic Net TEU values to zero cumulative till previous row 

                prenetteus = 0;
                prenetteuload40 = 0;
                prenetteuload20 = 0;
                prenetteudis40 = 0;
                prenetteudis20 = 0;
                
                prenetteus = 0;

                // Checking if the current row value is other heading row of the table

                if (i > 1) {
                   
                    prenetteus = $("[id*=lblTEUsTotal]", Previousrow).text();

                    // Calculating dynamic Net TEU values cumulative till previous row 

                    for (var j = 1; j <= i-1; j++)
                    {
                        var jthrow = table.rows[j];
                        var tempjthrowload40 = 0, tempjthrowload20 = 0;
                        var tempjthrowDisc40 = 0, tempjthrowDisc20 = 0;

                        tempjthrowload40 = 0;
                        tempjthrowload20 = 0;

                        tempjthrowDisc40 = 0;
                        tempjthrowDisc20 = 0;

                        jthrow = table.rows[j];

                        if (parseInt(2 * (jthrow.cells[3].childNodes[0].value)) > 0)
                            {
                            tempjthrowload40 = parseInt(2 * (jthrow.cells[3].childNodes[0].value));
                            }
                        
                        if (parseInt(jthrow.cells[4].childNodes[0].value) > 0) {
                            tempjthrowload20 = parseInt(jthrow.cells[4].childNodes[0].value);
                             }

                        if (parseInt(2 * (jthrow.cells[6].childNodes[0].value)) > 0) {
                            tempjthrowDisc40 = parseInt(2 * (jthrow.cells[6].childNodes[0].value));
                             }
                        
                        if (parseInt(jthrow.cells[7].childNodes[0].value) > 0) {
                            tempjthrowDisc20 = parseInt(jthrow.cells[7].childNodes[0].value);
                             }

                        prenetteuload40 = parseInt(prenetteuload40) + tempjthrowload40;
                        prenetteuload20 = parseInt(prenetteuload20) + tempjthrowload20;
                        prenetteudis40 = parseInt(prenetteudis40) + tempjthrowDisc40;
                        prenetteudis20 = parseInt(prenetteudis20) + tempjthrowDisc20;

                    } //End of internal for loop
                } // End of i > 1 if condition
               
                // Capturing the invitation flag for loop current row

                invitationflg = $("[id*=lblinvitation]", flag).text();


                // Capturing the available free space for the current row in the loop
                Availablefreespc = $("[id*=lblAvailableSpaceTEU]", currntrow).text();

                TEUsload40 = 0;
                TEUsload20 = 0;

                TEUsdis40 = 0;
                TEUsdis20 = 0;

                // Capturing the current row values for TEUs load 40,20,Net load and Disc40,20,net discharge
                if (currntrow.cells[3].childNodes[0].value > 0) {
                    TEUsload40 = currntrow.cells[3].childNodes[0].value;
                }
                
                if (currntrow.cells[4].childNodes[0].value > 0) {
                    TEUsload20 = currntrow.cells[4].childNodes[0].value;
                }                
                
                netload = 2 * parseInt(TEUsload40) + parseInt(TEUsload20);
               
                if (currntrow.cells[6].childNodes[0].value > 0) {
                    TEUsdis40 = currntrow.cells[6].childNodes[0].value;
                }

                if (currntrow.cells[7].childNodes[0].value > 0) {
                    TEUsdis20 = currntrow.cells[7].childNodes[0].value;
                }

                netdis = 2 * parseInt(TEUsdis40) + parseInt(TEUsdis20);

                // calculation of Net TEUs
                Netteus = (parseInt(netload) - parseInt(netdis));

                //Once load data is entered, track this value in prevnetTEUs              
                //If load exists on previous row, then use the formula of netteus as sum of prevTEUs and present row TEUS

                prevNetTEU = prenetteus;

                if (prevNetTEU == 0) {
                    prevNetTEU = Netteus;
                } else
                {
                    intNetteus = parseInt(prenetteus) + parseInt(Netteus);
                    prevNetTEU = intNetteus;
                }

                prevNetload40 = prenetteuload40;

                if (prevNetload40 == 0)
                {
                    prevNetload40 = (2*parseInt(TEUsload40));
                } else{
                      prevNetload40 = parseInt(prevNetload40) + parseInt(2 * parseInt(TEUsload40));
                }

                prevNetload20 = prenetteuload20;
                if (prevNetload20 == 0) {
                    prevNetload20 = TEUsload20;
                } else{
                        prevNetload20 = parseInt(prenetteuload20) + parseInt(TEUsload20);
                }

                prevNetDisc40 = prenetteudis40;
                if (prevNetDisc40 == 0) {
                    prevNetDisc40 = (2*parseInt(TEUsdis40));
                } else {
                        prevNetDisc40 = parseInt(prenetteudis40) + parseInt(2 * parseInt(TEUsdis40));
                }


                prevNetDisc20 = prenetteudis20;
                if (prevNetDisc20 == 0) {
                    prevNetDisc20 = TEUsdis20;
                } else{
                        prevNetDisc20 = parseInt(prenetteudis20) + parseInt(TEUsdis20);
                }
                
                // Set totals of load 40, load 20, discharge 40, discharge 20 in the header
                $("[id*=lblLoad40sum]").html(parseInt(prevNetload40)/2);
                $("[id*=lblLoad20sum]").html(prevNetload20);
                $("[id*=lblDisc40sum]").html(parseInt(prevNetDisc40)/2);
                $("[id*=lblDisc20sum]").html(prevNetDisc20);
                $("[id*=lblOnship40]").html((parseInt(prevNetload40) / 2) - (parseInt(prevNetDisc40) / 2));
                $("[id*=lblOnship20]").html(prevNetload20 - prevNetDisc20);
                $("[id*=lblAvlDischarge40]").html(((prevNetload40 / 2) - row.cells[3].childNodes[0].value) - (prevNetDisc40 / 2));
                $("[id*=lblAvlDischarge20]").html((prevNetload20 - row.cells[4].childNodes[0].value) - prevNetDisc20);


                // Validation of dynamic Net TEU Discharge less than TEU load values cumulative till current row                
                if ((prevNetDisc40 / 2) > ((prevNetload40 / 2) - TEUsload40)) {
                  //  alert("(prevNetDisc40 / 2): " + (prevNetDisc40 / 2) + " ((prevNetload40 / 2) - TEUsload40): " + ((prevNetload40 / 2) - TEUsload40));
                    $("[id*=lblapplyMsg]").text("Total Discharge for 40' container is more than the total load for 40' container by " + ((prevNetDisc40/2) - ((prevNetload40/2) - TEUsload40))).css("color", "red");
                    validation_error_flag = true;
                    for(var j=currentrow+1; j<= lastRow; j++)
                    {
                        jthrow = table.rows[j];
                        $("[id*=lblTEUsTotal]", jthrow).html("");
                    }
                }

                                 
                 if (prevNetDisc20 > (prevNetload20 - TEUsload20)) {
                    $("[id*=lblapplyMsg]").text("Total Discharge for 20' container is more than the total load for 20' container by " + (prevNetDisc20 - (prevNetload20 - TEUsload20))).css("color", "red");
                    validation_error_flag = true;

                    for (var p = currentrow + 1; p <= lastRow; p++) {
                        pthrow = table.rows[p];
                        $("[id*=lblTEUsTotal]", pthrow).html("");
                    }
                 }
                 
                if (invitationflg == "N") {
                    $("[id*=Txtload40inch]", currntrow).html(0);
                    $("[id*=Txtload20inch]", currntrow).html(0);
                    $("[id*=Txtload40inch]", currntrow).attr("disabled", "disabled");
                    $("[id*=Txtload20inch]", currntrow).attr("disabled", "disabled");                    
                }
               

                $("[id*=lblTEUsLoaded]", currntrow).html(netload);
                $("[id*=lblTEUsDischarged]", currntrow).html(netdis);
                $("[id*=lblTEUsTotal]", currntrow).html(prevNetTEU);

                // Show message to discharge all the load if the invite flag is N.
                if (invitationflg == "N" && validation_error_flag == false) {

                    var originport = table.rows[i-1];
                    var destinationport = table.rows[i];
                    
                    var origin = $("[id*=lblshiporignport]", originport).text().split('-', 1);
                    var destination = $("[id*=lblshiporignport]", destinationport).text();

                    $("[id*=lblapplyMsg]").text("The load  " + prevNetTEU + " TEUs need to be discharged at voyage sequence no. " + destinationport.rowIndex + ",as there is no invite at voyage sequence no. " + destinationport.rowIndex).css("color", "red");
                    //$("[id*=lblTEUsLoaded]", currntrow).html(netload);
                    //$("[id*=lblTEUsDischarged]", currntrow).html(netdis);
                    //$("[id*=lblTEUsTotal]", currntrow).html(prevNetTEU);

                    InvtNOnetteu = prevNetTEU;
                    if (prevNetTEU == 0 ) {
                        $("[id*=lblapplyMsg]").text(" ");
                    } else {
                        validation_error_flag = true;
                        $("[id*=Txtdis40inch]", currntrow).removeAttr("disabled");
                        $("[id*=Txtdis20inch]", currntrow).removeAttr("disabled");
                    }

                } //else { //Invitation = YES OR Validation flag = TRUE                    

                if (Availablefreespc < prevNetTEU && validation_error_flag == false) {
                        $("[id*=lblapplyMsg]").text("The maximum TEUs load you can apply for is " + Availablefreespc + " TEUs. The effective TEUs loaded on Voyage Sequence " + i + " are " + prevNetTEU).css("color", "red");
                        if (currntrow.rowIndex != lastRow)
                            {
                        for (var j = currentrow + 1; j <= lastRow; j++) {
                            jthrow = table.rows[j];
                            $("[id*=lblTEUsTotal]", jthrow).html("");
                        }
                        }
                        validation_error_flag = true;
                            
                    } // Available free space condition
                    //else { //Invitation = YES and Available free space > NetTEUS hence go ahead
                        var discurrentvalid = table.rows[i];
                        var discurrentvalidnoti = table.rows[i - 1];

                        Firstrowdisctot = $("[id*=lblTEUsTotal]", discurrentvalidnoti).text();
                        if(Firstrowdisctot == ""){
                            Firstrowdisctot = 0;
                        }

                       if (Firstrowdisctot == 0 ) {
                            $("[id*=Txtdis40inch]", currntrow).attr("disabled", "disabled");
                            $("[id*=Txtdis20inch]", currntrow).attr("disabled", "disabled");
                            $("[id*=Txtdis40inch]", currntrow).val("");
                            $("[id*=Txtdis20inch]", currntrow).val("");
                            $("[id*=lblTEUsDischarged]", currntrow).html("");
                            $("[id*=lblTEUsTotal]", currntrow).html(netload);
                        }
                        else {
                            $("[id*=Txtdis40inch]", currntrow).removeAttr("disabled");
                            $("[id*=Txtdis20inch]", currntrow).removeAttr("disabled");
                        }

                        if (validation_error_flag == false) {
                        $("[id*=lblTEUsTotal]", currntrow).html(prevNetTEU);
                        $("[id*=lblTEUsLoaded]", currntrow).html(netload);
                        $("[id*=lblTEUsDischarged]", currntrow).html(netdis);
                        }
                      
                        if (prevNetTEU == 0 && validation_error_flag==false)
                        {
                            $("[id*=btnApply]").removeAttr("disabled");
                            validation_apply_flag == true;
                        }
                        else if (prevNetTEU > 0 && currntrow.rowIndex == lastRow && validation_error_flag == false)
                        {
                            $("[id*=btnApply]").removeAttr("disabled");
                            validation_apply_flag == true;
                        }
                        else {
                           
                            $("[id*=btnApply]").attr("disabled", "disabled");
                            validation_apply_flag == false;
                      }
                     
                    //} // End of success condition
                //} // End of Avaiable freespace = YES
             } // End of for loop
             validation_error_flag = false;
        }
    </script>

    <script>
        function getColumnCount() {
            return document.getElementById('tblApplyTEUS').getElementsByTagName('tr')[0].getElementsByTagName('th').length;
        }

        function getRowCount() {
            return document.getElementById('tblApplyTEUS').rows.length;
        }
        function applycolor()
        {
            
            var table = document.getElementById("tblApplyTEUS");
            
            var tableVoyageInvitation = document.getElementById("tblVoyageinvitation");
            var tblCharges = document.getElementById("tblCharges");
            
            var lastCol = getColumnCount() - 1;
            var lastRow = getRowCount() - 1;
            

            
            for(var i=1; i<=lastRow; i++)
            {
                var Agreementfee;
                var  ithrow=tableVoyageInvitation.rows[i];
                var color = table.rows[i];
                var Chargestbl = tblCharges.rows[i+1];
                var inviteflg = $("[id*=lblinvitation]", ithrow).text();
                if ($("[id*=CkVSAArrangementFeeAgreed]", Chargestbl).is(':checked'))
                {
                    Agreementfee = "Y";
                }
                else
                {
                    Agreementfee = "N";
                }
                
                if (color.rowIndex == 1) {
                    $("[id*=Txtdis40inch]", color).attr("disabled", "disabled");
                    $("[id*=Txtdis20inch]", color).attr("disabled", "disabled");
                }

                

                if (Agreementfee == "N" && inviteflg == "Y")
                {
                    $("[id*=lblshiporign]", ithrow).css("color", "green");
                    $("[id*=lblshiporignport]", color).css("color", "green");
                    $("[id*=lblshiporign]", Chargestbl).css("color", "green");
                }

                if(inviteflg == "N")
                {
                    $("[id*=lblshiporign]", ithrow).css("color", "red");
                    $("[id*=CkVSAArrangementFeeAgreed]", Chargestbl).prop('checked', false);
                    $("[id*=CkVSAArrangementFeeAgreed]", Chargestbl).attr("disabled", "disabled");
                    $("[id*=lblshiporignport]", color).css("color", "red");
                    $("[id*=lblshiporign]", Chargestbl).css("color", "red");
                    $("[id*=Txtload40inch]", color).attr("disabled", "disabled");
                    $("[id*=Txtload20inch]", color).attr("disabled", "disabled");
                }
            }

        }
        window.onload = applycolor;
    </script>

  
</asp:Content>
