 <%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VsaInvite.aspx.cs" Inherits="VesselSharingAgreement.VsaInvite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 100%;
            left: -6px;
            top: 2px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container banner-top-heading">
  <h3>Invitation for Vessel Sharing to Transport Container Cargo</h3>
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
            <h4>Free Space Availability in TEUs&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label runat="server" ID="lblerrfreespace" ForeColor="red" Font-Size="Large"></asp:Label></h4>
            <div class="padding-top-10">
              <!-- Nav tabs -->
              <ul class="nav nav-tabs responsive-tabs inner-tabs">
				<li role="presentation" class="active"><a href="#voyage" class="bold"> <img src="images/Voyage-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> Voyage Invitation Details <br /><input type="checkbox" onchange="invite()" id="CkVoyage" runat="server" value="1" name="select" validate="required:true, minlength:1"/> <span class="cr"></span><asp:Label runat="server" id="incomplete" ForeColor="Red">Incomplete</asp:Label><asp:Label runat="server" id="Completed" ForeColor="Green" style="display:none">Complete</asp:Label> </a></li> 
				<li role="presentation"><a href="#teus" onclick="getSelectedTabText()"  class="bold"><img src="images/TEUs-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> TEUs <br /> <input type="checkbox" onchange="invite()" id="CkTEUS" runat="server" checked="checked" value="1" name="select" validate="required:true, minlength:1"/><span class="cr"></span><asp:Label runat="server" id="lblTeusComplete" ForeColor="Green">Completed</asp:Label></a></li>
				<li role="presentation"><a href="#charges" class="bold"> <img src="images/Price-img-01.png" class="img-responsive max-height-80 inline-block img-circle" /> Charges<br /> <input type="checkbox" onchange="invite()" runat="server" value="1" id="CKCharges" name="select" validate="required:true, minlength:1"/><span class="cr"></span><asp:Label runat="server" id="lblChargesInComplete" ForeColor="Red">Incomplete</asp:Label><asp:Label runat="server" id="lblChargesComplete" ForeColor="Green" style="display:none">Complete</asp:Label></a></li>
              </ul>
<!--              <ul class="nav nav-tabs responsive-tabs inner-tabs">
				<li role="presentation" class="active"><a href="#voyage" class="bold"><i class="fa fa-ship" aria-hidden="true"></i> Voyage Invitation Details</a></li>
				<li role="presentation"><a href="#teus" class="bold"><i class="fa fa-cubes" aria-hidden="true"></i> TEUs</a></li>
				<li role="presentation"><a href="#charges" class="bold"><i class="fa fa-usd" aria-hidden="true"></i> Charges</a></li>
              </ul>
