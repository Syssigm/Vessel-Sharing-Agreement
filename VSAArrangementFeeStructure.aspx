<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VSAArrangementFeeStructure.aspx.cs" Inherits="VesselSharingAgreement.VSAArrangementFeeStructure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" ClientIDMode="Static" runat="server">
    <div class="container banner-top-heading">
  <h3>Create Customer VSA Arrangement Fee Structure</h3>
</div>
    <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-12 form-horizontal">
        <h4>Enter Details Here</h4>
        <div class="row">
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Year</label>
                <div class="col-md-8">
                <asp:DropDownList ID="ddlyear" class="form-control input-lg" runat="server"></asp:DropDownList>
                    </div>
                </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Month</label>
                <div class="col-md-8">
                <asp:DropDownList ID="ddlMonth" class="form-control input-lg" runat="server">
                    <asp:ListItem Selected="True" Value="0">--Select Month--</asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
              </div>
                    <%--<div class="col-md-8">
                <select class="form-control input-lg">
                  <option selected="selected">Select</option>
                  <option>January</option>
                  <option>February</option>
                  <option>March</option>
                  <option>April</option>
                  <option>May</option>
                  <option>June</option>
                  <option>July</option>
                  <option>August</option>
                  <option>September</option>
                  <option>October</option>
                  <option>November</option>
                  <option>December</option>
                </select>
              </div>--%>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Currency</label>
                <div class="col-md-8">
                <asp:DropDownList ID="ddlCurrency" class="form-control input-lg" runat="server"></asp:DropDownList>
                </div>
                    <%--<div class="col-md-8">
                <select class="form-control input-lg">
                <option>Select</option>
              <option id="aud" value="0.09479">AUD</option><option id="bgn" value="0.12217">BGN</option><option id="brl" value="0.23521">BRL</option><option id="cad" value="0.092354">CAD</option><option id="chf" value="0.072159">CHF</option><option id="cny" value="0.48792">CNY</option><option id="czk" value="1.6173">CZK</option><option id="dkk" value="0.46498">DKK</option><option id="gbp" value="0.056365">GBP</option><option id="hkd" value="0.57826">HKD</option><option id="hrk" value="0.46905">HRK</option><option id="huf" value="19.313">HUF</option><option id="idr" value="1000.4">IDR</option><option id="ils" value="0.25852">ILS</option><option id="inr" value="4.816">INR</option><option id="jpy" value="8.3153">JPY</option><option id="krw" value="83.822">KRW</option><option id="mxn" value="1.3873">MXN</option><option id="myr" value="0.31255">MYR</option><option id="nok" value="0.58468">NOK</option><option id="nzd" value="0.10416">NZD</option><option id="php" value="3.8167">PHP</option><option id="pln" value="0.26691">PLN</option><option id="ron" value="0.28683">RON</option><option id="rub" value="4.2728">RUB</option><option id="sek" value="0.59893">SEK</option><option id="sgd" value="0.10029">SGD</option><option id="thb" value="2.4521">THB</option><option id="try" value="0.27127">TRY</option><option id="usd" value="0.074058">USD</option><option id="eur" value="0.062465">EUR</option></select>
			  
              </div>--%>
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Customer Type ID</label>
                <div class="col-md-8">
                <asp:DropDownList ID="ddlCustomerType" class="form-control input-lg" runat="server"></asp:DropDownList>
                </div>
                    <%--<div class="col-md-8">
                <select class="form-control input-lg">
                  <option selected="selected">Select</option>
                  <option>Type 1</option>
                  <option>Type 2</option>
                  <option>Type 3</option>
                </select>
              </div>--%>
            </div>
            <div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal">Customer Rating</label>
                <div class="col-md-8">
                <asp:DropDownList ID="ddlCustomerRating" class="form-control input-lg" runat="server">
                    <asp:ListItem Selected="True" Value="0">--Select Customer Rating--</asp:ListItem>
                    <asp:ListItem Value="1">5</asp:ListItem>
                    <asp:ListItem Value="2">4</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">2</asp:ListItem>
                    <asp:ListItem Value="5">1</asp:ListItem>
                </asp:DropDownList>
                </div>
                    <%--<div class="col-md-8">
                <select class="form-control input-lg">
                  <option selected="selected">Select</option>
                  <option>5 Star</option>
                  <option>4 Star</option>
                  <option>3 Star</option>
                  <option>2 Star</option>
                  <option>1 Star</option>
                </select>
              </div>--%>
            </div>
			
			<div class="form-group form-group-lg">
              <label for="inputEmail3" class="col-md-4 control-label normal padding-top-0">VSA Arrangement Fee Per TEU in $</label>
              <div class="col-md-8">
                <asp:TextBox runat="server" ID="TxtArrangeFee" type="text" placeholder="Arrangement Fee" class="form-control"/>
              </div>
            </div>

          </div>
        </div>

          </div>
		  
        </div>


        <div class="row padding-top-10">
          <div class="col-sm-12 col-xs-12">
              <asp:Button ID="BtnFeeCreate" class="btn btn-primary btn-lg min-width-200" runat="server" Text="Fee Create" OnClick="BtnFeeCreate_Click" />
              <asp:Button class="btn btn-default btn-lg" ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
              <%--<div class="padding-top-10 text-center"><a href="#" class="btn btn-primary btn-lg min-width-200" accesskey="f">Fee Create</a> <a href="#" accesskey="c" class="btn btn-default btn-lg">Cancel</a></div>--%>
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
	datepicker:false,
	format:'H:i',
	step:5
});
$('.date').datetimepicker({
	timepicker:false,
	format:'m/d/Y',
	formatDate:'Y/m/d',
});

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

$('.responsive-tabs').responsiveTabs({
  accordionOn: ['xs', 'sm'] // xs, sm, md, lg
});
</script>
</asp:Content>
