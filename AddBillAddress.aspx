<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddBillAddress.aspx.cs" Inherits="VesselSharingAgreement.AddBillAddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container banner-top-heading">
  <h3>Add New Address</h3>
</div>
        <div align="center">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
<div class="container-fluid solutions-section">
  <div class="container">
    <div class="row">
      <div class="col-sm-6 col-md-7">
        
        <div class="row">
        <div id="addbilladdress" class="gray-border padding-10 padding-bottom-0 padding-top-0 margin-bottom-10 whiteBg img-rounded" runat="server">
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
                <asp:TextBox runat="server" TabIndex="1" ID="TxtBillBuildingNum" MaxLength="10" type="text" class="form-control input-lg" placeholder="Building Number" />
                  <asp:RequiredFieldValidator ID="BuildingNumValidation" ForeColor="Red" runat="server" ControlToValidate="TxtBillBuildingNum" ErrorMessage="Please enter Building Number"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="2" ID="TxtBillStreet" MaxLength="100" type="text" class="form-control input-lg" placeholder="Street" />
                  <asp:RequiredFieldValidator ID="StreetValidation" ForeColor="Red" ControlToValidate="TxtBillStreet" runat="server" ErrorMessage="Please enter street Number/Name"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="3" ID="TxtBillCity" MaxLength="40" type="text" class="form-control input-lg" placeholder="City" />
              <asp:RequiredFieldValidator ID="CityValidation" ForeColor="Red" ControlToValidate="TxtBillCity" runat="server" ErrorMessage="Please enter City"></asp:RequiredFieldValidator>
              </div>
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="5" ID="TxtBillState" MaxLength="25" type="text" class="form-control input-lg" placeholder="State" />
              <asp:RequiredFieldValidator ID="StateValidation" ForeColor="Red" ControlToValidate="TxtBillState" runat="server" ErrorMessage="Please enter State"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" ID="TxtBillPostZip" TabIndex="4" MaxLength="12" onkeypress="return isNumber(event)" type="text" class="form-control input-lg" placeholder="Postal Code/Zip Code" />
              <asp:RequiredFieldValidator ID="PostZipValidation" ForeColor="Red" ControlToValidate="TxtBillPostZip" runat="server" ErrorMessage="Please enter Zip Code"></asp:RequiredFieldValidator>
              </div>
              <div class="padding-bottom-15">
                  <asp:DropDownList ID="ddlcountryBill" TabIndex="6" class="form-control input-lg" runat="server"></asp:DropDownList>
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
		</div>
        <div class="row padding-top-5">
          <div class="col-sm-12 col-xs-12">
            <div class="padding-top-10">
                <asp:LinkButton runat="server" ID="addbilled" TabIndex="7" class="btn btn-primary btn-lg min-width-200" CommandArgument='<%#Eval("id")%>' Text="Add" OnClick="addbill_Click"></asp:LinkButton>
              <asp:Button ID="btnCancel" class="btn btn-default btn-lg" TabIndex="8" runat="server" CausesValidation="false" Text="Cancel" />
                <!--<a href="#" class="btn btn-primary btn-lg min-width-200">Register</a> <a href="#" class="btn btn-default btn-lg">Cancel</a>--></div>
          </div>
        </div>
      </div>
     
    </div>
  </div>
</div>
    
</asp:Content>