-->              <!-- Tab panes -->
              <div class="tab-content">
                <div role="tabpanel" class="gray-border-left gray-border-right gray-border-bottom whiteBg tab-pane padding-10 active" id="voyage">
                  <div class="table-responsive">
                    <table class="table table-bordered table-striped" id="myTablevoyage">
                      <thead>
                        <tr class="bg-primary">
                          <th class="max-width-90 text-center">Voyage Sequence#</th>
                          <th class="text-center">Voyage Segment</th>
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
                              <asp:Label ID="lblorignport" Visible="false" runat="server" Text='<%# Eval("shiporign") %>'></asp:Label>
                          </td>
                          <td>
                              <input type="checkbox" id="ckshareParticipants" checked="checked" onchange="check(this)" runat="server" value="1" name="select" validate="required:true, minlength:1"/>
                              <%--<asp:CheckBox ID="ckshareParticipants" onchange="doAddr(this)" Checked="true" runat="server" />--%>
                              <asp:Label ID="lblckshareParticipants" Visible="false" runat="server" Text='<%# Eval("VSAParticipantsFlag") %>'></asp:Label>
                          </td>
                          <td class="text-center"><asp:TextBox value="0" min="0" onchange="check(this)"  runat="server" ID="TxtTotalParticipants" class="form-control inline-block max-width-70 text-right grp"/>
                              <asp:Label ID="lblTotalParticipants" Visible="false" runat="server" Text='<%# Eval("MaxNumberofParticipants") %>'></asp:Label>
                              <asp:RegularExpressionValidator  Runat="server" ID="valNumbersOnly" ControlToValidate="TxtTotalParticipants" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                          </td>
                          <td>
                              <asp:TextBox ID="TxtVSANotes" MaxLength="500" TextMode="MultiLine" class="form-control min-width-200 grp" placeholder="VSA Notes" runat="server"></asp:TextBox>
                              <asp:Label ID="lblVSANotes" Visible="false" runat="server" Text='<%# Eval("VSANotes") %>'></asp:Label>
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
                          <th class="text-center">TEUs on ship</th>
                          <th class="text-center">TEUs Discharged</th>
                          <th class="text-center">TEUs Loaded</th>
                          <th class="text-center">Total TEUs</th>
                          <th class="max-width-90 text-center">Available free space in TEUs</th>
                        </tr>
                      </thead>
                      <tbody>
                          <asp:Repeater runat="server" ID="rptTEUs">
                              <ItemTemplate>
                        <tr>
                          <th scope="row"><asp:Label ID="lblVoyageSegmentSequenceNumber" runat="server" Text='<%# Eval("VoyageSegmentSequenceNumber") %>'></asp:Label></th>
                          <td><asp:Label ID="Label2" runat="server" Text='<%# Eval("shiporignport") %>'></asp:Label></td>
                          <td class="text-center"><asp:TextBox runat="server" min="0" onclick="doAdd(this)" onchange="doAdd(this)" name="txt" value="0" id="TxtTEUsonship" type="text" class="txt form-control inline-block max-width-120 text-right grp" />
                              <asp:Label ID="lblTEUsonship" runat="server" Visible="false" Text='<%# Eval("lblTEUsonship") %>'></asp:Label>
                              <asp:RegularExpressionValidator  Runat="server" ID="valTEUsonship" ControlToValidate="TxtTEUsonship" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                          </td>
                          <td class="text-center"><asp:TextBox runat="server" min="0" onchange="doAdd(this)" value="0" ID="TxtTEUsDischarged" type="text" class="form-control inline-block max-width-120 text-right grp" />
                              <asp:Label ID="lblTEUsDischarged" runat="server" Visible="false" Text='<%# Eval("lblTEUsDischarged") %>'></asp:Label>
                                 <asp:RegularExpressionValidator  Runat="server" ID="valTEUsDischarged" ControlToValidate="TxtTEUsDischarged" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                              <asp:TextBox runat="server" min="0" Visible="false" value="0" ID="edtTxtTEUsDischarged" Text='<%# Eval("lblTEUsDischarged") %>' type="text" class="form-control inline-block max-width-120 text-right" />
                          </td>
                          <td class="text-center"><asp:TextBox runat="server" min="0" onchange="doAdd(this)" value="0" ID="TxtTEUsLoaded" type="text" class="form-control inline-block max-width-120 text-right grp" />
                              <asp:Label ID="lblTEUsLoaded" runat="server" Visible="false" Text='<%# Eval("lblTEUsLoaded") %>'></asp:Label>
                            <asp:RegularExpressionValidator  Runat="server" ID="valTEUsLoaded" ControlToValidate="TxtTEUsLoaded" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                          <asp:TextBox runat="server" min="0" Visible="false" value="0" ID="edtTxtTEUsLoaded" Text='<%# Eval("lblTEUsLoaded") %>' type="text" class="form-control inline-block max-width-120 text-right" />
                          </td>
                          <td class="text-center"><asp:Label ID="lblsum" min="0" value="0" runat="server" ></asp:Label>
                              <asp:Label ID="lblTotalTEUS" runat="server" Visible="false" Text='<%# Eval("lblTotalTEUS") %>'></asp:Label>
                          </td>
                          <td class="text-right"><asp:Label ID="lblFreespace" min="0" value="0" runat="server" ></asp:Label>
                              <asp:Label ID="lblAvailabeFreespace" Visible="false" runat="server" Text='<%# Eval("lblAvailabeFreespace") %>'></asp:Label>
                          </td>
                            <td class="freespace text-right" hidden="hidden"><%# Eval("VesselCapacityTEU") %></td>
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
                          <%--<th rowspan="2"  class="max-width-120">Inviting Vessel Sharing Participants? <i class="fa fa-info-circle" title="Cargo Operators, Agents, Vessel Operators etc."></i></th>--%>
                          <th class="min-width-70">Charge by TEU or Container Size?</th>
                            <th class="text-center">Charge per TEU in USD</th>
                          <th class="">40' Size</th>
                          <th class="">20' Size</th>
                          <th class="max-width-120"> Agreeing for VSA Arrangement Fees in USD	</th>
                          <th class="max-width-120">VSA Arrangement Fee(USD) for each TEU</th>
                            <th class="max-width-120" runat="server" visible="false" id="AllowtoInviteTH"></th>
                        </tr>
                        
                      </thead>
                      <tbody id="my">
                          <asp:Repeater runat="server" ID="rptcharges">
                              <ItemTemplate>
                        <tr>
                          <th scope="row"><asp:Label ID="Label3" runat="server" Text='<%# Eval("VoyageSegmentSequenceNumber") %>'></asp:Label></th>
                          <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("shiporignport") %>'></asp:Label></td>
                          <%--<td><asp:Label ID="lblshareParticipants" runat="server" Text="Yes"></asp:Label></td>--%>
                          <td><asp:DropDownList runat="server" ID="ddlchargesPer" onchange="checkcharges(this)" class="form-control grp">
                              <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                              <asp:ListItem>TEU</asp:ListItem>
                              <asp:ListItem>ContainerSize</asp:ListItem></asp:DropDownList>
                              <asp:Label ID="lblchargesPerTue" Visible="false" runat="server" Text='<%# Eval("lblchargesPerTue") %>'></asp:Label>
                              <%--<asp:RequiredFieldValidator ID="ddlchargesPerRequiredFieldValidator" runat="server" ControlToValidate="ddlchargesPer" ErrorMessage="Please Select the Charge By" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                          </td>
                          <td><asp:TextBox runat="server" type="text" min="0" value="0" onkeypress="checkcharges(this)" onkeyup="checkcharges(this)" onchange="checkcharges(this)" ID="TxtPricePerTue" onclick="enableInvite(this);" class="form-control text-right grp" />
                              <asp:Label ID="lblPricePerTue" Visible="false" runat="server" Text='<%# Eval("lblPricePerTue") %>'></asp:Label>
                          <asp:RegularExpressionValidator  Runat="server" ID="valPricePerTue" ControlToValidate="TxtPricePerTue" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                          </td>
                          <td><asp:TextBox runat="server" type="text" min="0" value="0" ID="Txt40Size" onclick="enableInvite(this);" onkeypress="checkcharges(this)" onkeyup="checkcharges(this)" onchange="checkcharges(this)" class="form-control text-right grp" />
                              <asp:Label ID="lbl40Size" Visible="false" runat="server" Text='<%# Eval("lbl40Size") %>'></asp:Label>
                          <asp:RegularExpressionValidator  Runat="server" ID="val40Size" ControlToValidate="Txt40Size" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                          </td>
                          <td><asp:TextBox runat="server" type="text" min="0" value="0" ID="Txt20Size" onclick="enableInvite(this);" onkeypress="checkcharges(this)" onkeyup="checkcharges(this)" onchange="checkcharges(this)" class="form-control text-right grp" />
                              <asp:Label ID="lbl20Size" Visible="false" runat="server" Text='<%# Eval("lbl20Size") %>'></asp:Label>
                          <asp:RegularExpressionValidator  Runat="server" ID="val20Size" ControlToValidate="Txt20Size" ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter only Numeric values" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                          </td>
                          <td><asp:CheckBox ID="ckFees" Checked="true" runat="server" class="grp" />
                              <asp:Label ID="lblckFees" runat="server" Visible="false" Text='<%# Eval("lblckFees") %>'></asp:Label>
                          </td>
                          <td><%--<div class="text-right">$123</div>--%>
                              <asp:Label ID="lblAgreementfees" runat="server" Text='<%# Eval("lblAgreementfee") %>'></asp:Label>
                              <asp:Label ID="lblAgreementfee" runat="server" Visible="false" Text='<%# Eval("lblAgreementfee") %>'></asp:Label>
                          </td>
                            <td runat="server" id="AllowtoInviteTD" visible="false" width="90px">
                                <asp:Button ID="EditAllowtoInvite" runat="server" Visible="false" TabIndex="5" CommandArgument='<%# Eval("VoyageSegmentSequenceNumber") %>' class="btn btn-success btn-sm" Text="Invite" OnClick="BtneditAllowtoInvite_Click"/>
                                <asp:LinkButton ID="EditAllowtoSave" runat="server" Visible="false" TabIndex="6" CommandArgument='<%# Eval("VoyageSegmentSequenceNumber") %>' class="btn btn-success btn-sm" Text="Save" OnClick="lnkeditAllowtoSave_Click"><span class="glyphicon glyphicon-floppy-disk"></span></asp:LinkButton>
                                <asp:LinkButton ID="EditAllowtoCancel" runat="server" Visible="false" TabIndex="7" class="btn btn-success btn-sm" OnClick="EditAllowtoCancel_Click" Text="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            </td>
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
            <div class="padding-top-10 text-center"><%--<a href="#" class="btn btn-primary btn-lg min-width-200" accesskey="i"><span class="underline">I</span>nvite</a>--%> <%--<a href="#" class="btn btn-info btn-lg" accesskey="e"><span class="underline">E</span>dit / Modify</a>--%> <%--<a href="#" class="btn btn-default btn-lg" accesskey="c"><span class="underline">C</span>ancel</a>--%>
                <asp:Button ID="btnInvite" runat="server" TabIndex="3" class="btn btn-primary btn-lg min-width-200" Text="Invite" OnClick="btnInvite_Click" />
                <asp:Button ID="Btncancel" runat="server" TabIndex="4" class="btn btn-default btn-lg" Text="Cancel" OnClick="Btncancel_Click" />
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
<script>
$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});
</script>
    
    <script src="js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function invitecharges() {
            //alert("invitecharges");
            $("[id*=ddlchargesPer]").change(function () {
                if ($(this).val() == "TEU") {
                    var row = $(this).closest("tr");
                    $("[id*=TxtPricePerTue]",row).removeAttr('disabled');
                $("[id*=TxtPricePerTue]",row).attr("enabled", "enabled");
                $("[id*=Txt40Size]",row).attr("disabled", "disabled");
                $("[id*=Txt20Size]",row).attr("disabled", "disabled");
            } 
                if ($(this).val() == "ContainerSize") {
                    var row = $(this).closest("tr");
                $("[id*=TxtPricePerTue]",row).attr("disabled", "disabled");
                $("[id*=Txt40Size]",row).removeAttr('disabled');
                $("[id*=Txt40Size]",row).attr("enabled", "enabled");
                $("[id*=Txt20Size]",row).removeAttr('disabled');
                $("[id*=Txt20Size]",row).attr("enabled", "enabled");
                }
                if ($(this).val() == "0") {
                    var row = $(this).closest("tr");
                    $("[id*=TxtPricePerTue]", row).attr("disabled", "disabled");
                    
                    $("[id*=Txt40Size]", row).attr("disabled", "disabled");
                    
                    $("[id*=Txt20Size]", row).attr("disabled", "disabled");
                    
                }
            });
        });
        window.onload = invitecharges;
