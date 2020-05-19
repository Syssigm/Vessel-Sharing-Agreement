<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddDBAAddress.aspx.cs" Inherits="VesselSharingAgreement.AddDBAAddress" %>
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
        <div id="dbaaddress" class="gray-border padding-10 padding-bottom-0 padding-top-0 margin-bottom-10 whiteBg img-rounded" runat="server">
          <div class="padding-bottom-10"><strong>DBA</strong>
            <%--<div class="checkbox inline-block margin-left-20" id="samedba" runat="server">
              <label class="padding-left-0">
                  <asp:CheckBox ID="samedbaAddres"  runat="server" text="Same as Registered Address" />
              <span class="cr"><i class="cr-icon glyphicon glyphicon-ok brandcolor"></i></span> Same as Registered Address</label>
            </div>--%>
          </div>
          <div class="row">
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="1" ID="TxtDBABuildingNum" MaxLength="10" type="text" class="form-control input-lg" placeholder="Building Number" />
              <asp:RequiredFieldValidator ID="BuildingNumValidation" ForeColor="Red" runat="server" ControlToValidate="TxtDBABuildingNum" ErrorMessage="Please enter Building Number"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="2" ID="TxtDBAStreet" MaxLength="100" type="text" class="form-control input-lg" placeholder="Street" />
              <asp:RequiredFieldValidator ID="StreetValidation" ForeColor="Red" ControlToValidate="TxtDBAStreet" runat="server" ErrorMessage="Please enter street Number/Name"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="3" ID="TxtDBACity" MaxLength="40" type="text" class="form-control input-lg" placeholder="City" />
              <asp:RequiredFieldValidator ID="CityValidation" ForeColor="Red" ControlToValidate="TxtDBACity" runat="server" ErrorMessage="Please enter City"></asp:RequiredFieldValidator>
              </div>
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="5" ID="TxtDBAState" MaxLength="25" type="text" class="form-control input-lg" placeholder="State" />
              <asp:RequiredFieldValidator ID="StateValidation" ForeColor="Red" ControlToValidate="TxtDBAState" runat="server" ErrorMessage="Please enter State"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="col-md-6">
              <div class="padding-bottom-15">
                <asp:TextBox runat="server" TabIndex="4" ID="TxtDBAPostZip" MaxLength="12" type="text" onkeypress="return isNumber(event)" class="form-control input-lg" placeholder="Post Code/Zip Code" />
              <asp:RequiredFieldValidator ID="PostZipValidation" ForeColor="Red" ControlToValidate="TxtDBAPostZip" runat="server" ErrorMessage="Please enter Zip Code"></asp:RequiredFieldValidator>
              </div>
              <div class="padding-bottom-15">
                  <asp:DropDownList ID="ddlCountrydba" TabIndex="6" class="form-control input-lg" runat="server"></asp:DropDownList>
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
                <asp:Button ID="adddba" TabIndex="7" class="btn btn-primary btn-lg min-width-200" runat="server" Text="Add" OnClick="adddba_Click"/>
                <asp:Button ID="btnCancel" TabIndex="8" class="btn btn-default btn-lg" runat="server" CausesValidation="false" Text="Cancel" />
                <!--<a href="#" class="btn btn-primary btn-lg min-width-200">Register</a> <a href="#" class="btn btn-default btn-lg">Cancel</a>--></div>
          </div>
        </div>
      </div>
     
    </div>
  </div>
</div>
</asp:Content>
