using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public partial class ViewModifyCustomer : System.Web.UI.Page
    {
        string custid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string urlName = "0";

                var db = new VesselAgreement();
                if(Request.UrlReferrer != null)
                { 
                 urlName = Request.UrlReferrer.ToString();
                }
                ddlcontryreg();
                ddlcontrybill();
                ddlcontrydba();

                custid = Convert.ToString(Session["CustomerID"]);

                if (custid == "Admin")
                {
                    if (Request.QueryString["Name"] != string.Empty)
                    {
                        custid = Request.QueryString["Name"];
                    }
                    if (custid == null)
                    {
                        Response.Redirect("Logout.aspx", false);
                    }
                }

                if (urlName == "http://localhost:50885/AddBillAddress.aspx" || urlName == "http://localhost:50885/AddDBAAddress.aspx")
                {
                    viewcustomer();
                    Editcustomer();
                }
                else
                {
                    viewcustomer();
                }
            }
            lblmsg.Text = "";
            lblerrCustomerType.Text = "";
        }

        // Method ddlcontryreg() Use : Populate the country code values in the dropdown related to registered address
        public void ddlcontryreg()
        {
            try
            {
                var db = new VesselAgreement();

                ddlcountryReg.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlcountryReg.DataTextField = "Country_Name";
                ddlcountryReg.DataValueField = "Country_Code";
                ddlcountryReg.DataBind();
                ddlcountryReg.Items.Insert(0, new ListItem("--Select country--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlcontrybill() Use : Populate the country code values in the dropdown related to billing address
        public void ddlcontrybill()
        {
            try
            {
                var db = new VesselAgreement();

                ddlcountryBill.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlcountryBill.DataTextField = "Country_Name";
                ddlcountryBill.DataValueField = "Country_Code";
                ddlcountryBill.DataBind();
                ddlcountryBill.Items.Insert(0, new ListItem("--Select country--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlcontrydba() Use : Populate the country code values in the dropdown related to dba address
        public void ddlcontrydba()
        {
            try
            {
                var db = new VesselAgreement();

                ddlCountrydba.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlCountrydba.DataTextField = "Country_Name";
                ddlCountrydba.DataValueField = "Country_Code";
                ddlCountrydba.DataBind();
                ddlCountrydba.Items.Insert(0, new ListItem("--Select country--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method viewcustomer() Use : To Fetch the updated customer details from database to screen
        public void viewcustomer()
        {
            try
            {
                var db = new VesselAgreement();

                if (Convert.ToString(Session["CustomerID"]) == "Admin")
                {
                    if (Request.QueryString["Name"] != string.Empty)
                    {
                        custid = Request.QueryString["Name"];
                    }
                    if (custid == null)
                    {
                        Response.Redirect("Logout.aspx", false);
                    }
                }
                else
                {
                    custid = Convert.ToString(Session["CustomerID"]);
                }

                Session["Customer"] = custid;

                var viewcustoReg = (from q in db.VSA_Master_Customer
                                    join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                    join s in db.VSA_Address on p.AddressID equals s.AddressID
                                    where p.CustomerID == custid
                                    where s.AddressType == "Registered"
                                    select new
                                    {
                                        q.CustomerID,
                                        q.CompanyName,
                                        q.IMO_companyNumber,
                                        q.ContactFirstName,
                                        q.ContactLastName,
                                        q.ContactNumber,
                                        q.EmailID,
                                        q.AlternateContactNumber,

                                        s.AddressType,
                                        s.BuildingNumber,
                                        s.StreetName1,
                                        s.CityName,
                                        s.Zipcode,
                                        s.State,
                                        s.Country_Code,

                                    }).SingleOrDefault();

                var viewcustoBill = (from q in db.VSA_Master_Customer
                                     join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                     join r in db.VSA_Address on p.AddressID equals r.AddressID
                                     where q.CustomerID == custid
                                     where r.AddressType == "Billing"
                                     select new
                                     {
                                         q.CustomerID,
                                         q.CompanyName,
                                         q.IMO_companyNumber,
                                         q.ContactFirstName,
                                         q.ContactLastName,
                                         q.ContactNumber,
                                         q.EmailID,
                                         q.AlternateContactNumber,

                                         billAddressType = r.AddressType,
                                         billBuildingNumber = r.BuildingNumber,
                                         billStreetName1 = r.StreetName1,
                                         billCityName = r.CityName,
                                         billZipcode = r.Zipcode,
                                         billState = r.State,
                                         billCountry_Code = r.Country_Code,

                                     }).SingleOrDefault();

                var viewcustoDBA = (from q in db.VSA_Master_Customer
                                    join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                    join t in db.VSA_Address on p.AddressID equals t.AddressID
                                    where q.CustomerID == custid
                                    where t.AddressType == "DBA"
                                    select new
                                    {
                                        q.CustomerID,
                                        q.CompanyName,
                                        q.IMO_companyNumber,
                                        q.ContactFirstName,
                                        q.ContactLastName,
                                        q.ContactNumber,
                                        q.EmailID,
                                        q.AlternateContactNumber,


                                        dbaAddressType = t.AddressType,
                                        dbaBuildingNumber = t.BuildingNumber,
                                        dbaStreetName1 = t.StreetName1,
                                        dbaCityName = t.CityName,
                                        dbaZipcode = t.Zipcode,
                                        dbaState = t.State,
                                        dbaCountry_Code = t.Country_Code,
                                    }).SingleOrDefault();

                if (viewcustoReg != null)
                {
                    var SSLine = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "SSLINE" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (SSLine.Count() != 0)
                    {
                        ckSSLine.Checked = true;
                        ckSSLine.Disabled = true;
                    }
                    else
                    {
                        ckSSLine.Checked = false;
                        ckSSLine.Disabled = true;
                    }

                    var VSLOPR = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "VSLOPR" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (VSLOPR.Count() != 0)
                    {
                        ckVeselOperator.Checked = true;
                        ckVeselOperator.Disabled = true;
                    }
                    else
                    {
                        ckVeselOperator.Checked = false;
                        ckVeselOperator.Disabled = true;
                    }

                    var CRGOOP = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "CRGOOP" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (CRGOOP.Count() != 0)
                    {
                        ckCargOperator.Checked = true;
                        ckCargOperator.Disabled = true;
                    }
                    else
                    {
                        ckCargOperator.Checked = false;
                        ckCargOperator.Disabled = true;
                    }

                    var AGENTS = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "AGENTS" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (AGENTS.Count() != 0)
                    {
                        ckAgent.Checked = true;
                        ckAgent.Disabled = true;
                    }
                    else
                    {
                        ckAgent.Checked = false;
                        ckAgent.Disabled = true;
                    }

                    regaddress.Visible = true;

                    TxtCompanyName.Text = viewcustoReg.CompanyName;
                    TxtIMOShipId.Text = viewcustoReg.IMO_companyNumber;
                    TxtFirstName.Text = viewcustoReg.ContactFirstName;
                    TxtLastName.Text = viewcustoReg.ContactLastName;
                    TxtPhoneNumber.Text = viewcustoReg.ContactNumber;
                    TxtEmailID.Text = viewcustoReg.EmailID;
                    TxtAltPhoneNumber.Text = viewcustoReg.AlternateContactNumber;


                    TxtBuildingNum.Text = viewcustoReg.BuildingNumber;
                    TxtStreet.Text = viewcustoReg.StreetName1;
                    TxtCity.Text = viewcustoReg.CityName;
                    TxtPostZip.Text = viewcustoReg.Zipcode;
                    TxtState.Text = viewcustoReg.State;
                    ddlcountryReg.SelectedValue = viewcustoReg.Country_Code;

                    TxtCompanyName.ReadOnly = true;
                    TxtIMOShipId.ReadOnly = true;
                    TxtFirstName.ReadOnly = true;
                    TxtLastName.ReadOnly = true;
                    TxtPhoneNumber.ReadOnly = true;
                    TxtEmailID.ReadOnly = true;
                    TxtAltPhoneNumber.ReadOnly = true;

                    TxtBuildingNum.ReadOnly = true;
                    TxtStreet.ReadOnly = true;
                    TxtCity.ReadOnly = true;
                    TxtPostZip.ReadOnly = true;
                    TxtState.ReadOnly = true;
                    ddlcountryReg.Enabled = false;
                    ddlcountryReg.CssClass = "form-control input-lg";
                }
                else
                {
                    regaddress.Visible = false;
                }


                if (viewcustoBill != null)
                {
                    var SSLine = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "SSLINE" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (SSLine.Count() != 0)
                    {
                        ckSSLine.Checked = true;
                        ckSSLine.Disabled = true;
                    }
                    else
                    {
                        ckSSLine.Checked = false;
                        ckSSLine.Disabled = true;
                    }

                    var VSLOPR = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "VSLOPR" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (VSLOPR.Count() != 0)
                    {
                        ckVeselOperator.Checked = true;
                        ckVeselOperator.Disabled = true;
                    }
                    else
                    {
                        ckVeselOperator.Checked = false;
                        ckVeselOperator.Disabled = true;
                    }

                    var CRGOOP = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "CRGOOP" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (CRGOOP.Count() != 0)
                    {
                        ckCargOperator.Checked = true;
                        ckCargOperator.Disabled = true;
                    }
                    else
                    {
                        ckCargOperator.Checked = false;
                        ckCargOperator.Disabled = true;
                    }

                    var AGENTS = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "AGENTS" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (AGENTS.Count() != 0)
                    {
                        ckAgent.Checked = true;
                        ckAgent.Disabled = true;
                    }
                    else
                    {
                        ckAgent.Checked = false;
                        ckAgent.Disabled = true;
                    }

                    billaddress.Visible = true;

                    lnkaddbill.Visible = false;



                    TxtCompanyName.Text = viewcustoBill.CompanyName;
                    TxtIMOShipId.Text = viewcustoBill.IMO_companyNumber;
                    TxtFirstName.Text = viewcustoBill.ContactFirstName;
                    TxtLastName.Text = viewcustoBill.ContactLastName;
                    TxtPhoneNumber.Text = viewcustoBill.ContactNumber;
                    TxtEmailID.Text = viewcustoBill.EmailID;
                    TxtAltPhoneNumber.Text = viewcustoBill.AlternateContactNumber;

                    TxtBillBuildingNum.Text = viewcustoBill.billBuildingNumber;
                    TxtBillStreet.Text = viewcustoBill.billStreetName1;
                    TxtBillCity.Text = viewcustoBill.billCityName;
                    TxtBillPostZip.Text = viewcustoBill.billZipcode;
                    TxtBillState.Text = viewcustoBill.billState;
                    ddlcountryBill.SelectedValue = viewcustoBill.billCountry_Code;

                    TxtCompanyName.ReadOnly = true;
                    TxtIMOShipId.ReadOnly = true;
                    TxtFirstName.ReadOnly = true;
                    TxtLastName.ReadOnly = true;
                    TxtPhoneNumber.ReadOnly = true;
                    TxtEmailID.ReadOnly = true;
                    TxtAltPhoneNumber.ReadOnly = true;

                    TxtBillBuildingNum.ReadOnly = true;
                    TxtBillStreet.ReadOnly = true;
                    TxtBillCity.ReadOnly = true;
                    TxtBillPostZip.ReadOnly = true;
                    TxtBillState.ReadOnly = true;
                    ddlcountryBill.Enabled = false;
                    ddlcountryBill.CssClass = "form-control input-lg";
                }
                else
                {
                    billaddress.Visible = false;
                }

                if (viewcustoDBA != null)
                {
                    var SSLine = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "SSLINE" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (SSLine.Count() != 0)
                    {
                        ckSSLine.Checked = true;
                        ckSSLine.Disabled = true;
                    }
                    else
                    {
                        ckSSLine.Checked = false;
                        ckSSLine.Disabled = true;
                    }

                    var VSLOPR = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "VSLOPR" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (VSLOPR.Count() != 0)
                    {
                        ckVeselOperator.Checked = true;
                        ckVeselOperator.Disabled = true;
                    }
                    else
                    {
                        ckVeselOperator.Checked = false;
                        ckVeselOperator.Disabled = true;
                    }

                    var CRGOOP = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "CRGOOP" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (CRGOOP.Count() != 0)
                    {
                        ckCargOperator.Checked = true;
                        ckCargOperator.Disabled = true;
                    }
                    else
                    {
                        ckCargOperator.Checked = false;
                        ckCargOperator.Disabled = true;
                    }

                    var AGENTS = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "AGENTS" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (AGENTS.Count() != 0)
                    {
                        ckAgent.Checked = true;
                        ckAgent.Disabled = true;
                    }
                    else
                    {
                        ckAgent.Checked = false;
                        ckAgent.Disabled = true;
                    }

                    dbaaddress.Visible = true;
                    lnkadddba.Visible = false;


                    TxtCompanyName.Text = viewcustoDBA.CompanyName;
                    TxtIMOShipId.Text = viewcustoDBA.IMO_companyNumber;
                    TxtFirstName.Text = viewcustoDBA.ContactFirstName;
                    TxtLastName.Text = viewcustoDBA.ContactLastName;
                    TxtPhoneNumber.Text = viewcustoDBA.ContactNumber;
                    TxtEmailID.Text = viewcustoDBA.EmailID;
                    TxtAltPhoneNumber.Text = viewcustoDBA.AlternateContactNumber;

                    TxtDBABuildingNum.Text = viewcustoDBA.dbaBuildingNumber;
                    TxtDBAStreet.Text = viewcustoDBA.dbaStreetName1;
                    TxtDBACity.Text = viewcustoDBA.dbaCityName;
                    TxtDBAPostZip.Text = viewcustoDBA.dbaZipcode;
                    TxtDBAState.Text = viewcustoDBA.dbaState;
                    ddlCountrydba.SelectedValue = viewcustoDBA.dbaCountry_Code;

                    TxtCompanyName.ReadOnly = true;
                    TxtIMOShipId.ReadOnly = true;
                    TxtFirstName.ReadOnly = true;
                    TxtLastName.ReadOnly = true;
                    TxtPhoneNumber.ReadOnly = true;
                    TxtEmailID.ReadOnly = true;
                    TxtAltPhoneNumber.ReadOnly = true;

                    TxtDBABuildingNum.ReadOnly = true;
                    TxtDBAStreet.ReadOnly = true;
                    TxtDBACity.ReadOnly = true;
                    TxtDBAPostZip.ReadOnly = true;
                    TxtDBAState.ReadOnly = true;
                    ddlCountrydba.Enabled = false;
                    ddlCountrydba.CssClass = "form-control input-lg";
                }
                else
                {
                    dbaaddress.Visible = false;
                }

                lnkaddbill.Visible = false;
                lnkadddba.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method Editcustomer() Use : To make the customer details fields editable
        public void Editcustomer()
        {
            try
            {
                var db = new VesselAgreement();


                if (Convert.ToString(Session["CustomerID"]) == "Admin")
                {
                    if (Request.QueryString["Name"] != string.Empty)
                    {
                        custid = Request.QueryString["Name"];
                    }
                    if (custid == null)
                    {
                        Response.Redirect("Logout.aspx", false);
                    }
                }
                else
                {
                    custid = Convert.ToString(Session["CustomerID"]);
                }

                var viewcustoReg = (from q in db.VSA_Master_Customer
                                    join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                    join s in db.VSA_Address on p.AddressID equals s.AddressID
                                    where q.CustomerID == custid
                                    where s.AddressType == "Registered"
                                    select new
                                    {

                                    }).SingleOrDefault();

                var viewcustoBill = (from q in db.VSA_Master_Customer
                                     join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                     join r in db.VSA_Address on p.AddressID equals r.AddressID
                                     where q.CustomerID == custid
                                     where r.AddressType == "Billing"
                                     select new
                                     {
                                     }).SingleOrDefault();

                var viewcustoDBA = (from q in db.VSA_Master_Customer
                                    join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                    join t in db.VSA_Address on p.AddressID equals t.AddressID
                                    where q.CustomerID == custid
                                    where t.AddressType == "DBA"
                                    select new
                                    {

                                    }).SingleOrDefault();

                if (viewcustoReg != null)
                {
                    var SSLine = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "SSLINE" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (SSLine.Count() != 0)
                    {
                        ckSSLine.Checked = true;
                        ckSSLine.Disabled = false;
                    }
                    else
                    {
                        ckSSLine.Checked = false;
                        ckSSLine.Disabled = false;
                    }

                    var VSLOPR = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "VSLOPR" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (VSLOPR.Count() != 0)
                    {
                        ckVeselOperator.Checked = true;
                        ckVeselOperator.Disabled = false;
                    }
                    else
                    {
                        ckVeselOperator.Checked = false;
                        ckVeselOperator.Disabled = false;
                    }

                    var CRGOOP = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "CRGOOP" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (CRGOOP.Count() != 0)
                    {
                        ckCargOperator.Checked = true;
                        ckCargOperator.Disabled = false;
                    }
                    else
                    {
                        ckCargOperator.Checked = false;
                        ckCargOperator.Disabled = false;
                    }

                    var AGENTS = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "AGENTS" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (AGENTS.Count() != 0)
                    {
                        ckAgent.Checked = true;
                        ckAgent.Disabled = false;
                    }
                    else
                    {
                        ckAgent.Checked = false;
                        ckAgent.Disabled = false;
                    }

                    regaddress.Visible = true;


                    TxtCompanyName.ReadOnly = false;
                    TxtFirstName.ReadOnly = false;
                    TxtLastName.ReadOnly = false;
                    TxtPhoneNumber.ReadOnly = false;
                    TxtEmailID.ReadOnly = false;
                    TxtAltPhoneNumber.ReadOnly = false;

                    TxtBuildingNum.ReadOnly = false;
                    TxtStreet.ReadOnly = false;
                    TxtCity.ReadOnly = false;
                    TxtPostZip.ReadOnly = false;
                    TxtState.ReadOnly = false;
                    ddlcountryReg.Enabled = true;
                    ddlcountryReg.CssClass = "form-control input-lg";
                }
                else
                {
                    regaddress.Visible = false;
                }



                if (viewcustoBill != null)
                {
                    var SSLine = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "SSLINE" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (SSLine.Count() != 0)
                    {
                        ckSSLine.Checked = true;
                        ckSSLine.Disabled = false;
                    }
                    else
                    {
                        ckSSLine.Checked = false;
                        ckSSLine.Disabled = false;
                    }

                    var VSLOPR = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "VSLOPR" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (VSLOPR.Count() != 0)
                    {
                        ckVeselOperator.Checked = true;
                        ckVeselOperator.Disabled = false;
                    }
                    else
                    {
                        ckVeselOperator.Checked = false;
                        ckVeselOperator.Disabled = false;
                    }

                    var CRGOOP = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "CRGOOP" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (CRGOOP.Count() != 0)
                    {
                        ckCargOperator.Checked = true;
                        ckCargOperator.Disabled = false;
                    }
                    else
                    {
                        ckCargOperator.Checked = false;
                        ckCargOperator.Disabled = false;
                    }

                    var AGENTS = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "AGENTS" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (AGENTS.Count() != 0)
                    {
                        ckAgent.Checked = true;
                        ckAgent.Disabled = false;
                    }
                    else
                    {
                        ckAgent.Checked = false;
                        ckAgent.Disabled = false;
                    }

                    billaddress.Visible = true;
                    lnkaddbill.Visible = false;

                    TxtCompanyName.ReadOnly = false;
                    TxtFirstName.ReadOnly = false;
                    TxtLastName.ReadOnly = false;
                    TxtPhoneNumber.ReadOnly = false;
                    TxtEmailID.ReadOnly = false;
                    TxtAltPhoneNumber.ReadOnly = false;

                    TxtBillBuildingNum.ReadOnly = false;
                    TxtBillStreet.ReadOnly = false;
                    TxtBillCity.ReadOnly = false;
                    TxtBillPostZip.ReadOnly = false;
                    TxtBillState.ReadOnly = false;
                    ddlcountryBill.Enabled = true;
                    ddlcountryBill.CssClass = "form-control input-lg";
                }
                else
                {
                    billaddress.Visible = false;
                    lnkaddbill.Visible = true;
                }

                if (viewcustoDBA != null)
                {
                    var SSLine = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "SSLINE" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (SSLine.Count() != 0)
                    {
                        ckSSLine.Checked = true;
                        ckSSLine.Disabled = false;
                    }
                    else
                    {
                        ckSSLine.Checked = false;
                        ckSSLine.Disabled = false;
                    }

                    var VSLOPR = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "VSLOPR" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (VSLOPR.Count() != 0)
                    {
                        ckVeselOperator.Checked = true;
                        ckVeselOperator.Disabled = false;
                    }
                    else
                    {
                        ckVeselOperator.Checked = false;
                        ckVeselOperator.Disabled = false;
                    }

                    var CRGOOP = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "CRGOOP" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (CRGOOP.Count() != 0)
                    {
                        ckCargOperator.Checked = true;
                        ckCargOperator.Disabled = false;
                    }
                    else
                    {
                        ckCargOperator.Checked = false;
                        ckCargOperator.Disabled = false;
                    }

                    var AGENTS = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerTypeID == "AGENTS" && q.CustomerID == custid
                                  select new { q.CustomerTypeID }).ToList();

                    if (AGENTS.Count() != 0)
                    {
                        ckAgent.Checked = true;
                        ckAgent.Disabled = false;
                    }
                    else
                    {
                        ckAgent.Checked = false;
                        ckAgent.Disabled = false;
                    }

                    dbaaddress.Visible = true;
                    lnkadddba.Visible = false;

                    TxtCompanyName.ReadOnly = false;
                    TxtFirstName.ReadOnly = false;
                    TxtLastName.ReadOnly = false;
                    TxtPhoneNumber.ReadOnly = false;
                    TxtEmailID.ReadOnly = false;
                    TxtAltPhoneNumber.ReadOnly = false;

                    TxtDBABuildingNum.ReadOnly = false;
                    TxtDBAStreet.ReadOnly = false;
                    TxtDBACity.ReadOnly = false;
                    TxtDBAPostZip.ReadOnly = false;
                    TxtDBAState.ReadOnly = false;
                    ddlCountrydba.Enabled = true;
                    ddlCountrydba.CssClass = "form-control input-lg";
                }
                else
                {
                    dbaaddress.Visible = false;
                    lnkadddba.Visible = true;
                }

                btnEdit.Visible = false;
                lnkSave.Visible = true;
                btnCancel.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method btnEdit_Click Use : To trigger the edit button click event
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Editcustomer();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method lnkSave_Click Use : To save the edited details of the customer

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            {
                
                var crtcusttyp = new VSA_Master_Customer_and_CustomerTypes();

                if (Convert.ToString(Session["CustomerID"]) == "Admin")
                {
                    if (Request.QueryString["Name"] != string.Empty)
                    {
                        custid = Request.QueryString["Name"];
                    }
                    if (custid == null)
                    {
                        Response.Redirect("Logout.aspx", false);
                    }
                }
                else
                {
                    custid = Convert.ToString(Session["CustomerID"]);
                }


                var updatecustomer = (from q in db.VSA_Master_Customer
                                      where q.CustomerID == custid
                                      select new { q }).SingleOrDefault();

                updatecustomer.q.CompanyName = TxtCompanyName.Text;
                updatecustomer.q.ContactFirstName = TxtFirstName.Text;
                updatecustomer.q.ContactLastName = TxtLastName.Text;
                updatecustomer.q.ContactNumber = TxtPhoneNumber.Text;
                updatecustomer.q.AlternateContactNumber = TxtAltPhoneNumber.Text;
                updatecustomer.q.EmailID = TxtEmailID.Text;

                var viewcustoReg = (from q in db.VSA_Master_Customer
                                    join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                    join s in db.VSA_Address on p.AddressID equals s.AddressID
                                    where p.CustomerID == custid
                                    where s.AddressType == "Registered"
                                    select new
                                    {
                                        s
                                    }).SingleOrDefault();

                if (viewcustoReg != null)
                {
                    viewcustoReg.s.BuildingNumber = TxtBuildingNum.Text;
                    viewcustoReg.s.StreetName1 = TxtStreet.Text;
                    viewcustoReg.s.CityName = TxtCity.Text;
                    viewcustoReg.s.State = TxtState.Text;
                    viewcustoReg.s.Zipcode = TxtPostZip.Text;
                    viewcustoReg.s.Country_Code = ddlcountryReg.SelectedValue;
                }

                var viewcustoBill = (from q in db.VSA_Master_Customer
                                     join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                     join r in db.VSA_Address on p.AddressID equals r.AddressID
                                     where q.CustomerID == custid
                                     where r.AddressType == "Billing"
                                     select new
                                     {
                                         r
                                     }).SingleOrDefault();
                if (viewcustoBill != null)
                {
                    viewcustoBill.r.BuildingNumber = TxtBillBuildingNum.Text;
                    viewcustoBill.r.StreetName1 = TxtBillStreet.Text;
                    viewcustoBill.r.CityName = TxtBillCity.Text;
                    viewcustoBill.r.State = TxtBillState.Text;
                    viewcustoBill.r.Zipcode = TxtPostZip.Text;
                    viewcustoBill.r.Country_Code = ddlcountryBill.SelectedValue;
                }

                var viewcustoDBA = (from q in db.VSA_Master_Customer
                                    join p in db.VSA_Master_Customer_Addresses on q.CustomerID equals p.CustomerID
                                    join t in db.VSA_Address on p.AddressID equals t.AddressID
                                    where q.CustomerID == custid
                                    where t.AddressType == "DBA"
                                    select new
                                    {
                                        t
                                    }).SingleOrDefault();
                if (viewcustoDBA != null)
                {
                    viewcustoDBA.t.BuildingNumber = TxtDBABuildingNum.Text;
                    viewcustoDBA.t.StreetName1 = TxtDBAStreet.Text;
                    viewcustoDBA.t.CityName = TxtDBACity.Text;
                    viewcustoDBA.t.State = TxtDBAState.Text;
                    viewcustoDBA.t.Zipcode = TxtPostZip.Text;
                    viewcustoDBA.t.Country_Code = ddlCountrydba.SelectedValue;
                }
                if ((ckSSLine.Checked == false) && (ckVeselOperator.Checked == false) && (ckCargOperator.Checked == false) && (ckAgent.Checked == false))
                {
                    String strAppMsg = ConfigurationManager.AppSettings["MCselectonecusttype"];
                    lblmsg.Text = strAppMsg;
                    lblerrCustomerType.ForeColor = System.Drawing.Color.Red;
                }
                else
                { 
                var existSSLINEtype = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                       where q.CustomerID == custid
                                       where q.CustomerTypeID == "SSLINE"
                                       select new { q.CustomerTypeID }).ToList();

                if (existSSLINEtype.Count == 0)
                {
                    if (ckSSLine.Checked)
                    {
                        crtcusttyp.CustomerTypeID = "SSLINE";
                        crtcusttyp.CustomerID = custid;

                        db.VSA_Master_Customer_and_CustomerTypes.Add(crtcusttyp);
                        db.SaveChanges();
                        
                        }
                }
                else
                {
                    if (!ckSSLine.Checked)
                    {
                        var deleteexistSSLINE = db.VSA_Master_Customer_and_CustomerTypes.Where(x => x.CustomerID == custid && x.CustomerTypeID == "SSLINE").FirstOrDefault();

                        if (deleteexistSSLINE != null)
                        {
                            db.VSA_Master_Customer_and_CustomerTypes.Remove(deleteexistSSLINE);
                            db.SaveChanges();
                            
                            }

                    }
                }

                var existCRGOOPtype = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                       where q.CustomerID == custid
                                       where q.CustomerTypeID == "CRGOOP"
                                       select new { q.CustomerTypeID }).ToList();

                if (existCRGOOPtype.Count == 0)
                {
                    if (ckCargOperator.Checked)
                    {
                        crtcusttyp.CustomerTypeID = "CRGOOP";
                        crtcusttyp.CustomerID = custid;
                        db.VSA_Master_Customer_and_CustomerTypes.Add(crtcusttyp);
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (!ckCargOperator.Checked)
                    {
                        var deleteexistCRGOOP = db.VSA_Master_Customer_and_CustomerTypes.Where(x => x.CustomerID == custid && x.CustomerTypeID == "CRGOOP").FirstOrDefault();

                        if (deleteexistCRGOOP != null)
                        {
                            db.VSA_Master_Customer_and_CustomerTypes.Remove(deleteexistCRGOOP);
                            db.SaveChanges();
                        }

                    }
                }

                var existAGENTStype = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                       where q.CustomerID == custid
                                       where q.CustomerTypeID == "AGENTS"
                                       select new { q.CustomerTypeID }).ToList();

                if (existAGENTStype.Count == 0)
                {
                    if (ckAgent.Checked)
                    {
                        crtcusttyp.CustomerTypeID = "AGENTS";
                        crtcusttyp.CustomerID = custid;
                        db.VSA_Master_Customer_and_CustomerTypes.Add(crtcusttyp);
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (!ckAgent.Checked)
                    {
                        var deleteexistAGENTS = db.VSA_Master_Customer_and_CustomerTypes.Where(x => x.CustomerID == custid && x.CustomerTypeID == "AGENTS").FirstOrDefault();

                        if (deleteexistAGENTS != null)
                        {
                            db.VSA_Master_Customer_and_CustomerTypes.Remove(deleteexistAGENTS);
                            db.SaveChanges();
                        }

                    }
                }

                var existVSLOPRtype = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                       where q.CustomerID == custid
                                       where q.CustomerTypeID == "VSLOPR"
                                       select new { q.CustomerTypeID }).ToList();

                if (existVSLOPRtype.Count == 0)
                {
                    if (ckVeselOperator.Checked)
                    {
                        crtcusttyp.CustomerTypeID = "VSLOPR";
                        crtcusttyp.CustomerID = custid;
                        db.VSA_Master_Customer_and_CustomerTypes.Add(crtcusttyp);
                        db.SaveChanges();
                    }

                }
                else
                {
                    if (!ckVeselOperator.Checked)
                    {
                        var deleteexistVSLOPR = db.VSA_Master_Customer_and_CustomerTypes.Where(x => x.CustomerID == custid && x.CustomerTypeID == "VSLOPR").FirstOrDefault();

                        if (deleteexistVSLOPR != null)
                        {
                            db.VSA_Master_Customer_and_CustomerTypes.Remove(deleteexistVSLOPR);
                            db.SaveChanges();
                        }

                    }
                }

                    db.SaveChanges();
                    String strAppMsg = ConfigurationManager.AppSettings["MCdetailsupdated"];
                    DbTrans.Commit();
                    lblmsg.Text = strAppMsg;
                    lblmsg.ForeColor = System.Drawing.Color.ForestGreen;

                viewcustomer();
                btnEdit.Visible = true;
                lnkSave.Visible = false;
                btnCancel.Visible = false;

            }
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method btnCancel_Click Use : To cancel the action and move to view/modify page        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewModifyCustomer.aspx");
        }

        // Method lnkaddbill_Click Use : To redirect to addbill page
        protected void lnkaddbill_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBillAddress.aspx");
        }

        // Method lnkadddba_Click Use : To redirect to addDBAAddress page
        protected void lnkadddba_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddDBAAddress.aspx");
        }
    }
}