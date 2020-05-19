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
    public partial class AddDBAAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddlcontrydba();
            }
        }

        // Method ddlcontrydba() : For populating the country code details in DBA address

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

        // Method adddba_Click : For adding dba address details to the database
        protected void adddba_Click(object sender, EventArgs e)
        {
            try
            {
                var db = new VesselAgreement();
                string id = Session["Customer"].ToString();

                var custadddba = new VSA_Address();
                var mstraddress3 = new VSA_Master_Customer_Addresses();

                custadddba.AddressType = "DBA";
                custadddba.BuildingNumber = TxtDBABuildingNum.Text;
                custadddba.CityName = TxtDBACity.Text;
                custadddba.StreetName1 = TxtDBAStreet.Text;
                custadddba.StreetName2 = TxtDBAStreet.Text;
                custadddba.State = TxtDBAState.Text;
                custadddba.Zipcode = TxtDBAPostZip.Text;
                custadddba.Country_Code = ddlCountrydba.SelectedValue;
                custadddba.AddressStatus = "A";
                custadddba.Registered_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                mstraddress3.AddressID = custadddba.AddressID;
                mstraddress3.CustomerID = id;

                db.VSA_Master_Customer_Addresses.Add(mstraddress3);
                db.VSA_Address.Add(custadddba);
                db.SaveChanges();
                String strAppMsg = ConfigurationManager.AppSettings["ADAcreate"];
                lblmsg.Text = strAppMsg;
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
                Response.Redirect("ViewModifyCustomer.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

    }
}