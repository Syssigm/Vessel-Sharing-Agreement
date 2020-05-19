<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FinalVSA.aspx.cs" Inherits="VesselSharingAgreement.FinalVSA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <div class="container banner-top-heading">
  <h3>Final VSA Details</h3>
</div>
    <div align="center">
        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
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
                  <asp:DropDownList ID="ddlVesselId" TabIndex="1" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVesselId_SelectedIndexChanged"></asp:DropDownList>
                  <asp:RequiredFieldValidator ID="ddlVesselIdRequiredFieldValidator" runat="server" ControlToValidate="ddlVesselId" ErrorMessage="Please Select the Vessel ID" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Vessel Name</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" type="text" class="form-control" ID="TxtVesselName" disabled="disabled"/>
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
                  <asp:DropDownList ID="ddlVoyageId" TabIndex="2" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVoyageId_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="ddlVoyageIdFieldValidator1" runat="server" ControlToValidate="ddlVoyageId" ErrorMessage="Please Select the Vessel ID" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Capacity in TEUs</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtCapacityTEUs" type="text" class="form-control" disabled="disabled" />
              </div>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Destination Port</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtDestinationPort" type="text" class="form-control" disabled="disabled" />
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal padding-top-0">Containers Loaded at Origin (In TEU)</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" type="text" class="form-control" ID="TxtContainersLoadedatOrigin" disabled="disabled" />
              </div>
            </div>
            </div>
			          <div class="col-md-6">

            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal padding-top-0">Gross Tonnage of Ship at Origin</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtGrossTonnage" type="text" class="form-control" disabled="disabled" />
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="auto-style1">
            <h4 class="margin-top-20">Ship Route Details</h4>
            <div class="table-responsive">
              <table class="table table-bordered">
                <thead>
                  <tr class="bg-primary">
                    <th class="max-width-90">Voyage Sequence#</th>
                    <th class="min-width-280">Origin / Transit port</th>
                    <th class="max-width-70 text-center">Expected Start <br>Date & Time</th>
                    <th class="min-width-280">Transit / Destination Port</th>
                    <th class="max-width-70 text-center">Expected Arrival <br>Date & Time</th>
                  </tr>
                </thead>
                <tbody>
                  <asp:Repeater runat="server" ID="rpt">
                        <ItemTemplate>
				                  <tr>
                    <th scope="row"><%# Eval("VoyageSegmentSequenceNumber") %></th>
                    <td><%# Eval("shiporignport") %></td>
                    <td><%# Eval("ExpectedDepartureDateTime","{0:hh:mm tt}") %></td>
                    <td><%# Eval("shipdestiport") %></td>
                    <td><%# Eval("ExpectedArrivalDateTime","{0:hh:mm tt}") %></td>
                  </tr>
                            </ItemTemplate>
                    </asp:Repeater>
                </tbody>
              </table>
            </div>
              
            <h4 runat="server" visible="false" id="labelCustomer">Customer Id</h4>
            <div class="row">
            <div class="col-sm-6">
            <div class="col-md-8">
             <asp:DropDownList ID="ddlcustomerID" Visible="false" class="form-control input-lg" runat="server" OnSelectedIndexChanged="ddlcustomerID_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
              </div>
              </div>
              </div>
              <div class="padding-top-10">
              <h4>Final VSA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label runat="server" ID="lblerrfreespace" ForeColor="red" Font-Size="Large"></asp:Label></h4>
              </div>
              <div class="padding-top-10">
              <!-- Nav tabs -->
              <ul class="nav nav-tabs responsive-tabs inner-tabs">
				<li role="presentation" class="active"><a href="#voyage" class="bold"> <img src="images/Voyage-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> Voyage Invitation Details</a></li> 
				<li role="presentation"><a href="#teus" onclick="getSelectedTabText()"  class="bold"><img src="images/TEUs-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> TEUs </a></li>
				<li role="presentation"><a href="#charges" class="bold"> <img src="images/Price-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> Charges</a></li>
              </ul>
                <!--<ul class="nav nav-tabs responsive-tabs inner-tabs">
				<li role="presentation" class="active"><a href="#voyage" class="bold"><i class="fa fa-ship" aria-hidden="true"></i> Voyage Invitation Details</a></li>
				<li role="presentation"><a href="#teus" class="bold"><i class="fa fa-cubes" aria-hidden="true"></i> TEUs</a></li>
				<li role="presentation"><a href="#charges" class="bold"><i class="fa fa-usd" aria-hidden="true"></i> Charges</a></li>
              </ul>-->              <!-- Tab panes -->
              <div class="tab-content">
                <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10 active" id="voyage">
                  <div class="table-responsive">
                    <table class="table table-bordered table-striped" id="myTablevoyage">
                      <thead>
                        <tr class="bg-primary">
                          <th class="max-width-90 text-center">Voyage Sequence#</th>
                          <th class="text-center">Voyage Segment</th>
                          <th class="max-width-90">Vessel Agreement Id <i class="fa fa-info-circle" title="Cargo Operators, Agents, Vessel Operators etc."></i></th>
                          <th class="max-width-100">Inviting vessel sharing Participants?<i class="fa fa-info-circle" title="Cargo Operators, Agents, Vessel Operators etc."></i></th>
                          <th class="max-width-80 text-center">Total Number of VSA Participants Allowed</th>
                          <th>VSA Notes</th>
                        </tr>
                      </thead>
                      <tbody>
                          <asp:Repeater runat="server" ID="rptVoyageInvitationDetails">
                              <ItemTemplate>
                        <tr>
                          <td>
                             <asp:Label ID="lblVoyageSegmentSequenceNumber" runat="server" Text='<%# Eval("VoyageSegmentSequenceNumber") %>'></asp:Label></td>
                          <td>
                              <asp:Label ID="lblshiporignport" runat="server" Text='<%# Eval("shiporignport") %>'></asp:Label>
                          </td>
                          <td><%# Eval("VesselAgreementId") %></td>
                          <td>
                          <asp:Label ID="lblckshareParticipants" runat="server" Text='<%# Eval("VSAParticipantsFlag") %>'></asp:Label>
                          </td>
                          <td class="text-center">
                              <asp:Label ID="lblTotalParticipants" runat="server" Text='<%# Eval("MaxNumberofParticipants") %>'></asp:Label>
                          </td>
                          <td>
                              <asp:TextBox runat="server" TextMode="MultiLine" Height="80px" Width="500px" ReadOnly="true" ID="TextBox1" Text='<%# Eval("VSANotes") %>'></asp:TextBox>
                          </td>
                        </tr>
                        </ItemTemplate>
                          </asp:Repeater>
                      </tbody>
                    </table>
                  </div>
                </div>
                <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10" id="teus">
                  <div class="table-responsive">
                    <table class="table table-bordered table-striped" id="myTableTEUs">
                      <thead>
                        <tr class="bg-primary">
                          <th class="max-width-90">Voyage Sequence#</th>
                          <th class="text-center">Voyage Segment</th>
                          <th class="text-center">Initial Available free space in TEUs</th>
                          <th class="text-center">Accepted Quantity of load in 40' eq TEU</th>
                          <th class="text-center">Accepted Quantity of load in 20' eq TEU</th>
                          <th class="text-center">Accepted Quantity of Discharge 40' eq TEU</th>
                          <th class="text-center">Accepted Quantity of Discharge 20' eq TEU</th>
                          <th class="text-center brandcolorBg">Net TEUs Applied for Voyage Segment</th>
                          <th class="text-center brandcolorBg"><div class="text-center">Approved for Voyage Segment(Net TEUs +Loaded/<br />-Discharge)</div></th>
                        </tr>
                      </thead>
                      <tbody>
                          <asp:Repeater runat="server" ID="rptTEUs">
                              <ItemTemplate>
                        <tr>
                          <th scope="row"><asp:Label ID="lblVoyageSegmentSequenceNumber" runat="server" Text='<%# Eval("VoyageSegmentSequenceNumber") %>'></asp:Label></th>
                          <td><asp:Label ID="lblshiporignport" runat="server" Text='<%# Eval("shiporignport") %>'></asp:Label></td>
                          
                          <td class="text-center">
                              <asp:Label ID="lblInitialAvailableSpaceTEU" runat="server" Text='<%# Eval("InitialAvailableSpaceTEU") %>'></asp:Label>
                          </td>
                          <td class="text-center">
                              <asp:Label ID="lblAccepted40Load" runat="server" Text='<%# Eval("lblAccepted40Load") %>'></asp:Label>
                          </td>
                            <td class="text-center">
                              <asp:Label ID="lblAccepted20Load" runat="server" Text='<%# Eval("lblAccepted20Load") %>'></asp:Label>
                          </td>
                          
                            <td class="text-center">
                              <asp:Label ID="lblAccepted40Disch" runat="server" Text='<%# Eval("lblAccepted40Disch") %>'></asp:Label>
                          </td>
                            <td class="text-center">
                              <asp:Label ID="lblAccepted20Disch" runat="server" Text='<%# Eval("lblAccepted20Disch") %>'></asp:Label>
                          </td>
                          <td class="text-center">
                              <asp:Label ID="lblNetTEUsApplied" runat="server" Text='<%# Eval("NetTEUSowned") %>'></asp:Label>
                          </td>
                            <td class="text-center">
                              <asp:Label ID="lblNetTEUsApp" runat="server" Text='<%# Eval("lblNetTEUsApp") %>'></asp:Label>
                          </td>
                        </tr>
                        </ItemTemplate>
                          </asp:Repeater>
                      </tbody>
                    </table>
                  </div>
                </div>
              
                <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10 " id="charges">
                  <div class="table-responsive">
                    <table class="table table-bordered table-striped" id="myTableCharges">
                      <thead>
                        <tr class="bg-primary">
                          <th class="max-width-100">Voyage Sequence#</th>
                          <th rowspan="2"  class="min-width-200">Voyage Segment</th>
                          <th class="min-width-70">Charge by TEU or Container Size</th>
                          <th class="text-center">Charge per 20 FTTEU in USD</th>
                          <th class="text-center">Price by 40ft containers in USD</th>
                          <th class="text-center">Price by 20ft containers in USD</th>
                          <th class="text-center">Approved Net TEUs</th>
                          <th class="text-center">Equivalent 40 Ft TEU Containers</th>
                          <th class="text-center">Equivalent 20 Ft TEU Containers</th>
                          <th class="text-center"> Agreeing for VSA Arrangement Fees in USD	</th>
                          <th class="text-center">VSA Arrangement Fee(USD) for TEUs</th>
                          <th class="text-center">Price of TEUs in USD</th>
                          <th class="text-center">Total Price of TEUs in USD</th>
                        </tr>
                        
                      </thead>
                      <tbody id="my">
                          <asp:Repeater runat="server" ID="rptcharges">
                              <ItemTemplate>
                        <tr>
                          <th scope="row"><asp:Label ID="lblVoyageSegmentSequenceNumber" runat="server" Text='<%# Eval("VoyageSegmentSequenceNumber") %>'></asp:Label></th>
                          <td><asp:Label ID="lblshiporignport" runat="server" Text='<%# Eval("shiporignport") %>'></asp:Label></td>
                          <td>
                              <asp:Label ID="lblchargesPerTue" runat="server" Text='<%# Eval("lblchargesPerTue") %>'></asp:Label>
                          </td>
                          <td>
                              <asp:Label ID="lblPricePerTue" runat="server" Text='<%# Eval("lblPricePerTue") %>'></asp:Label>
                          </td>
                          <td>
                              <asp:Label ID="lbl40Size" runat="server" Text='<%# Eval("lbl40Size") %>'></asp:Label>
                          </td>
                          <td>
                              <asp:Label ID="lbl20Size" runat="server" Text='<%# Eval("lbl20Size") %>'></asp:Label>
                          </td>
                            <td>
                                <asp:Label ID="lblNetTeus" runat="server" Text='<%# Eval("lblNetTEUsApp") %>'/>
                            </td>
                            <td>
                                <asp:Label ID="lblEquivalent40FtContainers" runat="server" Text='<%# Eval("lblEquivalent40FtContainers") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEquivalent20FtContainers" runat="server" Text='<%# Eval("lblEquivalent20FtContainers") %>'></asp:Label>
                            </td>
                          <td>
                              <asp:Label ID="lblckFees" runat="server" Text='<%# Eval("lblckFees") %>'></asp:Label>
                          </td>
                          <td>
                              <asp:Label ID="lblAgreementfee" runat="server" Text='<%# Eval("lblAgreementfee") %>'></asp:Label>
                          </td>
                            <td><asp:Label ID="lblTotalprice" runat="server" Text='<%# Eval("lblTotalprice") %>'></asp:Label></td>
                            <td><asp:Label ID="lblpriceofallTeus" runat="server" Text='<%# Eval("lblpriceofallTeus") %>'></asp:Label></td>
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
        <%--<div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10 text-center">
                <asp:Button ID="btnInvite" runat="server" TabIndex="3" class="btn btn-primary btn-lg min-width-200" Text="Invite" OnClick="btnInvite_Click" />
                <asp:Button ID="Btncancel" runat="server" TabIndex="4" class="btn btn-default btn-lg" Text="Cancel" OnClick="Btncancel_Click" />
            </div>
          </div>
        </div>--%>
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
</asp:Content>
