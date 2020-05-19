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
    public partial class PortRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Countrydropdown();
                ddlport();
                rbContainer1.Checked = true;
            }
        }

        // Method Countrydropdown(): Use : To Populate Country Code Values in the port address dropdown
        public void Countrydropdown()
        {
            try
            {
                var db = new VesselAgreement();

                ddlCountry.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x=> x.Country_Name);
                ddlCountry.DataTextField = "Country_Name";
                ddlCountry.DataValueField = "Country_Code";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("--Select country--", "0"));


                
                ddlCountryadd.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlCountryadd.DataTextField = "Country_Name";
                ddlCountryadd.DataValueField = "Country_Code";
                ddlCountryadd.DataBind();
                ddlCountryadd.Items.Insert(0, new ListItem("--Select country--", "0"));
                ddlCountryadd.SelectedValue = ddlCountry.SelectedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method Countrydropdown(): Use : To Populate port zone Values in the port address dropdown
        public void ddlport()
        {
            try
            {
                var db = new VesselAgreement();

                var prtzone = (from q in db.VSA_Zone_Country_Code
                               where q.Country_Code == ddlCountry.SelectedValue
                               select new { q.ZoneCode }).Distinct().ToList().OrderBy(x => x.ZoneCode);

                ddlPortZone.DataSource = prtzone;
                ddlPortZone.DataTextField = "ZoneCode";
                ddlPortZone.DataValueField = "ZoneCode";
                ddlPortZone.DataBind();
                ddlPortZone.Items.Insert(0, new ListItem("--Select Port Zone--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method portDetails(): Use : To insert port details into the database
        public void portDetails()
        {

            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            {
               
                
                var portdtls = new VSA_Config_Port();
                var PortAdress = new VSA_Address();

                var portvalid = (from q in db.VSA_Config_Port
                                 where q.PortID == TxtPortId.Text
                                 select new { q.PortID }).ToList();

                if(portvalid.Count == 0)
                { 
                portdtls.PortID = TxtPortId.Text;
                portdtls.PortName = TxtPortName.Text;

                if (rbContainer1.Checked)
                {
                    portdtls.PortCargoTerminalType = "Container";
                }
                if (rbNonContainer2.Checked)
                {
                    portdtls.PortCargoTerminalType = "Non Container";
                }

                portdtls.ContainerCargoTerminalNumber = Convert.ToInt32(TxtTerminalNumber.Text);
                portdtls.PortCountry_Code = ddlCountry.SelectedValue;
                portdtls.PortCreate_Ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                portdtls.PortZoneCode = ddlPortZone.SelectedItem.Text;
                portdtls.AddressID = PortAdress.AddressID;

                PortAdress.BuildingNumber = TxtBuildingNumber.Text;
                PortAdress.CityName = TxtCity.Text;
                PortAdress.State = TxtState.Text;
                PortAdress.StreetName1 = TxtStreet.Text;
                PortAdress.Zipcode = TxtZipCode.Text;
                PortAdress.AddressType = "Port Address";
                PortAdress.Country_Code = ddlCountryadd.SelectedValue;
                PortAdress.Registered_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                PortAdress.AddressStatus = "A";

                db.VSA_Address.Add(PortAdress);
                db.VSA_Config_Port.Add(portdtls);
                db.SaveChanges();
                String strAppMsg = ConfigurationManager.AppSettings["PRportRegistration"];
                lblmsg.Text = strAppMsg;
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
                DbTrans.Commit();
                clear();
            }
                else
                {
                    String strAppMsg = ConfigurationManager.AppSettings["PRduplicatePortid"];
                    lblmsg.Text = strAppMsg;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    DbTrans.Rollback();
                    clear();
                }
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                throw ex;

            }
        }

        // Method btnRegister_Click: Use : To register the port details into the database
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                portDetails();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method clear(): Use : To clear the values in the page
        public void clear()
        {
            try
            {
                TxtBuildingNumber.Text = "";
                TxtCity.Text = "";
                TxtEmailid.Text = "";
                TxtPortId.Text = "";
                TxtPortName.Text = "";
                TxtState.Text = "";
                TxtStreet.Text = "";
                TxtTerminalNumber.Text = "";
                TxtZipCode.Text = "";
                rbContainer1.Checked = true;
                Countrydropdown();
                ddlport();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method btnCancel_Click: Use : To trigger the clear method
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        // Method ddlCountry_SelectedIndexChanged: Use : To trigger the country name change event
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlport();
                ddlCountryadd.SelectedValue = ddlCountry.SelectedValue;
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}