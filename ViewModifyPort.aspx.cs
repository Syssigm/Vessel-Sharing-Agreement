using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;
using System.Xml.Serialization;
namespace VesselSharingAgreement
{
    public partial class ViewModifyPort : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                port();
                Countrydropdown();
                ddlportZone();
                if(Convert.ToString(Session["CustomerID"]) == "Admin")
                {
                    string portid = Request.QueryString["Name"];
                    if (portid != null)
                    {
                        ddlport.SelectedValue = portid;

                        TodisplayportDetails();
                    }
                    else
                    {
                        Response.Redirect("Logout.aspx", false);
                    }
                }
            }
        }
        public void port()
        {
            try
            {
                var db = new VesselAgreement();

                //string userAuthenticationURI = "http://localhost:51991/api/home";
                //List<VSA_Config> results = new List<VSA_Config>();
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userAuthenticationURI);
                //request.Method = "GET";
                //request.ContentType = "text/xml"; //"application /json";
                //WebResponse response = request.GetResponse();
                //using (var reader = new StreamReader(response.GetResponseStream()))
                //{
                //    var ApiStatus = reader.ReadToEnd();
                //    //serializer se = new serializer();
                //     results = JsonConvert.DeserializeObject<List<VSA_Config>>(ApiStatus);

                    ddlport.DataSource = db.VSA_Config_Port.ToList().OrderBy(x => x.PortName); /*results.ToList().OrderBy(x => x.PortName);*/
                ddlport.DataTextField = "PortName";
                    ddlport.DataValueField = "PortID";
                    ddlport.DataBind();
                    ddlport.Items.Insert(0, new ListItem("--Select Port--", "0"));
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Countrydropdown()
        {
            try
            {
                var db = new VesselAgreement();

                ddlCountry.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
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
        public void ddlportZone()
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
        public void TodisplayportDetails()
        {
            try
            {
                var db = new VesselAgreement();

                //string userAuthenticationURI = "http://localhost:51991/api/home/" + ddlport.SelectedValue;

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userAuthenticationURI);
                //request.Method = "GET";
                //request.ContentType = "application/json";
                //WebResponse response = request.GetResponse();
                //using (var reader = new StreamReader(response.GetResponseStream()))
                //{
                //    var ApiStatus = reader.ReadToEnd();

                //    VSA_Config results = JsonConvert.DeserializeObject<VSA_Config>(ApiStatus);

                    //JsonData data = JsonMapper.ToObject(ApiStatus);
                    //string status = data["Status"].ToString();
                    //if (status.ToLower() == "success")
                    //{
                    //    postOfficeResult = JsonMapper.ToObject<PostOfficeResult>(ApiStatus);

                    //}
                    //if (postOfficeResult != null)
                    //{
                    //    grdAreaPostOffice.DataSource = postOfficeResult.PostOffice;
                    //    grdAreaPostOffice.DataBind();
                    //}
                    //else
                    //{
                    //    lblMessage.Text = data["Message"].ToString();
                    //}

                    var portDetails = (from q in db.VSA_Config_Port
                                       join p in db.VSA_Address on q.AddressID equals p.AddressID
                                       join u in db.VSA_Config_Country_Code on q.PortCountry_Code equals u.Country_Code
                                       join f in db.VSA_Config_Country_Code on p.Country_Code equals f.Country_Code
                                       where q.PortID == ddlport.SelectedValue
                                       select new
                                       {
                                           q.PortID,
                                           u.Country_Name,
                                           q.PortCountry_Code,
                                           q.PortZoneCode,
                                           q.PortCargoTerminalType,
                                           q.ContainerCargoTerminalNumber,
                                           p.PhoneNumberTaggedToAddress,
                                           p.EmailTaggedToAddress,
                                           p.BuildingNumber,
                                           p.StreetName1,
                                           p.CityName,
                                           p.State,
                                           addcountry = f.Country_Name,
                                           p.Country_Code,
                                           p.Zipcode
                                       }).SingleOrDefault();
                    TxtPortName.Text = ddlport.SelectedItem.Text;//results.PortName;
                TxtBuildingNumber.Text = portDetails.BuildingNumber;
                    TxtCity.Text =portDetails.CityName;
                    TxtCountryadddsbl.Text = portDetails.addcountry;
                    ddlCountryadd.SelectedValue = portDetails.PortCountry_Code;//results.PortCountry_Code;
                TxtCountrydsbl.Text = portDetails.Country_Name;
                    ddlCountry.SelectedValue = portDetails.Country_Code;//results.PortCountry_Code;
                TxtPortId.Text = portDetails.PortID;//results.PortID;
                TxtPortZonedsbl.Text =  portDetails.PortZoneCode;//results.PortZoneCode;
                ddlPortZone.SelectedItem.Text = portDetails.PortZoneCode;
                    TxtState.Text = portDetails.State;
                    TxtStreet.Text = portDetails.StreetName1;
                    TxtTerminalNumber.Text = Convert.ToString(portDetails.ContainerCargoTerminalNumber);//Convert.ToString(results.ContainerCargoTerminalNumber);
                TxtZipCode.Text = portDetails.Zipcode;
                    if (portDetails.PortCargoTerminalType == "Container")//results.PortCargoTerminalType == "Container"
                {
                        rbContainer1.Checked = true;
                        rbContainer1.Disabled = true;
                        rbNonContainer2.Disabled = true;
                    }
                    if (portDetails.PortCargoTerminalType == "Non Container")//results.PortCargoTerminalType == "Non Container")
                {
                        rbNonContainer2.Checked = true;
                        rbNonContainer2.Disabled = true;
                        rbContainer1.Disabled = true;
                    }
                //}
                ddlport.Visible = true;
                TxtPortName.Visible = false;
                TxtPortId.ReadOnly = true;
                ddlCountry.Visible = false;
                TxtCountrydsbl.Visible = true;
                TxtCountrydsbl.ReadOnly = true;
                ddlPortZone.Visible = false;
                TxtPortZonedsbl.Visible = true;
                TxtPortZonedsbl.ReadOnly = true;
                rbContainer1.Disabled = true;
                rbNonContainer2.Disabled = true;
                TxtTerminalNumber.ReadOnly = true;
                TxtBuildingNumber.ReadOnly = true;
                TxtStreet.ReadOnly = true;
                TxtCity.ReadOnly = true;
                TxtState.ReadOnly = true;
                ddlCountryadd.Visible = false;
                TxtCountryadddsbl.Visible = true;
                TxtCountryadddsbl.ReadOnly = true;
                TxtZipCode.ReadOnly = true;
                btnSave.Visible = false;
                btnRegister.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TodisplayportDetails();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlport.SelectedValue != "0")
                {
                    ddlport.Visible = false;
                    TxtPortName.Visible = true;
                    TxtPortId.ReadOnly = false;
                    ddlCountry.Visible = true;
                    TxtCountrydsbl.Visible = false;
                    ddlPortZone.Visible = true;
                    TxtPortZonedsbl.Visible = false;
                    rbContainer1.Disabled = false;
                    rbNonContainer2.Disabled = false;
                    TxtTerminalNumber.ReadOnly = false;
                    TxtBuildingNumber.ReadOnly = false;
                    TxtStreet.ReadOnly = false;
                    TxtCity.ReadOnly = false;
                    TxtState.ReadOnly = false;
                    ddlCountryadd.Visible = true;
                    TxtCountryadddsbl.Visible = false;
                    TxtZipCode.ReadOnly = false;
                    btnSave.Visible = true;
                    btnRegister.Visible = false;
                }
                else
                {

                    lblmsg.Text = "Please Select Port Name to Edit";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewModifyPort.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {

                var modifyport = (from q in db.VSA_Config_Port
                                  join p in db.VSA_Address on q.AddressID equals p.AddressID
                                  where q.PortID == TxtPortId.Text.Trim()
                                  select new { q, p }).SingleOrDefault();
                modifyport.q.PortName = TxtPortName.Text.Trim();
                modifyport.q.PortID = TxtPortId.Text.Trim();
                modifyport.q.PortCountry_Code = ddlCountry.SelectedValue;
                modifyport.q.PortZoneCode = ddlPortZone.SelectedItem.Text.Trim();
                if (rbContainer1.Checked)
                {
                    modifyport.q.PortCargoTerminalType = "Container";
                }
                if (rbNonContainer2.Checked)
                {
                    modifyport.q.PortCargoTerminalType = "Non Container";
                }
                modifyport.q.ContainerCargoTerminalNumber = Convert.ToInt32(TxtTerminalNumber.Text.Trim());

                modifyport.p.BuildingNumber = TxtBuildingNumber.Text;
                modifyport.p.CityName = TxtCity.Text;
                modifyport.p.State = TxtState.Text;
                modifyport.p.StreetName1 = TxtStreet.Text;
                modifyport.p.Zipcode = TxtZipCode.Text;
                modifyport.p.AddressType = "Port Address";
                modifyport.p.Country_Code = ddlCountryadd.SelectedValue;
                modifyport.p.Registered_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                modifyport.p.AddressStatus = "A";

                db.SaveChanges();
                DbTrans.Commit();
                TodisplayportDetails();
                lblmsg.Text = "Port Details Updated Successfully";
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    //public class VSA_Config
    //{
    //    [Key]
    //    [StringLength(5)]
    //    public string PortID { get; set; }

    //    [Required]
    //    [StringLength(40)]
    //    public string PortName { get; set; }

    //    public int ContainerCargoTerminalNumber { get; set; }

    //    [Required]
    //    [StringLength(8)]
    //    public string PortZoneCode { get; set; }

    //    [Required]
    //    [StringLength(3)]
    //    public string PortCountry_Code { get; set; }

    //    [Required]
    //    [StringLength(25)]
    //    public string PortCargoTerminalType { get; set; }

    //    public DateTime PortCreate_Ts { get; set; }

    //    public int? AddressID { get; set; }
    //}
}