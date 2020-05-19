using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;
namespace VesselSharingAgreement
{
    public partial class VesselVoyageDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlvessel();
                ddlOriginPortID();
                ddldestinationportid();
                ddlshiproutedestinationportid();
                ddlvoyagelist();
                showvoyage();
                
            }
            btninsert.Visible = true;
            Origin.Visible = true;
            Destination.Visible = true;
            extddlOriginport.Visible = false;
            extddlDestination.Visible = false;
            lblmsg.Text = "";
            lblContainerTypeMsg.Text = "";
        }
        // Method ddlvessel(): Use : To populate all the vessel ids in the ddlvessel dropdown
        public void ddlvessel()
        {
            try
            {
                var db = new VesselAgreement();
                string custid = Convert.ToString(Session["CustomerID"]);

                var vesselddlname = (from q in db.VSA_Master_Vessel
                                     where q.VesselOperatorId == custid
                                     select new
                                     {
                                         vesselidname = q.VesselID + "-" + q.NameoftheVessel,
                                         vesselid = q.VesselID,
                                     }).ToList();

                ddlVesselId.DataSource = vesselddlname.OrderBy(x => x.vesselidname);
                ddlVesselId.DataTextField = "vesselidname";
                ddlVesselId.DataValueField = "vesselid";
                ddlVesselId.DataBind();
                ddlVesselId.Items.Insert(0, new ListItem("--Select Vessel--", "0"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlvoyagelist(): Use : To initialise empty voyage list drop down
        public void ddlvoyagelist()
        {
            try
            {
                ddlVoyageId.DataSource = "";
                ddlVoyageId.DataTextField = "";
                ddlVoyageId.DataValueField = "";
                ddlVoyageId.DataBind();
                ddlVoyageId.Items.Insert(0, new ListItem("Select Voyage Id", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlvoyage(): Use : To populate voyage list in the drop down related to the selected vessel id
        public void ddlvoyage()
        {
            try
            {
                var db = new VesselAgreement();

                var voyid = (from q in db.VSA_Txn_VesselVoyage
                             join p in db.VSA_Txn_VesselVoyageTransitShipRoute on q.VoyageID equals p.VoyageID
                             where q.VesselID == ddlVesselId.SelectedValue
                             where p.ExpectedArrivalDateTime >= DateTime.Now
                             select new { q.VoyageID }).ToList();
                var vo = (from q in db.VSA_Txn_VesselVoyage
                          where q.VesselID == ddlVesselId.SelectedValue
                          where q.Voyageshiproutecompleteflag == "N"
                          select new { q.VoyageID }
                        ).ToList();

                var vogid = Enumerable.Union(voyid, vo);


                ddlVoyageId.DataSource = vogid.OrderBy(x => x.VoyageID);
                ddlVoyageId.DataTextField = "VoyageID";
                ddlVoyageId.DataValueField = "VoyageID";
                ddlVoyageId.DataBind();
                ddlVoyageId.Items.Insert(0, new ListItem("Select Voyage Id", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method showvessel(): Use : To show the vessel details related a selected vessel id 
        public void showvessel()
        {
            try
            {
                var db = new VesselAgreement();
                if (ddlVesselId.SelectedValue != "0")
                {
                    var sv = (from q in db.VSA_Master_Vessel
                              join d in db.VSA_Config_Country_Code on q.RegisteredCountryCode equals d.Country_Code
                              join e in db.VSA_Config_Country_Code on q.VesselFlagCountryCode equals e.Country_Code
                              where q.VesselID == ddlVesselId.SelectedValue

                              select new
                              {
                                  d.Country_Name,
                                  VesselFlagCountry = e.Country_Name,
                                  q.VesselID,
                                  q.NameoftheVessel,
                                  q.VesselCapacityTEU,
                                  q.ContainerCargovesseltypeConfirmation,
                                  q.Can_Carry_Refrigerated_Container,
                                  q.Can_Carry_Hazmat_Container,
                                  q.RegisteredCountryCode,
                                  q.VesselFlagCountryCode,
                                  q.OperatingWeightTon,
                              }).SingleOrDefault();

                    TxtVesselName.Text = sv.NameoftheVessel;
                    TxtVesselTEUs.Text = Convert.ToString(sv.VesselCapacityTEU);
                    TxtRegCountry.Text = sv.Country_Name;
                    TxtFlagCountry.Text = sv.VesselFlagCountry;
                    TxtOperatingtonnage.Text =Convert.ToString(sv.OperatingWeightTon);

                    if (sv.ContainerCargovesseltypeConfirmation.Trim() == "Y")
                    {
                        ckContainerCargoVessel.Checked = true;
                        ckContainerCargoVessel.Disabled = true;
                    }
                    //else
                    //{
                    //    ckContainerCargoVessel.Checked = false;
                    //    ckContainerCargoVessel.Disabled = false;
                    //}

                    if (sv.Can_Carry_Hazmat_Container.Trim() == "Y")
                    {
                        ckHazardousMaterial.Checked = true;
                        ckHazardousMaterial.Disabled = false;
                    }
                    else
                    {
                        ckHazardousMaterial.Checked = false;
                        ckHazardousMaterial.Disabled = true;
                    }

                    if (sv.Can_Carry_Refrigerated_Container.Trim() == "Y")
                    {
                        ckRefrigeratedContainerCargo.Checked = true;
                        ckRefrigeratedContainerCargo.Disabled = false;
                    }
                    else
                    {
                        ckRefrigeratedContainerCargo.Checked = false;
                        ckRefrigeratedContainerCargo.Disabled = true;
                    }
                }
                else
                {
                    TxtVesselName.Text = "";
                    TxtVesselTEUs.Text = "";
                    TxtRegCountry.Text = "";
                    TxtFlagCountry.Text = "";
                    TxtOperatingtonnage.Text = "";
                    ckContainerCargoVessel.Checked = false;
                    ckHazardousMaterial.Checked = false;
                    ckRefrigeratedContainerCargo.Checked = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlOriginPortID(): Use : To populate the Origin Port drop down with values from Database
        public void ddlOriginPortID()
        {
            try
            {
                var db = new VesselAgreement();

                ddlOrigin.DataSource = db.VSA_Config_Port.ToList().OrderBy(x => x.PortName);
                ddlOrigin.DataTextField = "PortName";
                ddlOrigin.DataValueField = "PortID";
                ddlOrigin.DataBind();
                ddlOrigin.Items.Insert(0, new ListItem("Select Origin Port", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddldestinationportid(): Use : To populate the Destination Port drop down with values from Database
        public void ddldestinationportid()
        {
            try
            {
                var db = new VesselAgreement();

                ddlDestination.DataSource = db.VSA_Config_Port.ToList().OrderBy(x => x.PortName);
                ddlDestination.DataTextField = "PortName";
                ddlDestination.DataValueField = "PortID";
                ddlDestination.DataBind();
                ddlDestination.Items.Insert(0, new ListItem("Select Destination Port", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method VoyageDetails(): Use : To create a new vessel voyage for the selected vessel id
        public void VoyageDetails()
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
            
                var voyagevalid = (from q in db.VSA_Txn_VesselVoyage
                                   where q.VesselID == ddlVesselId.SelectedValue
                                   where q.VoyageID == TxtVoyageId.Text
                                   select new { q.VoyageID }).ToList();
                if (voyagevalid.Count == 0)
                {
                    if (ckHazardousMaterial.Checked)
                    {
                        string confirmValue = Request.Form["confirm_value"];
                        if (confirmValue == "Yes")
                        {
                            if (ddlOrigin.SelectedValue != ddlDestination.SelectedValue)
                            {

                                var voyagedtls = new VSA_Txn_VesselVoyage();

                                voyagedtls.VoyageID = TxtVoyageId.Text;
                                voyagedtls.VesselID = ddlVesselId.SelectedValue;
                                voyagedtls.OriginloadTEU = Convert.ToInt32(TxtContainersTEUs.Text);
                                voyagedtls.OriginGrossTonnage = Convert.ToInt32(TxtGrossTonnage.Text);
                                voyagedtls.Create_TS = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                                voyagedtls.Update_TS = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                                voyagedtls.OriginPortID = ddlOrigin.SelectedValue;
                                voyagedtls.DestinationPortID = ddlDestination.SelectedValue;
                                voyagedtls.Voyageshiproutecompleteflag = "N";
                                if (ckContainerCargoVessel.Checked)
                                {
                                    voyagedtls.Isit_Containercargovessel = "Y";
                                }
                                else
                                {
                                    voyagedtls.Isit_Containercargovessel = "N";
                                }
                                if (ckHazardousMaterial.Checked)
                                {
                                    voyagedtls.HazardousAllowFlag = "Y";
                                }
                                else
                                {
                                    voyagedtls.HazardousAllowFlag = "N";
                                }
                                if (ckRefrigeratedContainerCargo.Checked)
                                {
                                    voyagedtls.RefrigeratedAllowFlag = "Y";
                                }
                                else
                                {
                                    voyagedtls.RefrigeratedAllowFlag = "N";
                                }
                                if (Convert.ToInt32(TxtVesselTEUs.Text) > Convert.ToInt32(TxtContainersTEUs.Text))
                                {
                                    var Operatingtonnage = (from q in db.VSA_Master_Vessel
                                                        where q.VesselID == ddlVesselId.SelectedValue
                                                        select new
                                                        {
                                                            q.OperatingWeightTon,
                                                        }).SingleOrDefault();
                                    if (Convert.ToInt32(TxtGrossTonnage.Text.Trim()) <= Operatingtonnage.OperatingWeightTon)
                                    {
                                        db.VSA_Txn_VesselVoyage.Add(voyagedtls);
                                        db.SaveChanges();
                                        String strAppMsg = ConfigurationManager.AppSettings["VoyageDetails"];
                                        lblContainerTypeMsg.Text = strAppMsg;
                                        lblContainerTypeMsg.ForeColor = System.Drawing.Color.ForestGreen;


                                        TxtOriginportid.Text = ddlOrigin.SelectedItem.Text;

                                        btncreate.Visible = false;
                                        btnCancel.Visible = false;
                                    }
                                    else
                                    {
                                        String strAppMsg = ConfigurationManager.AppSettings["VVgrostonnageoptlimit"];
                                        lblContainerTypeMsg.Text = strAppMsg;
                                        lblContainerTypeMsg.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                                else
                                {
                                    String strAppMsg = ConfigurationManager.AppSettings["VVloarcapacity"];
                                    lblContainerTypeMsg.Text = strAppMsg;
                                    lblContainerTypeMsg.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                String strAppMsg = ConfigurationManager.AppSettings["VVOrignDestination"];
                                lblmsg.Text = strAppMsg;
                                lblmsg.ForeColor = System.Drawing.Color.Red;
                            }
                            if (Convert.ToInt32(TxtVesselTEUs.Text) > Convert.ToInt32(TxtContainersTEUs.Text))
                            {
                                var Operatingtonnage = (from q in db.VSA_Master_Vessel
                                                    where q.VesselID == ddlVesselId.SelectedValue
                                                    select new
                                                    {
                                                        q.OperatingWeightTon,
                                                    }).SingleOrDefault();

                                if (Convert.ToInt32(TxtGrossTonnage.Text.Trim()) <= Operatingtonnage.OperatingWeightTon)
                                {
                                    showvoyage();
                                    ddlvoyage();

                                    var vo = TxtVoyageId.Text;
                                    ddlVoyageId.Items.FindByValue(vo).Selected = true;
                                    showVoyagedtls();
                                    insrtrow.Visible = true;
                                    TxtVoyageSequence.Visible = true;
                                    TxtOriginportid.Visible = true;
                                    TxtStartTime.Visible = true;
                                    TxtStartDate.Visible = true;
                                    TxtArrivalDate.Visible = true;
                                    TxtArrivalTime.Visible = true;
                                    ddldestintnportid.Visible = true;
                                }
                            }
                        }

                        if (confirmValue == "No")
                        {
                            ckHazardousMaterial.Checked = false;
                        }

                    }
                    else
                    {
                        string confirmValue = Request.Form["confirm_value"];

                        if (ddlOrigin.SelectedValue != ddlDestination.SelectedValue)
                        {
                            
                            var voyagedtls = new VSA_Txn_VesselVoyage();

                            voyagedtls.VoyageID = TxtVoyageId.Text;
                            voyagedtls.VesselID = ddlVesselId.SelectedValue;
                            voyagedtls.OriginloadTEU = Convert.ToInt32(TxtContainersTEUs.Text);
                            voyagedtls.OriginGrossTonnage = Convert.ToInt32(TxtGrossTonnage.Text);
                            voyagedtls.Create_TS = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                            voyagedtls.Update_TS = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                            voyagedtls.OriginPortID = ddlOrigin.SelectedValue;
                            voyagedtls.DestinationPortID = ddlDestination.SelectedValue;
                            voyagedtls.Voyageshiproutecompleteflag = "N";
                            if (ckContainerCargoVessel.Checked)
                            {
                                voyagedtls.Isit_Containercargovessel = "Y";
                            }
                            else
                            {
                                voyagedtls.Isit_Containercargovessel = "N";
                            }
                            if (ckHazardousMaterial.Checked)
                            {
                                voyagedtls.HazardousAllowFlag = "Y";
                            }
                            else
                            {
                                voyagedtls.HazardousAllowFlag = "N";
                            }
                            if (ckRefrigeratedContainerCargo.Checked)
                            {
                                voyagedtls.RefrigeratedAllowFlag = "Y";
                            }
                            else
                            {
                                voyagedtls.RefrigeratedAllowFlag = "N";
                            }
                            if(Convert.ToInt32(TxtVesselTEUs.Text) > Convert.ToInt32(TxtContainersTEUs.Text) )
                            {
                                var Operatingtonnage = (from q in db.VSA_Master_Vessel
                                                    where q.VesselID == ddlVesselId.SelectedValue
                                                    select new
                                                    {
                                                        q.OperatingWeightTon,
                                                    }).SingleOrDefault();
                                if (Convert.ToInt32(TxtGrossTonnage.Text.Trim()) <= Operatingtonnage.OperatingWeightTon)
                                {

                                    db.VSA_Txn_VesselVoyage.Add(voyagedtls);
                                    db.SaveChanges();
                                    DbTrans.Commit();
                                    String strAppMsg = ConfigurationManager.AppSettings["VVvoyageDetails"];
                                    lblContainerTypeMsg.Text = strAppMsg;
                                    lblContainerTypeMsg.ForeColor = System.Drawing.Color.ForestGreen;
                                    TxtOriginportid.Text = ddlOrigin.SelectedItem.Text;
                                    btncreate.Visible = false;
                                    btnCancel.Visible = false;
                                }
                                else
                                {
                                    String strAppMsg = ConfigurationManager.AppSettings["VVvoyagegrostonnageopt"];
                                    lblContainerTypeMsg.Text = strAppMsg;
                                    lblContainerTypeMsg.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                String strAppMsg = ConfigurationManager.AppSettings["VVloarcapacity"];
                                lblContainerTypeMsg.Text = strAppMsg;
                                lblContainerTypeMsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            String strAppMsg = ConfigurationManager.AppSettings["VVOrignDestination"];
                            lblmsg.Text = strAppMsg;
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                        }
                        if (Convert.ToInt32(TxtVesselTEUs.Text) > Convert.ToInt32(TxtContainersTEUs.Text))
                        {
                            var Operatingtonnage = (from q in db.VSA_Master_Vessel
                                                where q.VesselID == ddlVesselId.SelectedValue
                                                select new
                                                {
                                                    q.OperatingWeightTon,
                                                }).SingleOrDefault();

                            if (Convert.ToInt32(TxtGrossTonnage.Text.Trim()) <= Operatingtonnage.OperatingWeightTon)
                            {
                                showvoyage();
                                ddlvoyage();

                                
                                var vo = TxtVoyageId.Text;
                                ddlVoyageId.Items.FindByValue(vo).Selected = true;
                                showVoyagedtls();
                                insrtrow.Visible = true;
                                TxtVoyageSequence.Visible = true;
                                TxtOriginportid.Visible = true;
                                TxtStartTime.Visible = true;
                                TxtStartDate.Visible = true;
                                TxtArrivalDate.Visible = true;
                                TxtArrivalTime.Visible = true;
                                ddldestintnportid.Visible = true;
                            }
                        }
                    }

                }
                else
                {
                    String strAppMsg = ConfigurationManager.AppSettings["VVduplicateVoyageid"];
                    lblContainerTypeMsg.Text = strAppMsg;
                    lblContainerTypeMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                throw ex;
            }

        }

        // Method showVoyagedtls(): Use : To display the already created Vessel voyage details on the screen 
        public void showVoyagedtls()
        {
            try
            {
                var db = new VesselAgreement();
                if (ddlVoyageId.SelectedValue != "0")
                {
                    var vodtls = (from q in db.VSA_Txn_VesselVoyage
                                  join o in db.VSA_Config_Port on q.OriginPortID equals o.PortID
                                  join d in db.VSA_Config_Port on q.DestinationPortID equals d.PortID
                                  where q.VoyageID == ddlVoyageId.SelectedValue
                                  select new
                                  {
                                      q.VoyageID,
                                      OriginportName = o.PortName,
                                      DestinationportName = d.PortName,
                                      q.OriginloadTEU,
                                      q.OriginGrossTonnage,
                                      q.RefrigeratedAllowFlag,
                                      q.HazardousAllowFlag,
                                      q.Isit_Containercargovessel
                                  }).SingleOrDefault();

                    TxtVoyageId.ReadOnly = true;
                    TxtddlOriginport.ReadOnly = true;
                    TxtddlOriginport.Visible = true;
                    
                    TxtddlDestination.ReadOnly = true;
                    TxtddlDestination.Visible = true;
                    
                    TxtContainersTEUs.ReadOnly = true;
                    TxtGrossTonnage.ReadOnly = true;
                    ckContainerCargoVessel.Disabled = true;
                    ckHazardousMaterial.Disabled = true;
                    ckRefrigeratedContainerCargo.Disabled = true;
                    extddlDestination.Visible = true;
                    extddlOriginport.Visible = true;
                    Origin.Visible = false;
                    Destination.Visible = false;

                    TxtVoyageId.Text = vodtls.VoyageID;
                    TxtddlOriginport.Text = vodtls.OriginportName;
                    TxtddlDestination.Text = vodtls.DestinationportName;
                    TxtContainersTEUs.Text = Convert.ToString(vodtls.OriginloadTEU);
                    TxtGrossTonnage.Text = Convert.ToString(vodtls.OriginGrossTonnage);

                    if (vodtls.Isit_Containercargovessel == "Y")
                    {
                        ckContainerCargoVessel.Checked = true;
                        ckContainerCargoVessel.Disabled = true;
                    }
                    else
                    {
                        ckContainerCargoVessel.Checked = false;
                        ckContainerCargoVessel.Disabled = true;
                    }
                    if (vodtls.HazardousAllowFlag == "Y")
                    {
                        ckHazardousMaterial.Checked = true;
                        ckHazardousMaterial.Disabled = true;
                    }
                    else
                    {
                        ckHazardousMaterial.Checked = false;
                        ckHazardousMaterial.Disabled = true;
                    }
                    if (vodtls.RefrigeratedAllowFlag == "Y")
                    {
                        ckRefrigeratedContainerCargo.Checked = true;
                        ckRefrigeratedContainerCargo.Disabled = true;
                    }
                    else
                    {
                        ckRefrigeratedContainerCargo.Checked = false;
                        ckRefrigeratedContainerCargo.Disabled = true;
                    }

                    btncreate.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                    TxtVoyageId.ReadOnly = false;

                    TxtContainersTEUs.ReadOnly = false;
                    TxtGrossTonnage.ReadOnly = false;

                    TxtVoyageId.Text = ""; 
                    TxtddlDestination.Visible = false;
                    TxtddlOriginport.Visible = false;
                    
                    TxtContainersTEUs.Text = "";
                    TxtGrossTonnage.Text = "";

                    btncreate.Visible = true;
                    btnCancel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlshiproutedestinationportid(): Use : To populate dropdown values for the destination port id drop down      

        public void ddlshiproutedestinationportid()
        {
            try
            {
                var db = new VesselAgreement();

                ddldestintnportid.DataSource = db.VSA_Config_Port.ToList().OrderBy(x => x.PortName);
                ddldestintnportid.DataTextField = "PortName";
                ddldestintnportid.DataValueField = "PortID";
                ddldestintnportid.DataBind();
                ddldestintnportid.Items.Insert(0, new ListItem("Select Destination Port Id", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Method ShipRouteDetails(): Use : To create a new ship route for a particular voyage sequence id
        public void ShipRouteDetails()
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
                
                var srd = new VSA_Txn_VesselVoyageTransitShipRoute();
                var oripotid = (from q in db.VSA_Config_Port
                                where q.PortName == TxtOriginportid.Text
                                select new { q.PortID }).SingleOrDefault();


                srd.VoyageSegmentSequenceNumber = Convert.ToInt32(TxtVoyageSequence.Value);
                srd.DestinationTransitPortID = ddldestintnportid.SelectedValue;
                srd.ExpectedDepartureDateTime = Convert.ToDateTime(TxtStartDate.Text + " " + TxtStartTime.Text, new CultureInfo("en-GB"));
                srd.ExpectedArrivalDateTime = Convert.ToDateTime(TxtArrivalDate.Text + " " + TxtArrivalTime.Text, new CultureInfo("en-GB"));
                srd.OriginTransitPortID = oripotid.PortID;
                srd.VoyageID = TxtVoyageId.Text;
                srd.ShipRouteComplete_Flag = "N";
                

                var voyagedestiportid = (from q in db.VSA_Txn_VesselVoyage
                                         where q.VoyageID == TxtVoyageId.Text
                                         select new
                                         {
                                             q.DestinationPortID
                                         }).SingleOrDefault();

                if (srd.OriginTransitPortID != voyagedestiportid.DestinationPortID)
                {
                    if (srd.VoyageSegmentSequenceNumber == 1)
                    {
                        if (Convert.ToDateTime(srd.ExpectedArrivalDateTime) > Convert.ToDateTime(srd.ExpectedDepartureDateTime))
                        {
                            if(srd.OriginTransitPortID != srd.DestinationTransitPortID)
                            { 
                                db.VSA_Txn_VesselVoyageTransitShipRoute.Add(srd);
                                db.SaveChanges();
                                String strAppMsg = ConfigurationManager.AppSettings["VVvoyageTransit"];
                                lblshiproutemsg.Text = strAppMsg;
                                lblshiproutemsg.ForeColor = System.Drawing.Color.ForestGreen;
                            }
                            else
                            {
                                String strAppMsg = ConfigurationManager.AppSettings["VVoridestportsnotsame"];
                                lblshiproutemsg.Text = strAppMsg;
                                lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            String strAppMsg = ConfigurationManager.AppSettings["VVstartDateArrivalDate"];
                            lblshiproutemsg.Text = strAppMsg;
                            lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    if (srd.VoyageSegmentSequenceNumber != 1)
                    {
                        int VoyageSeq = Convert.ToInt32(TxtVoyageSequence.Value);
                        var arrtime = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                       where q.VoyageID == TxtVoyageId.Text
                                       where q.VoyageSegmentSequenceNumber == VoyageSeq - 1
                                       select new { q.ExpectedArrivalDateTime }).SingleOrDefault();
                        if (Convert.ToDateTime(srd.ExpectedDepartureDateTime) > Convert.ToDateTime(arrtime.ExpectedArrivalDateTime))
                        {
                            if (srd.ExpectedArrivalDateTime > srd.ExpectedDepartureDateTime)
                            {
                                if (srd.OriginTransitPortID != srd.DestinationTransitPortID)
                                {
                                    db.VSA_Txn_VesselVoyageTransitShipRoute.Add(srd);
                                    db.SaveChanges();
                                    String strAppMsg = ConfigurationManager.AppSettings["VVvoyageTransit"];
                                    lblshiproutemsg.Text = strAppMsg;
                                    lblshiproutemsg.ForeColor = System.Drawing.Color.ForestGreen;

                                    //below code is done on 16/2/2018.To add last port.

                                    //if (srd.DestinationTransitPortID == voyagedestiportid.DestinationPortID)
                                    //{
                                    //    var lstprtid = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                    //                    join d in db.VSA_Config_Port on q.DestinationTransitPortID equals d.PortID
                                    //                    where q.VoyageID == TxtVoyageId.Text
                                    //                    select new { q.VoyageSegmentSequenceNumber, d.PortName }).OrderByDescending(x => x.VoyageSegmentSequenceNumber).First();
                                    //    TxtOriginportid.Text = lstprtid.PortName;
                                    //    TxtVoyageSequence.Value = Convert.ToString((lstprtid.VoyageSegmentSequenceNumber) + 1);

                                    //    var lastport = new VSA_Txn_VesselVoyageTransitShipRoute();

                                    //    lastport.VoyageSegmentSequenceNumber = Convert.ToInt32(TxtVoyageSequence.Value);
                                    //    lastport.DestinationTransitPortID = srd.DestinationTransitPortID;
                                    //    lastport.ExpectedDepartureDateTime = srd.ExpectedArrivalDateTime;
                                    //    lastport.ExpectedArrivalDateTime = srd.ExpectedArrivalDateTime;
                                    //    lastport.OriginTransitPortID = srd.DestinationTransitPortID;
                                    //    lastport.VoyageID = TxtVoyageId.Text;
                                    //    lastport.ShipRouteComplete_Flag = "N";

                                    //    db.VSA_Txn_VesselVoyageTransitShipRoute.Add(lastport);
                                    //    db.SaveChanges();

                                    //}

                                }
                                else
                                {
                                    String strAppMsg = ConfigurationManager.AppSettings["VVoridestportsnotsame"];
                                    lblshiproutemsg.Text = strAppMsg;
                                    lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                String strAppMsg = ConfigurationManager.AppSettings["VVstartDateArrivalDate"];
                                lblshiproutemsg.Text = strAppMsg;
                                lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            String strAppMsg = ConfigurationManager.AppSettings["VVarrivalDateStartDate"];
                            lblshiproutemsg.Text = strAppMsg;
                            lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else
                {
                    String strAppMsg = ConfigurationManager.AppSettings["VVtransits"];
                    lblshiproutemsg.Text = strAppMsg;
                    lblshiproutemsg.ForeColor = System.Drawing.Color.ForestGreen;
                    
                }
                if (voyagedestiportid.DestinationPortID == ddldestintnportid.SelectedValue)
                {

                    //var transitflag = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                    //                   where q.VoyageID == TxtVoyageId.Text
                    //                   where q.ShipRouteComplete_Flag == "N"
                    //                   select new { });
                    var voyid = db.VSA_Txn_VesselVoyage.Where(x => x.VoyageID == TxtVoyageId.Text && x.Voyageshiproutecompleteflag == "N");
                    foreach (var item in voyid)
                    {
                        item.Voyageshiproutecompleteflag = "Y";
                    }
                    var transitflag = db.VSA_Txn_VesselVoyageTransitShipRoute.Where(x => x.VoyageID == TxtVoyageId.Text && x.ShipRouteComplete_Flag == "N");
                    foreach (var item in transitflag)
                    {
                        item.ShipRouteComplete_Flag = "Y";
                    }
                    db.SaveChanges();
                    
                }
                DbTrans.Commit();
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                throw ex;
            }
            int seqno= Convert.ToInt32(TxtVoyageSequence.Value);
            var seqvalidtn = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                              where q.VoyageID == ddlVoyageId.SelectedValue && q.VoyageSegmentSequenceNumber == seqno
                              select new { }).ToList();
            if(seqvalidtn.Count() !=0)
            { 
            clear();
            }
        }

        // Method btncreate_Click: Use : This is to trigger the click event of the create button
        protected void btncreate_Click(object sender, EventArgs e)
        {
            try
            {
                VoyageDetails();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method deleteshiproute(): Use : This method is delete the already created ship route
        public void deleteshiproute()
        {
            try
            {
                var db = new VesselAgreement();
                var deleteshowtransits = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                          where q.ShipRouteComplete_Flag == "N"
                                          select new
                                          {
                                              q
                                          }).ToList();
                if (deleteshowtransits.Count > 0)
                {
                    var deletetransits = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                          where q.ShipRouteComplete_Flag == "N"
                                          select new { q }).FirstOrDefault();
                    db.VSA_Txn_VesselVoyageTransitShipRoute.Remove(deletetransits.q);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method showvoyage(): Use : To display the already created vessel voyage details
        public void showvoyage()
        {
            try
            {
                var db = new VesselAgreement();
                var show = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                            join d in db.VSA_Config_Port on q.OriginTransitPortID equals d.PortID
                            join p in db.VSA_Config_Port on q.DestinationTransitPortID equals p.PortID
                            where q.VoyageID == TxtVoyageId.Text
                            select new
                            {
                                ExpectedStartDate = q.ExpectedDepartureDateTime,
                                ExpectedStartTime = q.ExpectedDepartureDateTime,
                                ExpectedArrivalDate = q.ExpectedArrivalDateTime,
                                ExpectedArrivalTime = q.ExpectedArrivalDateTime,

                                DestinationTransitPortID = p.PortName,
                                OriginTransitPortID = d.PortName,
                                VoyageSegmentSequenceNumber = q.VoyageSegmentSequenceNumber,

                            }).ToList();


                rpt.DataSource = show;
                rpt.DataBind();

                if (show.Count > 0)
                {
                    var lstprtid = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                    join d in db.VSA_Config_Port on q.DestinationTransitPortID equals d.PortID
                                    where q.VoyageID == TxtVoyageId.Text
                                    select new { q.VoyageSegmentSequenceNumber, d.PortName }).OrderByDescending(x => x.VoyageSegmentSequenceNumber).First();
                    TxtOriginportid.Text = lstprtid.PortName;
                    TxtVoyageSequence.Value = Convert.ToString((lstprtid.VoyageSegmentSequenceNumber) + 1);
                }
                else
                {

                    TxtVoyageSequence.Value = "1";

                }
                
                if (TxtddlDestination.Text == ddldestintnportid.SelectedItem.Text)
                {
                    insrtrow.Visible = false;
                    String strAppMsg = ConfigurationManager.AppSettings["VVtransits"];
                    lblshiproutemsg.Text = strAppMsg;
                    lblshiproutemsg.ForeColor = System.Drawing.Color.ForestGreen;
                }
                if (ddlDestination.SelectedValue != "0")
                {
                    if (ddlDestination.SelectedValue == ddldestintnportid.SelectedValue)
                    {
                        insrtrow.Visible = false;
                        String strAppMsg = ConfigurationManager.AppSettings["VVtransits"];
                        lblshiproutemsg.Text = strAppMsg;
                        lblshiproutemsg.ForeColor = System.Drawing.Color.ForestGreen;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method clear(): Use : To clear all the values to default
        public void clear()
        {
            try
            {
                TxtStartDate.Text = "";
                TxtStartTime.Text = "";
                TxtArrivalDate.Text = "";
                TxtArrivalTime.Text = "";
                TxtVoyageSequence.Value = "";
                
                if (IsPostBack)
                {
                    showvoyage();
                }
                ddlshiproutedestinationportid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method cancel: Use : To clear the values based on cancel button click event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                TxtVoyageId.Text = "";
                TxtGrossTonnage.Text = "";
                TxtContainersTEUs.Text = "";
                ddlOriginPortID();
                ddldestinationportid();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method btninsert_Click Use : To trigger the insert button click event to create a new ship route
        protected void btninsert_Click(object sender, EventArgs e)
        {
            try
            {
                extddlOriginport.Visible = true;
                extddlDestination.Visible = true;
                Origin.Visible = false;
                Destination.Visible = false;
                ShipRouteDetails();
                
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method btnedit_Click Use : To trigger the edit button click event to update existing ship route
        protected void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                var db = new VesselAgreement();
                int id = int.Parse((sender as LinkButton).CommandArgument);
                Origin.Visible = false;
                Destination.Visible = false;
                extddlOriginport.Visible = true;
                extddlDestination.Visible = true;

                var lastvoqseq = db.VSA_Txn_VesselVoyageTransitShipRoute.Where(x => x.VoyageID == ddlVoyageId.SelectedValue).OrderByDescending(x => x.VoyageSegmentSequenceNumber).First();
                int k = lastvoqseq.VoyageSegmentSequenceNumber;
                RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
                if(lastvoqseq.VoyageSegmentSequenceNumber != id)
                { 
                this.ToggleElements(item, true);
                }
                else
                {
                    this.ToggleElementsddl(item, true);
                }

                var show = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                            join d in db.VSA_Config_Port on q.OriginTransitPortID equals d.PortID
                            join p in db.VSA_Config_Port on q.DestinationTransitPortID equals p.PortID
                            where q.VoyageID == ddlVoyageId.SelectedValue
                            where q.VoyageSegmentSequenceNumber == id
                            select new
                            {
                                DestinationTransitPortID = p.PortID,
                            }).SingleOrDefault();
                string vo = show.DestinationTransitPortID;
                for (int i = 0; i < rpt.Items.Count; i++)
                {
                    int a = id - 1;
                    if (i ==a )
                    {
                        DropDownList rptddldestintnportid = (DropDownList)rpt.Items[i].FindControl("rptddldestintnportid");
                        rptddldestintnportid.SelectedValue = vo;
                       
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method ToggleElements Use : To make the elements within the grid editable/non editable
        private void ToggleElements(RepeaterItem item, bool isEdit)
        {
            try
            {
                item.FindControl("lnkedit").Visible = !isEdit;
                item.FindControl("lnkupdate").Visible = isEdit;
                item.FindControl("lnkecancel").Visible = isEdit;
                


                //Toggle Labels.
                item.FindControl("lblVoyageSegmentSequenceNum").Visible = !isEdit;
                item.FindControl("lblOriginTransitPortID").Visible = !isEdit;
                item.FindControl("lblExpectedStartDate").Visible = !isEdit;
                item.FindControl("lblExpectedStartTime").Visible = !isEdit;
                item.FindControl("lblDestinationTransitPortID").Visible = !isEdit;
                item.FindControl("lblExpectedArrivalDate").Visible = !isEdit;
                item.FindControl("lblExpectedArrivalTime").Visible = !isEdit;


                //Toggle TextBoxes.
                item.FindControl("edtVoyageSegmentSequenceNum").Visible = isEdit;
                item.FindControl("edtOriginTransitPortID").Visible = isEdit;
                item.FindControl("edtExpectedStartDate").Visible = isEdit;
                item.FindControl("edtExpectedStartTime").Visible = isEdit;
                item.FindControl("rptddldestintnportid").Visible = isEdit;
                item.FindControl("edtExpectedArrivalDate").Visible = isEdit;
                item.FindControl("edtExpectedArrivalTime").Visible = isEdit;

            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method ToggleElementsddl Use : To make the dropdown elements within the grid editable/non editable
        private void ToggleElementsddl(RepeaterItem item, bool isEdit)
        {
            try
            {
                item.FindControl("lnkedit").Visible = !isEdit;
                item.FindControl("lnkupdate").Visible = isEdit;
                item.FindControl("lnkecancel").Visible = isEdit;
                //item.FindControl("lnkDelete").Visible = !isEdit;


                //Toggle Labels.
                item.FindControl("lblVoyageSegmentSequenceNum").Visible = !isEdit;
                item.FindControl("lblOriginTransitPortID").Visible = !isEdit;
                item.FindControl("lblExpectedStartDate").Visible = !isEdit;
                item.FindControl("lblExpectedStartTime").Visible = !isEdit;
                item.FindControl("lblDestinationTransitPortID").Visible = !isEdit;
                item.FindControl("lblExpectedArrivalDate").Visible = !isEdit;
                item.FindControl("lblExpectedArrivalTime").Visible = !isEdit;


                //Toggle TextBoxes.
                item.FindControl("edtVoyageSegmentSequenceNum").Visible = isEdit;
                item.FindControl("edtOriginTransitPortID").Visible = isEdit;
                item.FindControl("edtExpectedStartDate").Visible = isEdit;
                item.FindControl("edtExpectedStartTime").Visible = isEdit;
                item.FindControl("rptddldestintnportid").Visible = isEdit;
                item.FindControl("edtExpectedArrivalDate").Visible = isEdit;
                item.FindControl("edtExpectedArrivalTime").Visible = isEdit;
               DropDownList d=(DropDownList)item.FindControl("rptddldestintnportid");
                d.Enabled = false;
                d.CssClass = "form-control";
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }


        // Method rpt_ItemDataBound Use : To populate port ids in the destination port dropdown with in the item grid
        protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DropDownList selectList = e.Item.FindControl("rptddldestintnportid") as DropDownList;
                if (selectList != null)
                {
                    var db = new VesselAgreement();

                    selectList.DataSource = db.VSA_Config_Port.ToList(); //your datasource
                    selectList.DataTextField = "PortName";
                    selectList.DataValueField = "PortID";
                    selectList.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method btnupdate_Click Use : To update the details for a vessel voyage row using the button update click event
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
                
                RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
                int id = int.Parse((sender as LinkButton).CommandArgument);

                string VoyageSegmentSequenceNum = (item.FindControl("edtVoyageSegmentSequenceNum") as TextBox).Text.Trim();
                string OriginTransitPortID = (item.FindControl("edtOriginTransitPortID") as TextBox).Text.Trim();
                string ExpectedStartDate = (item.FindControl("edtExpectedStartDate") as TextBox).Text.Trim();
                string ExpectedStartTime = (item.FindControl("edtExpectedStartTime") as TextBox).Text.Trim();
                string DestinationTransitPortID = (item.FindControl("rptddldestintnportid") as DropDownList).SelectedValue.Trim();
                string ExpectedArrivalDate = (item.FindControl("edtExpectedArrivalDate") as TextBox).Text.Trim();
                string ExpectedArrivalTime = (item.FindControl("edtExpectedArrivalTime") as TextBox).Text.Trim();
                var updt = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                            where q.VoyageID == ddlVoyageId.SelectedValue
                            where q.VoyageSegmentSequenceNumber == id
                            select new
                            {
                                q
                            }).SingleOrDefault();

                var orgin = (from q in db.VSA_Config_Port
                             where q.PortName == OriginTransitPortID
                             select new { q.PortID }).SingleOrDefault();

                updt.q.VoyageSegmentSequenceNumber = Convert.ToInt32(VoyageSegmentSequenceNum);
                updt.q.ExpectedDepartureDateTime = DateTime.Parse(ExpectedStartDate + " " + ExpectedStartTime, new CultureInfo("en-GB"));
                updt.q.ExpectedArrivalDateTime = DateTime.Parse(ExpectedArrivalDate + " " + ExpectedArrivalTime, new CultureInfo("en-GB"));
                updt.q.OriginTransitPortID = orgin.PortID;

                var lastvoqseq = db.VSA_Txn_VesselVoyageTransitShipRoute.Where(x => x.VoyageID == ddlVoyageId.SelectedValue).OrderByDescending(x => x.VoyageSegmentSequenceNumber).First();
                if(lastvoqseq.VoyageSegmentSequenceNumber != id)
                { 
                updt.q.DestinationTransitPortID = DestinationTransitPortID;
                }
                
                
                updt.q.ShipRouteComplete_Flag = "Y";

                var voyagedestiportid = (from q in db.VSA_Txn_VesselVoyage
                                         where q.VoyageID == ddlVoyageId.SelectedValue
                                         select new
                                         {
                                             q.DestinationPortID
                                         }).SingleOrDefault();

                if (updt.q.OriginTransitPortID != voyagedestiportid.DestinationPortID)
                {
                    if (updt.q.VoyageSegmentSequenceNumber == 1)
                    {
                        if (Convert.ToDateTime(updt.q.ExpectedArrivalDateTime) > Convert.ToDateTime(updt.q.ExpectedDepartureDateTime))
                        {

                            var arrtime = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                           where q.VoyageID == TxtVoyageId.Text
                                           where q.VoyageSegmentSequenceNumber == updt.q.VoyageSegmentSequenceNumber + 1
                                           select new { q.ExpectedDepartureDateTime }).SingleOrDefault();

                            if (Convert.ToDateTime(updt.q.ExpectedArrivalDateTime) < arrtime.ExpectedDepartureDateTime)
                            {
                                if(updt.q.OriginTransitPortID != updt.q.DestinationTransitPortID)
                                { 
                                    db.SaveChanges();
                                    DbTrans.Commit();
                                    String strAppMsg = ConfigurationManager.AppSettings["VVtransitUpdated"];
                                    lblshiproutemsg.Text = strAppMsg;
                                    lblshiproutemsg.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    String strAppMsg = ConfigurationManager.AppSettings["VVoridestportsnotsame"];
                                    lblshiproutemsg.Text = strAppMsg;
                                    lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                String strAppMsg = ConfigurationManager.AppSettings["VVarrivalLessStart"];
                                lblshiproutemsg.Text = strAppMsg;
                                lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            String strAppMsg = ConfigurationManager.AppSettings["VVstartDateArrivalDate"];
                            lblshiproutemsg.Text = strAppMsg;
                            lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                            
                        }
                    }
                    if (updt.q.VoyageSegmentSequenceNumber != 1)
                    {
                        int VoyageSeq = updt.q.VoyageSegmentSequenceNumber;
                        var arrtime = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                       where q.VoyageID == ddlVoyageId.SelectedValue
                                       where q.VoyageSegmentSequenceNumber == VoyageSeq - 1
                                       select new { q.ExpectedArrivalDateTime }).SingleOrDefault();

                        if (Convert.ToDateTime(updt.q.ExpectedDepartureDateTime) > Convert.ToDateTime(arrtime.ExpectedArrivalDateTime))
                        {
                            if (updt.q.ExpectedArrivalDateTime > updt.q.ExpectedDepartureDateTime)
                            {
                                if (updt.q.OriginTransitPortID != updt.q.DestinationTransitPortID)
                                {
                                    db.SaveChanges();
                                    DbTrans.Commit();
                                    String strAppMsg = ConfigurationManager.AppSettings["VVtransitUpdated"];
                                    lblshiproutemsg.Text = strAppMsg;
                                    lblshiproutemsg.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    String strAppMsg = ConfigurationManager.AppSettings["VVoridestportsnotsame"];
                                    lblshiproutemsg.Text = strAppMsg;
                                    lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                String strAppMsg = ConfigurationManager.AppSettings["VVstartDateArrivalDate"];
                                lblshiproutemsg.Text = strAppMsg;
                                lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            String strAppMsg = ConfigurationManager.AppSettings["VVpreviousArrivalDate"];
                            lblshiproutemsg.Text = strAppMsg;
                            lblshiproutemsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    var maxcount = db.VSA_Txn_VesselVoyageTransitShipRoute.Where(x => x.VoyageID == ddlVoyageId.SelectedValue).ToList();
                    var updtnext = db.VSA_Txn_VesselVoyageTransitShipRoute.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == id + 1);
                    if (updt.q.OriginTransitPortID != updt.q.DestinationTransitPortID)
                    {
                        if (id < maxcount.Count)
                        {
                            foreach (var h in updtnext)
                            {
                                h.OriginTransitPortID = DestinationTransitPortID;
                            }
                            db.SaveChanges();
                            DbTrans.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            showvoyagetransits();
            showVoyagedtls();
        }

        // Method btnecancel_Click Use : To reset the values to default upon trigger of Cancel button
        protected void btnecancel_Click(object sender, EventArgs e)
        {
            try
            {
                //Find the reference of the Repeater Item.
                Origin.Visible = false;
                Destination.Visible = false;
                extddlOriginport.Visible = true;
                extddlDestination.Visible = true;
                RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
                this.ToggleElements(item, false);
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        //Method ddlVesselId_SelectedIndexChanged Use : On Change of vessel id in the drop down corresponding vessel details need to be displayed 

        protected void ddlVesselId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlvoyage();
                showvessel();
                showVoyagedtls();
                showvoyage();
                TxtVoyageId.ReadOnly = false;
                TxtContainersTEUs.ReadOnly = false;
                TxtGrossTonnage.ReadOnly = false;

                TxtVoyageId.Text = "";
                TxtddlDestination.Visible = false;
                TxtddlOriginport.Visible = false;
                ddlDestination.Visible = true;
                ddlOrigin.Visible = true;
                TxtContainersTEUs.Text = "";
                TxtGrossTonnage.Text = "";
                ddlOriginPortID();
                ddldestinationportid();
                ddlOrigin.Enabled = true;
                ddlOrigin.CssClass = "form-control input-lg";
                ddlDestination.Enabled = true;
                ddlDestination.CssClass = "form-control input-lg";

                btncreate.Visible = true;
                btnCancel.Visible = true;

                insrtrow.Visible = false;
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }

        }

        // Method ddlVoyageId_SelectedIndexChanged Use : On Change of voyage id in the voyageid dropdown the relavent voyage details need to be displayed 
        protected void ddlVoyageId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                showVoyagedtls();
                showvoyagetransits();
                lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method showvoyagetransits Use : Method to display different voyagetransits
        public void showvoyagetransits()
        {
            try
            {
                var db = new VesselAgreement();

                var show = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                            join d in db.VSA_Config_Port on q.OriginTransitPortID equals d.PortID
                            join p in db.VSA_Config_Port on q.DestinationTransitPortID equals p.PortID
                            where q.VoyageID == ddlVoyageId.SelectedValue
                            select new
                            {
                                ExpectedStartDate = q.ExpectedDepartureDateTime,
                                ExpectedStartTime = q.ExpectedDepartureDateTime,
                                ExpectedArrivalDate = q.ExpectedArrivalDateTime,
                                ExpectedArrivalTime = q.ExpectedArrivalDateTime,
                                ColumnName="",
                                DestinationTransitPortID = p.PortName,
                                OriginTransitPortID = d.PortName,
                                VoyageSegmentSequenceNumber = q.VoyageSegmentSequenceNumber,

                            }).ToList();


                rpt.DataSource = show;
                rpt.DataBind();

                if(show.Count == 0)
                {
                    insrtrow.Visible = true;
                    TxtVoyageSequence.Visible = true;
                    TxtOriginportid.Visible = true;
                    TxtStartTime.Visible = true;
                    TxtStartDate.Visible = true;
                    TxtArrivalDate.Visible = true;
                    TxtArrivalTime.Visible = true;
                    ddldestintnportid.Visible = true;
                }
                else
                {
                    insrtrow.Visible = false;
                    TxtVoyageSequence.Visible = false;
                    TxtOriginportid.Visible = false;
                    TxtStartTime.Visible = false;
                    TxtStartDate.Visible = false;
                    TxtArrivalDate.Visible = false;
                    TxtArrivalTime.Visible = false;
                    ddldestintnportid.Visible = false;
                }

                if (show.Count > 0)
                {
                    var lstprtid = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                    join d in db.VSA_Config_Port on q.DestinationTransitPortID equals d.PortID
                                    where q.VoyageID == TxtVoyageId.Text
                                    select new { q.VoyageSegmentSequenceNumber, d.PortName }).OrderByDescending(x => x.VoyageSegmentSequenceNumber).First();
                    

                    if (lstprtid.PortName != TxtddlDestination.Text)
                    {
                        insrtrow.Visible = true;
                        TxtVoyageSequence.Visible = true;
                        TxtOriginportid.Visible = true;
                        TxtStartTime.Visible = true;
                        TxtStartDate.Visible = true;
                        TxtArrivalDate.Visible = true;
                        TxtArrivalTime.Visible = true;
                        ddldestintnportid.Visible = true;

                        TxtVoyageSequence.Value = Convert.ToString(lstprtid.VoyageSegmentSequenceNumber + 1);
                        TxtOriginportid.Text = lstprtid.PortName;
                    }
                }
                else
                {
                    TxtVoyageSequence.Value = "1";
                    TxtOriginportid.Text = TxtddlOriginport.Text;
                }
                if (ddlVoyageId.SelectedValue == "0")
                {
                    insrtrow.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}