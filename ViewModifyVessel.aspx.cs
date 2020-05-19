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
    public partial class ViewModifyVessel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddNameOFvessel();
                ddlcountry();
                if (Convert.ToString(Session["CustomerID"]) == "Admin")
                {
                    string vesselid = Request.QueryString["Name"];
                    if (vesselid != null)
                    {
                        ddlvesselName.ClearSelection();
                        //ddlvesselName.Items.FindByValue(vesselid.Trim()).Selected = true;
                        ddlvesselName.SelectedValue = vesselid.Trim();
                        ViewVesselDetails();
                    }
                    else
                    {
                        Response.Redirect("Logout.aspx", false);
                    }
                }
            }
            ddlvesselOprator();
            lblmsg.Text = "";
            lblerrVesselType.Text = "";
        }

        // Method ddNameOFvessel() Use : To get the names of the different vessel owned by particular customer id
        public void ddNameOFvessel()
        {
            try
            {
                var db = new VesselAgreement();
                if (Convert.ToString(Session["CustomerID"]) != "Admin")
                {
                    string custid = Convert.ToString(Session["CustomerID"]);
                    var VeselName = (from q in db.VSA_Master_Vessel
                                     where q.VesselOwnerId == custid
                                     select new
                                     {
                                         NameoftheVessel = q.VesselID + "-" + q.NameoftheVessel,
                                         VesselID = q.VesselID,
                                     }).ToList();

                    ddlvesselName.DataSource = VeselName.OrderBy(x => x.NameoftheVessel);
                }
                else
                {
                    ddlvesselName.DataSource = db.VSA_Master_Vessel.Select(x=> new { NameoftheVessel = x.VesselID+"-"+x.NameoftheVessel, VesselID = x.VesselID }).ToList().OrderBy(x => x.NameoftheVessel);
                }
                ddlvesselName.DataTextField = "NameoftheVessel";
                ddlvesselName.DataValueField = "VesselID";
                ddlvesselName.DataBind();
                ddlvesselName.Items.Insert(0, new ListItem("--Select Vessel Name--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlvesselOprator() Use : To get the names of the different customers of type vessel operator in the database       

        public void ddlvesselOprator()
        {
            try
            {

                var db = new VesselAgreement();
                var veseloptr = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                 where q.CustomerTypeID == "VSLOPR"
                                 //join p in db.VSA_Config_CustomerType on q.CustomerTypeID equals p.CustomerTypeID
                                 //where q.CustomerID == ddlVesselOwner.SelectedValue
                                 select new
                                 {
                                     q.CustomerID,
                                     q.CustomerTypeID,
                                 }).ToList();

                ddlVesselOperator.DataSource = veseloptr.OrderBy(x => x.CustomerID);
                ddlVesselOperator.DataTextField = "CustomerID";
                ddlVesselOperator.DataValueField = "CustomerID";
                ddlVesselOperator.DataBind();
                ddlVesselOperator.Items.Insert(0, new ListItem("--Select Operator--", "0"));

                var existingOperator = (from q in db.VSA_Master_Vessel
                                        where q.VesselID == ddlvesselName.SelectedValue
                                        select new { q.VesselOperatorId }).SingleOrDefault();
                if (existingOperator != null)
                {
                    ddlVesselOperator.SelectedValue = Convert.ToString(existingOperator.VesselOperatorId);
                    TxtOperatorName.Text = ddlVesselOperator.SelectedItem.Text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlcountry() Use : To populate different country codes for the registered as well as vessel flag country dropdowns      
        public void ddlcountry()
        {
            try
            {
                var db = new VesselAgreement();

                ddlregcountry.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlregcountry.DataTextField = "Country_Name";
                ddlregcountry.DataValueField = "Country_Code";
                ddlregcountry.DataBind();
                ddlregcountry.Items.Insert(0, new ListItem("--Select Registered Country--", "0"));

                ddlVesFlagCountry.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlVesFlagCountry.DataTextField = "Country_Name";
                ddlVesFlagCountry.DataValueField = "Country_Code";
                ddlVesFlagCountry.DataBind();
                ddlVesFlagCountry.Items.Insert(0, new ListItem("--Select Vessel Flag Country--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ViewVesselDetails() Use : Based on vessel id selection to fetch the vessel details and display

        public void ViewVesselDetails()
        {
            try
            {
                var db = new VesselAgreement();
                string vessid = ddlvesselName.SelectedValue;
                if (Convert.ToString(Session["CustomerID"]) == "Admin")
                {
                    string vesselid = Request.QueryString["Name"];
                    if (vesselid != null)
                    {
                        vessid = vesselid;
                        ddlvesselName.SelectedValue = vessid;
                    }
                }

                    var show = (from q in db.VSA_Master_Vessel
                            where q.VesselID == vessid
                                select new
                            {
                                q.NameoftheVessel,
                                q.IMOShipID,
                                q.RegisteredCountryCode,
                                q.VesselFlagCountryCode,
                                q.Can_Carry_Hazmat_Container,
                                q.Can_Carry_Refrigerated_Container,
                                q.Can_Carry_RoRo,
                                q.ContainerCargovesseltypeConfirmation,
                                q.VesselCapacityTEU,
                                q.GrossWeightTon,
                                q.OperatingWeightTon,
                            }).SingleOrDefault();

                if(show != null)
                { 
                TxtVesselName.Text = show.NameoftheVessel;
                TxtIMOShipId.Text = show.IMOShipID;
                TxtIMOId.Text= "IMO" + show.IMOShipID;
                ddlregcountry.SelectedValue = show.RegisteredCountryCode;
                Txtregcountry.Text = ddlregcountry.SelectedItem.Text;
                ddlVesFlagCountry.SelectedValue = show.VesselFlagCountryCode;
                TxtVesFlagCountry.Text = ddlVesFlagCountry.SelectedItem.Text;
                if (show.Can_Carry_Hazmat_Container == "Y")
                {
                    ckHazardousMaterialContainerCargo.Checked = true;
                }
                else
                {
                    ckHazardousMaterialContainerCargo.Checked = false;
                }
                if (show.Can_Carry_Refrigerated_Container == "Y")
                {
                    ckRefrigeratedContainerCargo.Checked = true;
                }
                else
                {
                    ckRefrigeratedContainerCargo.Checked = false;
                }
                if(show.Can_Carry_RoRo=="Y")
                {
                    ckCanCarryRORO.Checked = true;
                }
                else
                {
                    ckCanCarryRORO.Checked = false;
                }
                if(show.ContainerCargovesseltypeConfirmation =="Y")
                {
                    ckContainerCargoVessel.Checked = true;
                }
                else
                {
                    ckContainerCargoVessel.Checked = false;
                }

                TxtTEUs.Text = Convert.ToString(show.VesselCapacityTEU);
                TxtGrossTonnageLimit.Text = Convert.ToString(show.GrossWeightTon);
                TxtOperatingTonnageLimit.Text = Convert.ToString(show.OperatingWeightTon);

                TxtVesselName.ReadOnly = true;
                TxtVesselName.Visible = false;
                TxtIMOId.ReadOnly = true;
                TxtIMOId.Visible = true;
                TxtIMOShipId.Visible = false;
                ddlVesselOperator.Enabled = false;
                ddlVesselOperator.CssClass = "form-control input-lg";
                ddlregcountry.Enabled = false;
                ddlregcountry.CssClass = "form-control input-lg";
                ddlVesFlagCountry.Enabled = false;
                ddlVesFlagCountry.CssClass = "form-control input-lg";
                ckCanCarryRORO.Disabled = true;
                ckContainerCargoVessel.Disabled = true;
                ckHazardousMaterialContainerCargo.Disabled = true;
                ckRefrigeratedContainerCargo.Disabled = true;
                TxtTEUs.ReadOnly = true;
                TxtGrossTonnageLimit.ReadOnly = true;
                TxtOperatingTonnageLimit.ReadOnly = true;
                ddlvesselName.Enabled = true;
                ddlvesselName.CssClass = "form-control input-lg";
                    ddlvesselName.Visible = true;
                    //Vesselh4.Visible = true;
                   

                btnEdit.Visible = true;
                btnCancel.Visible = false;
                lnkSave.Visible = false;

                }
                else
                {
                    Response.Redirect("ViewModifyVessel.aspx", false);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // Method EditVesselDetails() Use : To enable different controls containing vessel details in edit mode
        public void EditVesselDetails()
        {
            TxtVesselName.ReadOnly = false;
            TxtOperatorName.Visible = false;
            Txtregcountry.Visible = false;
            TxtVesFlagCountry.Visible = false;
            TxtIMOShipId.Visible = true;
            TxtIMOId.Visible = false;
            ddlvesselName.Enabled = false;
            ddlvesselName.CssClass = "form-control input-lg";
            ddlVesselOperator.Visible = true;
            ddlVesselOperator.Enabled = true;
            ddlVesselOperator.CssClass = "form-control input-lg";
            ddlregcountry.Visible = true;
            ddlregcountry.Enabled = true;
            ddlregcountry.CssClass = "form-control input-lg";
            ddlVesFlagCountry.Visible = true;
            ddlVesFlagCountry.Enabled = true;
            ddlVesFlagCountry.CssClass = "form-control input-lg";
            ckCanCarryRORO.Disabled = false;
            ckContainerCargoVessel.Disabled = false;
            ckHazardousMaterialContainerCargo.Disabled = false;
            ckRefrigeratedContainerCargo.Disabled = false;
            TxtTEUs.ReadOnly = false;
            TxtGrossTonnageLimit.ReadOnly = false;
            TxtOperatingTonnageLimit.ReadOnly = false;
            ddlvesselName.Visible = false;
            //Vesselh4.Visible = true;
            TxtVesselName.Visible = true;


            btnEdit.Visible = false;
            lnkSave.Visible = true;
            btnCancel.Visible = true;
            
        }

        // Method ddlvesselName_SelectedIndexChanged Use : This method gets triggered when there is a change in vessel name drop down value

        protected void ddlvesselName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewVesselDetails();
        }

        // Method btnEdit_Click Use : This is to trigger the editvesseldetails method
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (ddlvesselName.SelectedValue != "0")
            {
                EditVesselDetails();
            }
            else
            {
                String strAppMsg = ConfigurationManager.AppSettings["MVselectvessel"];
                lblmsg.Text = strAppMsg;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method btnCancel_Click Use : This is will redirect the page to viewmodifyvessel page
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewModifyVessel.aspx", false);
        }

        // Method lnkSave_Click Use : This is to save the edited vessel details.
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            { 
            

                var UpdateVessel = (from q in db.VSA_Master_Vessel
                                    where q.VesselID == ddlvesselName.SelectedValue
                                    select new {q}).SingleOrDefault();

                //UpdateVessel.q.NameoftheVessel = TxtVesselName.Text;
                UpdateVessel.q.IMOShipID = TxtIMOShipId.Text;
                UpdateVessel.q.VesselOperatorId = ddlVesselOperator.SelectedValue;
                UpdateVessel.q.RegisteredCountryCode = ddlregcountry.SelectedValue;
                UpdateVessel.q.VesselFlagCountryCode = ddlVesFlagCountry.SelectedValue;

                if(ckHazardousMaterialContainerCargo.Checked)
                {
                    UpdateVessel.q.Can_Carry_Hazmat_Container = "Y";
                }
                else
                {
                    UpdateVessel.q.Can_Carry_Hazmat_Container = "N";
                }
                if (ckRefrigeratedContainerCargo.Checked)
                {
                    UpdateVessel.q.Can_Carry_Refrigerated_Container = "Y";
                }
                else
                {
                    UpdateVessel.q.Can_Carry_Refrigerated_Container = "N";
                }
                if (ckContainerCargoVessel.Checked)
                {
                    UpdateVessel.q.ContainerCargovesseltypeConfirmation = "Y";
                }
                else
                {
                    UpdateVessel.q.ContainerCargovesseltypeConfirmation = "N";
                }
                if (ckCanCarryRORO.Checked)
                {
                    UpdateVessel.q.Can_Carry_RoRo = "Y";
                }
                else
                {
                    UpdateVessel.q.Can_Carry_RoRo = "N";
                }

                UpdateVessel.q.VesselCapacityTEU = Convert.ToInt32(TxtTEUs.Text);
                UpdateVessel.q.GrossWeightTon = Convert.ToInt32(TxtGrossTonnageLimit.Text);
                UpdateVessel.q.OperatingWeightTon = Convert.ToInt32(TxtOperatingTonnageLimit.Text);


                    string imo = TxtIMOShipId.Text.Trim();

                    if (imo.Length == 7)
                    {
                        int a = Convert.ToInt32(imo.Substring(0, 1));
                        var b = Convert.ToInt32(imo.Substring(1, 1));
                        var c = Convert.ToInt32(imo.Substring(2, 1));
                        var d = Convert.ToInt32(imo.Substring(3, 1));
                        var h = Convert.ToInt32(imo.Substring(4, 1));
                        var f = Convert.ToInt32(imo.Substring(5, 1));
                        var g = Convert.ToInt32(imo.Substring(6, 1));

                        var verifiedsum = (a * 7) + (b * 6) + (c * 5) + (d * 4) + (h * 3) + (f * 2);

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
                        if((ckContainerCargoVessel.Checked==false) && (ckRefrigeratedContainerCargo.Checked == false) && (ckCanCarryRORO.Checked == false) && (ckHazardousMaterialContainerCargo.Checked == false))
                        {
                            String strAppMsg = ConfigurationManager.AppSettings["VRselectonetype"];
                            lblmsg.Text = strAppMsg;
                            lblerrVesselType.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(TxtOperatingTonnageLimit.Text.Trim()) <= Convert.ToInt32(TxtGrossTonnageLimit.Text.Trim()))
                            {
                                db.SaveChanges();
                                String strAppMsg = ConfigurationManager.AppSettings["MVupdateVessel"];
                                lblmsg.Text = strAppMsg;
                                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
                                DbTrans.Commit();

                                ddlcountry();
                                ddlvesselOprator();
                                ViewVesselDetails();
                            }
                            else
                            {
                                
                                String strAppMsg = ConfigurationManager.AppSettings["VRopttonnagegroslimit"];
                                lblmsg.Text = strAppMsg;
                                lblmsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    }
                    else
                    {
                    String strAppMsg = ConfigurationManager.AppSettings["VRimoinvalid"];
                    lblmsg.Text = strAppMsg;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
            }
            catch(Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text=ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}