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
    public partial class VesselRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //vesselowner();
                ddlvesselOprator();
                ddlcountry();
            }
            lblerrVesselType.Text = "";
            lblmsg.Text = "";
        }

        // Method ddlvesselOprator(): Use : To populate the vessel operator dropdown with Vessel Operator customer types
        public void ddlvesselOprator()
        {
            try
            {
                //string f = ddlVesselOwner.SelectedValue;
                var db = new VesselAgreement();
                var veseloptr = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                 join p in db.VSA_Master_Customer on q.CustomerID equals p.CustomerID
                                 where q.CustomerTypeID == "VSLOPR"
                                 //join p in db.VSA_Config_CustomerType on q.CustomerTypeID equals p.CustomerTypeID
                                 //where q.CustomerID == ddlVesselOwner.SelectedValue
                                 select new
                                 {
                                     q.CustomerID,
                                     q.CustomerTypeID,
                                     p.CompanyName,
                                 }).ToList();

                ddlVesselOperator.DataSource = veseloptr.OrderBy(x => x.CompanyName);
                ddlVesselOperator.DataTextField = "CompanyName";
                ddlVesselOperator.DataValueField = "CustomerID";
                ddlVesselOperator.DataBind();
                ddlVesselOperator.Items.Insert(0, new ListItem("Vessel Operators", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlcountry(): Use : To populate the country name dropdown with different country codes
        public void ddlcountry()
        {
            try
            {
                var db = new VesselAgreement();

                ddlregcountry.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlregcountry.DataTextField = "Country_Name";
                ddlregcountry.DataValueField = "Country_Code";
                ddlregcountry.DataBind();
                ddlregcountry.Items.Insert(0, new ListItem("Select Registered Country", "0"));

                ddlVesFlagCountry.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlVesFlagCountry.DataTextField = "Country_Name";
                ddlVesFlagCountry.DataValueField = "Country_Code";
                ddlVesFlagCountry.DataBind();
                ddlVesFlagCountry.Items.Insert(0, new ListItem("Select Vessel Flag Country", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method VesselDetails(): Use : To insert the newly entered vessel details into the database
        public void VesselDetails()
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
            
                var vesselvalid = (from q in db.VSA_Master_Vessel
                                   where q.VesselID == TxtVesselId.Text
                                   select new { q.VesselID }).ToList();
                if (vesselvalid.Count == 0)
                {
                    var vesseldtls = new VSA_Master_Vessel();
                    string custid = Convert.ToString(Session["CustomerID"]);
                    vesseldtls.VesselID = TxtVesselId.Text;
                    vesseldtls.NameoftheVessel = TxtVesselName.Text;
                    vesseldtls.IMOShipID = TxtIMOShipId.Text;
                    vesseldtls.RegisteredCountryCode = ddlregcountry.SelectedValue;
                    vesseldtls.VesselFlagCountryCode = ddlVesFlagCountry.SelectedValue;
                    if (ckContainerCargoVessel.Checked)
                    {
                        vesseldtls.ContainerCargovesseltypeConfirmation = "Y";
                    }
                    else
                    {
                        vesseldtls.ContainerCargovesseltypeConfirmation = "N";
                    }
                    if (ckCanCarryRORO.Checked)
                    {
                        vesseldtls.Can_Carry_RoRo = "Y";
                    }
                    else
                    {
                        vesseldtls.Can_Carry_RoRo = "N";
                    }
                    if (ckRefrigeratedContainerCargo.Checked)
                    {
                        vesseldtls.Can_Carry_Refrigerated_Container = "Y";
                    }
                    else
                    {
                        vesseldtls.Can_Carry_Refrigerated_Container = "N";
                    }
                    if (ckHazardousMaterialContainerCargo.Checked)
                    {
                        vesseldtls.Can_Carry_Hazmat_Container = "Y";
                    }
                    else
                    {
                        vesseldtls.Can_Carry_Hazmat_Container = "N";
                    }
                    vesseldtls.VesselCapacityTEU = Convert.ToInt32(TxtTEUs.Text);
                    vesseldtls.GrossWeightTon = Convert.ToInt32(TxtGrossTonnageLimit.Text);
                    vesseldtls.OperatingWeightTon = Convert.ToInt32(TxtOperatingTonnageLimit.Text);
                    vesseldtls.VesselOwnerId = custid;
                    vesseldtls.VesselOperatorId = ddlVesselOperator.SelectedValue;

                    

                    string imo = TxtIMOShipId.Text.ToUpper();

                    
                        if (imo.Length == 7)
                        {
                            int a = Convert.ToInt32(imo.Substring(0, 1));
                        var b = Convert.ToInt32(imo.Substring(1, 1));
                        var c = Convert.ToInt32(imo.Substring(2, 1));
                        var d = Convert.ToInt32(imo.Substring(3, 1));
                        var e = Convert.ToInt32(imo.Substring(4, 1));
                        var f = Convert.ToInt32(imo.Substring(5, 1));
                        var g = Convert.ToInt32(imo.Substring(6, 1));

                        var verifiedsum = (a * 7) + (b * 6) + (c * 5) + (d * 4) + (e * 3) + (f * 2);

                        int verifiedsumCount = verifiedsum.ToString().Length;

                        int matchcount = Convert.ToInt32(verifiedsum.ToString().Substring(verifiedsumCount - 1, 1));

                        if (matchcount != g)
                        {
                            String strAppMsg = ConfigurationManager.AppSettings["VRimoinvalid"];
                            lblmsg.Text = strAppMsg;
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            if ((ckContainerCargoVessel.Checked == false) && (ckRefrigeratedContainerCargo.Checked == false) && (ckCanCarryRORO.Checked == false) && (ckHazardousMaterialContainerCargo.Checked == false))
                            {
                                String strAppMsg = ConfigurationManager.AppSettings["VRselectonetype"];
                                lblmsg.Text = strAppMsg;
                                lblerrVesselType.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                if (Convert.ToInt32(TxtOperatingTonnageLimit.Text.Trim()) <= Convert.ToInt32(TxtGrossTonnageLimit.Text.Trim()))
                                {
                                    db.VSA_Master_Vessel.Add(vesseldtls);
                                    db.SaveChanges();
                                    String strAppMsg = ConfigurationManager.AppSettings["VRvesselregistration"];
                                    lblmsg.Text = strAppMsg;
                                    lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
                                    DbTrans.Commit();
                                    clear();
                                }
                                else
                                {
                                    String strAppMsg = ConfigurationManager.AppSettings["VRopttonnagegroslimit"];
                                    lblmsg.Text = strAppMsg;
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    DbTrans.Rollback();
                                }
                            }
                        }
                    }
                        else
                        {
                        String strAppMsg = ConfigurationManager.AppSettings["VRimoinvalid"];
                        lblmsg.Text = strAppMsg;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        DbTrans.Rollback();
                    }
                }
                else
                {
                    String strAppMsg = ConfigurationManager.AppSettings["VRduplicateVesselid"];
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

        // Method btnRegister_Click: Use : To trigger the click event of vessel registration button
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
                VesselDetails();
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method clear(): Use : To clear all the text fields with default values 
        public void clear()
        {
            try
            {
                TxtGrossTonnageLimit.Text = "";
                TxtIMOShipId.Text = "";
                TxtOperatingTonnageLimit.Text = "";
                TxtTEUs.Text = "";
                TxtVesselId.Text = "";
                TxtVesselName.Text = "";
                ckCanCarryRORO.Checked = false;
                ckContainerCargoVessel.Checked = false;
                ckHazardousMaterialContainerCargo.Checked = false;
                ckRefrigeratedContainerCargo.Checked = false;
                //vesselowner();
                ddlvesselOprator();
                ddlcountry();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Method btnCancel_Click: Use : To trigger the cancel button click event 
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
    }
}