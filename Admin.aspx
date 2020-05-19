<%@ Page Title="" Language="C#" MasterPageFile="~/VSAadmin.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="VesselSharingAgreement.Admin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container banner-top-heading">
  <h3>VSA SUPER ADMIN</h3>
</div>
    <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="form-horizontal">


            <%--<div class="col-md-7">
               <div class="input-group my-group">
                   <asp:DropDownList ID="ddlCategory" onchange="$find('AutoCompleteExtender1').set_contextKey(this.value);" CssClass="btn btn-default dropdown-toggle" runat="server">
                       <asp:ListItem Selected="True" Value="0">Select Here</asp:ListItem>
                       <asp:ListItem Value="1">Port</asp:ListItem>
                       <asp:ListItem Value="2">Customer</asp:ListItem>
                       <asp:ListItem Value="3">Vessel</asp:ListItem>
                       <asp:ListItem Value="4">Vessel Voyage</asp:ListItem>
                       <asp:ListItem Value="5">VSA Invites</asp:ListItem>
                       <asp:ListItem Value="6">VSA Applications</asp:ListItem>
                       <asp:ListItem Value="7">Final VSA</asp:ListItem>
                   </asp:DropDownList>
                  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
          <%--<select id="vsacat" class="selectpicker form-control" data-live-search="true" title="Select">
            <option selected="selected">Select Here</option>
            <option>Port</option>
            <option>Customer</option>
            <option>Vessel</option>
            <option>Vessel Voyage</option>
            <option>VSA Invites</option>
            <option>VSA Applications</option>
            <option>Final VSA</option>
          </select>
          <asp:TextBox runat="server" ID="TxtSearchBox" type="text" class="form-control" name="snpid" placeholder="Type your text" />
                  <cc1:AutoCompleteExtender ServiceMethod="Search" 
                            MinimumPrefixLength="1" UseContextKey = "true"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" 
                            TargetControlID="TxtSearchBox"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
                        </cc1:AutoCompleteExtender>
          <span class="input-group-btn">
          <asp:LinkButton runat="server" ID="lnkSearch" class="btn btn-lg btn-success" type="submit" OnClick="lnkSearch_Click"><i class="fa fa-search"></i> Search</asp:LinkButton>
          </span> </div>
              </div>--%>
        <div class="form-group form-group-lg margin-top-10 margin-bottom-0">
            <h4 class="col-md-3 control-label padding-top-25">Select VSA Category:</h4>
            <div class="col-lg-6 col-md-6 col-sm-4 col-xs-12 padding-top-25 text-center">
                        <div class="input-group">
                            <div class="input-group-btn">
                                <!--<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">All Categories <span class="caret"></span></button>-->
                                <asp:DropDownList ID="ddlCategory" AutoPostBack="true" Width="150" class="form-control input-lg" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server">
                                    <asp:ListItem Selected="True" Value="0">Select Here</asp:ListItem>
                       <asp:ListItem Value="1">Port</asp:ListItem>
                       <asp:ListItem Value="2">Customer</asp:ListItem>
                       <asp:ListItem Value="3">Vessel</asp:ListItem>
                       <asp:ListItem Value="4">Vessel Voyage</asp:ListItem>
                       <asp:ListItem Value="5">VSA Invite</asp:ListItem>
                       <asp:ListItem Value="6">VSA Applications</asp:ListItem>
                       <asp:ListItem Value="7">Final VSA</asp:ListItem>
                                </asp:DropDownList>
                                
                                
                            </div>
                            <div class="input-group-btn">
                                <asp:TextBox ID="TxtSearchBox" CssClass="form-control" aria-label="..." Width="290" runat="server" placeholder="Search"></asp:TextBox>
                                </div>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="lnkSearch" Class="btn btn-primary btn-lg min-width-100" runat="server" OnClick="lnkSearch_Click"><span class="glyphicon glyphicon-search"></span>Search</asp:LinkButton>
                                </div>
                            <!-- /btn-group -->
                            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
                              
                           <cc1:AutoCompleteExtender ServiceMethod="Search" 
                            MinimumPrefixLength="1" UseContextKey = "true"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" 
                            TargetControlID="TxtSearchBox"  
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
                        </cc1:AutoCompleteExtender>
                            <div class="input-group-btn">
                                
                            </div>
                        </div>
                        <!-- /input-group -->
                    </div>
            </div>
    </div>
  </div>
  <hr>
  <div class="container">
    <h4 class="margin-top-0">Category: <asp:Label ID="lblCategoryType" runat="server" Text=""></asp:Label></h4>
    <div class="table-responsive" runat="server" id="Port" visible="false">
      <table class="table table-bordered" >
        <thead>
          <tr class="bg-primary">
            <th class="max-width-50">S. No</th>
            <th class="">Port ID</th>
            <th class="">Port Name</th>
            <th class="text-center max-width-100">Action</th>
          </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="rptPortDetails">
                <ItemTemplate>
            <tr>
            <td class="max-width-50">
                <asp:Label ID="lblSno" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPortID" runat="server" Text='<%# Eval("PortID") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPortName" runat="server" Text='<%# Eval("PortName") %>'></asp:Label>
            </td>
            <td class="text-center  max-width-100"><asp:LinkButton runat="server" class="btn btn-sm btn-info" CommandArgument='<%# Eval("PortID") %>' onclick="lnkEditPort_Click"><i class="fa fa-pencil"></i> Edit</asp:LinkButton> <asp:LinkButton runat="server" class="btn btn-sm btn-danger" CommandArgument='<%# Eval("PortID") %>' onclick="lnkDeletePort_Click" ><i class="fa fa-trash"></i> Delete</asp:LinkButton></td>
          </tr>
          </ItemTemplate>
            </asp:Repeater>
        </tbody>
      </table>
        </div>
      <div class="table-responsive" runat="server" id="Vessel" visible="false">
        <table class="table table-bordered">
        <thead>
          <tr class="bg-primary">
            <th class="max-width-50">S. No</th>
            <th class="">Vessel Name</th>
            <th class="">IMO Number</th>
            <th class="text-center max-width-100">Action</th>
          </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="rptVesselDetails">
                <ItemTemplate>
            <tr>
            <td class="max-width-50">
                <asp:Label ID="lblSno" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("PortID") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIMOno" runat="server" Text='<%# Eval("PortName") %>'></asp:Label>
            </td>
            <td class="text-center  max-width-100"><asp:LinkButton runat="server" class="btn btn-sm btn-info" CommandArgument='<%# Eval("VesselID") %>' onclick="lnkEditVes_Click"><i class="fa fa-pencil"></i> Edit</asp:LinkButton> <asp:LinkButton runat="server" class="btn btn-sm btn-danger" CommandArgument='<%# Eval("VesselID") %>' onclick="lnkDeleteVes_Click"><i class="fa fa-trash"></i> Delete</asp:LinkButton></td>
          </tr>
          </ItemTemplate>
            </asp:Repeater>
        </tbody>
      </table>
    </div>
      <div class="table-responsive" runat="server" id="Customer" visible="false">
        <table class="table table-bordered">
        <thead>
          <tr class="bg-primary">
            <th class="max-width-50">S. No</th>
            <th class="">Customer Name</th>
            <th class="">Company Name</th>
            <th class="text-center max-width-100">Action</th>
          </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="rptCustomerDetails">
                <ItemTemplate>
            <tr>
            <td class="max-width-50">
                <asp:Label ID="lblSno" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
            </td>
            <td class="text-center  max-width-100"><asp:LinkButton runat="server" class="btn btn-sm btn-info" CommandArgument='<%# Eval("CustomerID") %>' onclick="btnEditCust_Click"><i class="fa fa-pencil"></i> Edit</asp:LinkButton> <asp:LinkButton runat="server" class="btn btn-sm btn-danger" CommandArgument='<%# Eval("CustomerID") %>' onclick="btnDeleteCust_Click" ><i class="fa fa-trash"></i> Delete</asp:LinkButton></td>
          </tr>
          </ItemTemplate>
            </asp:Repeater>
        </tbody>
      </table>
    </div>
      <div class="table-responsive" runat="server" id="VesselVoyage" visible="false">
        <table class="table table-bordered">
        <thead>
          <tr class="bg-primary">
            <th class="max-width-50">S. No</th>
            <th class="">Voyage Name</th>
            <th class="">Vessel Name</th>
            <th class="text-center max-width-100">Action</th>
          </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="rptVoyageDetails">
                <ItemTemplate>
            <tr>
            <td class="max-width-50">
                <asp:Label ID="lblSno" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblVoyageName" runat="server" Text='<%# Eval("VoyageID") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("VesselID") %>'></asp:Label>
            </td>
            <td class="text-center  max-width-100"><a class="btn btn-sm btn-info"><i class="fa fa-pencil"></i> Edit</a> <a class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</a></td>
          </tr>
          </ItemTemplate>
            </asp:Repeater>
        </tbody>
      </table>
    </div>
      <div class="table-responsive" runat="server" id="invitedtls" visible="false">
        <table class="table table-bordered">
        <thead>
          <tr class="bg-primary">
            <th class="max-width-50">S. No</th>
            <th class="">Voyage Name</th>
            <th class="">Customer Participant</th>
            <th class="text-center max-width-100">Action</th>
          </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="rptinviteDetails">
                <ItemTemplate>
            <tr>
            <td class="max-width-50">
                <asp:Label ID="lblSno" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblVoyageName" runat="server" Text='<%# Eval("VoyageID") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("Freespace") %>'></asp:Label>
            </td>
            <td class="text-center  max-width-100"><a class="btn btn-sm btn-info"><i class="fa fa-pencil"></i> Edit</a> <a class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</a></td>
          </tr>
          </ItemTemplate>
            </asp:Repeater>
        </tbody>
      </table>
    </div>
            <div class="table-responsive" runat="server" id="VsaApplication" visible="false">
        <table class="table table-bordered">
        <thead>
          <tr class="bg-primary">
            <th class="max-width-50">S. No</th>
            <th class="">Voyage Name</th>
            <th class="">Available Free space</th>
            <th class="text-center max-width-100">Action</th>
          </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="rptvsaapplicationDetails">
                <ItemTemplate>
            <tr>
            <td class="max-width-50">
                <asp:Label ID="lblSno" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblVoyageName" runat="server" Text='<%# Eval("VoyageID") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("Customer") %>'></asp:Label>
            </td>
            <td class="text-center  max-width-100"><a class="btn btn-sm btn-info"><i class="fa fa-pencil"></i> Edit</a> <a class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</a></td>
          </tr>
          </ItemTemplate>
            </asp:Repeater>
        </tbody>
      </table>
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
	datepicker:false,
	format:'H:i',
	step:5
});
$('.date').datetimepicker({
	timepicker:false,
	format:'m/d/Y',
	formatDate:'Y/m/d',
});

</script>
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
      initialCountry: "auto",
      // nationalMode: false,
      // onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
      placeholderNumberType: "MOBILE",
      // preferredCountries: ['cn', 'jp'],
      separateDialCode: true,
      utilsScript: "js/utils.js"
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
