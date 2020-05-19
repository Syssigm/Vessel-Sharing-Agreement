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
    public partial class AddBillAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { 
            ddlcontrybilled();
            }
        }

        // Method ddlcontrybilled(): To populate the country details for the billing address
        public void ddlcontrybilled()
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

        // Method addbill_Click : For adding billing address details to the database
        protected void addbill_Click(object sender, EventArgs e)
        {
            try
            { 
            var db = new VesselAgreement();
            string id = Session["Customer"].ToString();

            

            var custaddbill = new VSA_Address();
            var mstraddress2 = new VSA_Master_Customer_Addresses();

            custaddbill.AddressType = "Billing";
            custaddbill.BuildingNumber = TxtBillBuildingNum.Text;
            custaddbill.CityName = TxtBillCity.Text;
            custaddbill.StreetName1 = TxtBillStreet.Text;
            custaddbill.StreetName2 = TxtBillStreet.Text;
            custaddbill.State = TxtBillState.Text;
            custaddbill.Zipcode = TxtBillPostZip.Text;
            custaddbill.Country_Code = ddlcountryBill.SelectedValue;
            custaddbill.AddressStatus = "A";
            custaddbill.Registered_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

            mstraddress2.AddressID = custaddbill.AddressID;
            mstraddress2.CustomerID = id;

            db.VSA_Master_Customer_Addresses.Add(mstraddress2);
            db.VSA_Address.Add(custaddbill);
            db.SaveChanges();
                String strAppMsg = ConfigurationManager.AppSettings["ABAcreate"];
                lblmsg.Text = strAppMsg;
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
               
            Response.Redirect("ViewModifyCustomer.aspx",false);
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}