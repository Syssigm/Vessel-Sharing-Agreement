using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public partial class ReviewAndApproved : System.Web.UI.Page
    {
        string custid;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblmsg.Text = string.Empty;
            lblreviewmsg.Text = string.Empty;
            custid = Convert.ToString(Session["CustomerID"]);
            if (!IsPostBack)
            {
                var db = new VesselAgreement();


                var ifcustomer = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerID == custid
                                  where q.CustomerTypeID == "VSLOPR"
                                  select new { q.CustomerTypeID }).SingleOrDefault();
                if (ifcustomer == null)
                {
                    ddlParticipantRoleRow.Visible = false;
                    Company.Visible = false;
                    btnApprove.Visible = false;
                    Btncancel.Visible = false;
                    tblDetails.Visible = false;
                    Chages.Visible = true;
                    ddlvessel();
                }
                else
                {
                    ddlParticipantRoleRow.Visible = true;
                    Company.Visible = true;
                    ddlvesselid.Items.Insert(0, new ListItem("Select Vessel", "0"));
                }
                ddlvoyageid.Items.Insert(0, new ListItem("Select Voyage Id", "0"));
                ddlcustomerid.Items.Insert(0, new ListItem("Select Company Name", "0"));
            }
        }

        // Method ddlParticipantRole_SelectedIndexChanged: Use : Populate vessel ids in the vessel id dropdown based on customer role
        protected void ddlParticipantRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlvessel();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method ddlvessel(): Use : To Populate all the vessel ids in dropdown for the vessel operator type VSLOPR
        public void ddlvessel()
        {
            try
            {
                string custid = Convert.ToString(Session["CustomerID"]);
                var db = new VesselAgreement();

                var ifcustomer = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerID == custid
                                  where q.CustomerTypeID == "VSLOPR"
                                  select new { q.CustomerTypeID }).SingleOrDefault();
                if (ifcustomer == null)
                {


                    var reviewvesselcust = (from q in db.VSA_Txn_Participant_Application
                                            where q.VSAParticipantCustomerID == custid
                                            join p in db.VSA_Master_Customer_and_CustomerTypes on q.VSAParticipantCustomerID equals p.CustomerID
                                            where (p.CustomerTypeID == "CRGOOP" || p.CustomerTypeID == "AGENTS" || p.CustomerTypeID == "SSLINE")
                                            join s in db.VSA_Txn_VesselVoyage on q.VoyageID equals s.VoyageID
                                            join r in db.VSA_Master_Vessel on s.VesselID equals r.VesselID
                                            select new
                                            {
                                                vesselidname = r.VesselID + "-" + r.NameoftheVessel,
                                                vesselid = r.VesselID,
                                            }).ToList().Distinct();

                    ddlvesselid.DataSource = reviewvesselcust.OrderBy(x => x.vesselidname);
                    ddlvesselid.DataTextField = "vesselidname";
                    ddlvesselid.DataValueField = "vesselid";
                    ddlvesselid.DataBind();
                    ddlvesselid.Items.Insert(0, new ListItem("--Select Vessel--", "0"));
                    ddlvoyage();
                }
                else
                {
                    if (ddlParticipantRole.SelectedIndex == 1)
                    {
                        var reviewvesseloperator = (from q in db.VSA_Txn_Participant_Application
                                                    join p in db.VSA_Txn_VesselVoyage on q.VoyageID equals p.VoyageID
                                                    join r in db.VSA_Master_Vessel on p.VesselID equals r.VesselID
                                                    where r.VesselOperatorId == custid
                                                    join s in db.VSA_Master_Customer_and_CustomerTypes on r.VesselOperatorId equals s.CustomerID
                                                    where s.CustomerTypeID == "VSLOPR"
                                                    select new
                                                    {
                                                        vesselidname = r.VesselID + "-" + r.NameoftheVessel,
                                                        vesselid = r.VesselID,
                                                    }).ToList().Distinct();

                        ddlvesselid.DataSource = reviewvesseloperator.OrderBy(x => x.vesselidname);
                        ddlvesselid.DataTextField = "vesselidname";
                        ddlvesselid.DataValueField = "vesselid";
                        ddlvesselid.DataBind();
                        ddlvesselid.Items.Insert(0, new ListItem("--Select Vessel--", "0"));
                        ddlvoyage();

                        appstatus.Visible = false;
                    }
                    if (ddlParticipantRole.SelectedIndex == 2)
                    {
                        var reviewvesselcust = (from q in db.VSA_Txn_Participant_Application
                                                where q.VSAParticipantCustomerID == custid
                                                join p in db.VSA_Master_Customer_and_CustomerTypes on q.VSAParticipantCustomerID equals p.CustomerID
                                                where (p.CustomerTypeID == "CRGOOP" || p.CustomerTypeID == "AGENTS" || p.CustomerTypeID == "SSLINE" || p.CustomerTypeID == "VSLOPR")
                                                join s in db.VSA_Txn_VesselVoyage on q.VoyageID equals s.VoyageID
                                                join r in db.VSA_Master_Vessel on s.VesselID equals r.VesselID
                                                select new
                                                {
                                                    vesselidname = r.VesselID + "-" + r.NameoftheVessel,
                                                    vesselid = r.VesselID,
                                                }).ToList().Distinct();



                        ddlvesselid.DataSource = reviewvesselcust.OrderBy(x => x.vesselidname);
                        ddlvesselid.DataTextField = "vesselidname";
                        ddlvesselid.DataValueField = "vesselid";
                        ddlvesselid.DataBind();
                        ddlvesselid.Items.Insert(0, new ListItem("--Select Vessel--", "0"));
                        ddlvoyage();

                        Company.Visible = false;
                    }
                }
                ddlvesselid.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlvoyage(): Use : To Populate the voyage list in dropdown based on Vesselid Selection
        public void ddlvoyage()
        {
            try
            {
                var db = new VesselAgreement();

                var ifcustomer = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerID == custid
                                  where q.CustomerTypeID == "VSLOPR"
                                  select new { q.CustomerTypeID }).SingleOrDefault();

                if (ifcustomer != null)
                {
                    var voyid = (from q in db.VSA_Txn_Participant_Application
                                 join p in db.VSA_Txn_VesselVoyage on q.VoyageID equals p.VoyageID
                                 where p.VesselID == ddlvesselid.SelectedValue
                                 select new { q.VoyageID }).ToList();

                    ddlvoyageid.DataSource = voyid.GroupBy(x => x.VoyageID, (key, group) => group.FirstOrDefault()).ToArray().OrderBy(x => x.VoyageID);
                    ddlvoyageid.DataTextField = "VoyageID";
                    ddlvoyageid.DataValueField = "VoyageID";
                    ddlvoyageid.DataBind();
                    ddlvoyageid.Items.Insert(0, new ListItem("Select Voyage Id", "0"));
                }
                else
                {
                    var voyid = (from q in db.VSA_Txn_Participant_Application
                                 join p in db.VSA_Txn_VesselVoyage on q.VoyageID equals p.VoyageID
                                 where p.VesselID == ddlvesselid.SelectedValue
                                 where q.VSAParticipantCustomerID == custid
                                 select new { q.VoyageID }).ToList();

                    ddlvoyageid.DataSource = voyid.GroupBy(x => x.VoyageID, (key, group) => group.FirstOrDefault()).ToArray().OrderBy(x => x.VoyageID);
                    ddlvoyageid.DataTextField = "VoyageID";
                    ddlvoyageid.DataValueField = "VoyageID";
                    ddlvoyageid.DataBind();
                    ddlvoyageid.Items.Insert(0, new ListItem("Select Voyage Id", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Customerid()
        {
            try
            {
                var db = new VesselAgreement();

                var opatorname = (from q in db.VSA_Txn_Participant_Application
                                  join p in db.VSA_Master_Customer on q.VSAParticipantCustomerID equals p.CustomerID
                                  where q.VoyageID == ddlvoyageid.SelectedItem.Text
                                  select new { p.CompanyName, p.CustomerID }).ToList().Distinct();
                ddlcustomerid.DataSource = opatorname;
                ddlcustomerid.DataTextField = "CompanyName";
                ddlcustomerid.DataValueField = "CustomerID";
                ddlcustomerid.DataBind();
                ddlcustomerid.Items.Insert(0, new ListItem("Select Company Name", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Method showVesselshare(): Use : To populate the voyage details based on based on voyage selection
        public void showVesselshare()
        {
            try
            {
                var db = new VesselAgreement();
                if (ddlvoyageid.SelectedValue != "0")
                {
                    var vvd = (from q in db.VSA_Txn_VesselVoyage
                               join p in db.VSA_Master_Vessel on q.VesselID equals p.VesselID
                               join r in db.VSA_Config_Port on q.OriginPortID equals r.PortID
                               join s in db.VSA_Config_Port on q.DestinationPortID equals s.PortID
                               where q.VoyageID == ddlvoyageid.SelectedValue
                               select new
                               {
                                   p.NameoftheVessel,
                                   p.VesselCapacityTEU,
                                   r.PortName,
                                   destiport = s.PortName,
                                   q.OriginloadTEU,
                                   q.OriginGrossTonnage,

                               }).SingleOrDefault();
                    TxtVesselCapacity.Text = Convert.ToString(vvd.VesselCapacityTEU);
                    TxtDestinationPort.Text = vvd.destiport;
                    TxtOriginPort.Text = vvd.PortName;
                    TxtVesselName.Text = vvd.NameoftheVessel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlvesselid_SelectedIndexChanged(): Use : To populate the voyage details based on vesselid selection change
        protected void ddlvesselid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlvoyage();
                ddlAppStatus.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method ddlvoyageid_SelectedIndexChanged: Use : To populate the vessel voyage details based on voyage id selection
        protected void ddlvoyageid_SelectedIndexChanged(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            string custid = Convert.ToString(Session["CustomerID"]);

            var ctypeVSLOPR = (from q in db.VSA_Master_Customer_and_CustomerTypes
                               where q.CustomerID == custid
                               select new { CustomerType = q.CustomerTypeID }).Where(x => x.CustomerType == "VSLOPR").ToList();



            if (ctypeVSLOPR.Count != 0)
            {
                var vesselsvoyage = (from q in db.VSA_Master_Vessel
                                     where q.VesselID == ddlvesselid.SelectedValue.Trim()
                                     where q.VesselOperatorId == custid
                                     select new { }).ToList();
                if (vesselsvoyage.Count != 0)
                {
                    if (ddlParticipantRole.SelectedIndex == 1)
                    {
                        Customerid();
                        showVesselshare();
                        appstatus.Visible = false;
                    }
                    else
                    {
                        appstatus.Visible = true;
                        showVesselshare();
                    }
                }
                else
                {
                    appstatus.Visible = true;
                    showVesselshare();
                }
            }
            else
            {
                appstatus.Visible = true;
                showVesselshare();
            }
            ddlAppStatus.SelectedIndex = 0;
        }



        // Method showReviewdtls(): Use : To display already reviewed VSA details
        public void showReviewdtls()
        {
            var db = new VesselAgreement();
            try
            {
                var allowable = (from a in db.VSA_Txn_Participant_Review_and_Allowable
                                 join q in db.VSA_Txn_Participant_Application on a.VoyageID equals q.VoyageID
                                 join c in db.VSA_Master_Customer on a.VSAParticipantCustomerID equals c.CustomerID
                                 join p in db.VSA_Txn_Invite on a.VoyageID equals p.VoyageID
                                 join u in db.VSA_Config_Port on a.OriginTransitPortID equals u.PortID
                                 join s in db.VSA_Txn_VesselVoyageTransitShipRoute on a.VoyageID equals s.VoyageID
                                 join t in db.VSA_Config_Port on s.DestinationTransitPortID equals t.PortID
                                 where a.VoyageID == ddlvoyageid.SelectedValue
                                 where a.VSAParticipantCustomerID == ddlcustomerid.SelectedValue
                                 where q.VoyageSegmentSequencenumber == a.VoyageSegmentSequenceNumber
                                 where q.VSAParticipantCustomerID == ddlcustomerid.SelectedValue
                                 where s.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber
                                 where p.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber

                                 select new
                                 {


                                     VoyageID = q.VoyageID,
                                     VSAParticipantCustomerID = q.VSAParticipantCustomerID,
                                     VoyageSegmentSequencenumber = q.VoyageSegmentSequencenumber,
                                     shiporignport = u.PortName,
                                     DestinationPortID = t.PortName,
                                     InviteVSAParticipantsFlag = p.InviteVSAParticipantsFlag,
                                     CargoOperatorAgentName = c.CompanyName,
                                     VSAnotes = a.VSANotes,
                                     InitialAvailableSpaceTEU = p.AvailableSpaceTEU,
                                     lblNetTEUsApp = a.NetTEUSAllowedforVSAParticipantforVoyageSegment,
                                     AvailableSpaceTEU = (p.AvailableSpaceTEU - (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment))) != null ? (p.AvailableSpaceTEU - (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment))) : p.AvailableSpaceTEU,//== null ? (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment)) : p.AvailableSpaceTEU), // : p.AvailableSpaceTEU),
                                     lblload40inch = q.Load_40Feet_Containers_Apply,
                                     lblload20inch = q.Load_20Feet_Containers_Apply,
                                     lbllblTEUsLoaded = q.LoadTEU_Apply,
                                     lblAccepted40Load = a.Load_40Feet_Containers_AllowableforCargoOperator,
                                     lblAccepted20Load = a.Load_20Feet_Containers_AllowableforCargoOperator,
                                     lblAllowLoad = a.LoadTEU_AllowableforCargoOperator,
                                     lbldis40inch = q.Discharge_40Feet_Containers_Apply,
                                     lbldis20inch = q.Discharge_20Feet_Containers_Apply,
                                     lbllblTEUsDischarged = q.DischargeTEU_Apply,
                                     lblAccepted40Disch = a.Discharge_40Feet_Containers_AllowableforCargoOperator,
                                     lblAccepted20Disch = a.Discharge_20Feet_Containers_AllowableforCargoOperator,
                                     lblAllowDisch = a.DischargeTEU_AllowableforCargoOperator,
                                     NetTEUSowned = q.NetTEUSownedByVSAParticipantforVoyageSegment,
                                     lblAppStatus = a.Application_Status,
                                     ApplyVSANotes = a.VSANotes,
                                     lblload = "",
                                     txt40load = "",
                                     txt20load = "",
                                 }).ToList();




                if (allowable.Count == 0)
                {
                    var ApprovedoragreedVSA = (from q in db.VSA_Txn_Participant_Application
                                               join c in db.VSA_Master_Customer on q.VSAParticipantCustomerID equals c.CustomerID
                                               join p in db.VSA_Txn_Invite on q.VoyageID equals p.VoyageID
                                               join u in db.VSA_Config_Port on q.OriginTransitDestinationPortID equals u.PortID
                                               join s in db.VSA_Txn_VesselVoyageTransitShipRoute on q.VoyageID equals s.VoyageID
                                               join t in db.VSA_Config_Port on s.DestinationTransitPortID equals t.PortID
                                               where q.VoyageID == ddlvoyageid.SelectedValue
                                               where s.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber
                                               where p.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber
                                               select new
                                               {
                                                   VoyageSegmentSequencenumber = q.VoyageSegmentSequencenumber,
                                                   shiporignport = u.PortName,
                                                   DestinationPortID = t.PortName,
                                                   InviteVSAParticipantsFlag = p.InviteVSAParticipantsFlag,
                                                   customerid = q.VSAParticipantCustomerID,
                                                   VoyageID = q.VoyageID,
                                                   CargoOperatorAgentName = c.CompanyName,
                                                   InitialAvailableSpaceTEU = p.AvailableSpaceTEU,
                                                   AvailableSpaceTEU = (p.AvailableSpaceTEU - (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment))) != null ? (p.AvailableSpaceTEU - (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment))): p.AvailableSpaceTEU,
                                                   lblload40inch = q.Load_40Feet_Containers_Apply,
                                                   lblload20inch = q.Load_20Feet_Containers_Apply,
                                                   lbllblTEUsLoaded = q.LoadTEU_Apply,
                                                   lbldis40inch = q.Discharge_40Feet_Containers_Apply,
                                                   lbldis20inch = q.Discharge_20Feet_Containers_Apply,
                                                   lbllblTEUsDischarged = q.DischargeTEU_Apply,
                                                   NetTEUSowned = q.NetTEUSownedByVSAParticipantforVoyageSegment,
                                                   lblAccepted40Load = "",
                                                   lblAccepted20Load = "",
                                                   lblAllowLoad = "",
                                                   lblAccepted40Disch = "",
                                                   lblAccepted20Disch = "",
                                                   lblAllowDisch = "",
                                                   lblNetTEUsApp = "",
                                                   lblload = "",
                                                   txt40load = "",
                                                   txt20load = "",
                                                   ApplyVSANotes = q.VSANotes,
                                                   VSAnotes = "",
                                                   lblAppStatus = q.Application_Status,

                                               }).ToList();


                    var notapproved = (from q in ApprovedoragreedVSA
                                       where q.CargoOperatorAgentName == ddlcustomerid.SelectedItem.Text
                                       select new
                                       {
                                           VoyageSegmentSequencenumber = q.VoyageSegmentSequencenumber,
                                           shiporignport = q.shiporignport,
                                           DestinationPortID = q.DestinationPortID,
                                           InviteVSAParticipantsFlag = q.InviteVSAParticipantsFlag,
                                           CargoOperatorAgentName = q.CargoOperatorAgentName,
                                           InitialAvailableSpaceTEU = q.InitialAvailableSpaceTEU,
                                           AvailableSpaceTEU = q.AvailableSpaceTEU,
                                           lblload40inch = q.lblload40inch,
                                           lblload20inch = q.lblload20inch,
                                           lbllblTEUsLoaded = q.lbllblTEUsLoaded,
                                           lbldis40inch = q.lbldis40inch,
                                           lbldis20inch = q.lbldis20inch,
                                           lbllblTEUsDischarged = q.lbllblTEUsDischarged,
                                           NetTEUSowned = q.NetTEUSowned,
                                           lblAccepted40Load = "",
                                           lblAccepted20Load = "",
                                           lblAllowLoad = "",
                                           lblAccepted40Disch = "",
                                           lblAccepted20Disch = "",
                                           lblAllowDisch = "",
                                           lblNetTEUsApp = "",
                                           lblload = "",
                                           txt40load = "",
                                           txt20load = "",
                                           ApplyVSANotes = q.ApplyVSANotes,
                                           VSAnotes = "",
                                           lblAppStatus = q.lblAppStatus,
                                       }).OrderBy(x => x.CargoOperatorAgentName).ToList();

                    VoyageInvitation.DataSource = notapproved;
                    VoyageInvitation.DataBind();

                    Teus.DataSource = notapproved;
                    Teus.DataBind();

                    ApprovedSummary.DataSource = notapproved;
                    ApprovedSummary.DataBind();

                    btnApprove.Visible = true;
                    Btncancel.Visible = true;
                    tblDetails.Visible = true;
                }
                else
                {
                    var DisagreeStatus = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                          where q.VoyageID == ddlvoyageid.SelectedValue
                                          where q.VSAParticipantCustomerID == ddlcustomerid.SelectedValue
                                          where q.Application_Status == "DisAgreed"
                                          select new { }).Count();
                    if (DisagreeStatus == 0)
                    {
                        VoyageInvitation.DataSource = allowable;
                        VoyageInvitation.DataBind();

                        Teus.DataSource = allowable;
                        Teus.DataBind();

                        ApprovedSummary.DataSource = allowable;
                        ApprovedSummary.DataBind();
                    }
                    else
                    {
                        var avlfreespc = (from a in db.VSA_Txn_Participant_Review_and_Allowable
                                          join q in db.VSA_Txn_Participant_Application on a.VoyageID equals q.VoyageID
                                          join c in db.VSA_Master_Customer on a.VSAParticipantCustomerID equals c.CustomerID
                                          join p in db.VSA_Txn_Invite on a.VoyageID equals p.VoyageID
                                          join u in db.VSA_Config_Port on a.OriginTransitPortID equals u.PortID
                                          join s in db.VSA_Txn_VesselVoyageTransitShipRoute on a.VoyageID equals s.VoyageID
                                          join t in db.VSA_Config_Port on s.DestinationTransitPortID equals t.PortID
                                          where a.VoyageID == ddlvoyageid.SelectedValue
                                          where a.VSAParticipantCustomerID == ddlcustomerid.SelectedValue
                                          where q.VoyageSegmentSequencenumber == a.VoyageSegmentSequenceNumber
                                          where q.VSAParticipantCustomerID == ddlcustomerid.SelectedValue
                                          where s.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber
                                          where p.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber

                                          select new
                                          {
                                              VoyageID = q.VoyageID,
                                              VSAParticipantCustomerID = q.VSAParticipantCustomerID,
                                              VoyageSegmentSequencenumber = q.VoyageSegmentSequencenumber,
                                              shiporignport = u.PortName,
                                              DestinationPortID = t.PortName,
                                              InviteVSAParticipantsFlag = p.InviteVSAParticipantsFlag,
                                              CargoOperatorAgentName = c.CompanyName,
                                              VSAnotes = a.VSANotes,
                                              InitialAvailableSpaceTEU = p.AvailableSpaceTEU,
                                              lblNetTEUsApp = a.NetTEUSAllowedforVSAParticipantforVoyageSegment,
                                              AvailableSpaceTEU = (p.AvailableSpaceTEU - (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment))) + a.NetTEUSAllowedforVSAParticipantforVoyageSegment,
                                              lblload40inch = q.Load_40Feet_Containers_Apply,
                                              lblload20inch = q.Load_20Feet_Containers_Apply,
                                              lbllblTEUsLoaded = q.LoadTEU_Apply,
                                              lblAccepted40Load = a.Load_40Feet_Containers_AllowableforCargoOperator,
                                              lblAccepted20Load = a.Load_20Feet_Containers_AllowableforCargoOperator,
                                              lblAllowLoad = a.LoadTEU_AllowableforCargoOperator,
                                              lbldis40inch = q.Discharge_40Feet_Containers_Apply,
                                              lbldis20inch = q.Discharge_20Feet_Containers_Apply,
                                              lbllblTEUsDischarged = q.DischargeTEU_Apply,
                                              lblAccepted40Disch = a.Discharge_40Feet_Containers_AllowableforCargoOperator,
                                              lblAccepted20Disch = a.Discharge_20Feet_Containers_AllowableforCargoOperator,
                                              lblAllowDisch = a.DischargeTEU_AllowableforCargoOperator,
                                              NetTEUSowned = q.NetTEUSownedByVSAParticipantforVoyageSegment,
                                              lblAppStatus = a.Application_Status,
                                              ApplyVSANotes = a.VSANotes,
                                              lblload = "",
                                              txt40load = "",
                                              txt20load = "",
                                          }).ToList();



                        VoyageInvitation.DataSource = avlfreespc;
                        VoyageInvitation.DataBind();

                        Teus.DataSource = avlfreespc;
                        Teus.DataBind();

                        ApprovedSummary.DataSource = avlfreespc;
                        ApprovedSummary.DataBind();
                    }

                    for (int i = 0; i < Teus.Items.Count; i++)
                    {

                        TextBox TxtAccepted40Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Load");
                        TextBox TxtAccepted20Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Load");
                        TextBox TxtAccepted40Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Disch");
                        TextBox TxtAccepted20Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Disch");
                        TextBox TxtVSAnotestodisplay = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSAnotestodisplay");
                        TextBox TxtVSANotes = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSANotes");
                        Label lblStatus = (Label)ApprovedSummary.Items[i].FindControl("lblStatus");


                        TxtAccepted40Load.Visible = false;
                        TxtAccepted20Load.Visible = false;
                        TxtAccepted40Disch.Visible = false;
                        TxtAccepted20Disch.Visible = false;
                        TxtVSANotes.Visible = false;
                        TxtVSAnotestodisplay.Visible = true;

                        Label lblAccepted40Load = (Label)Teus.Items[i].FindControl("lblAccepted40Load");
                        Label lblAccepted20Load = (Label)Teus.Items[i].FindControl("lblAccepted20Load");
                        Label lblAccepted40Disch = (Label)Teus.Items[i].FindControl("lblAccepted40Disch");
                        Label lblAccepted20Disch = (Label)Teus.Items[i].FindControl("lblAccepted20Disch");
                        Label lblNetTEUsApp = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApp");
                        Label lblNetTEUsApproved = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApproved");
                        Label lblNetTEUsAppTeus = (Label)Teus.Items[i].FindControl("lblNetTEUsApp");
                        Label lblNetTEUsApprovedTeus = (Label)Teus.Items[i].FindControl("lblNetTEUsApproved");


                        lblAccepted40Load.Visible = true;
                        lblAccepted20Load.Visible = true;
                        lblAccepted40Disch.Visible = true;
                        lblAccepted20Disch.Visible = true;
                        lblNetTEUsApp.Visible = true;
                        lblNetTEUsApproved.Visible = false;
                        lblNetTEUsAppTeus.Visible = true;
                        lblNetTEUsApprovedTeus.Visible = false;

                        if (lblStatus.Text == "DisAgreed")
                        {
                            BtnEditDisagreed.Visible = true;
                            btnApprove.Visible = false;
                        }
                        else
                        {
                            BtnEditDisagreed.Visible = false;
                            btnApprove.Visible = false;
                            Btncancel.Visible = false;
                        }

                        tblDetails.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        // Method lnkApproved_Click: Use : To approve the VSA agreement details by the reviewer
        protected void btnApproved_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            {
                string custid = Convert.ToString(Session["CustomerID"]);

                int TeusCount = Teus.Items.Count;
                for (int i = 0; i < Teus.Items.Count; i++)
                {
                    var approved = new VSA_Txn_Participant_Review_and_Allowable();
                    HiddenField TxtEqvlntNetTEUsApproved = (HiddenField)Teus.Items[i].FindControl("TxtEqvlntNetTEUsApproved");
                    HiddenField TxtEqvlntAcceptedDischarge = (HiddenField)Teus.Items[i].FindControl("TxtEqvlntAcceptedDischarge");
                    HiddenField TxtEqvlntAcceptedload = (HiddenField)Teus.Items[i].FindControl("TxtEqvlntAcceptedload");
                    HiddenField HiddenNet40 = (HiddenField)Teus.Items[i].FindControl("HiddenNet40");
                    HiddenField HiddenNet20 = (HiddenField)Teus.Items[i].FindControl("HiddenNet20");
                    Label lblVoyageSeqnum = (Label)VoyageInvitation.Items[i].FindControl("lblVoyageSeqnum");
                    Label lblorignport = (Label)VoyageInvitation.Items[i].FindControl("lblorignport");
                    Label Acceptedload = (Label)Teus.Items[i].FindControl("lblAcceptedload");

                    TextBox TxtAccepted40Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Load");
                    TextBox TxtAccepted20Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Load");
                    Label lblcompanyname = (Label)Teus.Items[i].FindControl("lblcompanyname");
                    Label AcceptedDischarge = (Label)Teus.Items[i].FindControl("lblAcceptedDischarge");

                    TextBox TxtAccepted40Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Disch");
                    TextBox TxtAccepted20Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Disch");
                    Label lblNetTEUsApproved = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApproved");

                    TextBox TxtVsaNotes = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSANotes");

                    var originport = (from q in db.VSA_Config_Port
                                      where q.PortName == lblorignport.Text.Trim()
                                      select new
                                      {
                                          originID = q.PortID
                                      }).SingleOrDefault();

                    var aord = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                where q.VoyageID == ddlvoyageid.SelectedValue
                                where q.Application_Status == "DisAgreed"
                                select new { q.Application_Status }).ToList();

                    var company = (from q in db.VSA_Master_Customer
                                   where q.CompanyName == lblcompanyname.Text.Trim()
                                   select new { q.CustomerID }).SingleOrDefault();
                    int seqno = Convert.ToInt32(lblVoyageSeqnum.Text.Trim());

                    if (aord.Count == 0)
                    {
                        approved.VoyageID = ddlvoyageid.SelectedValue;
                        approved.VoyageSegmentSequenceNumber = Convert.ToInt32(lblVoyageSeqnum.Text.Trim());
                        approved.VSAParticipantCustomerID = company.CustomerID;
                        approved.OriginTransitPortID = originport.originID;
                        if (TxtAccepted40Load.Text.Trim() != string.Empty)
                        {
                            approved.Load_40Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(TxtAccepted40Load.Text.Trim());
                        }
                        else
                        {
                            approved.Load_40Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if (TxtAccepted20Load.Text.Trim() != string.Empty)
                        {
                            approved.Load_20Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(TxtAccepted20Load.Text.Trim());
                        }
                        else
                        {
                            approved.Load_20Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if (TxtEqvlntAcceptedload.Value != string.Empty)
                        {
                            approved.LoadTEU_AllowableforCargoOperator = Convert.ToInt32(TxtEqvlntAcceptedload.Value);
                        }
                        else
                        {
                            approved.LoadTEU_AllowableforCargoOperator = 0;
                        }
                        if (TxtAccepted40Disch.Text.Trim() != string.Empty)
                        {
                            approved.Discharge_40Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(TxtAccepted40Disch.Text.Trim());
                        }
                        else
                        {
                            approved.Discharge_40Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if (TxtAccepted20Disch.Text.Trim() != string.Empty)
                        {
                            approved.Discharge_20Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(TxtAccepted20Disch.Text.Trim());
                        }
                        else
                        {
                            approved.Discharge_20Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if(TxtEqvlntAcceptedDischarge.Value != string.Empty)
                        { 
                        approved.DischargeTEU_AllowableforCargoOperator = Convert.ToInt32(TxtEqvlntAcceptedDischarge.Value);
                        }
                        else
                        {
                        approved.DischargeTEU_AllowableforCargoOperator = 0;
                        }
                        if(TxtEqvlntNetTEUsApproved.Value != string.Empty)
                        {
                        approved.NetTEUSAllowedforVSAParticipantforVoyageSegment = Convert.ToInt32(TxtEqvlntNetTEUsApproved.Value);
                        }
                        else
                        {
                        approved.NetTEUSAllowedforVSAParticipantforVoyageSegment = 0;
                        }

                        var notes = (from q in db.VSA_Txn_Participant_Application
                                     where q.VoyageID == ddlvoyageid.SelectedValue
                                     where q.VoyageSegmentSequencenumber == approved.VoyageSegmentSequenceNumber
                                     where q.VSAParticipantCustomerID == company.CustomerID
                                     select new { q.VSANotes }).SingleOrDefault();

                        approved.VSANotes = notes.VSANotes + Environment.NewLine + "#Date:" + DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss") + "#CustID:" + custid + "# Comments:" + TxtVsaNotes.Text;
                        approved.Create_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                        approved.Update_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                        approved.Application_Status = "Approved";
                        if(HiddenNet40.Value != string.Empty)
                        { 
                        approved.Net_40Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(HiddenNet40.Value);
                        }
                        else
                        {
                            approved.Net_40Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if(HiddenNet20.Value != string.Empty)
                        { 
                        approved.Net_20Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(HiddenNet20.Value);
                        }
                        else
                        {
                        approved.Net_20Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        var updateStatus = (from q in db.VSA_Txn_Participant_Application
                                            where q.VoyageID == ddlvoyageid.SelectedValue
                                            where q.VoyageSegmentSequencenumber == approved.VoyageSegmentSequenceNumber
                                            where q.VSAParticipantCustomerID == company.CustomerID
                                            select new { q }).SingleOrDefault();
                        updateStatus.q.Application_Status = "Approved";

                        var loadanddisch = (from q in db.VSA_Txn_Participant_Application
                                            where q.VoyageID == approved.VoyageID
                                            where q.VoyageSegmentSequencenumber == approved.VoyageSegmentSequenceNumber
                                            where q.VSAParticipantCustomerID == approved.VSAParticipantCustomerID
                                            select new
                                            {
                                                q.Load_40Feet_Containers_Apply,
                                                q.Load_20Feet_Containers_Apply,
                                                q.Discharge_40Feet_Containers_Apply,
                                                q.Discharge_20Feet_Containers_Apply,
                                            }).SingleOrDefault();

                        if (loadanddisch.Load_40Feet_Containers_Apply >= approved.Load_40Feet_Containers_AllowableforCargoOperator && loadanddisch.Load_20Feet_Containers_Apply >= approved.Load_20Feet_Containers_AllowableforCargoOperator && loadanddisch.Discharge_40Feet_Containers_Apply >= approved.Discharge_40Feet_Containers_AllowableforCargoOperator && loadanddisch.Discharge_20Feet_Containers_Apply >= approved.Discharge_20Feet_Containers_AllowableforCargoOperator)
                        {


                            db.VSA_Txn_Participant_Review_and_Allowable.Add(approved);
                            db.SaveChanges();
                            String strAppMsg = ConfigurationManager.AppSettings["RAapproved"];
                            lblreviewmsg.Text = strAppMsg;
                            lblreviewmsg.ForeColor = System.Drawing.Color.ForestGreen;
                            showVesselshare();
                        }
                        else
                        {
                            showReviewdtls();
                            showVesselshare();
                            lblreviewmsg.Text = "You can not approve more than applied";
                            lblreviewmsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {

                        var UpdateDisagreed = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                               where q.VoyageID == ddlvoyageid.SelectedValue
                                               where q.VSAParticipantCustomerID == ddlcustomerid.SelectedValue
                                               where q.VoyageSegmentSequenceNumber == seqno
                                               select new { q }).SingleOrDefault();
                        if (TxtAccepted40Load.Text.Trim() != string.Empty)
                        {
                            UpdateDisagreed.q.Load_40Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(TxtAccepted40Load.Text.Trim());
                        }
                        else
                        {
                            UpdateDisagreed.q.Load_40Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if (TxtAccepted20Load.Text.Trim() != string.Empty)
                        {
                            UpdateDisagreed.q.Load_20Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(TxtAccepted20Load.Text.Trim());
                        }
                        else
                        {
                            UpdateDisagreed.q.Load_20Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if(TxtEqvlntAcceptedload.Value != string.Empty)
                        { 
                        UpdateDisagreed.q.LoadTEU_AllowableforCargoOperator = Convert.ToInt32(TxtEqvlntAcceptedload.Value);
                        }
                        else
                        {
                            UpdateDisagreed.q.LoadTEU_AllowableforCargoOperator = 0;
                        }
                        if (TxtAccepted40Disch.Text.Trim() != string.Empty)
                        {
                            UpdateDisagreed.q.Discharge_40Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(TxtAccepted40Disch.Text.Trim());
                        }
                        else
                        {
                            UpdateDisagreed.q.Discharge_40Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if (TxtAccepted20Disch.Text.Trim() != string.Empty)
                        {
                        UpdateDisagreed.q.Discharge_20Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(TxtAccepted20Disch.Text.Trim());
                        }
                        else
                        {
                        UpdateDisagreed.q.Discharge_20Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if(TxtEqvlntAcceptedDischarge.Value != string.Empty)
                        { 
                        UpdateDisagreed.q.DischargeTEU_AllowableforCargoOperator = Convert.ToInt32(TxtEqvlntAcceptedDischarge.Value);
                        }
                        else
                        {
                        UpdateDisagreed.q.DischargeTEU_AllowableforCargoOperator = 0;
                        }
                        if(TxtEqvlntNetTEUsApproved.Value != string.Empty)
                        { 
                        UpdateDisagreed.q.NetTEUSAllowedforVSAParticipantforVoyageSegment = Convert.ToInt32(TxtEqvlntNetTEUsApproved.Value);
                        }
                        else
                        {
                        UpdateDisagreed.q.NetTEUSAllowedforVSAParticipantforVoyageSegment = 0;
                        }
                        var notes = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                     where q.VoyageID == ddlvoyageid.SelectedValue
                                     where q.VSAParticipantCustomerID == ddlcustomerid.SelectedValue
                                     where q.VoyageSegmentSequenceNumber == seqno
                                     select new { q.VSANotes }).SingleOrDefault();

                        UpdateDisagreed.q.VSANotes = notes.VSANotes + Environment.NewLine + "#Date:" + DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss") + "#CustID:" + custid + "# Comments:" + TxtVsaNotes.Text;
                        UpdateDisagreed.q.Update_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                        UpdateDisagreed.q.Application_Status = "Approved";
                        if(HiddenNet40.Value !=string.Empty)
                        { 
                        UpdateDisagreed.q.Net_40Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(HiddenNet40.Value);
                        }
                        else
                        {
                        UpdateDisagreed.q.Net_40Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        if(HiddenNet20.Value != string.Empty)
                        { 
                        UpdateDisagreed.q.Net_20Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(HiddenNet20.Value);
                        }
                        else
                        {
                        UpdateDisagreed.q.Net_20Feet_Containers_AllowableforCargoOperator = 0;
                        }
                        var updateStatus = (from q in db.VSA_Txn_Participant_Application
                                            where q.VoyageID == ddlvoyageid.SelectedValue
                                            where q.VoyageSegmentSequencenumber == seqno
                                            where q.VSAParticipantCustomerID == ddlcustomerid.SelectedValue
                                            select new { q }).SingleOrDefault();
                        updateStatus.q.Application_Status = "Approved";

                        db.SaveChanges();
                        String strAppMsg = ConfigurationManager.AppSettings["RAapproved"];
                        lblreviewmsg.Text = strAppMsg;
                        lblreviewmsg.ForeColor = System.Drawing.Color.ForestGreen;
                    }

                }
                DbTrans.Commit();
                showVesselshare();
                showReviewdtls();
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method ddlAppStatus_SelectedIndexChanged: Use : To display VSA details based on VSA progress status drop down
        protected void ddlAppStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAppStatus.SelectedValue.Trim() == "Review In Progress")
                {
                    alltbls.Visible = true;
                    lnkAgree.Visible = true;
                    lnkDisAgree.Visible = true;

                    showAllowable();
                    showVesselshare();
                }
                //if (ddlAppStatus.SelectedValue.Trim() == "Final VSA")
                //{
                //    alltbls.Visible = true;
                //    lnkAgree.Visible = true;
                //    lnkDisAgree.Visible = true;

                //    showApproved();
                //    showVesselshare();
                //}
                if (ddlAppStatus.SelectedValue.Trim() == "0")
                {
                    showVesselshare();
                    alltbls.Visible = false;
                    lnkAgree.Visible = false;
                    lnkDisAgree.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method showAllowable(): Use : To display allowed VSA details, so that reviewer can enter allowable values

        public void showAllowable()
        {
            var db = new VesselAgreement();

            try
            {
                string custid = Convert.ToString(Session["CustomerID"]);

                var TotalVSA = (from a in db.VSA_Txn_Participant_Review_and_Allowable
                                join q in db.VSA_Txn_Participant_Application on a.VoyageID equals q.VoyageID
                                join c in db.VSA_Master_Customer on a.VSAParticipantCustomerID equals c.CustomerID
                                join p in db.VSA_Txn_Invite on a.VoyageID equals p.VoyageID
                                join u in db.VSA_Config_Port on a.OriginTransitPortID equals u.PortID
                                join s in db.VSA_Txn_VesselVoyageTransitShipRoute on a.VoyageID equals s.VoyageID
                                join t in db.VSA_Config_Port on s.DestinationTransitPortID equals t.PortID
                                where a.VoyageID == ddlvoyageid.SelectedValue
                                where q.VoyageID == ddlvoyageid.SelectedValue
                                where p.VoyageID == ddlvoyageid.SelectedValue
                                where s.VoyageID == ddlvoyageid.SelectedValue
                                where q.VoyageSegmentSequencenumber == a.VoyageSegmentSequenceNumber
                                where s.VoyageSegmentSequenceNumber == a.VoyageSegmentSequenceNumber
                                where p.VoyageSegmentSequenceNumber == a.VoyageSegmentSequenceNumber
                                where a.VSAParticipantCustomerID == custid
                                where q.VSAParticipantCustomerID == custid
                                where !db.VSA_Txn_Participant_Review_and_Approved.Any(o => o.VoyageID == a.VoyageID && o.VoyageSegmentSequenceNumber == a.VoyageSegmentSequenceNumber && o.VSAParticipantCustomerID == a.VSAParticipantCustomerID)
                                select new
                                {
                                    VoyageSegmentSequencenumber = q.VoyageSegmentSequencenumber,
                                    shiporignport = u.PortName,
                                    DestinationPortID = t.PortName,
                                    InviteVSAParticipantsFlag = p.InviteVSAParticipantsFlag,
                                    CargoOperatorAgentName = c.CompanyName,
                                    InitialAvailableSpaceTEU = p.AvailableSpaceTEU,
                                    AvailableSpaceTEU = (p.AvailableSpaceTEU - (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment))) != null ? (p.AvailableSpaceTEU - (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment))) : p.AvailableSpaceTEU,
                                    lblload40inch = q.Load_40Feet_Containers_Apply,
                                    lblload20inch = q.Load_20Feet_Containers_Apply,
                                    lbllblTEUsLoaded = q.LoadTEU_Apply,
                                    lblAccepted40Load = a.Load_40Feet_Containers_AllowableforCargoOperator,
                                    lblAccepted20Load = a.Load_20Feet_Containers_AllowableforCargoOperator,
                                    lblAllowLoad = a.LoadTEU_AllowableforCargoOperator,
                                    lbldis40inch = q.Discharge_40Feet_Containers_Apply,
                                    lbldis20inch = q.Discharge_20Feet_Containers_Apply,
                                    lbllblTEUsDischarged = q.DischargeTEU_Apply,
                                    lblAccepted40Disch = a.Discharge_40Feet_Containers_AllowableforCargoOperator,
                                    lblAccepted20Disch = a.Discharge_20Feet_Containers_AllowableforCargoOperator,
                                    lblAllowDisch = a.DischargeTEU_AllowableforCargoOperator,
                                    NetTEUSowned = q.NetTEUSownedByVSAParticipantforVoyageSegment,
                                    lblNetTEUsApp = a.NetTEUSAllowedforVSAParticipantforVoyageSegment,
                                    //VSAnotes = a.VSANotes,
                                    lblAppStatus = a.Application_Status,
                                    ApplyVSANotes = a.VSANotes,
                                    lblload = "",
                                    txt40load = "",
                                    txt20load = "",
                                    // charges tab
                                    lblTransitPort = u.PortName + " - " + t.PortName,
                                    lblNetTeus = a.NetTEUSAllowedforVSAParticipantforVoyageSegment,
                                    lblEquivalent40FtContainers = a.Net_40Feet_Containers_AllowableforCargoOperator,
                                    lblEquivalent20FtContainers = a.Net_20Feet_Containers_AllowableforCargoOperator,
                                    lblPricePerTEU = p.PricePerTEU,
                                    lblPriceby40ftcontainers = p.PricePer40FeetContainer,//q.Net_40Feet_Containers_AllowableforCargoOperator * p.PricePer40FeetContainer,
                                    lblPriceby20ftcontainers = p.PricePer20FeetContainer,
                                    lblTotalprice = (a.NetTEUSAllowedforVSAParticipantforVoyageSegment * p.PricePerTEU) + ((a.Net_40Feet_Containers_AllowableforCargoOperator * p.PricePer40FeetContainer) + (a.Net_20Feet_Containers_AllowableforCargoOperator * p.PricePer20FeetContainer)),
                                }).ToList();



                Teus.DataSource = TotalVSA;
                Teus.DataBind();

                ApprovedSummary.DataSource = TotalVSA;
                ApprovedSummary.DataBind();

                for (int i = 0; i < Teus.Items.Count; i++)
                {
                    Label lblAccepted40Load = (Label)Teus.Items[i].FindControl("lblAccepted40Load");
                    Label lblAccepted20Load = (Label)Teus.Items[i].FindControl("lblAccepted20Load");
                    Label lblAccepted40Disch = (Label)Teus.Items[i].FindControl("lblAccepted40Disch");
                    Label lblAccepted20Disch = (Label)Teus.Items[i].FindControl("lblAccepted20Disch");
                    Label lblAllowDisch = (Label)Teus.Items[i].FindControl("lblAllowDisch");
                    Label lblAllowLoad = (Label)Teus.Items[i].FindControl("lblAllowLoad");
                    Label lblNetTEUsApp = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApp");
                    Label lblNetTEUsAppTeus = (Label)Teus.Items[i].FindControl("lblNetTEUsApp");
                    Label lblStatus = (Label)ApprovedSummary.Items[i].FindControl("lblStatus");


                    lblAccepted40Load.Visible = true;
                    lblAccepted20Load.Visible = true;
                    lblAccepted40Disch.Visible = true;
                    lblAccepted20Disch.Visible = true;
                    lblAllowLoad.Visible = true;
                    lblAllowDisch.Visible = true;
                    lblNetTEUsApp.Visible = true;
                    lblNetTEUsAppTeus.Visible = true;


                    TextBox TxtAccepted40Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Load");
                    TextBox TxtAccepted20Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Load");
                    Label lblAcceptedload = (Label)Teus.Items[i].FindControl("lblAcceptedload");
                    TextBox TxtAccepted40Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Disch");
                    TextBox TxtAccepted20Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Disch");
                    Label lblAcceptedDischarge = (Label)Teus.Items[i].FindControl("lblAcceptedDischarge");
                    Label lblNetTEUsApproved = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApproved");
                    Label lblNetTEUsApprovedTeus = (Label)Teus.Items[i].FindControl("lblNetTEUsApproved");
                    TextBox TxtVSANotes = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSANotes");
                    TextBox TxtVSAnotestodisplay = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSAnotestodisplay");

                    TxtAccepted40Load.Visible = false;
                    TxtAccepted20Load.Visible = false;
                    lblAcceptedload.Visible = false;
                    TxtAccepted40Disch.Visible = false;
                    TxtAccepted20Disch.Visible = false;
                    lblAcceptedDischarge.Visible = false;
                    lblNetTEUsApproved.Visible = false;
                    lblNetTEUsApprovedTeus.Visible = false;
                    TxtVSANotes.Visible = true;


                    if (lblStatus.Text == "Approved")
                    {
                        lnkAgree.Visible = true;
                        lnkDisAgree.Visible = true;
                    }
                    else
                    {
                        lnkAgree.Visible = false;
                        lnkDisAgree.Visible = false;
                        TxtVSAnotestodisplay.Visible = true;
                        TxtVSANotes.Visible = false;
                    }


                }

                VoyageInvitation.DataSource = TotalVSA;
                VoyageInvitation.DataBind();

                RptCharges.DataSource = TotalVSA;
                RptCharges.DataBind();

                //Charges();

                if (TotalVSA.Count == 0)
                {
                    lnkAgree.Visible = false;
                    lnkDisAgree.Visible = false;
                }


                btnApprove.Visible = false;
                Btncancel.Visible = false;
                tblDetails.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method lnkAgree_Click: Use : To agree to a VSA which is already approved by the reviewer
        protected void lnkAgree_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            {

                string custid = Convert.ToString(Session["CustomerID"]);
                for (int i = 0; i < Teus.Items.Count; i++)
                {
                    var agree = new VSA_Txn_Participant_Review_and_Approved();


                    Label lblVoyageSeqnum = (Label)VoyageInvitation.Items[i].FindControl("lblVoyageSeqnum");
                    Label lblorignport = (Label)VoyageInvitation.Items[i].FindControl("lblorignport");
                    Label lblAccepted40Load = (Label)Teus.Items[i].FindControl("lblAccepted40Load");
                    Label lblAccepted20Load = (Label)Teus.Items[i].FindControl("lblAccepted20Load");
                    Label lblAccepted40Disch = (Label)Teus.Items[i].FindControl("lblAccepted40Disch");
                    Label lblAccepted20Disch = (Label)Teus.Items[i].FindControl("lblAccepted20Disch");
                    Label lblAllowDisch = (Label)Teus.Items[i].FindControl("lblAllowDisch");
                    Label lblAllowLoad = (Label)Teus.Items[i].FindControl("lblAllowLoad");
                    Label lblNetTEUsApp = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApp");
                    Label lblEquivalent40FtContainers = (Label)RptCharges.Items[i].FindControl("lblEquivalent40FtContainers");
                    Label lblEquivalent20FtContainers = (Label)RptCharges.Items[i].FindControl("lblEquivalent20FtContainers");
                    TextBox TxtVSANotes = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSANotes");

                    int id = Convert.ToInt32(lblVoyageSeqnum.Text);

                    var originport = (from q in db.VSA_Config_Port
                                      where q.PortName == lblorignport.Text.Trim()
                                      select new
                                      {
                                          originID = q.PortID
                                      }).SingleOrDefault();

                    var invtdt = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                  join p in db.VSA_Txn_Invite on q.VoyageID equals p.VoyageID
                                  join s in db.VSA_Txn_Participant_Application on q.VoyageID equals s.VoyageID
                                  where q.VoyageID == ddlvoyageid.SelectedValue
                                  where q.VSAParticipantCustomerID == custid
                                  where q.VoyageSegmentSequenceNumber == id
                                  where p.VoyageID == ddlvoyageid.SelectedValue
                                  where p.VoyageSegmentSequenceNumber == id
                                  where s.VoyageID == ddlvoyageid.SelectedValue
                                  where s.VoyageSegmentSequencenumber == id
                                  where s.VSAParticipantCustomerID == custid
                                  select new
                                  {
                                      p.IsPricingbyTEUorByContainerSize,
                                      p.PricePer20FeetContainer,
                                      p.PricePer40FeetContainer,
                                      p.PricePerTEU,
                                      p.VSAArrangementFeeAgreed_VO,
                                      p.VSAArrangementFeePerTEU_VO,
                                      s.VSAArrangementFeeAgreed_CO,
                                      s.VSAArrangementFeePerTEU_CO,
                                  }).SingleOrDefault();

                    string seqid = lblVoyageSeqnum.Text, voyid = ddlvoyageid.SelectedValue, customerid = custid;
                    if(lblVoyageSeqnum.Text.Length < 2)
                    {
                        seqid = "0" + lblVoyageSeqnum.Text;
                    }
                    else
                    {
                        seqid = lblVoyageSeqnum.Text;
                    }

                    if(ddlvoyageid.SelectedValue.Length < 5)
                    {
                        for (int j= ddlvoyageid.SelectedValue.Length; j <= 4; j++ )
                        {
                            voyid = voyid + ddlvoyageid.SelectedValue.Substring(ddlvoyageid.SelectedValue.Length - 1);
                        }
                    }
                    if (custid.Length < 5)
                    {
                        for (int j = custid.Length; j <= 4; j++)
                        {
                            customerid = customerid + custid.Substring(custid.Length - 1);
                        }
                    }


                    string VsaAgreementId = voyid.Substring(0, 5).ToUpper() + customerid.Substring(0, 5).ToUpper() + seqid.Substring(0, 2);

                    agree.VSA_Partcipant_Agreement_ID = VsaAgreementId;
                    agree.VoyageID = ddlvoyageid.SelectedValue;
                    if(lblVoyageSeqnum.Text.Trim() != string.Empty)
                    { 
                    agree.VoyageSegmentSequenceNumber = Convert.ToInt32(lblVoyageSeqnum.Text.Trim());
                    }
                    else
                    {
                        agree.VoyageSegmentSequenceNumber = 0;
                    }
                    agree.VSAParticipantCustomerID = custid;
                    agree.OriginTransitPortID = originport.originID;
                    if(lblAllowLoad.Text.Trim() != string.Empty)
                    { 
                    agree.LoadTEU_ApprovedforCargoOperator = Convert.ToInt32(lblAllowLoad.Text.Trim());
                    }
                    else
                    {
                    agree.LoadTEU_ApprovedforCargoOperator = 0;
                    }
                    if(lblAccepted40Load.Text.Trim() != string.Empty)
                    { 
                    agree.Load_40Feet_Containers_ApprovedforCargoOperator = Convert.ToInt32(lblAccepted40Load.Text.Trim());
                    }
                    else
                    {
                    agree.Load_40Feet_Containers_ApprovedforCargoOperator = 0;
                    }
                    if(lblAccepted20Load.Text.Trim() != string.Empty)
                    { 
                    agree.Load_20Feet_Containers_ApprovedforCargoOperator = Convert.ToInt32(lblAccepted20Load.Text.Trim());
                    }
                    else
                    {
                        agree.Load_20Feet_Containers_ApprovedforCargoOperator = 0;
                    }
                    if(lblAllowDisch.Text.Trim() != string.Empty)
                    { 
                    agree.DischargeTEU_ApprovedforCargoOperator = Convert.ToInt32(lblAllowDisch.Text.Trim());
                    }
                    else
                    {
                        agree.DischargeTEU_ApprovedforCargoOperator = 0;
                    }
                    if (lblAccepted40Disch.Text.Trim() != string.Empty)
                    {
                        agree.Discharge_40Feet_Containers_ApprovedforCargoOperator = Convert.ToInt32(lblAccepted40Disch.Text.Trim());
                    }
                    else
                    {
                        agree.Discharge_40Feet_Containers_ApprovedforCargoOperator = 0;
                    }
                    if (lblAccepted20Disch.Text.Trim() != string.Empty)
                    {
                        agree.Discharge_20Feet_Containers_ApprovedforCargoOperator = Convert.ToInt32(lblAccepted20Disch.Text.Trim());
                    }
                    else
                    {
                        agree.Discharge_20Feet_Containers_ApprovedforCargoOperator = 0;
                    }
                    if (lblNetTEUsApp.Text.Trim() != string.Empty)
                    {
                        agree.NetTEUSApprovedforVSAParticipantforVoyageSegment = Convert.ToInt32(lblNetTEUsApp.Text.Trim());
                    }
                    else
                    {
                        agree.NetTEUSApprovedforVSAParticipantforVoyageSegment = 0;
                    }
                    if(lblEquivalent40FtContainers.Text.Trim() != string.Empty)
                    {
                        agree.Net_40Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(lblEquivalent40FtContainers.Text.Trim());
                    }
                    else
                    {
                        agree.Net_40Feet_Containers_AllowableforCargoOperator = 0;
                    }
                    if(lblEquivalent20FtContainers.Text.Trim() != string.Empty)
                    {
                        agree.Net_20Feet_Containers_AllowableforCargoOperator = Convert.ToInt32(lblEquivalent20FtContainers.Text.Trim());
                    }
                    else
                    {
                        agree.Net_20Feet_Containers_AllowableforCargoOperator = 0;
                    }
                    agree.VSA_Participant_ConsentFlag = "Y";
                    agree.IsPricingbyTEUorByContainerSize = invtdt.IsPricingbyTEUorByContainerSize;
                    agree.PricePer40FeetContainer = invtdt.PricePer40FeetContainer;
                    agree.PricePer20FeetContainer = invtdt.PricePer20FeetContainer;
                    agree.PricePerTEU = invtdt.PricePerTEU;
                    agree.VSAArrangementFeeAgreed_CO = invtdt.VSAArrangementFeeAgreed_CO;
                    agree.VSAArrangementFeeAgreed_VO = invtdt.VSAArrangementFeeAgreed_VO;
                    agree.VSAArrangementFeePerTEU_CO = invtdt.VSAArrangementFeePerTEU_CO;
                    agree.VSAArrangementFeePerTEU_VO = invtdt.VSAArrangementFeePerTEU_VO;
                    agree.VSA_Agreement_Create_Ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:sszzz"));

                    var notes = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                 where q.VoyageID == ddlvoyageid.SelectedValue
                                 where q.VoyageSegmentSequenceNumber == id
                                 where q.VSAParticipantCustomerID == custid
                                 select new { q.VSANotes }).SingleOrDefault();

                    agree.VSANotes = notes.VSANotes + Environment.NewLine + "#Date:" + DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss") + "#CustID:" + custid + "# Comments:" + TxtVSANotes.Text;
                    agree.VSA_Update_Ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:sszzz"));
                    agree.Application_Status = "Final VSA";
                    var updateStatus = (from q in db.VSA_Txn_Participant_Application
                                        join p in db.VSA_Txn_Participant_Review_and_Allowable on q.VoyageID equals p.VoyageID
                                        where q.VoyageID == ddlvoyageid.SelectedValue
                                        where q.VoyageSegmentSequencenumber == agree.VoyageSegmentSequenceNumber
                                        where q.VSAParticipantCustomerID == agree.VSAParticipantCustomerID
                                        where p.VoyageSegmentSequenceNumber == agree.VoyageSegmentSequenceNumber
                                        where p.VSAParticipantCustomerID == agree.VSAParticipantCustomerID
                                        select new { q, p }).SingleOrDefault();
                    updateStatus.q.Application_Status = "Final VSA";
                    updateStatus.p.Application_Status = "Final VSA";


                    db.VSA_Txn_Participant_Review_and_Approved.Add(agree);
                    db.SaveChanges();
                    String strAppMsg = ConfigurationManager.AppSettings["RAagreed"];
                    lblreviewmsg.Text = strAppMsg;
                    lblreviewmsg.ForeColor = System.Drawing.Color.ForestGreen;
                }
                DbTrans.Commit();
                showVesselshare();
                showApproved();
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method UniqueNumber(): Use : To autogenerate unique Vessel Share Agreement Unique ID
        //public string UniqueNumber()
        //{
        //    try
        //    {
        //        Random unique1 = new Random();
        //        string custid = Convert.ToString(Session["CustomerID"]);

        //        string s = custid.Substring(0, 5).ToUpper() + ddlvoyageid.SelectedValue.Substring(0, 5).ToUpper();
        //        int n = 0;
        //        while (n <= 7)
        //        {
        //            s += unique1.Next(10).ToString();
        //            n++;
        //        }
        //        return s;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        protected void lnkDisAgree_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
                string custid = Convert.ToString(Session["CustomerID"]);

                for (int i = 0; i < Teus.Items.Count; i++)
                {

                    Label lblVoyageSeqnum = (Label)VoyageInvitation.Items[i].FindControl("lblVoyageSeqnum");
                    int id = Convert.ToInt32(lblVoyageSeqnum.Text);

                    var disagreed = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                     where q.VoyageID == ddlvoyageid.SelectedValue && q.VoyageSegmentSequenceNumber == id && q.VSAParticipantCustomerID == custid
                                     select new { q }).SingleOrDefault();


                    TextBox TxtVSANotes = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSANotes");

                    var notes = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                 where q.VoyageID == ddlvoyageid.SelectedValue
                                 where q.VoyageSegmentSequenceNumber == id
                                 where q.VSAParticipantCustomerID == custid
                                 select new { q.VSANotes }).SingleOrDefault();

                    disagreed.q.VSANotes = notes.VSANotes + Environment.NewLine + "#Date:" + DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss") + "#CustID:" + custid + "# Comments:" + TxtVSANotes.Text;
                    disagreed.q.Update_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:sszzz"));
                    disagreed.q.Application_Status = "DisAgreed";

                    var updateStatus = (from q in db.VSA_Txn_Participant_Application
                                        where q.VoyageID == ddlvoyageid.SelectedValue
                                        where q.VoyageSegmentSequencenumber == id
                                        where q.VSAParticipantCustomerID == custid
                                        select new { q }).SingleOrDefault();

                    updateStatus.q.Application_Status = "DisAgreed";

                    db.SaveChanges();
                    lblreviewmsg.Text = "Customer Not Agreed";
                    lblreviewmsg.ForeColor = System.Drawing.Color.ForestGreen;

                }
                DbTrans.Commit();
                showVesselshare();
                showAllowable();
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnEditDisagreed_Click(object sender, EventArgs e)
        {
            try
            {
                var db = new VesselAgreement();

                var EditDisagreed = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                                     where q.VoyageID == ddlvoyageid.SelectedValue && q.VSAParticipantCustomerID == ddlcustomerid.SelectedValue && q.Application_Status == "DisAgreed"
                                     select new
                                     {
                                         q.Load_40Feet_Containers_AllowableforCargoOperator,
                                         q.Load_20Feet_Containers_AllowableforCargoOperator,
                                         q.Discharge_40Feet_Containers_AllowableforCargoOperator,
                                         q.Discharge_20Feet_Containers_AllowableforCargoOperator,
                                         q.NetTEUSAllowedforVSAParticipantforVoyageSegment,
                                         q.LoadTEU_AllowableforCargoOperator,
                                         q.DischargeTEU_AllowableforCargoOperator,
                                     }).ToList();

                for (int i = 0; i < EditDisagreed.Count; i++)
                {

                    TextBox TxtAccepted40Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Load");
                    TextBox TxtAccepted20Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Load");
                    TextBox TxtAccepted40Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Disch");
                    TextBox TxtAccepted20Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Disch");
                    TextBox TxtVSAnotestodisplay = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSAnotestodisplay");
                    TextBox TxtVSANotes = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSANotes");
                    Label lblStatus = (Label)ApprovedSummary.Items[i].FindControl("lblStatus");
                    HiddenField TxtEqvlntNetTEUsApproved = (HiddenField)Teus.Items[i].FindControl("TxtEqvlntNetTEUsApproved");
                    HiddenField TxtEqvlntAcceptedDischarge = (HiddenField)Teus.Items[i].FindControl("TxtEqvlntAcceptedDischarge");
                    HiddenField TxtEqvlntAcceptedload = (HiddenField)Teus.Items[i].FindControl("TxtEqvlntAcceptedload");


                    TxtAccepted40Load.Visible = true;
                    TxtAccepted20Load.Visible = true;
                    TxtAccepted40Disch.Visible = true;
                    TxtAccepted20Disch.Visible = true;
                    TxtVSANotes.Visible = true;
                    TxtVSAnotestodisplay.Visible = false;

                    Label lblAccepted40Load = (Label)Teus.Items[i].FindControl("lblAccepted40Load");
                    Label lblAccepted20Load = (Label)Teus.Items[i].FindControl("lblAccepted20Load");
                    Label lblAccepted40Disch = (Label)Teus.Items[i].FindControl("lblAccepted40Disch");
                    Label lblAccepted20Disch = (Label)Teus.Items[i].FindControl("lblAccepted20Disch");
                    Label lblNetTEUsApp = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApp");
                    Label lblNetTEUsAppTeus = (Label)Teus.Items[i].FindControl("lblNetTEUsApp");
                    Label lblNetTEUsApproved = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApproved");
                    Label lblNetTEUsApprovedTeus = (Label)Teus.Items[i].FindControl("lblNetTEUsApproved");

                    TxtAccepted40Load.Text = Convert.ToString(EditDisagreed[i].Load_40Feet_Containers_AllowableforCargoOperator);
                    TxtAccepted20Load.Text = Convert.ToString(EditDisagreed[i].Load_20Feet_Containers_AllowableforCargoOperator);
                    TxtAccepted40Disch.Text = Convert.ToString(EditDisagreed[i].Discharge_40Feet_Containers_AllowableforCargoOperator);
                    TxtAccepted20Disch.Text = Convert.ToString(EditDisagreed[i].Discharge_20Feet_Containers_AllowableforCargoOperator);
                    lblNetTEUsApproved.Text = Convert.ToString(EditDisagreed[i].NetTEUSAllowedforVSAParticipantforVoyageSegment);
                    lblNetTEUsApprovedTeus.Text = Convert.ToString(EditDisagreed[i].NetTEUSAllowedforVSAParticipantforVoyageSegment);
                    TxtEqvlntNetTEUsApproved.Value = Convert.ToString(EditDisagreed[i].NetTEUSAllowedforVSAParticipantforVoyageSegment);
                    TxtEqvlntAcceptedload.Value = Convert.ToString(EditDisagreed[i].LoadTEU_AllowableforCargoOperator);
                    TxtEqvlntAcceptedDischarge.Value = Convert.ToString(EditDisagreed[i].DischargeTEU_AllowableforCargoOperator);

                    lblAccepted40Load.Visible = false;
                    lblAccepted20Load.Visible = false;
                    lblAccepted40Disch.Visible = false;
                    lblAccepted20Disch.Visible = false;
                    lblNetTEUsApp.Visible = false;
                    lblNetTEUsAppTeus.Visible = false;
                    lblNetTEUsApproved.Visible = true;
                    lblNetTEUsApprovedTeus.Visible = true;


                }
                btnApprove.Visible = true;
                Btncancel.Visible = true;
                BtnEditDisagreed.Visible = false;
                tblDetails.Visible = true;

                showVesselshare();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method showApproved(): Use : To display the details of the approved VSA
        public void showApproved()
        {
            var db = new VesselAgreement();

            try
            {
                string custid = Convert.ToString(Session["CustomerID"]);
                //var approvedload = db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == portidvoyageseq).ToList().Sum(x => x.LoadTEU_AllowableforCargoOperator);

                //var approveddischarge = db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == portidvoyageseq).ToList().Sum(x => x.DischargeTEU_AllowableforCargoOperator);


                var TotalVSA = (from b in db.VSA_Txn_Participant_Review_and_Approved
                                join u in db.VSA_Config_Port on b.OriginTransitPortID equals u.PortID
                                join s in db.VSA_Txn_VesselVoyageTransitShipRoute on b.VoyageID equals s.VoyageID
                                join t in db.VSA_Config_Port on s.DestinationTransitPortID equals t.PortID
                                join c in db.VSA_Master_Customer on b.VSAParticipantCustomerID equals c.CustomerID
                                join p in db.VSA_Txn_Invite on b.VoyageID equals p.VoyageID
                                join q in db.VSA_Txn_Participant_Application on b.VoyageID equals q.VoyageID
                                where b.VoyageID == ddlvoyageid.SelectedValue
                                where s.VoyageID == ddlvoyageid.SelectedValue
                                where p.VoyageID == ddlvoyageid.SelectedValue
                                where q.VoyageID == ddlvoyageid.SelectedValue
                                where s.VoyageSegmentSequenceNumber == b.VoyageSegmentSequenceNumber
                                where p.VoyageSegmentSequenceNumber == b.VoyageSegmentSequenceNumber
                                where q.VoyageSegmentSequencenumber == b.VoyageSegmentSequenceNumber
                                where b.VSAParticipantCustomerID == custid
                                where c.CustomerID == custid
                                where q.VSAParticipantCustomerID == custid
                                select new
                                {
                                    VoyageSegmentSequencenumber = b.VoyageSegmentSequenceNumber,
                                    shiporignport = u.PortName,
                                    DestinationPortID = t.PortName,
                                    InviteVSAParticipantsFlag = b.VSA_Partcipant_Agreement_ID,//p.InviteVSAParticipantsFlag,
                                    CargoOperatorAgentName = c.CompanyName,
                                    InitialAvailableSpaceTEU = p.AvailableSpaceTEU,
                                    AvailableSpaceTEU = (p.AvailableSpaceTEU - (db.VSA_Txn_Participant_Review_and_Allowable.Where(x => x.VoyageID == ddlvoyageid.SelectedValue && x.VoyageSegmentSequenceNumber == q.VoyageSegmentSequencenumber).ToList().Sum(x => (int?)x.NetTEUSAllowedforVSAParticipantforVoyageSegment))),
                                    lblload40inch = q.Load_40Feet_Containers_Apply,
                                    lblload20inch = q.Load_20Feet_Containers_Apply,
                                    lbllblTEUsLoaded = q.LoadTEU_Apply,
                                    lblAccepted40Load = b.Load_40Feet_Containers_ApprovedforCargoOperator,
                                    lblAccepted20Load = b.Load_20Feet_Containers_ApprovedforCargoOperator,
                                    lblAllowLoad = b.LoadTEU_ApprovedforCargoOperator,
                                    lbldis40inch = q.Discharge_40Feet_Containers_Apply,
                                    lbldis20inch = q.Discharge_20Feet_Containers_Apply,
                                    lbllblTEUsDischarged = q.DischargeTEU_Apply,
                                    lblAccepted40Disch = b.Discharge_40Feet_Containers_ApprovedforCargoOperator,
                                    lblAccepted20Disch = b.Discharge_20Feet_Containers_ApprovedforCargoOperator,
                                    lblAllowDisch = b.DischargeTEU_ApprovedforCargoOperator,
                                    NetTEUSowned = q.NetTEUSownedByVSAParticipantforVoyageSegment,
                                    lblNetTEUsApp = b.NetTEUSApprovedforVSAParticipantforVoyageSegment,
                                    VSAnotes = b.VSANotes,
                                    lblAppStatus = b.Application_Status,
                                    ApplyVSANotes = b.VSANotes,
                                    lblload = "",
                                    txt40load = "",
                                    txt20load = "",
                                }).OrderBy(x => x.VoyageSegmentSequencenumber).ToList();

                btnApprove.Visible = false;
                Btncancel.Visible = false;
                lnkAgree.Visible = false;
                lnkDisAgree.Visible = false;
                tblDetails.Visible = false;

                VoyageInvitation.DataSource = TotalVSA;
                VoyageInvitation.DataBind();

                Teus.DataSource = TotalVSA;
                Teus.DataBind();

                ApprovedSummary.DataSource = TotalVSA;
                ApprovedSummary.DataBind();

                for (int i = 0; i < Teus.Items.Count; i++)
                {
                    Label lblAccepted40Load = (Label)Teus.Items[i].FindControl("lblAccepted40Load");
                    Label lblAccepted20Load = (Label)Teus.Items[i].FindControl("lblAccepted20Load");
                    Label lblAccepted40Disch = (Label)Teus.Items[i].FindControl("lblAccepted40Disch");
                    Label lblAccepted20Disch = (Label)Teus.Items[i].FindControl("lblAccepted20Disch");
                    Label lblAllowDisch = (Label)Teus.Items[i].FindControl("lblAllowDisch");
                    Label lblAllowLoad = (Label)Teus.Items[i].FindControl("lblAllowLoad");
                    Label lblNetTEUsApp = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApp");
                    Label lblNetTEUsAppTeus = (Label)Teus.Items[i].FindControl("lblNetTEUsApp");
                    Label lblAppStatus = (Label)ApprovedSummary.Items[i].FindControl("lblStatus");
                    TextBox TxtVSAnotestodisplay = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSAnotestodisplay");

                    lblAccepted40Load.Visible = true;
                    lblAccepted20Load.Visible = true;
                    lblAccepted40Disch.Visible = true;
                    lblAccepted20Disch.Visible = true;
                    lblAllowLoad.Visible = true;
                    lblAllowDisch.Visible = true;
                    lblNetTEUsApp.Visible = true;
                    lblNetTEUsAppTeus.Visible = true;
                    lblAppStatus.Visible = true;
                    TxtVSAnotestodisplay.Visible = true;

                    TextBox TxtAccepted40Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Load");
                    TextBox TxtAccepted20Load = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Load");
                    Label lblAcceptedload = (Label)Teus.Items[i].FindControl("lblAcceptedload");
                    TextBox TxtAccepted40Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Disch");
                    TextBox TxtAccepted20Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Disch");
                    Label lblAcceptedDischarge = (Label)Teus.Items[i].FindControl("lblAcceptedDischarge");
                    Label lblNetTEUsApproved = (Label)ApprovedSummary.Items[i].FindControl("lblNetTEUsApproved");
                    Label lblNetTEUsApprovedTeus = (Label)Teus.Items[i].FindControl("lblNetTEUsApproved");
                    TextBox TxtVSANotes = (TextBox)ApprovedSummary.Items[i].FindControl("TxtVSANotes");


                    TxtAccepted40Load.Visible = false;
                    TxtAccepted20Load.Visible = false;
                    lblAcceptedload.Visible = false;
                    TxtAccepted40Disch.Visible = false;
                    TxtAccepted20Disch.Visible = false;
                    lblAcceptedDischarge.Visible = false;
                    lblNetTEUsApproved.Visible = false;
                    lblNetTEUsApprovedTeus.Visible = false;
                    TxtVSANotes.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlcustomerid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                showReviewdtls();
                showVesselshare();

                for (int i = 0; i < Teus.Items.Count; i++)
                {

                    TextBox TxtAccepted40Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted40Disch");
                    TextBox TxtAccepted20Disch = (TextBox)Teus.Items[i].FindControl("TxtAccepted20Disch");

                    TxtAccepted40Disch.Attributes.Add("readonly", "readonly");
                    TxtAccepted20Disch.Attributes.Add("readonly", "readonly");
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void Charges()
        {
            var db = new VesselAgreement();

            var Charges = (from q in db.VSA_Txn_Participant_Review_and_Allowable
                           join p in db.VSA_Txn_Invite on q.VoyageID equals p.VoyageID
                           join r in db.VSA_Txn_VesselVoyageTransitShipRoute on q.VoyageID equals r.VoyageID
                           join u in db.VSA_Config_Port on r.OriginTransitPortID equals u.PortID
                           join f in db.VSA_Config_Port on r.DestinationTransitPortID equals f.PortID
                           where r.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber
                           where p.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber
                           where q.VoyageID == ddlvoyageid.SelectedValue
                           where q.VSAParticipantCustomerID == custid
                           select new
                           {
                               lblTransitPort = u.PortName + " - " + f.PortName,
                               lblNetTeus = q.NetTEUSAllowedforVSAParticipantforVoyageSegment,
                               lblEquivalent40FtContainers = q.Net_40Feet_Containers_AllowableforCargoOperator,
                               lblEquivalent20FtContainers = q.Net_20Feet_Containers_AllowableforCargoOperator,
                               lblPricePerTEU = p.PricePerTEU,
                               lblPriceby40ftcontainers = p.PricePer40FeetContainer,//q.Net_40Feet_Containers_AllowableforCargoOperator * p.PricePer40FeetContainer,
                               lblPriceby20ftcontainers = p.PricePer20FeetContainer,//q.Net_20Feet_Containers_AllowableforCargoOperator * p.PricePer20FeetContainer,
                               lblTotalprice = (q.NetTEUSAllowedforVSAParticipantforVoyageSegment * p.PricePerTEU) + ((q.Net_40Feet_Containers_AllowableforCargoOperator * p.PricePer40FeetContainer) + (q.Net_20Feet_Containers_AllowableforCargoOperator * p.PricePer20FeetContainer)),
                               
                           }).ToList();

            RptCharges.DataSource = Charges;
            RptCharges.DataBind();
        }
    }
}