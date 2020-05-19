<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReviewAndApproved.aspx.cs" Inherits="VesselSharingAgreement.ReviewAndApproved" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container banner-top-heading">
  <h3>Vessel Sharing Participation Review and Approve</h3>
</div>
    <div align="center">
        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
    </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-12 form-horizontal">

          <div class="row" runat="server" id="ddlParticipantRoleRow">
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Select Role</label>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlParticipantRole" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlParticipantRole_SelectedIndexChanged">
                      <asp:ListItem Selected="True" Value="0">Select Role</asp:ListItem>
                      <asp:ListItem Value="1">VSA Participant Reviewer</asp:ListItem>
                      <asp:ListItem Value="2">VSA Participant Customer</asp:ListItem>
                  </asp:DropDownList>
              </div>
            </div>
              </div>
              </div> 
       <div class="row">
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Id</label>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlvesselid" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvesselid_SelectedIndexChanged"></asp:DropDownList>
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Name</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtVesselName" type="text" class="form-control"  disabled="disabled" />
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Origin Port</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtOriginPort" type="text" class="form-control" disabled="disabled" />
              </div>
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Voyage Id</label>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlvoyageid" class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlvoyageid_SelectedIndexChanged"></asp:DropDownList>
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Capacity in TEUs</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtVesselCapacity" type="text" class="form-control"  disabled="disabled" />
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Destination Port</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtDestinationPort" type="text" class="form-control"  disabled="disabled" />
              </div>
            </div>
          </div>
        </div>
        
		<hr />
          
          <div class="col-md-6">
              <%--<div class="form-group form-group-lg" runat="server" visible="false" id="port">
                  <h4>Port Id</h4>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlPortid" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPortid_SelectedIndexChanged"></asp:DropDownList>
              </div>
            </div>--%>
              <div class="form-group form-group-lg" runat="server" visible="false" id="appstatus">
                  <h4>Retrive By App Status</h4>
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlAppStatus" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAppStatus_SelectedIndexChanged">
                      <asp:ListItem Selected="True" Value="0">Select Status</asp:ListItem>
                      <asp:ListItem>Review In Progress</asp:ListItem>
                      <%--<asp:ListItem>Final VSA</asp:ListItem>--%>
                  </asp:DropDownList>
              </div>
            </div>
          </div>
          
          <div class="row" runat="server" id="Company">
              
              <h4>&nbsp;&nbsp; Participant CustomerID</h4>
          <div class="col-md-6">
              <div class="form-group form-group-lg">
              <div class="col-md-8">
                  <asp:DropDownList ID="ddlcustomerid" class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcustomerid_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
              </div>
          </div>
              
        <div class="row">
          <div class="col-md-12">
			<h4>Review of VSA Applications by Vessel Operator&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblreviewmsg" runat="server" ForeColor="Red" Text=""></asp:Label></h4>
			
			<div class="padding-top-10">
  <!-- Nav tabs -->
                 <style>
                    .mytbl table tr td, .mytbl table tr th {
                        padding: 0 5px!important;
                        font-size: 16px;
                    }

                </style>
              <ul class="nav nav-tabs responsive-tabs inner-tabs">
				<li role="presentation" class="active"><a href="#voyage" class="bold"><span class="label label-success">1</span> <img src="images/Voyage-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> Voyage Invitation</a></li> 
				<li role="presentation"><a href="#teus" class="bold">
                    <div class="inline-block" style="vertical-align: top;"> <span class="label label-success">2</span> <img src="images/TEUs-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> TEUs </div>
                    <div class="inline-block mytbl">
                        <table class="table table-bordered table-condensed margin-bottom-0" runat="server" id="tblDetails">
                       <tr>
                           <th></th>
                            <th style="color:black" >AppLoad</th>
                            <th style="color:black" >AppDis</th>
                            <th style="color:black" >AppOnShip</th>
                       </tr>
                        <tr>
                           <td style="color:black" >40'</td>
                            <td><asp:Label ID="lblLoad40sum" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblDisc40sum" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblOnship40" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                       </tr>
                        <tr>
                           <td style="color:black" >20'</td>
                            <td><asp:Label ID="lblLoad20sum" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblDisc20sum" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lblOnship20" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                       </tr>


                   </table>
                       </div>
				                        </a></li>
				<li role="presentation"><a href="#charges" class="bold"><span class="label label-success">3</span> <img src="images/MetroUI-Apps-Calculator-icon.png" class="img-responsive max-height-80 inline-block img-circle" /> Approved Summary</a></li>
                  <li role="presentation" visible="false" runat="server" id="Chages"><a href="#charges1" class="bold"><span class="label label-success">4</span> <img src="images/Price-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> Charges</a></li>
              </ul>

  <!-- Tab panes -->
  <div class="tab-content" runat="server" id="alltbls">
  <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10 active" id="voyage">
	<div class="table-responsive">
              <table class="table table-bordered table-striped" id="tblReviewVoyage">
                      <thead>
                        <tr class="bg-primary">
                          <th class="max-width-90">Voyage Sequence#</th>
                          <th>Origin / Transit port</th>
                          <th class="max-width-90">Vessel Agreement Id <i class="fa fa-info-circle" title="Cargo Operators, Agents, Vessel Operators etc."></i></th>
                          <th>Transit / Destination Port</th>
                            <th>VSA Notes</th>
                        </tr>
                      </thead>
                      <tbody>
                          <asp:Repeater runat="server" ID="VoyageInvitation">
                           <ItemTemplate>
                          <tr>
                          <th scope="row"><asp:Label ID="lblVoyageSeqnum" runat="server" Text='<%# Eval("VoyageSegmentSequencenumber") %>'></asp:Label></th>
                          <td><asp:Label ID="lblorignport" runat="server" Text='<%# Eval("shiporignport") %>'></asp:Label></td>
                          <td><%# Eval("InviteVSAParticipantsFlag") %></td>
                          <td><%# Eval("DestinationPortID") %></td>
                          <td><asp:TextBox runat="server" TextMode="MultiLine" Height="80px" Width="500px" ReadOnly="true" ID="lblvsanotes" Text='<%# Eval("ApplyVSANotes") %>'></asp:TextBox></td><%--<%# Eval("ApplyVSANotes") %>'--%>
                          </tr>
                           </ItemTemplate>
                          </asp:Repeater>
                      </tbody>
                    </table>
            </div>	
	</div>
    <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10" id="teus">
	<div class="table-responsive">
              <table class="table table-bordered table-striped" id="tblReviewTeus">
                <thead>
                  <tr class="bg-primary">
                          <th rowspan="2" class="max-width-80">Cargo Operator / Agent Name <span class="glyphicon glyphicon-chevron-up"></span></th>
                    <th rowspan="2" class="">Cargo Operator <br>Application for Activity <br>at this Port <span class="glyphicon glyphicon-chevron-down"></span></th>
                    <th rowspan="2" align="center" class="max-width-90">Initial Available free space in TEUs</th>
                    <th rowspan="2" align="center" class="max-width-90">Current Available free space in TEUs</th>
                    <th rowspan="2" align="center" class="max-width-90">Cargo Operator Applied Load 40'</th>
                    <th rowspan="2" align="center" class="max-width-90">Cargo Operator Applied Load 20'</th>
                    <th rowspan="2" align="center" hidden="hidden" class="max-width-90">Cargo Operator Applied Load Teus'</th>
                    <th rowspan="2" align="center" class="max-width-90">Accepted Quantity for Load 40'</th>
                    <th rowspan="2" align="center" class="max-width-90">Accepted Quantity for Load 20'</th>
                    <th rowspan="2" align="center" hidden="hidden" class="max-width-90">Accepted Quantity for Load Teus</th>
                    <th rowspan="2" align="center" class="max-width-90">Cargo Operator Applied Discharge 40'</th>
                    <th rowspan="2" align="center" class="max-width-90">Cargo Operator Applied Discharge 20'</th>
                    <th rowspan="2" align="center" hidden="hidden" class="max-width-90">Cargo Operator Applied Discharge Teus</th>
                    <th rowspan="2" align="center" class="max-width-90">Accepted Quantity for Discharge 40'</th>
                    <th rowspan="2" align="center" class="max-width-90">Accepted Quantity for Discharge 20'</th>
                    <th rowspan="2" align="center" hidden="hidden" class="max-width-90">Accepted Quantity for Discharge Teus</th>
                    <th align="center" class="max-width-90 brandcolorBg">Net TEUs Applied for Voyage Segment</th>
                    <th align="center" class="brandcolorBg max-width-110"><div class="text-center">Approved for Voyage Segment(Net TEUs +Loaded/<br />-Discharge)</div></th>
                  </tr>
                  
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="Teus">
                        <ItemTemplate>
                    <tr>
                    <td><asp:Label ID="lblcompanyname" runat="server" Text='<%# Eval("CargoOperatorAgentName") %>'></asp:Label></td>
                    <td><asp:Label ID="lblshiporignport" runat="server" Text='<%# Eval("shiporignport") %>'></asp:Label></td>
                    <td align="right"><asp:Label ID="lblInitialAvailableSpaceTEU" runat="server" Text='<%# Eval("InitialAvailableSpaceTEU") %>'></asp:Label></td>
                    <td align="right"><asp:Label ID="lblAvailableSpaceTEU" runat="server" Font-Bold="true" Text='<%# Eval("AvailableSpaceTEU") %>'></asp:Label></td>
                    <td align="right"><asp:Label ID="lblload40inch" runat="server" Text='<%# Eval("lblload40inch") %>'></asp:Label></td>
                    <td align="right"><asp:Label ID="lblload20inch" runat="server" Text='<%# Eval("lblload20inch") %>'></asp:Label></td>
                    <td align="right" hidden="hidden"><%# Eval("lbllblTEUsLoaded") %></td>
                    <td align="right">
                        <asp:TextBox runat="server" ID="TxtAccepted40Load" onchange="validateapprload(this)" onfocus="validateapprload(this)" Width="90" type="text" class="form-control inline-block max-width-70 text-right" />
                        <asp:Label ID="lblAccepted40Load" Visible="false" runat="server" Text='<%# Eval("lblAccepted40Load") %>'></asp:Label>
                    </td>
                    <td align="right">
                        <asp:TextBox runat="server" ID="TxtAccepted20Load" onchange="validateapprload(this)" onfocus="validateapprload(this)" Width="90" type="text" class="form-control inline-block max-width-70 text-right" />
                        <asp:Label ID="lblAccepted20Load" Visible="false" runat="server" Text='<%# Eval("lblAccepted20Load") %>'></asp:Label>
                    </td>
                    <td align="right" hidden="hidden">
                        <asp:Label ID="lblAcceptedload" runat="server" Text='<%# Eval("lblload") %>'></asp:Label>
                        <asp:HiddenField ID="TxtEqvlntAcceptedload" runat="server" />
                        <asp:Label ID="lblAllowLoad" Visible="false" runat="server" Text='<%# Eval("lblAllowLoad") %>'></asp:Label>
                    </td>
                    <td align="right"><asp:Label runat="server" ID="lbldis40inch" Text='<%# Eval("lbldis40inch") %>'></asp:Label></td>
                    <td align="right"><asp:Label runat="server" ID="lbldis20inch" Text='<%# Eval("lbldis20inch") %>'></asp:Label></td>

                    <td align="right" hidden="hidden"><%# Eval("lbllblTEUsDischarged") %></td>
                    <td align="right">
                        <asp:TextBox runat="server" ID="TxtAccepted40Disch" onchange="validateapprload(this)" onfocus="validateapprload(this)" Width="90" type="text" class="form-control inline-block max-width-70 text-right" />
                        <asp:Label ID="lblAccepted40Disch" Visible="false" runat="server" Text='<%# Eval("lblAccepted40Disch") %>'></asp:Label>
                    </td>
                    <td align="right"><asp:TextBox runat="server" ID="TxtAccepted20Disch" onchange="validateapprload(this)" onfocus="validateapprload(this)" Width="90" type="text" class="form-control inline-block max-width-70 text-right" />
                        <asp:Label ID="lblAccepted20Disch" Visible="false" runat="server" Text='<%# Eval("lblAccepted20Disch") %>'></asp:Label>
                    </td>
                    <td align="right" hidden="hidden">
                        <asp:Label ID="lblAcceptedDischarge" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="TxtEqvlntAcceptedDischarge" runat="server" />
                        <asp:Label ID="lblAllowDisch" Visible="false" runat="server" Text='<%# Eval("lblAllowDisch") %>'></asp:Label>
                    </td>
                        <td align="right"><%# Eval("NetTEUSowned") %></td>
                    <td align="right">
                        <asp:Label ID="lblNetTEUsApproved" runat="server" ></asp:Label>
                        <asp:HiddenField ID="TxtEqvlntNetTEUsApproved" runat="server" />
                        <asp:Label ID="lblNetTEUsApp" Visible="false" runat="server" Text='<%# Eval("lblNetTEUsApp") %>'></asp:Label>
                    </td>
                        <asp:HiddenField ID="HiddenNet40" runat="server" />
                        <asp:HiddenField ID="HiddenNet20" runat="server" />
                  </tr>
                  
                    </ItemTemplate>
                    </asp:Repeater>
                </tbody>
              </table>
            </div>	
	</div>

    <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10" id="charges">
	
	<div class="table-responsive">
              <table class="table table-bordered table-striped" id="tblReviewApproved">
                <thead>
                  <tr class="bg-primary">
                    <th rowspan="2" class="max-width-90">Voyage Sequence#</th>
				 <th class="">Cargo Operator / Agent Name</th>
                    <th class="">Origin / Transit Port</th>
                    <th class="">Transit / Destination Port</th>
                    <th align="center" class="max-width-90 brandcolorBg">Net TEUs Applied for Voyage Segment</th>
                    <th align="center" class="brandcolorBg max-width-110"><div class="text-center">Approved for Voyage Segment(Net TEUs +Loaded/<br />-Discharge)</div></th>
                      <th class="">VSA Remarks</th>
                      <th class="">Status</th>
                      <%--<th align="center" class="brandcolorBg max-width-90"><div class="text-center">Action</div></th>--%>
                  </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="ApprovedSummary">
                        <ItemTemplate>
                    <tr>
                    <th scope="row"><%# Eval("VoyageSegmentSequencenumber") %></th>
                    <td><%# Eval("CargoOperatorAgentName") %></td>
                    <td><%# Eval("shiporignport") %></td>
                    <td><%# Eval("DestinationPortID") %></td>
                    <td align="right"><%# Eval("NetTEUSowned") %></td>
                    <td align="right">
                        <asp:Label ID="lblNetTEUsApproved" runat="server"></asp:Label>
                        <asp:Label ID="lblNetTEUsApp" Visible="false" runat="server" Text='<%# Eval("lblNetTEUsApp") %>'></asp:Label>
                    </td>
                        <td align="right">
                            <asp:TextBox ID="TxtVSANotes" MaxLength="500" TextMode="MultiLine" class="form-control min-width-200" placeholder="VSA Notes" runat="server"></asp:TextBox>
                            <asp:TextBox ID="TxtVSAnotestodisplay" Visible="false" TextMode="MultiLine" Height="80px" Width="440px" ReadOnly="true" runat="server" Text='<%# Eval("ApplyVSANotes") %>'></asp:TextBox>
                            
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("lblAppStatus") %>'></asp:Label>
                        </td>
                  </tr>
                  </ItemTemplate>
                    </asp:Repeater>
                </tbody>
              </table>
			  
            </div>
	</div>
          <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10" id="charges1">
	<div class="table-responsive">
              <table class="table table-bordered table-striped" id="tblCharges">
                <thead>
                  <tr class="bg-primary">
                    <th rowspan="2" align="center" class="max-width-90">Origin / Transit Port</th>
                    <th rowspan="2" align="center" class="max-width-90">Approved Net TEUs</th>
                    <th rowspan="2" align="center" class="max-width-90">Equivalent 40 Ft Containers</th>
                    <th rowspan="2" align="center" class="max-width-90">Equivalent 20 Ft Containers</th>
                    <th rowspan="2" align="center" class="max-width-90">Price Per TEU</th>
                    <th rowspan="2" align="center" class="max-width-90">Price by 40ft containers</th>
                    <th rowspan="2" align="center" class="max-width-90">Price by 20ft containers</th>
                    <th rowspan="2" align="center" class="max-width-90">Total price</th>
                  </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="RptCharges">
                        <ItemTemplate>
                    <tr>
                    <td align="center"><asp:Label ID="lblTransitPort" runat="server" Text='<%# Eval("lblTransitPort") %>'></asp:Label></td>
                    <td align="center"><asp:Label ID="lblNetTeus" runat="server" Text='<%# Eval("lblNetTeus") %>'></asp:Label></td>
                    <td align="center"><asp:Label ID="lblEquivalent40FtContainers" runat="server" Text='<%# Eval("lblEquivalent40FtContainers") %>'></asp:Label></td>
                    <td align="center"><asp:Label ID="lblEquivalent20FtContainers" runat="server" Text='<%# Eval("lblEquivalent20FtContainers") %>'></asp:Label></td>
                    <td align="center"><asp:Label ID="lblPricePerTEU" runat="server" Text='<%# Eval("lblPricePerTEU") %>'></asp:Label></td>
                    <td align="center"><asp:Label ID="lblPriceby40ftcontainers" runat="server" Text='<%# Eval("lblPriceby40ftcontainers") %>'></asp:Label></td>
                    <td align="center"><asp:Label ID="lblPriceby20ftcontainers" runat="server" Text='<%# Eval("lblPriceby20ftcontainers") %>'></asp:Label></td>
                    <td align="center"><asp:Label ID="lblTotalprice" runat="server" Text='<%# Eval("lblTotalprice") %>'>></asp:Label></td>
                  </tr>
                  
                    </ItemTemplate>
                    </asp:Repeater>
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
            <div class="padding-top-10 text-center">
                <asp:Button ID="BtnEditDisagreed" runat="server" Visible="false" OnClick="BtnEditDisagreed_Click" class="btn btn-primary btn-lg min-width-200" Text="Edit" />
                <asp:Button ID="btnApprove" runat="server" OnClick="btnApproved_Click" class="btn btn-primary btn-lg min-width-200" Text="Approve" />
                <asp:Button ID="Btncancel" runat="server" class="btn btn-default btn-lg" Text="Cancel" />
                <asp:LinkButton ID="lnkAgree" Visible="false" OnClick="lnkAgree_Click" class="btn btn-primary btn-lg min-width-200" runat="server"><span class="glyphicon"></span>Agree</asp:LinkButton>
                <asp:LinkButton ID="lnkDisAgree" Visible="false" OnClick="lnkDisAgree_Click" class="btn btn-primary btn-lg min-width-200" runat="server"><span class="glyphicon"></span>DisAgree</asp:LinkButton>
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

    
    
    <script type="text/javascript">
       // Global Variables Declarations:
          var Availablefreespace,
           portinviteflag,
           validation_error_flag,
           validation_approve_flag,
           inviteflag,
           inviteflagrow,
           currentrow,
           previousrow,
           currntrow,
           lastCol,
           lastRow,
           //to Compare load vs discharge
           load40comp,
           load20comp,
           
      // Declaration of variables of applied load and discharge :
          TEUsloadapply40,TEUsloadapply20,NetTEUloadapply,
          TEUsdisapply40, TEUsdisapply20, NetTEUdisapply, NetTEUsapplied,

     //  Declaration of variables of approved load and discharge :
           TEUsloadapproved40, TEUsloadapproved20, NetTEUloadapproved,
           TEUsdisapproved40, TEUsdisapproved20, NetTEUdisapproved, NetTEUsapproved,

    //   Declaration of variables of row for which invite flag is "N"
           TEUsloadapprovedinviteN40, TEUsloadapprovedinviteN20, NetTEUloadapprovedinviteN,
           TEUsdisapprovedinviteN40, TEUsdisapprovedinviteN20, NetTEUinviteNdisapproved, NetTEUsinviteNapproved,

     //  Declaration of intermediate variables for calculation of cumulative loads and discharges
           TEUsloadintercumulapproved40, TEUsloadintercumulapproved20, TEUsNetloadintercumulapproved,
          TEUsdisintercumulapproved40, TEUsdisintercumulapproved20,TEUsNetdisintercumulapproved, TEUsintercumulapprovednet,
           
           TEUsloadintercumulapply40, TEUsloadintercumulapply20,
           TEUsdisintercumulapply40, TEUsdisintercumulapply20, TEUsintercumulapplynet,
          
         

     //  Declaration of variables of cumulative approved loads and discharges :
          TEUsloadcumulapproved40, TEUsloadcumulapproved20, TEUsNetloadcumulapproved,
        TEUsdiscumulapproved40, TEUsdiscumulapproved20, TEUsNetdiscumulapproved,TEUscumulnetapproved,

          TEUsloadcumulapply40, TEUsloadcumulapply20,
          TEUsdiscumulapply40, TEUsdiscumulapply20, TEUscumulapplynet;
          

    //  Initialisation of variables
          validation_error_flag = false;
          validation_approve_flag = false;
          previousrow = 0;
          TEUsloadintercumulapproved40 = 0;
          TEUsloadintercumulapproved20 = 0;
          TEUsdisintercumulapproved40 = 0;
          TEUsdisintercumulapproved20 = 0;
          TEUsNetloadintercumulapproved = 0;
          TEUsNetdisintercumulapproved = 0;
          TEUsintercumulapprovednet = 0;

          TEUsloadintercumulapply40 = 0;
          TEUsloadintercumulapply20 = 0;
          TEUsdisintercumulapply40 = 0;
          TEUsdisintercumulapply20 = 0;
          TEUsintercumulapplynet = 0;

          TEUsloadcumulapproved40 = 0, TEUsloadcumulapproved20 = 0, TEUsNetloadcumulapproved = 0,
          TEUsdiscumulapproved40 = 0, TEUsdiscumulapproved20 = 0, TEUsNetdiscumulapproved = 0, TEUscumulnetapproved = 0;

          TEUsloadcumulapply40=0, TEUsloadcumulapply20=0,
          TEUsdiscumulapply40=0, TEUsdiscumulapply20=0, TEUscumulapplynet=0;

          lastCol = 0;
          lastRow = 0;
          TEUsloadapproved40 = 0, TEUsloadapproved20 = 0, port;
          TEUsdisapproved40 = 0, TEUsdisapproved20 = 0;
          

          function validateapprload(r) {
              
              // Get the row count of the table

              function getRowCount() {
                  return document.getElementById('tblReviewTeus').rows.length;
              }

              // Get the column count of the table

              function getColumnCount() {
                  return document.getElementById('tblReviewTeus').getElementsByTagName('tr')[0].getElementsByTagName('th').length;
              }

              // Get the tables related to TEU and Voyage Invitation tab in review and approved screen

              var TEutabtable = document.getElementById("tblReviewTeus");
              var tblReviewApproved = document.getElementById("tblReviewApproved");
              var VoyageInvitationtabtable = document.getElementById("tblReviewVoyage");
              lastCol = getColumnCount() - 1;
              lastRow = getRowCount() - 1; 
              var row = TEutabtable.rows[r.parentNode.parentNode.rowIndex];
              currentrow = row.rowIndex;

              // The message should be blank to begin with

              $("[id*=lblreviewmsg]").text(" ");
              validation_error_flag = false;


              // Loop through all the rows and and validate the correct entry and load calculations per business logic

              for (var i = 1 ; ((i <= lastRow) && (validation_error_flag == false)) ; i++) {
                  
                  currntrow = TEutabtable.rows[i];
                  previousrow = TEutabtable.rows[i - 1];
                  inviteflagrow = VoyageInvitationtabtable.rows[i];
                  Approved = tblReviewApproved.rows[i];

                  TEUsloadintercumulapproved40 = 0;
                  TEUsloadintercumulapproved20 = 0;
                  TEUsdisintercumulapproved40 = 0;
                  TEUsdisintercumulapproved20 = 0;
                  TEUsNetloadintercumulapproved = 0;
                  TEUsNetdisintercumulapproved = 0;
                  TEUsintercumulapprovednet = 0;

                  TEUsloadintercumulapply40 = 0;
                  TEUsloadintercumulapply20 = 0;
                  TEUsdisintercumulapply40 = 0;
                  TEUsdisintercumulapply20 = 0;
                  TEUsintercumulapplynet = 0;

                    // Checking if the current row value is other heading row of the table    

                  //if (i > 1) {

                      // Calculating dynamic Net TEU values cumulative till previous row 

                        for (var j = 1; j <= i - 1; j++) {
                          
                          var jthrow = TEutabtable.rows[j];
                          
                          var tempjthrowappvdload40, tempjthrowappvdload20;
                          var tempjthrowappvdDisc40, tempjthrowappvdDisc20;
                          var tempjthrowapplyload40, tempjthrowapplyload20;
                          var tempjthrowapplyDisc40, tempjthrowapplyDisc20;

                          tempjthrowappvdload40 = 0,
                          tempjthrowappvdload20 = 0;
                          tempjthrowappvdDisc40 = 0;
                          tempjthrowappvdDisc20 = 0;

                          tempjthrowapplyDisc40 = 0;
                          tempjthrowapplyDisc20 = 0;
                          

                          if (parseInt(2 * (jthrow.cells[7].childNodes[1].value)) > 0) {
                              tempjthrowappvdload40 = parseInt(2 * (jthrow.cells[7].childNodes[1].value));
                          }

                          if (parseInt(jthrow.cells[8].childNodes[1].value) > 0) {
                              tempjthrowappvdload20 = parseInt(jthrow.cells[8].childNodes[1].value);
                          }

                          if (parseInt(2 * (jthrow.cells[13].childNodes[1].value)) > 0) {
                              tempjthrowappvdDisc40 = parseInt(2 * (jthrow.cells[13].childNodes[1].value));
                          }

                          if (parseInt(jthrow.cells[14].childNodes[0].value) > 0) {
                              tempjthrowappvdDisc20 = parseInt(jthrow.cells[14].childNodes[0].value);
                          }

                          if (parseInt(2 * (jthrow.cells[4].childNodes[0].textContent)) > 0) {
                              tempjthrowapplyload40 = parseInt(2 * (jthrow.cells[4].childNodes[0].textContent));
                          }
                          if (parseInt(jthrow.cells[5].childNodes[0].textContent) > 0) {
                              tempjthrowapplyload20 = parseInt(jthrow.cells[5].childNodes[0].textContent);
                          }

                          if (parseInt(2 * (jthrow.cells[10].childNodes[0].textContent)) > 0) {
                              tempjthrowapplyDisc40 = parseInt(2 * (jthrow.cells[10].childNodes[0].textContent));
                          }
                          if (parseInt(jthrow.cells[11].childNodes[0].textContent) > 0) {
                              tempjthrowapplyDisc20 = parseInt(jthrow.cells[11].childNodes[0].textContent);
                          }

                            //Approved cumulative load and discharges

                          TEUsloadintercumulapproved40 = parseInt(TEUsloadintercumulapproved40) + tempjthrowappvdload40;
                          TEUsloadintercumulapproved20 = parseInt(TEUsloadintercumulapproved20) + tempjthrowappvdload20;
                          TEUsdisintercumulapproved40 = parseInt(TEUsdisintercumulapproved40) + tempjthrowappvdDisc40;
                          TEUsdisintercumulapproved20 = parseInt(TEUsdisintercumulapproved20) + tempjthrowappvdDisc20;
                          TEUsNetloadintercumulapproved = TEUsloadintercumulapproved40 + TEUsloadintercumulapproved20;
                          TEUsNetdisintercumulapproved = TEUsdisintercumulapproved40 + TEUsdisintercumulapproved20;
                          TEUsintercumulapprovednet = TEUsNetloadintercumulapproved - TEUsNetdisintercumulapproved;

                            //Applied cumulative load and discharges

                          TEUsloadintercumulapply40 = parseInt(TEUsloadintercumulapply40) + tempjthrowapplyload40;
                          TEUsloadintercumulapply20 = parseInt(TEUsloadintercumulapply20) + tempjthrowapplyload20;
                          TEUsdisintercumulapply40 = parseInt(TEUsdisintercumulapply40) + tempjthrowapplyDisc40;
                          TEUsdisintercumulapply20 = parseInt(TEUsdisintercumulapply20) + tempjthrowapplyDisc20;

                      } //End of internal for loop
                  //} // End of i > 1 if condition

                  TEUsloadapply40 = 0;
                  TEUsloadapply20 = 0;
                  TEUsdisapply40 = 0;
                  TEUsdisapply20 = 0;
                  // Capturing the applied discharges for the current row and cumulative applied discharge loads till current row

                  if (parseInt(currntrow.cells[4].childNodes[0].textContent) >= 0) {
                      TEUsloadapply40 = currntrow.cells[4].childNodes[0].textContent;
                  }

                  if (parseInt(currntrow.cells[5].childNodes[0].textContent) >= 0) {
                      TEUsloadapply20 = currntrow.cells[5].childNodes[0].textContent;
                  }
                  if (parseInt(currntrow.cells[6].childNodes[0].textContent) >= 0) {
                      NetTEUloadapply = currntrow.cells[6].childNodes[0].textContent;
                  }



                  if (parseInt(currntrow.cells[10].childNodes[0].textContent) >= 0) {
                      TEUsdisapply40 = currntrow.cells[10].childNodes[0].textContent;
                  }

                  if (parseInt(currntrow.cells[11].childNodes[0].textContent) >= 0) {
                      TEUsdisapply20 = currntrow.cells[11].childNodes[0].textContent;
                  }
                  if (parseInt(currntrow.cells[12].childNodes[0].textContent) >= 0) {
                      NetTEUdisapply = currntrow.cells[12].childNodes[0].textContent;
                  }


                  TEUsloadapproved40 = 0;
                  TEUsloadapproved20 = 0;
                  TEUsdisapproved40 = 0;
                  TEUsdisapproved20 = 0;

                  // Capturing current row values of invitation flag,Approved loads & discharges of 40,20 

                  inviteflag = $("[id*=lblinvitation]", inviteflagrow).text();

                  if (parseInt(currntrow.cells[7].childNodes[1].value) > 0) {
                      TEUsloadapproved40 = currntrow.cells[7].childNodes[1].value;
                  }

                  if (parseInt(currntrow.cells[8].childNodes[1].value) > 0) {
                      TEUsloadapproved20 = currntrow.cells[8].childNodes[1].value;
                  }

                  NetTEUloadapproved = 2 * parseInt(TEUsloadapproved40) + parseInt(TEUsloadapproved20);
                  
                  if (parseInt(currntrow.cells[13].childNodes[1].value) > 0) {
                      TEUsdisapproved40 = currntrow.cells[13].childNodes[1].value;
                  }

                  if (parseInt(currntrow.cells[14].childNodes[0].value) > 0) {
                      TEUsdisapproved20 = currntrow.cells[14].childNodes[0].value;
                  }

                  NetTEUdisapproved = 2 * parseInt(TEUsdisapproved40) + parseInt(TEUsdisapproved20);

                  // calculation of Net approved TEUs

                  NetTEUsapproved = (parseInt(NetTEUloadapproved) - parseInt(NetTEUdisapproved));

                  

                  TEUsloadcumulapproved40 = TEUsloadintercumulapproved40;

                  if (TEUsloadcumulapproved40 == 0) {
                      TEUsloadcumulapproved40 = (2 * parseInt(TEUsloadapproved40));
                  } else {
                      TEUsloadcumulapproved40 = parseInt(TEUsloadcumulapproved40) + parseInt(2 * parseInt(TEUsloadapproved40));
                  }

                  

                  TEUsloadcumulapproved20 = TEUsloadintercumulapproved20;

                  if (TEUsloadcumulapproved20 == 0) {
                      TEUsloadcumulapproved20 = TEUsloadapproved20;
                  } else {
                      TEUsloadcumulapproved20 = parseInt(TEUsloadcumulapproved20) + parseInt(TEUsloadapproved20);
                  }

                  TEUsNetloadcumulapproved = TEUsNetloadintercumulapproved;

                  if (TEUsNetloadcumulapproved == 0) {
                      TEUsNetloadcumulapproved = NetTEUloadapproved;
                  } else {
                      TEUsNetloadcumulapproved = parseInt(TEUsNetloadcumulapproved) + parseInt(NetTEUloadapproved);
                  }

                  //TEUsloadintercumulapply40 = 0;

                  TEUsdiscumulapproved40 = TEUsdisintercumulapproved40;

                  if (TEUsdiscumulapproved40 == 0) {
                      TEUsdiscumulapproved40 = (2 * parseInt(TEUsdisapproved40));
                  } else {
                      TEUsdiscumulapproved40 = parseInt(TEUsdiscumulapproved40) + parseInt(2 * parseInt(TEUsdisapproved40));
                  }

                  TEUsdiscumulapproved20 = TEUsdisintercumulapproved20;

                  if (TEUsdiscumulapproved20 == 0) {
                      TEUsdiscumulapproved20 = TEUsdisapproved20;
                  } else {
                      TEUsdiscumulapproved20 = parseInt(TEUsdiscumulapproved20) + parseInt(TEUsdisapproved20);
                  }

                  TEUsNetdiscumulapproved = TEUsNetdisintercumulapproved;

                  if (TEUsNetdiscumulapproved == 0) {
                      TEUsNetdiscumulapproved = NetTEUdisapproved;
                  } else {
                      TEUsNetdiscumulapproved = parseInt(TEUsNetdiscumulapproved) + parseInt(NetTEUdisapproved);
                  }

                  TEUscumulnetapproved = TEUsNetloadcumulapproved - TEUsNetdiscumulapproved;


                  TEUsloadcumulapply40 = TEUsloadintercumulapply40;

                  if (TEUsloadcumulapply40 == 0) {
                      TEUsloadcumulapply40 = (2 * parseInt(TEUsloadcumulapply40));
                  } else {
                      TEUsloadcumulapply40 = parseInt(TEUsloadcumulapply40) + parseInt(2 * parseInt(TEUsloadapply40));
                  }

                  TEUsloadcumulapply20 = TEUsloadintercumulapply20;

                  if (TEUsloadcumulapply20 == 0) {
                      TEUsloadcumulapply20 = parseInt(TEUsloadcumulapply20);
                  } else {
                      TEUsloadcumulapply20 = parseInt(TEUsloadcumulapply20) + parseInt(TEUsloadapply20);
                  }

                  
                  TEUsdiscumulapply40 = TEUsdisintercumulapply40;

                
                      TEUsdiscumulapply40 = parseInt(TEUsdiscumulapply40) + parseInt(2 * parseInt(TEUsdisapply40));
                 

                  TEUsdiscumulapply20 = TEUsdisintercumulapply20;

                  
                      TEUsdiscumulapply20 = parseInt(TEUsdiscumulapply20) + parseInt(TEUsdisapply20);
                  
                  //Addition of net40 and net20 for each row

                      TEUsNetcumulapproved40 = TEUsloadcumulapproved40 - TEUsdiscumulapproved40;

                      TEUsNetcumulapproved20 = TEUsloadcumulapproved20 - TEUsdiscumulapproved20;

                  // Validation of approved load vs applied load
                      
                  if (parseInt(TEUsloadapproved40) > parseInt(TEUsloadapply40) && i == currentrow) {
                        $("[id*=lblreviewmsg]").text("Cannot Approve more than Applied").css("color", "red");
                        currntrow.cells[7].childNodes[1].value = onemptied;
                        validation_error_flag = true;
                   }
                      
                  if (parseInt(TEUsloadapproved20) > parseInt(TEUsloadapply20) && i == currentrow) {
                      $("[id*=lblreviewmsg]").text("Cannot Approve more than Applied").css("color", "red");
                      currntrow.cells[8].childNodes[1].value = onemptied;
                      validation_error_flag = true;
                  }
                  
                  if ((TEUscumulnetapproved > parseInt(currntrow.cells[3].childNodes[0].textContent)) && i == currentrow) {
                      currntrow.cells[7].childNodes[1].value = onemptied;
                      currntrow.cells[8].childNodes[1].value = onemptied;
                      port = $("[id*=lblshiporignport]", currntrow).text();
                      
                      $("[id*=lblreviewmsg]").text("Cannot Approve more than Available free space at "+ port).css("color", "red");
                      validation_error_flag = true;
                  }

                  if ((parseInt(TEUsloadapproved40) > parseInt(TEUsloadapply40) && i == currentrow) == false && (parseInt(TEUsloadapproved20) > parseInt(TEUsloadapply20) && i == currentrow) == false)
                  {
                      $("[id*=lblLoad40sum]").html(parseInt(TEUsloadcumulapproved40) / 2);
                      $("[id*=lblLoad20sum]").html(TEUsloadcumulapproved20);
                      $("[id*=lblDisc40sum]").html(parseInt(TEUsdiscumulapproved40) / 2);
                      $("[id*=lblDisc20sum]").html(TEUsdiscumulapproved20);
                      $("[id*=lblOnship40]").html(((parseInt(TEUsloadcumulapproved40) / 2) - (parseInt(TEUsdiscumulapproved40) / 2)));
                      $("[id*=lblOnship20]").html((TEUsloadcumulapproved20 - TEUsdiscumulapproved20));
                      $("[id*=lblNetTEUsApproved]", currntrow).html(TEUscumulnetapproved);
                      $("[id*=HiddenNet40]", currntrow).val(TEUsNetcumulapproved40/2);
                      $("[id*=HiddenNet20]", currntrow).val(TEUsNetcumulapproved20);
                      $("[id*=TxtEqvlntNetTEUsApproved]", currntrow).val(TEUscumulnetapproved);
                      $("[id*=lblNetTEUsApproved]", Approved).html(TEUscumulnetapproved);
                  }
                  
                  

                  if (validation_error_flag == false) {
                      $("[id*=lblAcceptedload]", currntrow).html(NetTEUloadapproved);
                      $("[id*=TxtEqvlntAcceptedload]", currntrow).val(NetTEUloadapproved);
                      $("[id*=lblAcceptedDischarge]", currntrow).html(NetTEUdisapproved);
                      $("[id*=TxtEqvlntAcceptedDischarge]", currntrow).val(NetTEUdisapproved);
                    }
                 
                  // Validation of approved load vs applied discharge
                  
                      var cumlminus40 = ((TEUsloadcumulapproved40 - (2 * parseInt(TEUsloadapproved40))));
                      var currminus40 = (TEUsdiscumulapproved40 - (2 * parseInt(TEUsdisapproved40)));

                      //alert("cumlminus40: " + cumlminus40 + " currminus40: " + currminus40+" i: "+i)

                      if (parseInt(cumlminus40) < 0)
                      {
                          cumlminus40 = -parseInt(cumlminus40);
                      }
                     
                      if (parseInt(currminus40) < 0) {
                          currminus40 = -parseInt(currminus40);
                      }

                      if (parseInt(cumlminus40 - currminus40) < 0)
                      {
                          load40comp = -parseInt(cumlminus40 - currminus40);
                      }
                      else
                      {
                          load40comp = parseInt(cumlminus40 - currminus40);
                      }
                     //alert("load40comp: " + (parseInt(load40comp)/2) + " TEUsdisapply40: " + TEUsdisapply40 + " i: " + i)
                      if (((TEUsloadintercumulapproved40 - TEUsdisintercumulapproved40) / 2) >= parseInt(TEUsdisapply40) && validation_error_flag == false) {
                      currntrow.cells[13].childNodes[1].value = TEUsdisapply40;
                      $("[id*=lblAcceptedDischarge]", currntrow).html(NetTEUdisapproved);
                      $("[id*=TxtEqvlntAcceptedDischarge]", currntrow).val(NetTEUdisapproved);
                      }
                      
                    

                      if ((parseInt(load40comp))/2 == parseInt(TEUsdisapply40) && validation_error_flag == false) {
                      currntrow.cells[13].childNodes[1].value = TEUsdisapply40;
                      $("[id*=lblAcceptedDischarge]", currntrow).html(NetTEUdisapproved);
                      $("[id*=TxtEqvlntAcceptedDischarge]", currntrow).val(NetTEUdisapproved);
                      }

                      if (((TEUsloadintercumulapproved40 - TEUsdisintercumulapproved40) / 2) < parseInt(TEUsdisapply40) && validation_error_flag == false)
                      {
                        currntrow.cells[13].childNodes[1].value = (TEUsloadintercumulapproved40 - TEUsdisintercumulapproved40) / 2;
                        $("[id*=lblAcceptedDischarge]", currntrow).html(NetTEUdisapproved);
                        $("[id*=TxtEqvlntAcceptedDischarge]", currntrow).val(NetTEUdisapproved);
                      }
                          

                      var cumlminus20 = (TEUsloadcumulapproved20 - parseInt(TEUsloadapproved20));
                      var currminus20 = (TEUsdiscumulapproved20 - parseInt(TEUsdisapproved20));

                      if (parseInt(cumlminus20) < 0)
                      {
                          cumlminus20 = -parseInt(cumlminus20);
                      }

                      if (parseInt(currminus20) < 0) {
                          currminus20 = -parseInt(currminus20);
                      }

                      if (parseInt(cumlminus20 - currminus20) < 0)
                      {
                          load20comp = -parseInt(cumlminus20 - currminus20);
                      }
                      else
                      {
                          load20comp = parseInt(cumlminus20 - currminus20);
                      }


                      if (load20comp > parseInt(TEUsdisapply20) && validation_error_flag == false) {
                       currntrow.cells[14].childNodes[0].value = TEUsdisapply20;
                       $("[id*=lblAcceptedDischarge]", currntrow).html(NetTEUdisapproved);
                       $("[id*=TxtEqvlntAcceptedDischarge]", currntrow).val(NetTEUdisapproved);
                      }

                      if (load20comp == parseInt(TEUsdisapply20) && validation_error_flag == false) {
                       currntrow.cells[14].childNodes[0].value = TEUsdisapply20;
                       $("[id*=lblAcceptedDischarge]", currntrow).html(NetTEUdisapproved);
                       $("[id*=TxtEqvlntAcceptedDischarge]", currntrow).val(NetTEUdisapproved);
                      }

                      if (load20comp < parseInt(TEUsdisapply20) && validation_error_flag == false)
                      {
                       currntrow.cells[14].childNodes[0].value = (TEUsloadintercumulapproved20 - TEUsdisintercumulapproved20)
                       $("[id*=lblAcceptedDischarge]", currntrow).html(NetTEUdisapproved);
                       $("[id*=TxtEqvlntAcceptedDischarge]", currntrow).val(NetTEUdisapproved);
                      }

              }
              if (validation_error_flag == false) {
                  $("[id*=btnApprove]").removeAttr("disabled");
              }
              else {
                  $("[id*=btnApprove]").attr("disabled", "disabled");

              }
          }

          //function readonly()
          //{
          //    $("[id*=TxtAccepted40Disch]").attr("disabled", "disabled");
          //    $("[id*=TxtAccepted20Disch]").attr("disabled", "disabled");
          //}
          //window.onload = readonly;
        </script>
</asp:Content>
