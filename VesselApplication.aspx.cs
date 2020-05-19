using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;
namespace VesselSharingAgreement
{
    public partial class VesselApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlvessel();
            }
            lblmsg.Text = string.Empty;
            lblapplyMsg.Text = string.Empty;
        }

        // Method ddlvessel(): Use : To Populate vessel ids in the vessel drop down from the vessel_voyage table
        public void ddlvessel()
        {
            try
            {
                var db = new VesselAgreement();

                var vesselidName = (from q in db.VSA_Master_Vessel
                                    join p in db.VSA_Txn_VesselVoyage on q.VesselID equals p.VesselID
                                    where p.VesselID == q.VesselID
                                    select new
                                    {
                                        vesselidname = p.VesselID + "-" + q.NameoftheVessel,
                                       vesselid = p.VesselID,
                                    }).ToList().Distinct();



                //     ddlvesselid.DataSource = db.VSA_Txn_VesselVoyage.GroupBy(x => x.VesselID, (key, group) => group.FirstOrDefault()).ToArray();
                ddlvesselid.DataSource = vesselidName.OrderBy(x => x.vesselidname);
                ddlvesselid.DataTextField = "vesselidName";
                ddlvesselid.DataValueField = "vesselid";
                ddlvesselid.DataBind();
                ddlvesselid.Items.Insert(0, new ListItem("Select Vessel Id", "0"));
                //ddlapplyRoute.Items.Insert(0, new ListItem("--Select Route--", "0"));
                ddlvoyage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlvoyage(): Use : To Populate voyage ids in the vessel voyage drop down from the vessel_voyage table
        public void ddlvoyage()
        {
            try
            {
                var db = new VesselAgreement();
                var vesselvar = Convert.ToString(ddlvesselid.SelectedValue);
                var voyid = (from q in db.VSA_Txn_VesselVoyage
                             where q.VesselID == vesselvar
                             select new { q.VoyageID }).ToList();

                ddlvoyageid.DataSource = voyid.OrderBy(x => x.VoyageID);
                ddlvoyageid.DataTextField = "VoyageID";
                ddlvoyageid.DataValueField = "VoyageID";
                ddlvoyageid.DataBind();
                ddlvoyageid.Items.Insert(0, new ListItem("Select Voyage Id", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void showvesseldetails()
        {
            try
            {
                var db = new VesselAgreement();

                var vvd = (from q in db.VSA_Master_Vessel
                           where q.VesselID==ddlvesselid.SelectedValue
                           select new
                           {
                               q.NameoftheVessel,
                               q.VesselCapacityTEU,
                           }).SingleOrDefault();
                if (ddlvesselid.SelectedValue != "0")
                {
                    TxtCapacityTEUs.Text = Convert.ToString(vvd.VesselCapacityTEU);
                    TxtVeselName.Text = vvd.NameoftheVessel;
                }
                else
                {
                    String strAppMsg = ConfigurationManager.AppSettings["VAselectvesselandvoyage"];
                    lblmsg.Text = strAppMsg;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method showVesselshare(): Use : To display vessel voyage details for the selected vessel
        public void showVoyageDetails()
        {
            try
            {
                var db = new VesselAgreement();
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
                if(ddlvoyageid.SelectedValue!="0")
                { 
                TxtCapacityTEUs.Text = Convert.ToString(vvd.VesselCapacityTEU);
                TxtDestinatioPort.Text = vvd.destiport;
                TxtOrigiPort.Text = vvd.PortName;
                TxtVeselName.Text = vvd.NameoftheVessel;
                    
                }
                else
                {
                    String strAppMsg = ConfigurationManager.AppSettings["VAselectvesselandvoyage"];
                    lblmsg.Text = strAppMsg;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method Availableshiproutes(): Use : To display available ship routes details to apply for the ship route

        public void Availableshiproutes()
        {
            var db = new VesselAgreement();
            string custid = Convert.ToString(Session["CustomerID"]);
            try
            {
               var fee = (from q in db.VSA_Master_Customer_and_CustomerTypes
                           join p in db.VSA_Master_VSA_Arrangement_Fee on q.CustomerTypeID equals p.CustomerTypeID
                           where q.CustomerID == custid
                           where p.Month == DateTime.Now.Month.ToString()
                           where p.Year == DateTime.Now.Year
                           select new { p.Month, p.Year, p.VSAArrangementFeePerTEU }).ToList().Min(x => x.VSAArrangementFeePerTEU);

                var TodisplayInapply = (from q in db.VSA_Txn_Invite
                            let a = db.VSA_Txn_Participant_Application.FirstOrDefault(a => a.VoyageID == q.VoyageID && a.VoyageSegmentSequencenumber == q.VoyageSegmentSequenceNumber
                                    && a.VSAParticipantCustomerID == custid)
                            let d = db.VSA_Txn_VesselVoyageTransitShipRoute.FirstOrDefault(s => s.VoyageID == q.VoyageID && s.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber)
                            join u in db.VSA_Config_Port on d.OriginTransitPortID equals u.PortID
                            join f in db.VSA_Config_Port on d.DestinationTransitPortID equals f.PortID
                            where q.VoyageID == ddlvoyageid.SelectedValue
                            select new
                            {
                                VoyageID = q.VoyageID,
                                VoyageSegmentSequenceNumber = q.VoyageSegmentSequenceNumber,
                                shiporign = u.PortName + "-" + f.PortName,
                                shiporignport = u.PortName,
                                InviteVSAParticipantsFlag = q.InviteVSAParticipantsFlag,
                                MaxNumberofParticipants = q.MaxNumberofParticipants,
                                VSANotes = a.VSANotes != null ? a.VSANotes:q.VSANotes,
                                AppStatus = q.Application_Status,
                                AvailableSpaceTEU = q.AvailableSpaceTEU,
                                TEUsLoaded = q.TEUsLoaded,
                                TEUsDischarged = q.TEUsDischarged,
                                TEUsTotal = q.TEUsTotal,
                                IsPricingbyTEU = q.IsPricingbyTEUorByContainerSize,
                                PricePerTEU = q.PricePerTEU,
                                PricePer40FeetContainer = q.PricePer40FeetContainer,
                                PricePer20FeetContainer = q.PricePer20FeetContainer,
                                VSAArrangementFeePerTEU_VO = fee,
                                Status = a != null ? a.Application_Status : null,
                                lblload40inch = a != null ? a.Load_40Feet_Containers_Apply : 0,
                                lblload20inch= a != null ? a.Load_20Feet_Containers_Apply : 0,
                                lbldis40inch = a != null ? a.Discharge_40Feet_Containers_Apply : 0,
                                lbldis20inch = a != null ? a.Discharge_20Feet_Containers_Apply : 0,
                                lbllblTEUsLoaded= a != null ? a.LoadTEU_Apply : 0,
                                lbllblTEUsDischarged = a != null ? a.DischargeTEU_Apply : 0,
                                lbllblTEUsTotal = a != null ? a.NetTEUSownedByVSAParticipantforVoyageSegment : 0,
                                VSAArrangementFeeStatus = a != null ? a.VSAArrangementFeeAgreed_CO : "Y",
                            }).ToList();

                    apprpt.DataSource = TodisplayInapply;
                    apprpt.DataBind();


                    rptTuesApply.DataSource = TodisplayInapply;
                    rptTuesApply.DataBind();

                    RptCharges.DataSource = TodisplayInapply;
                    RptCharges.DataBind();

                    for (int i = 0; i < rptTuesApply.Items.Count; i++)
                    {
                        string VoyageID = TodisplayInapply[i].VoyageID;
                        int seqno = TodisplayInapply[i].VoyageSegmentSequenceNumber;
                        var ifApplied = (from q in db.VSA_Txn_Participant_Application
                                         where q.VoyageID == VoyageID && q.VoyageSegmentSequencenumber == seqno && q.VSAParticipantCustomerID==custid
                                         select new { }).ToList();
                    if (ifApplied.Count != 0)
                    {
                            Label voseqnum = (Label)rptTuesApply.Items[i].FindControl("lblVoyageSegmentSequenceNum");
                            Label lblinvitation = (Label)apprpt.Items[i].FindControl("lblinvitation");
                            Label shiporignport = (Label)rptTuesApply.Items[i].FindControl("lblshiporignport");
                            TextBox load40inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtload40inch");
                            TextBox load20inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtload20inch");
                            Label TEUsLoaded = (Label)rptTuesApply.Items[i].FindControl("lblTEUsLoaded");
                            TextBox dis40inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtdis40inch");
                            TextBox dis20inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtdis20inch");
                            Label TEUsDischarged = (Label)rptTuesApply.Items[i].FindControl("lblTEUsDischarged");
                            Label TEUsTotal = (Label)rptTuesApply.Items[i].FindControl("lblTEUsTotal");
                            Label VSAArrangementFeePerTEU = (Label)RptCharges.Items[i].FindControl("lblVSAArrangementFeePerTEU_VO");
                            //CheckBox VSAArrangementFeeAgreed = (CheckBox)RptCharges.Items[i].FindControl("CkVSAArrangementFeeAgreed");
                            CheckBox VSAArrangementFeeAgreed = (CheckBox)RptCharges.Items[i].FindControl("CkVSAArrangementFeeAgreed");
                            TextBox TxtVSARemarks = (TextBox)RptCharges.Items[i].FindControl("TxtVSARemarks");
                            TextBox TxtVSAnotestodisplay = (TextBox)RptCharges.Items[i].FindControl("TxtVSAnotestodisplay");
                            Label lblAppStatus = (Label)RptCharges.Items[i].FindControl("lblAppStatus");

                            if(TodisplayInapply[i].VSAArrangementFeeStatus == "N")
                            {
                            VSAArrangementFeeAgreed.Checked = false;
                            VSAArrangementFeeAgreed.Enabled = false;
                            }
                            else
                            {
                            VSAArrangementFeeAgreed.Checked = true;
                            VSAArrangementFeeAgreed.Enabled = false;
                            }
                        
                            load40inch.Visible = false;
                            load20inch.Visible = false;
                            dis40inch.Visible = false;
                            dis20inch.Visible = false;
                            TEUsLoaded.Visible = false;
                            TEUsDischarged.Visible = false;
                            TEUsTotal.Visible = false;
                           
                            TxtVSARemarks.Visible = false;
                            TxtVSAnotestodisplay.Visible = true;
                            lblAppStatus.Visible = false;

                            Label lblload40inch = (Label)rptTuesApply.Items[i].FindControl("lblload40inch");
                            Label lblload20inch = (Label)rptTuesApply.Items[i].FindControl("lblload20inch");
                            Label lbllblTEUsLoaded = (Label)rptTuesApply.Items[i].FindControl("lbllblTEUsLoaded");
                            Label lbldis40inch = (Label)rptTuesApply.Items[i].FindControl("lbldis40inch");
                            Label lbldis20inch = (Label)rptTuesApply.Items[i].FindControl("lbldis20inch");
                            Label lbllblTEUsDischarged = (Label)rptTuesApply.Items[i].FindControl("lbllblTEUsDischarged");
                            Label lbllblTEUsTotal = (Label)rptTuesApply.Items[i].FindControl("lbllblTEUsTotal");
                            Label lblStatus = (Label)RptCharges.Items[i].FindControl("lblStatus");
                            //Button linkapply = (Button)RptCharges.Items[i].FindControl("lnkapply");


                            lblload40inch.Visible = true;
                            lblload20inch.Visible = true;
                            lbllblTEUsLoaded.Visible = true;
                            lbldis40inch.Visible = true;
                            lbldis20inch.Visible = true;
                            lbllblTEUsDischarged.Visible = true;
                            lbllblTEUsTotal.Visible = true;
                            lblStatus.Visible = true;
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                

        // Method ddlvesselid_SelectedIndexChanged: Use : To trigger the vessel id change event in the vessel id dropdown
        protected void ddlvesselid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                ddlvoyage();
                showvesseldetails();
                //ddlapplyRoute.SelectedValue = "0";
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        // Method ddlvoyageid_SelectedIndexChanged: Use : To trigger the voyage id change event in the voyage id dropdown
        protected void ddlvoyageid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                showVoyageDetails();
                Availableshiproutes();
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        // Method lnkapply_Click: Use : To trigger the apply button click event to apply for a VSA route
        int load40=0, load20=0, disc40=0, disc20=0, netload, netdisc, net, Discvoyageseq, Loadvoyageseq, Maxparticipantsvoyageseq;
        string MaxparticipantsLimit,Originport;

        protected void lnkapply_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            {
               
                string custid = Convert.ToString(Session["CustomerID"]);

                for (int i = 0; i < rptTuesApply.Items.Count; i++)
                {
                    
                    Label voseqnum = (Label)rptTuesApply.Items[i].FindControl("lblVoyageSegmentSequenceNum");
                    Label shipRoute = (Label)rptTuesApply.Items[i].FindControl("lblshiporignport");
                    TextBox load40inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtload40inch");
                    TextBox load20inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtload20inch");
                    Label TEUsLoaded = (Label)rptTuesApply.Items[i].FindControl("lblTEUsLoaded");
                    TextBox dis40inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtdis40inch");
                    TextBox dis20inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtdis20inch");
                    Label lblinvitation = (Label)apprpt.Items[i].FindControl("lblinvitation");

                    int voseqnumber = Convert.ToInt32(voseqnum.Text);
                    
                    var previouslyapplied = (from q in db.VSA_Txn_Participant_Application
                                             where q.VoyageID == ddlvoyageid.SelectedValue
                                             where q.VoyageSegmentSequencenumber == voseqnumber
                                             where q.VSAParticipantCustomerID == custid
                                             select new { q.VoyageSegmentSequencenumber }).ToList();
                    if (previouslyapplied.Count == 0)
                    {
                        var maxparticipants = (from q in db.VSA_Txn_Invite
                                               where q.VoyageID == ddlvoyageid.SelectedValue
                                               where q.VoyageSegmentSequenceNumber == voseqnumber
                                               select new { q.MaxNumberofParticipants, }).SingleOrDefault();

                        var appliedparticipants = (from q in db.VSA_Txn_Participant_Application
                                                   where q.VoyageID == ddlvoyageid.SelectedValue
                                                   where q.VoyageSegmentSequencenumber == voseqnumber
                                                   select new { }).ToList();
                        
                        if (appliedparticipants.Count() < maxparticipants.MaxNumberofParticipants || appliedparticipants.Count() == 0 && lblinvitation.Text == "N")
                        {
                            MaxparticipantsLimit = "Y";

                            if (load40inch.Text.Trim() != string.Empty)
                            {
                                if(load40inch.Text != string.Empty)
                                { 
                                load40 = load40 + Convert.ToInt32(load40inch.Text);
                                }
                                else
                                {
                                    load40 = load40 + 0;
                                }
                                if (Loadvoyageseq == 0)
                                {
                                    Loadvoyageseq = Convert.ToInt32(voseqnum.Text.Trim());
                                }
                            }
                            else
                            {
                                load40 = load40 + 0;
                            }
                            if (load20inch.Text.Trim() != string.Empty)
                            {
                                load20 = load20 + Convert.ToInt32(load20inch.Text);
                                if (Loadvoyageseq == 0)
                                {
                                    Loadvoyageseq = Convert.ToInt32(voseqnum.Text.Trim());
                                }
                            }
                            else
                            {
                                load20 = load20 + 0;
                            }

                            netload = ((load40) * 2) + (load20);

                            if (dis20inch.Text.Trim() != string.Empty)
                            {
                                disc20 = disc20 + Convert.ToInt32(dis20inch.Text);
                                Discvoyageseq = Convert.ToInt32(voseqnum.Text.Trim());
                            }
                            else
                            {
                                disc20 = disc20 + 0;
                            }
                            if (dis40inch.Text.Trim() != string.Empty)
                            {
                                disc40 = disc40 + Convert.ToInt32(dis40inch.Text);
                                Discvoyageseq = Convert.ToInt32(voseqnum.Text.Trim());
                            }
                            else
                            {
                                disc40 = disc40 + 0;
                            }
                            netdisc = (disc40 * 2) + disc20;

                            net = netload - netdisc;
                        }
                        else
                        {
                            Originport = shipRoute.Text.Trim().Split('-')[0];
                            Maxparticipantsvoyageseq = voseqnumber;
                        }
                    }
                    
                }

                if(Maxparticipantsvoyageseq > Loadvoyageseq && Maxparticipantsvoyageseq <Discvoyageseq)
                {
                    MaxparticipantsLimit = "N";
                }


                if (MaxparticipantsLimit != "N")
                {
                    for (int i = 0; i < rptTuesApply.Items.Count; i++)
                    {
                        
                        var apply = new VSA_Txn_Participant_Application();

                        Label voseqnum = (Label)rptTuesApply.Items[i].FindControl("lblVoyageSegmentSequenceNum");
                        Label shipRoute = (Label)rptTuesApply.Items[i].FindControl("lblshiporignport");
                        Label shiporignport = (Label)rptTuesApply.Items[i].FindControl("lblshiporign");
                        Label lblAvailableSpaceTEU = (Label)rptTuesApply.Items[i].FindControl("lblAvailableSpaceTEU");
                        TextBox load40inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtload40inch");
                        TextBox load20inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtload20inch");
                        Label TEUsLoaded = (Label)rptTuesApply.Items[i].FindControl("lblTEUsLoaded");
                        TextBox dis40inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtdis40inch");
                        TextBox dis20inch = (TextBox)rptTuesApply.Items[i].FindControl("Txtdis20inch");
                        Label TEUsDischarged = (Label)rptTuesApply.Items[i].FindControl("lblTEUsDischarged");
                        Label TEUsTotal = (Label)rptTuesApply.Items[i].FindControl("lblTEUsTotal");

                        Label VSAArrangementFeePerTEU = (Label)RptCharges.Items[i].FindControl("lblVSAArrangementFeePerTEU_VO");
                        CheckBox VSAArrangementFeeAgreed = (CheckBox)RptCharges.Items[i].FindControl("CkVSAArrangementFeeAgreed");
                        //HtmlInputCheckBox VSAArrangementFeeAgreed = (HtmlInputCheckBox)RptCharges.Items[i].FindControl("CkVSAArrangementFeeAgreed");
                        TextBox TxtVSARemarks = (TextBox)RptCharges.Items[i].FindControl("TxtVSARemarks");
                        Label lblinvitation = (Label)apprpt.Items[i].FindControl("lblinvitation");

                        int voseq = Convert.ToInt32(voseqnum.Text);

                        //if (net == 0)
                        //{
                        //    if (voseq >= Loadvoyageseq && voseq <= Discvoyageseq)
                        //    {
                                var previouslyapplied = (from q in db.VSA_Txn_Participant_Application
                                                         where q.VoyageID == ddlvoyageid.SelectedValue
                                                         where q.VoyageSegmentSequencenumber == voseq
                                                         where q.VSAParticipantCustomerID == custid
                                                         select new { q.VoyageSegmentSequencenumber }).ToList();
                                if (previouslyapplied.Count == 0)
                                {
                                    var maxparticipants = (from q in db.VSA_Txn_Invite
                                                           where q.VoyageID == ddlvoyageid.SelectedValue
                                                           where q.VoyageSegmentSequenceNumber == voseq
                                                           select new { q.MaxNumberofParticipants }).SingleOrDefault();

                                    var appliedparticipants = (from q in db.VSA_Txn_Participant_Application
                                                               where q.VoyageID == ddlvoyageid.SelectedValue
                                                               where q.VoyageSegmentSequencenumber == voseq
                                                               select new { }).ToList();

                                    if (appliedparticipants.Count() < maxparticipants.MaxNumberofParticipants || appliedparticipants.Count() ==0 && lblinvitation.Text=="N")
                                    {
                                        //if (voseqnum.Text.Trim() == Convert.ToString(id))
                                        //{
                                        apply.VoyageID = ddlvoyageid.SelectedValue;
                                        apply.VoyageSegmentSequencenumber = Convert.ToInt32(voseqnum.Text);
                                        apply.VSAParticipantCustomerID = Convert.ToString(Session["CustomerID"]);

                                        var originport = (from q in db.VSA_Config_Port
                                                          where q.PortName == shiporignport.Text
                                                          select new { q.PortID }).SingleOrDefault();

                                        apply.OriginTransitDestinationPortID = originport.PortID;
                                        if (load40inch.Text.Trim() != string.Empty)
                                        {
                                            apply.Load_40Feet_Containers_Apply = Convert.ToInt32(load40inch.Text);
                                        }
                                        else
                                        {
                                            apply.Load_40Feet_Containers_Apply = 0;
                                            //lblapplyMsg.Text = "40' Load can not be empty";
                                            //lblapplyMsg.ForeColor = System.Drawing.Color.Red;
                                            //break;
                                        }
                                        if (load20inch.Text.Trim() != string.Empty)
                                        {
                                            apply.Load_20Feet_Containers_Apply = Convert.ToInt32(load20inch.Text);
                                        }
                                        else
                                        {
                                            apply.Load_20Feet_Containers_Apply = 0;
                                            //lblapplyMsg.Text = "20' Load can not be empty";
                                            //lblapplyMsg.ForeColor = System.Drawing.Color.Red;
                                            //break;
                                        }

                                        apply.LoadTEU_Apply = ((apply.Load_40Feet_Containers_Apply) * 2) + (apply.Load_20Feet_Containers_Apply);

                                        if (dis20inch.Text.Trim() != string.Empty)
                                        {
                                            apply.Discharge_20Feet_Containers_Apply = Convert.ToInt32(dis20inch.Text);
                                        }
                                        else
                                        {
                                            apply.Discharge_20Feet_Containers_Apply = 0;
                                            //lblapplyMsg.Text = "20' Discharge can not be empty";
                                            //lblapplyMsg.ForeColor = System.Drawing.Color.Red;
                                            //break;
                                        }
                                        if (dis40inch.Text.Trim() != string.Empty)
                                        {
                                            apply.Discharge_40Feet_Containers_Apply = Convert.ToInt32(dis40inch.Text);
                                        }
                                        else
                                        {
                                            apply.Discharge_40Feet_Containers_Apply = 0;
                                            //lblapplyMsg.Text = "40' Discharge can not be empty";
                                            //lblapplyMsg.ForeColor = System.Drawing.Color.Red;
                                            //break;
                                        }
                                        apply.DischargeTEU_Apply = (apply.Discharge_40Feet_Containers_Apply * 2) + apply.Discharge_20Feet_Containers_Apply;

                                        apply.VSAArrangementFeePerTEU_CO = Convert.ToInt32(VSAArrangementFeePerTEU.Text);
                                        var previousnetload = (from q in db.VSA_Txn_Participant_Application
                                                               where q.VoyageID == ddlvoyageid.SelectedValue
                                                               where q.VSAParticipantCustomerID == custid
                                                               where q.NetTEUSownedByVSAParticipantforVoyageSegment == apply.DischargeTEU_Apply
                                                               select new { }).ToList();
                                        var previousload = (from q in db.VSA_Txn_Participant_Application
                                                            where q.VoyageID == ddlvoyageid.SelectedValue
                                                            where q.VSAParticipantCustomerID == custid
                                                            select new { q.VoyageSegmentSequencenumber, q.LoadTEU_Apply, q.DischargeTEU_Apply }).ToList();
                                        var prevload = previousload.Where(x => x.VoyageSegmentSequencenumber < apply.VoyageSegmentSequencenumber).Sum(x => x.LoadTEU_Apply - x.DischargeTEU_Apply);

                                        apply.NetTEUSownedByVSAParticipantforVoyageSegment = (apply.LoadTEU_Apply + prevload) - apply.DischargeTEU_Apply;

                                        if (apply.NetTEUSownedByVSAParticipantforVoyageSegment < 0)
                                        {
                                            apply.NetTEUSownedByVSAParticipantforVoyageSegment = -(apply.NetTEUSownedByVSAParticipantforVoyageSegment);
                                        }

                                        //apply.NetTEUSownedByVSAParticipantforVoyageSegment = apply.NetTEUSownedByVSAParticipantforVoyageSegment - apply.DischargeTEU_Apply;

                                        //if (apply.NetTEUSownedByVSAParticipantforVoyageSegment < 0)
                                        //{
                                        //    apply.NetTEUSownedByVSAParticipantforVoyageSegment = -(apply.NetTEUSownedByVSAParticipantforVoyageSegment);
                                        //}

                                        apply.Application_Status = "Submitted";

                                        var notes = (from q in db.VSA_Txn_Invite
                                                     where q.VoyageID == ddlvoyageid.SelectedValue
                                                     where q.VoyageSegmentSequenceNumber == apply.VoyageSegmentSequencenumber
                                                     select new { q.VSANotes }).SingleOrDefault();

                                        apply.VSANotes = notes.VSANotes + Environment.NewLine + "#Date:" + DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss") + "#CustID:" + custid + "# Comments:" + TxtVSARemarks.Text;
                                        apply.Create_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                                        apply.Update_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                                if (VSAArrangementFeeAgreed.Checked)
                                {
                                    apply.VSAArrangementFeeAgreed_CO = "Y";
                                }
                                else
                                {
                                    apply.VSAArrangementFeeAgreed_CO = "N";
                                }
                                            //var applyupto = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                            //                 where q.VoyageID == ddlvoyageid.SelectedValue
                                            //                 where q.VoyageSegmentSequenceNumber == id
                                            //                 select new { q.ExpectedDepartureDateTime }).SingleOrDefault();

                                            //DateTime date = applyupto.ExpectedDepartureDateTime.AddDays(3);

                                            //if (DateTime.Now <= applyupto.ExpectedDepartureDateTime)
                                            //{ 
                                            if (Convert.ToInt32(lblAvailableSpaceTEU.Text) >= Convert.ToInt32(apply.NetTEUSownedByVSAParticipantforVoyageSegment))
                                            {


                                                if (prevload >= apply.DischargeTEU_Apply)
                                                {
                                                    db.VSA_Txn_Participant_Application.Add(apply);
                                                    db.SaveChanges();
                                                    
                                           
                                                if (i+1  == rptTuesApply.Items.Count)
                                                {
                                                        DbTrans.Commit();
                                                        String strAppMsg = ConfigurationManager.AppSettings["VAapply"];// + " " + shipRoute.Text;
                                                        lblapplyMsg.Text = strAppMsg;
                                                        lblapplyMsg.ForeColor = System.Drawing.Color.ForestGreen;
                                                        showvesseldetails();
                                                        showVoyageDetails();
                                                        Availableshiproutes();

                                                    }

                                                }
                                                else
                                                {
                                                    showvesseldetails();
                                                    showVoyageDetails();
                                                    String strAppMsg = ConfigurationManager.AppSettings["VAcannotdischarge"];
                                                    lblapplyMsg.Text = strAppMsg;
                                                    lblapplyMsg.ForeColor = System.Drawing.Color.Red;
                                                }
                                            }
                                            else
                                            {
                                                showvesseldetails();
                                                showVoyageDetails();
                                                String strAppMsg = ConfigurationManager.AppSettings["VAcannotloadmrfreespc"];
                                                lblmsg.Text = strAppMsg;
                                                lblmsg.ForeColor = System.Drawing.Color.Red;
                                            }
                                       }
                                }
                            //}
                        //}
                        //else
                        //{
                        //    lblapplyMsg.Text = "All the Loaded TEUs need to be discharged fully to apply";
                        //    lblapplyMsg.ForeColor = System.Drawing.Color.Red;
                        //    showvesseldetails();
                        //    showVoyageDetails();
                        //    Availableshiproutes();
                        //}
                    }
                    //DbTrans.Commit();
                }
                else
                {
                    lblapplyMsg.Text = "The limit for maximum no of applications for " + Originport + " has reached you cannot apply for this route through this port";
                    lblapplyMsg.ForeColor = System.Drawing.Color.Red;
                    showvesseldetails();
                    showVoyageDetails();
                    Availableshiproutes();
                }

                //}
                //else
                //{
                //    showvesseldetails();
                //    showVoyageDetails();
                //    String strAppMsg = ConfigurationManager.AppSettings["VAmaxnopartic"];
                //    lblapplyMsg.Text = strAppMsg;
                //    lblapplyMsg.ForeColor = System.Drawing.Color.Red;
                //}
                
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}