</script>

    <script type="text/javascript">
        function getColumnCount()
    {
        
            return document.getElementById('myTableTEUs').getElementsByTagName('tr')[0].getElementsByTagName('th').length;
    }
 
    function getRowCount()
    {
        
        return document.getElementById('myTableTEUs').rows.length;
    }
    
    function doAdd(ths)
    {
        //alert("hi");
        var lastCol = getColumnCount()-1;
        var lastRow = getRowCount()-1;
            
        //To find out Total TEUs
        var table = document.getElementById("myTableTEUs");
        
        var row = table.rows[ths.parentNode.parentNode.rowIndex];
        //alert("row:" + row);
        var totalTEUs = 0;
        var TEUloadOrgn = document.getElementById('<%= TxtContainersLoadedatOrigin.ClientID %>').value;
        var shipCapacity = document.getElementById('<%= TxtCapacityTEUs.ClientID %>').value;
            
        var availableFreeSpace = 0;

        //assign TEUloadOrgn and shipCapacity  from page
        row.cells[2].childNodes[0].value = TEUloadOrgn;

        //Total TEUs = TEUs on Ship minus TEUs Discharged plus TEUs Loaded
        
        totalTEUs=eval(row.cells[2].childNodes[0].value) - eval(row.cells[3].childNodes[0].value) + eval(row.cells[4].childNodes[0].value) ;
        row.cells[lastCol-1].innerHTML = totalTEUs;

        //Available Free Space = Ship Capacity minus Total TEUs on ship
        availableFreeSpace = shipCapacity - totalTEUs;
        row.cells[lastCol].innerHTML = availableFreeSpace;

        var cIndex = ths.parentNode.cellIndex;

        for(var i=1;i<lastRow;i++) {
            //Set TEUs on Ship on next row and make it editable. To do this, the HTML code for input needs to be put in again.
            var nxtrow = table.rows[i+1];
            var nxtrowtotalTEUs = 0;
            var nxtrowavailableFreeSpace = 0;

            var j=0;
            //This is the HTML code for input. It needs to be put in again using below technique
            //var inputtextbeg = '<input type="text"  name="inpTEUsonship" value="';
            //var inputtextend = '" onchange="doAdd(this)">';

                

            j = table.rows[i].cells[5].innerHTML;
                
            //table.rows[i + 1].cells[2].innerHTML = inputtextbeg + j + inputtextend;
            nxtrow.cells[2].childNodes[0].value = j;


            //Recompute Total TEUs and Available Free Space on next row to reflect new values
            nxtrowtotalTEUs=eval(nxtrow.cells[2].childNodes[0].value) - eval(nxtrow.cells[3].childNodes[0].value) + eval(nxtrow.cells[4].childNodes[0].value) ;
            nxtrow.cells[lastCol-1].innerHTML = nxtrowtotalTEUs;

            nxtrowavailableFreeSpace = shipCapacity - nxtrowtotalTEUs;
            nxtrow.cells[lastCol].innerHTML = nxtrowavailableFreeSpace;
        } 
    
    }
    </script>
    
<script type="text/javascript">

    function getColumn() {

        return document.getElementById('myTablevoyage').getElementsByTagName('tr')[0].getElementsByTagName('th').length;
    }

    function getRow() {

        return document.getElementById('myTablevoyage').rows.length;
    }

    function check(ths) {
        //alert("hi");
        var lastCol = getColumn() - 1;
        var lastRow = getRow() - 1;

        var inviteflag;
        var TotalParticipants = 0
        

        
        for (var i = 1; i <= lastRow; i++) {
            //alert("i:" + i);
            var table = document.getElementById("myTablevoyage");

            var currrow = table.rows[i];
            //alert("nxtrow:" + nxtrow);

            TotalParticipants = currrow.cells[3].childNodes[0].value;
            inviteflag = currrow.cells[2].childNodes[1].checked;
            
            if ((inviteflag == true && TotalParticipants > 0) || inviteflag == false)
            {
                $("[id*=CkVoyage]").attr('checked', true);
                    $("[id*=Completed]").show();
                    $("[id*=incomplete]").hide();
                    if(inviteflag == false)
                    {
                        currrow.cells[3].childNodes[0].value = 0;
                    }
            }
            else
            {
                $("[id*=CkVoyage]").attr('checked', false);
                $("[id*=Completed]").hide();
                $("[id*=incomplete]").show();
                break;
            }
            
        }
        invite();
    }
    window.onload = check;

    
    
    function getColumnCT() {

        return document.getElementById('myTableCharges').getElementsByTagName('tr')[0].getElementsByTagName('th').length;
    }

    function getRowCT() {

        return document.getElementById('myTableCharges').rows.length;
    }

    function checkcharges(ths) {
        //alert("hi");
        var lstCol = getColumnCT() -1;
        var lstRow = getRowCT()-1;

        //alert(lstRow);
        //To find out Total TEUs

        var table = document.getElementById("myTableCharges");

        var row = table.rows[ths.parentNode.parentNode.rowIndex];

        var TEUCount = 0;
        var ContainerCount = 0;
        var notselcount = 0;
        var total = 0;
        var Voyage, Charges, TEUS;
        for (var i = 1; i <= lstRow; i++)
        {
            //alert("i:" + i);
            var table = document.getElementById("myTableCharges");
            
            var nxtrow = table.rows[i];
            //alert("nxtrow:" + nxtrow);

            var dropdown = nxtrow.cells[2].childNodes[0].value;

            //alert("dropdown:" + dropdown)
            
            var ChargeTEU = nxtrow.cells[3].childNodes[0].value;

            //alert("ChargeTEU:" + ChargeTEU)

            var Containe40= nxtrow.cells[4].childNodes[0].value;

            //alert("Containe40:" + Containe40)

            var Containe20 = nxtrow.cells[5].childNodes[0].value;

            //alert("Containe20:" + Containe20);

            
            if (dropdown == 'TEU') {
                if (ChargeTEU > 0) {
                    TEUCount = TEUCount + 1;
                    total = (parseInt(TEUCount) + parseInt(ContainerCount));
                    //alert("TEUCount:" + TEUCount);
                }
            }
             if (dropdown == 'ContainerSize') {

                if (Containe40 > 0 && Containe20 > 0) {
                    ContainerCount = ContainerCount + 1;
                    total = (parseInt(TEUCount) + parseInt(ContainerCount));
                    //alert("ContainerCount:" + ContainerCount);
                }
                } 
             
             if (dropdown == 0) 
             {
                 //alert("select");
                 invite();
                 notselcount = notselcount + 1;
                 total = (parseInt(TEUCount) + parseInt(ContainerCount));
                 nxtrow.cells[3].childNodes[0].value = 0;
                 nxtrow.cells[4].childNodes[0].value = 0;
                 nxtrow.cells[5].childNodes[0].value = 0;

                 $("[id*=TxtPricePerTue]", nxtrow).attr("disabled", "disabled");

                 $("[id*=Txt40Size]", nxtrow).attr("disabled", "disabled");

                 $("[id*=Txt20Size]", nxtrow).attr("disabled", "disabled");

            }

            if ((parseInt(TEUCount) + parseInt(ContainerCount)) == lstRow)
            {
                //alert("last if");
                $("[id*=CKCharges]").attr('checked', true);
                $("[id*=lblChargesComplete]").show();
                $("[id*=lblChargesInComplete]").hide();
                
            }
            else
            {
                $("[id*=CKCharges]").attr('checked', false);
                $("[id*=lblChargesComplete]").hide();
                $("[id*=lblChargesInComplete]").show();
            }
            
        }

        invite();
    }
    window.onload = checkcharges;
    
    function invite()
    {
        //alert("invite");
        if ($("[id*=CkVoyage]").is(':checked')) {
            Voyage = true;
            //alert("Voyage is :" + Voyage);
        }
        else {
            Voyage = false;
            //alert("Voyage is :" + Voyage);
        }


        if ($("[id*=CkTEUS]").is(':checked')) {
            TEUS = true;
            //alert("CkTEUS is :" + TEUS);
        }
        else {
            //alert("CkTEUS is false");
            TEUS = false;
        }


        if ($("[id*=CKCharges]").is(':checked')) {
            Charges = true;
            //alert("Charges is :" + Charges);
        }
        else {
            Charges = false;
            //alert("Charges is :" + Charges);
        }

        //alert("Voyage,Charges,TEUS is :" + Voyage + Charges + TEUS);

        if (Voyage == true && Charges == true && TEUS == true) {

            //alert("Inside Voyage,Charges,TEUS is :" + Voyage + Charges + TEUS);
            $("[id*=btnInvite]").removeAttr("disabled");
        }
        else {
            $("[id*=btnInvite]").attr("disabled", "disabled");
        }
    }
    window.onload = invite;

    

</script>



</asp:Content>
