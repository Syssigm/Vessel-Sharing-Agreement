using System;
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
    public partial class VsaInvite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlvessel();
            }
            lblmsg.Text = string.Empty;
            lblerrfreespace.Text = string.Empty;
        }

        // Method ddlvessel() use : This is to populate the vesselvoyage ids based on vessel selection
        public void ddlvessel()
        {
            try
            {
                var db = new VesselAgreement();

                var Vesselidname = (from q in db.VSA_Txn_VesselVoyage
                                    join p in db.VSA_Master_Vessel on q.VesselID equals p.VesselID
                                    where p.VesselID == q.VesselID
                                    select new
                                    {
                                        vesselidname = p.VesselID + "-" + p.NameoftheVessel,
                                        vesselid = p.VesselID,
                                        
                                    }).ToList().Distinct();

                ddlVesselId.DataSource = Vesselidname.OrderBy(x => x.vesselidname);
                ddlVesselId.DataTextField = "vesselidname";
                ddlVesselId.DataValueField = "vesselid";
                ddlVesselId.DataBind();
                ddlVesselId.Items.Insert(0, new ListItem("Select Vessel Id", "0"));
                ddlVoyageId.Items.Insert(0, new ListItem("Voyage Id", "0"));
                ddlvoyage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlvoyage() use : This is to fetch all the voyage ids based on vessel id selection for which the ship route is completed
        public void ddlvoyage()
        {
            try
            {
                var db = new VesselAgreement();
                if (ddlVesselId.SelectedValue != "0")
                {
                    var voyid = (from q in db.VSA_Txn_VesselVoyage
                                 where q.VesselID == ddlVesselId.SelectedValue
                                 where q.Voyageshiproutecompleteflag == "Y"
                                 select new { q.VoyageID }).ToList();

                    ddlVoyageId.DataSource = voyid.OrderBy(x => x.VoyageID);
                    ddlVoyageId.DataTextField = "VoyageID";
                    ddlVoyageId.DataValueField = "VoyageID";
                    ddlVoyageId.DataBind();
                    ddlVoyageId.Items.Insert(0, new ListItem("Select Voyage Id", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlVesselId_SelectedIndexChanged use : This is to fetch all the voyage ids based on vessel id selection change for which the ship route is completed
        protected void ddlVesselId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlvoyage();
                ddlVoyageId.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method vesselvoyagedtls() use : This method is to get the voyage details based on voyage id selection in the drop down
        public void vesselvoyagedtls()
        {
            try
            {
                var db = new VesselAgreement();

                var vvd = (from q in db.VSA_Txn_VesselVoyage
                           join p in db.VSA_Master_Vessel on q.VesselID equals p.VesselID
                           join r in db.VSA_Config_Port on q.OriginPortID equals r.PortID
                           join s in db.VSA_Config_Port on q.DestinationPortID equals s.PortID
                           where q.VoyageID == ddlVoyageId.SelectedValue
                           select new
                           {
                               p.NameoftheVessel,
                               p.VesselCapacityTEU,
                               r.PortName,
                               destiport = s.PortName,
                               q.OriginloadTEU,
                               q.OriginGrossTonnage,

                           }).FirstOrDefault();

                TxtVesselName.Text = vvd.NameoftheVessel;
                TxtCapacityTEUs.Text = Convert.ToString(vvd.VesselCapacityTEU);
                Session["VesselCapacity"] = vvd.VesselCapacityTEU;
                TxtOriginPort.Text = vvd.PortName;
                TxtDestinationPort.Text = vvd.destiport;
                TxtContainersLoadedatOrigin.Text = Convert.ToString(vvd.OriginloadTEU);
                TxtGrossTonnage.Text = Convert.ToString(vvd.OriginGrossTonnage);

                var shiproute = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                 join u in db.VSA_Config_Port on q.OriginTransitPortID equals u.PortID
                                 join v in db.VSA_Config_Port on q.DestinationTransitPortID equals v.PortID
                                 where q.VoyageID == ddlVoyageId.SelectedValue
                                 select new
                                 {
                                     VoyageSegmentSequenceNumber = q.VoyageSegmentSequenceNumber,
                                     shiporignport = u.PortName,
                                     shipdestiport = v.PortName,
                                     ExpectedArrivalDateTime = q.ExpectedArrivalDateTime,
                                     ExpectedDepartureDateTime = q.ExpectedDepartureDateTime,
                                 }).ToList();
                rpt.DataSource = shiproute;
                rpt.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlVoyageId_SelectedIndexChanged use : Based on change in the voyage id fetch the details of the Voyage
        protected void ddlVoyageId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlVoyageId.SelectedValue != "0")
                {
                    vesselvoyagedtls();
                    showVoyageInvitationDetails();
                }

            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method showVoyageInvitationDetails() use : Based on change in the voyage id fetch the details of the Voyage

        public void showVoyageInvitationDetails()
        {
            try
            {
                var db = new VesselAgreement();
                var invitedornot = (from q in db.VSA_Txn_Invite
                                    where q.VoyageID == ddlVoyageId.SelectedValue
                                    select new { q }).ToList();
                string custid = Convert.ToString(Session["CustomerID"]);
                var fee = (from q in db.VSA_Master_Customer_and_CustomerTypes
                           join p in db.VSA_Master_VSA_Arrangement_Fee on q.CustomerTypeID equals p.CustomerTypeID
                           where q.CustomerID == custid
                           where p.Month == DateTime.Now.Month.ToString()
                           where p.Year == DateTime.Now.Year
                           select new { p.Month, p.Year, p.VSAArrangementFeePerTEU }).ToList().Min(x => x.VSAArrangementFeePerTEU);

                if (invitedornot.Count == 0)
                {
                    var InvitationDetails = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                             join d in db.VSA_Txn_VesselVoyage on q.VoyageID equals d.VoyageID
                                             join p in db.VSA_Master_Vessel on d.VesselID equals p.VesselID
                                             join u in db.VSA_Config_Port on q.OriginTransitPortID equals u.PortID
                                             join f in db.VSA_Config_Port on q.DestinationTransitPortID equals f.PortID
                                             where q.VoyageID == ddlVoyageId.SelectedValue
                                             select new
                                             {
                                                 VoyageSegmentSequenceNumber = q.VoyageSegmentSequenceNumber,
                                                 shiporignport = u.PortName + "-" + f.PortName,
                                                 shiporign = u.PortName,
                                                 VesselCapacityTEU = p.VesselCapacityTEU,
                                                 MaxNumberofParticipants = "0",
                                                 OriginloadTEU = d.OriginloadTEU,
                                                 VSANotes = string.Empty,
                                                 VSAParticipantsFlag = string.Empty,
                                                 freespace = p.VesselCapacityTEU - d.OriginloadTEU,
                                                 lblTEUsonship = string.Empty,
                                                 lblchargesPerTue = string.Empty,
                                                 lbl20Size = string.Empty,
                                                 lbl40Size = string.Empty,
                                                 lblPricePerTue = string.Empty,
                                                 lblTEUsDischarged = string.Empty,
                                                 lblTEUsLoaded = string.Empty,
                                                 lblTotalTEUS = string.Empty,
                                                 lblckFees = string.Empty,
                                                 lblAgreementfee = fee,
                                                 lblAvailabeFreespace = string.Empty,
                                             }).ToList();
                    rptVoyageInvitationDetails.DataSource = InvitationDetails;
                    rptVoyageInvitationDetails.DataBind();

                    rptcharges.DataSource = InvitationDetails;
                    rptcharges.DataBind();

                    rptTEUs.DataSource = InvitationDetails;
                    rptTEUs.DataBind();

                    btnInvite.Visible = true;
                    Btncancel.Visible = true;
                    AllowtoInviteTH.Visible = false;
                    CkVoyage.Visible = true;
                    CkTEUS.Visible = true;
                    CKCharges.Visible = true;
                    lblChargesInComplete.Visible = true;
                    lblChargesComplete.Visible = true;
                    Completed.Visible = true;
                    incomplete.Visible = true;
                    lblTeusComplete.Visible = true;
                }
                else
                {
                    
                    btnInvite.Visible = false;
                    Btncancel.Visible = false;
                    AllowtoInviteTH.Visible = false;
                    CkVoyage.Visible = false;
                    CkTEUS.Visible = false;
                    CKCharges.Visible = false;
                    lblChargesInComplete.Visible = false;
                    lblChargesComplete.Visible = false;
                    Completed.Visible = false;
                    incomplete.Visible = false;
                    lblTeusComplete.Visible = false;

                    var alreadyinvited = (from q in db.VSA_Txn_Invite
                                          join p in db.VSA_Txn_VesselVoyageTransitShipRoute on q.VoyageID equals p.VoyageID
                                          join u in db.VSA_Config_Port on p.OriginTransitPortID equals u.PortID
                                          join f in db.VSA_Config_Port on p.DestinationTransitPortID equals f.PortID
                                          where q.VoyageID == ddlVoyageId.SelectedValue
                                          where q.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber
                                          select new
                                          {
                                              VoyageSegmentSequenceNumber = q.VoyageSegmentSequenceNumber,
                                              shiporignport = u.PortName + "-" + f.PortName,
                                              shiporign = "",
                                              VSAParticipantsFlag = q.InviteVSAParticipantsFlag,
                                              MaxNumberofParticipants = q.MaxNumberofParticipants,
                                              VSANotes = q.VSANotes,
                                              lblAvailabeFreespace = q.AvailableSpaceTEU,
                                              lblTEUsonship = q.BeginTEUsonShip,
                                              lblchargesPerTue = q.IsPricingbyTEUorByContainerSize,
                                              lbl20Size = q.PricePer20FeetContainer,
                                              lbl40Size = q.PricePer40FeetContainer,
                                              lblPricePerTue = q.PricePerTEU,
                                              lblTEUsDischarged = q.TEUsDischarged,
                                              lblTEUsLoaded = q.TEUsLoaded,
                                              lblTotalTEUS = q.TEUsTotal,
                                              lblckFees = q.VSAArrangementFeeAgreed_VO,
                                              lblAgreementfee = fee,
                                              VesselCapacityTEU = "0",
                                          }).ToList();

                    rptVoyageInvitationDetails.DataSource = alreadyinvited;
                    rptVoyageInvitationDetails.DataBind();

                    rptTEUs.DataSource = alreadyinvited;
                    rptTEUs.DataBind();

                    rptcharges.DataSource = alreadyinvited;
                    rptcharges.DataBind();

                    for (int i = 0; i < alreadyinvited.Count; i++)
                    {

                        HtmlInputCheckBox shareParticipants = (HtmlInputCheckBox)rptVoyageInvitationDetails.Items[i].FindControl("ckshareParticipants");
                        TextBox TotalParticipants = (TextBox)rptVoyageInvitationDetails.Items[i].FindControl("TxtTotalParticipants");
                        TextBox VSANotes = (TextBox)rptVoyageInvitationDetails.Items[i].FindControl("TxtVSANotes");

                        shareParticipants.Visible = false;
                        TotalParticipants.Visible = false;
                        VSANotes.Visible = false;

                        Label lblckshareParticipants = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblckshareParticipants");
                        Label lblTotalParticipants = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblTotalParticipants");
                        Label lblVSANotes = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblVSANotes");

                        lblckshareParticipants.Visible = true;
                        lblTotalParticipants.Visible = true;
                        lblVSANotes.Visible = true;


                        TextBox TxtTEUsonship = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsonship");
                        TextBox TxtTEUsDischarged = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsDischarged");
                        TextBox TxtTEUsLoaded = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsLoaded");
                        Label lblsum = (Label)rptTEUs.Items[i].FindControl("lblsum");
                        Label lblFreespace = (Label)rptTEUs.Items[i].FindControl("lblFreespace");

                        TxtTEUsonship.Visible = false;
                        TxtTEUsDischarged.Visible = false;
                        TxtTEUsLoaded.Visible = false;
                        lblsum.Visible = false;
                        lblFreespace.Visible = false;

                        Label lblTEUsonship = (Label)rptTEUs.Items[i].FindControl("lblTEUsonship");
                        Label lblTEUsDischarged = (Label)rptTEUs.Items[i].FindControl("lblTEUsDischarged");
                        Label lblTEUsLoaded = (Label)rptTEUs.Items[i].FindControl("lblTEUsLoaded");
                        Label lblTotalTEUS = (Label)rptTEUs.Items[i].FindControl("lblTotalTEUS");
                        Label lblAvailabeFreespace = (Label)rptTEUs.Items[i].FindControl("lblAvailabeFreespace");

                        lblTEUsonship.Visible = true;
                        lblTEUsDischarged.Visible = true;
                        lblTEUsLoaded.Visible = true;
                        lblTotalTEUS.Visible = true;
                        lblAvailabeFreespace.Visible = true;

                        DropDownList ddlchargesPer = (DropDownList)rptcharges.Items[i].FindControl("ddlchargesPer");
                        TextBox TxtPricePerTue = (TextBox)rptcharges.Items[i].FindControl("TxtPricePerTue");
                        TextBox Txt40Size = (TextBox)rptcharges.Items[i].FindControl("Txt40Size");
                        TextBox Txt20Size = (TextBox)rptcharges.Items[i].FindControl("Txt20Size");
                        CheckBox ckFees = (CheckBox)rptcharges.Items[i].FindControl("ckFees");
                        Label lblAgreementfees = (Label)rptcharges.Items[i].FindControl("lblAgreementfees");

                        ddlchargesPer.Visible = false;
                        TxtPricePerTue.Visible = false;
                        Txt40Size.Visible = false;
                        Txt20Size.Visible = false;
                        ckFees.Visible = false;
                        lblAgreementfees.Visible = false;

                        Label lblchargesPerTue = (Label)rptcharges.Items[i].FindControl("lblchargesPerTue");
                        Label lblPricePerTue = (Label)rptcharges.Items[i].FindControl("lblPricePerTue");
                        Label lbl40Size = (Label)rptcharges.Items[i].FindControl("lbl40Size");
                        Label lbl20Size = (Label)rptcharges.Items[i].FindControl("lbl20Size");
                        Label lblckFees = (Label)rptcharges.Items[i].FindControl("lblckFees");
                        Label lblAgreementfee = (Label)rptcharges.Items[i].FindControl("lblAgreementfee");

                        lblchargesPerTue.Visible = true;
                        lblPricePerTue.Visible = true;
                        lbl40Size.Visible = true;
                        lbl20Size.Visible = true;
                        lblckFees.Visible = true;
                        lblAgreementfee.Visible = true;

                        if (alreadyinvited[i].VSAParticipantsFlag == "N")
                        {
                            Button AllowtoInvite = (Button)rptcharges.Items[i].FindControl("EditAllowtoInvite");
                            HtmlTableCell AllowtoInviteTD = (HtmlTableCell)rptcharges.Items[i].FindControl("AllowtoInviteTD");
                            AllowtoInvite.Visible = true;
                            AllowtoInviteTH.Visible = true;
                            AllowtoInviteTD.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method BtneditAllowtoInvite_Click use : This is to trigger the editing event of the already invited VSA 
        protected void BtneditAllowtoInvite_Click(object sender, EventArgs e)
        {
            try
            {
                vesselvoyagedtls();
                var db = new VesselAgreement();

                int id = int.Parse((sender as Button).CommandArgument);

                var alreadyinvited = (from q in db.VSA_Txn_Invite
                                      join p in db.VSA_Txn_VesselVoyageTransitShipRoute on q.VoyageID equals p.VoyageID
                                      join u in db.VSA_Config_Port on p.OriginTransitPortID equals u.PortID
                                      join f in db.VSA_Config_Port on p.DestinationTransitPortID equals f.PortID
                                      where q.VoyageID == ddlVoyageId.SelectedValue
                                      where q.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber
                                      select new
                                      {
                                          q.InviteVSAParticipantsFlag,
                                      }).ToList();

                for (int i = 0; i < alreadyinvited.Count; i++)
                {

                    if (i + 1 == id)
                    {
                        HtmlInputCheckBox shareParticipants = (HtmlInputCheckBox)rptVoyageInvitationDetails.Items[i].FindControl("ckshareParticipants");
                        TextBox TotalParticipants = (TextBox)rptVoyageInvitationDetails.Items[i].FindControl("TxtTotalParticipants");
                        TextBox VSANotes = (TextBox)rptVoyageInvitationDetails.Items[i].FindControl("TxtVSANotes");

                        shareParticipants.Visible = true;
                        shareParticipants.Checked = false;
                        TotalParticipants.Visible = true;
                        VSANotes.Visible = true;

                        Label lblckshareParticipants = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblckshareParticipants");
                        Label lblTotalParticipants = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblTotalParticipants");
                        Label lblVSANotes = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblVSANotes");

                        lblckshareParticipants.Visible = false;
                        lblTotalParticipants.Visible = false;
                        lblVSANotes.Visible = false;


                        TextBox TxtTEUsonship = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsonship");
                        TextBox TxtTEUsDischarged = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsDischarged");
                        TextBox TxtTEUsLoaded = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsLoaded");
                        Label lblsum = (Label)rptTEUs.Items[i].FindControl("lblsum");
                        Label lblFreespace = (Label)rptTEUs.Items[i].FindControl("lblFreespace");

                        TxtTEUsonship.Visible = false;
                        TxtTEUsDischarged.Visible = false;
                        TxtTEUsLoaded.Visible = false;
                        lblsum.Visible = false;
                        lblFreespace.Visible = false;


                        TextBox edtTxtTEUsDischarged = (TextBox)rptTEUs.Items[i].FindControl("edtTxtTEUsDischarged");
                        TextBox edtTxtTEUsLoaded = (TextBox)rptTEUs.Items[i].FindControl("edtTxtTEUsLoaded");

                        edtTxtTEUsDischarged.Visible = true;
                        edtTxtTEUsLoaded.Visible = true;

                        Label lblTEUsonship = (Label)rptTEUs.Items[i].FindControl("lblTEUsonship");
                        Label lblTEUsDischarged = (Label)rptTEUs.Items[i].FindControl("lblTEUsDischarged");
                        Label lblTEUsLoaded = (Label)rptTEUs.Items[i].FindControl("lblTEUsLoaded");
                        Label lblTotalTEUS = (Label)rptTEUs.Items[i].FindControl("lblTotalTEUS");
                        Label lblAvailabeFreespace = (Label)rptTEUs.Items[i].FindControl("lblAvailabeFreespace");

                        lblTEUsonship.Visible = true;
                        lblTEUsDischarged.Visible = false;
                        lblTEUsLoaded.Visible = false;
                        lblTotalTEUS.Visible = true;
                        lblAvailabeFreespace.Visible = true;

                        DropDownList ddlchargesPer = (DropDownList)rptcharges.Items[i].FindControl("ddlchargesPer");
                        TextBox TxtPricePerTue = (TextBox)rptcharges.Items[i].FindControl("TxtPricePerTue");
                        TextBox Txt40Size = (TextBox)rptcharges.Items[i].FindControl("Txt40Size");
                        TextBox Txt20Size = (TextBox)rptcharges.Items[i].FindControl("Txt20Size");
                        CheckBox ckFees = (CheckBox)rptcharges.Items[i].FindControl("ckFees");
                        Label lblAgreementfees = (Label)rptcharges.Items[i].FindControl("lblAgreementfees");

                        ddlchargesPer.Visible = true;
                        TxtPricePerTue.Visible = true;
                        Txt40Size.Visible = true;
                        Txt20Size.Visible = true;
                        ckFees.Visible = true;
                        lblAgreementfees.Visible = true;

                        Label lblchargesPerTue = (Label)rptcharges.Items[i].FindControl("lblchargesPerTue");
                        Label lblPricePerTue = (Label)rptcharges.Items[i].FindControl("lblPricePerTue");
                        Label lbl40Size = (Label)rptcharges.Items[i].FindControl("lbl40Size");
                        Label lbl20Size = (Label)rptcharges.Items[i].FindControl("lbl20Size");
                        Label lblckFees = (Label)rptcharges.Items[i].FindControl("lblckFees");
                        Label lblAgreementfee = (Label)rptcharges.Items[i].FindControl("lblAgreementfee");

                        lblchargesPerTue.Visible = false;
                        lblPricePerTue.Visible = false;
                        lbl40Size.Visible = false;
                        lbl20Size.Visible = false;
                        lblckFees.Visible = false;
                        lblAgreementfee.Visible = false;


                        Button AllowtoInvite = (Button)rptcharges.Items[i].FindControl("EditAllowtoInvite");
                        HtmlTableCell AllowtoInviteTD = (HtmlTableCell)rptcharges.Items[i].FindControl("AllowtoInviteTD");
                        AllowtoInvite.Visible = false;
                        AllowtoInviteTH.Visible = true;
                        AllowtoInviteTD.Visible = true;

                        LinkButton AllowtoSave = (LinkButton)rptcharges.Items[i].FindControl("EditAllowtoSave");
                        LinkButton AllowtoCancel = (LinkButton)rptcharges.Items[i].FindControl("EditAllowtoCancel");
                        AllowtoSave.Visible = true;
                        AllowtoCancel.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method lnkeditAllowtoSave_Click use : This is to save the edited VSA details into database. 
        protected void lnkeditAllowtoSave_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
                int id = int.Parse((sender as LinkButton).CommandArgument);
                string custid = Convert.ToString(Session["CustomerID"]);
                

                var updatetoY = (from q in db.VSA_Txn_Invite
                                 where q.VoyageID == ddlVoyageId.SelectedValue
                                 where q.VoyageSegmentSequenceNumber == id
                                 select new { q }).SingleOrDefault();


                Label voseqnum = (Label)rptVoyageInvitationDetails.Items[id - 1].FindControl("lblVoyageSegmentSequenceNumber");
                voseq = Convert.ToInt32(voseqnum.Text);
                Label orignport = (Label)rptVoyageInvitationDetails.Items[id - 1].FindControl("lblshiporignport");

                Label lblorignport = (Label)rptVoyageInvitationDetails.Items[id - 1].FindControl("lblorignport");
                TextBox TotalParticipants = (TextBox)rptVoyageInvitationDetails.Items[id - 1].FindControl("TxtTotalParticipants");
                TextBox VSANotes = (TextBox)rptVoyageInvitationDetails.Items[id - 1].FindControl("TxtVSANotes");
                HtmlInputCheckBox shareParticipants = (HtmlInputCheckBox)rptVoyageInvitationDetails.Items[id - 1].FindControl("ckshareParticipants");
                orginportname = lblorignport.Text;


                TextBox TEUsonship = (TextBox)rptTEUs.Items[id - 1].FindControl("TxtTEUsonship");
                TextBox TEUsDischarged = (TextBox)rptTEUs.Items[id - 1].FindControl("TxtTEUsDischarged");
                lblmsg.Text = TEUsDischarged.Text;
                lblmsg.Text = TEUsonship.Text;
                TextBox TEUsLoaded = (TextBox)rptTEUs.Items[id - 1].FindControl("TxtTEUsLoaded");
                Label TEUsTotal = (Label)rptTEUs.Items[id - 1].FindControl("lblsum");
                Label Freespace = (Label)rptTEUs.Items[id - 1].FindControl("lblFreespace");
                System.Web.UI.HtmlControls.HtmlInputControl inpTEUsonship = (System.Web.UI.HtmlControls.HtmlInputControl)rptTEUs.Items[0].FindControl("inpTEUsonship");

                TextBox edtTxtTEUsDischarged = (TextBox)rptTEUs.Items[id - 1].FindControl("edtTxtTEUsDischarged");
                TextBox edtTxtTEUsLoaded = (TextBox)rptTEUs.Items[id - 1].FindControl("edtTxtTEUsLoaded");



                Label Participantsflag = (Label)rptcharges.Items[id - 1].FindControl("lblshareParticipants");
                DropDownList chargesByTues = (DropDownList)rptcharges.Items[id - 1].FindControl("ddlchargesPer");
                TextBox PriceperTues = (TextBox)rptcharges.Items[id - 1].FindControl("TxtPricePerTue");
                TextBox Priceper40 = (TextBox)rptcharges.Items[id - 1].FindControl("Txt40Size");
                TextBox Priceper20 = (TextBox)rptcharges.Items[id - 1].FindControl("Txt20Size");
                CheckBox ArrangementFees = (CheckBox)rptcharges.Items[id - 1].FindControl("ckFees");

                if (ArrangementFees.Checked)
                {
                    //string custid = Convert.ToString(Session["CustomerID"]);
                    var fee = (from q in db.VSA_Master_Customer_and_CustomerTypes
                               join p in db.VSA_Master_VSA_Arrangement_Fee on q.CustomerTypeID equals p.CustomerTypeID
                               where q.CustomerID == custid
                               where p.Month == DateTime.Now.Month.ToString()
                               where p.Year == DateTime.Now.Year
                               select new { p.Month, p.Year, p.VSAArrangementFeePerTEU }).ToList().Min(x => x.VSAArrangementFeePerTEU);

                    updatetoY.q.VSAArrangementFeeAgreed_VO = "Y";
                    updatetoY.q.VSAArrangementFeePerTEU_VO = fee;
                }
                else
                {
                    updatetoY.q.VSAArrangementFeeAgreed_VO = "N";
                    updatetoY.q.VSAArrangementFeePerTEU_VO = 0;
                }

                if (shareParticipants.Checked)
                {
                    updatetoY.q.InviteVSAParticipantsFlag = "Y";
                }
                else
                {
                    updatetoY.q.InviteVSAParticipantsFlag = "N";
                }

                //item.AvailableSpaceTEU = Convert.ToInt32(Freespace.Text);
                if(TotalParticipants.Text != string.Empty)
                { 
                updatetoY.q.MaxNumberofParticipants = Convert.ToInt32(TotalParticipants.Text);
                }
                else
                {
                    updatetoY.q.MaxNumberofParticipants = 0;
                }
                updatetoY.q.VSANotes ="#Date:" + DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss") + "#CustID:" + custid + "# Comments:" + VSANotes.Text;
                if(edtTxtTEUsDischarged.Text != string.Empty)
                { 
                updatetoY.q.TEUsDischarged = Convert.ToInt32(edtTxtTEUsDischarged.Text);
                }
                else
                {
                    updatetoY.q.TEUsDischarged = 0;
                }
                if(edtTxtTEUsLoaded.Text != string.Empty)
                {
                    updatetoY.q.TEUsLoaded = Convert.ToInt32(edtTxtTEUsLoaded.Text);
                }
                else
                {
                    updatetoY.q.TEUsLoaded = 0;
                }
                
                int total = updatetoY.q.BeginTEUsonShip - updatetoY.q.TEUsDischarged;
                updatetoY.q.TEUsTotal = total + updatetoY.q.TEUsLoaded;

                int vesselcapacity = Convert.ToInt32(Session["VesselCapacity"]);

                updatetoY.q.AvailableSpaceTEU = vesselcapacity - updatetoY.q.TEUsTotal;

                updatetoY.q.IsPricingbyTEUorByContainerSize = chargesByTues.SelectedValue;

                if (PriceperTues.Text.Trim() != string.Empty)
                {
                    updatetoY.q.PricePerTEU = Convert.ToInt32(PriceperTues.Text);
                }
                else
                {
                    updatetoY.q.PricePerTEU = 0;
                }
                if (Priceper20.Text.Trim() != string.Empty)
                {
                    updatetoY.q.PricePer20FeetContainer = Convert.ToInt32(Priceper20.Text.Trim());
                    
                }
                else
                {
                    updatetoY.q.PricePer20FeetContainer = 0;
                }
                if(Priceper40.Text.Trim() != string.Empty)
                {
                    updatetoY.q.PricePer40FeetContainer = Convert.ToInt32(Priceper40.Text.Trim());
                }
                else
                {
                    updatetoY.q.PricePer40FeetContainer = 0;
                }
                
                updatetoY.q.Update_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                //var InviteDeadLine = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                //                      where q.VoyageID == ddlVoyageId.SelectedValue
                //                      where q.VoyageSegmentSequenceNumber == 1
                //                      select new { q.ExpectedDepartureDateTime }).SingleOrDefault();

                //DateTime lastdatetoinvite = InviteDeadLine.ExpectedDepartureDateTime.AddDays(-4);

                //if (DateTime.Now < InviteDeadLine.ExpectedDepartureDateTime.AddDays(-4))
                //{
                    db.SaveChanges();
                String strAppMsg = ConfigurationManager.AppSettings["INinviteupdated"];
                lblerrfreespace.Text = strAppMsg;
                lblerrfreespace.ForeColor = System.Drawing.Color.ForestGreen;
                DbTrans.Rollback();

                var tuescount = (from q in db.VSA_Txn_Invite
                                     where q.VoyageID == ddlVoyageId.SelectedValue
                                     select new { }).ToList();

                    for (int i = id; i < tuescount.Count; i++)
                    {
                        var totaltues = (from q in db.VSA_Txn_Invite
                                         where q.VoyageID == ddlVoyageId.SelectedValue
                                         where q.VoyageSegmentSequenceNumber == i
                                         select new { q.TEUsTotal }).SingleOrDefault();
                        int j = i + 1;

                        var InviteJ = (from q in db.VSA_Txn_Invite
                                       where q.VoyageID == ddlVoyageId.SelectedValue
                                       where q.VoyageSegmentSequenceNumber == j
                                       select new { q }).SingleOrDefault();

                        InviteJ.q.BeginTEUsonShip = totaltues.TEUsTotal;

                        InviteJ.q.TEUsTotal = InviteJ.q.BeginTEUsonShip - InviteJ.q.TEUsDischarged + InviteJ.q.TEUsLoaded;

                        int vesselcapacityJ = Convert.ToInt32(Session["VesselCapacity"]);
                        InviteJ.q.AvailableSpaceTEU = vesselcapacityJ - InviteJ.q.TEUsTotal;

                        db.SaveChanges();
                    }
                //}

                //else
                //{
                //    lblmsg.Text = "You Can not Invite after "+ lastdatetoinvite;
                //    lblmsg.ForeColor = System.Drawing.Color.Red;
                //}
                vesselvoyagedtls();
                showVoyageInvitationDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        // Method EditAllowtoCancel_Click use :This is to show to voyage invite details on click of cancel button intended for cancelling edit operation
        protected void EditAllowtoCancel_Click(object sender, EventArgs e)
        {
            showVoyageInvitationDetails();
        }

        string orginportname;
        int voseq;

        // Method InsertVoyageInvitationDetails use :This method is insert the voyage invite details into database
        public void InsertVoyageInvitationDetails()
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
                string custid = Convert.ToString(Session["CustomerID"]);
               for (int i = 0; i < rptVoyageInvitationDetails.Items.Count; i++)
                {
                    var item = new VSA_Txn_Invite();

                    Label voseqnum = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblVoyageSegmentSequenceNumber");
                    voseq = Convert.ToInt32(voseqnum.Text);
                    Label orignport = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblshiporignport");

                    Label lblorignport = (Label)rptVoyageInvitationDetails.Items[i].FindControl("lblorignport");
                    TextBox TotalParticipants = (TextBox)rptVoyageInvitationDetails.Items[i].FindControl("TxtTotalParticipants");
                    TextBox VSANotes = (TextBox)rptVoyageInvitationDetails.Items[i].FindControl("TxtVSANotes");
                    HtmlInputCheckBox shareParticipants = (HtmlInputCheckBox)rptVoyageInvitationDetails.Items[i].FindControl("ckshareParticipants");
                    orginportname = lblorignport.Text;
                    TextBox TEUsonship = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsonship");
                    TextBox TEUsDischarged = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsDischarged");
                    
                    TextBox TEUsLoaded = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsLoaded");
                    Label TEUsTotal = (Label)rptTEUs.Items[i].FindControl("lblsum");
                    Label Freespace = (Label)rptTEUs.Items[i].FindControl("lblFreespace");
                    System.Web.UI.HtmlControls.HtmlInputControl inpTEUsonship = (System.Web.UI.HtmlControls.HtmlInputControl)rptTEUs.Items[i].FindControl("inpTEUsonship");

                    Label Participantsflag = (Label)rptcharges.Items[i].FindControl("lblshareParticipants");
                    DropDownList chargesByTues = (DropDownList)rptcharges.Items[i].FindControl("ddlchargesPer");
                    TextBox PriceperTues = (TextBox)rptcharges.Items[i].FindControl("TxtPricePerTue");
                    TextBox Priceper40 = (TextBox)rptcharges.Items[i].FindControl("Txt40Size");
                    TextBox Priceper20 = (TextBox)rptcharges.Items[i].FindControl("Txt20Size");
                    CheckBox ArrangementFees = (CheckBox)rptcharges.Items[i].FindControl("ckFees");

                    if (ArrangementFees.Checked)
                    {
                        item.VSAArrangementFeeAgreed_VO = "Y";
                        item.VSAArrangementFeePerTEU_VO = 123;
                    }
                    else
                    {
                        item.VSAArrangementFeeAgreed_VO = "N";
                        item.VSAArrangementFeePerTEU_VO = 0;
                    }

                    if (shareParticipants.Checked)
                    {
                        item.InviteVSAParticipantsFlag = "Y";
                    }
                    else
                    {
                        item.InviteVSAParticipantsFlag = "N";
                    }

                    var ttues = (from q in db.VSA_Master_Vessel
                                 join d in db.VSA_Txn_VesselVoyage on q.VesselID equals d.VesselID
                                 where q.VesselID == ddlVesselId.SelectedValue
                                 where d.VoyageID == ddlVoyageId.SelectedValue
                                 select new
                                 {
                                     q.VesselCapacityTEU
                                 }).SingleOrDefault();

                    if(TotalParticipants.Text != string.Empty)
                    { 
                    item.MaxNumberofParticipants = Convert.ToInt32(TotalParticipants.Text);
                    }
                    else
                    {
                        item.MaxNumberofParticipants = 0;
                    }
                    item.VSANotes = "#Date:" + DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss") + "#CustID:" + custid + "# Comments:" + VSANotes.Text;
                    item.Application_Status = "Not Submitted";
                    item.VoyageSegmentSequenceNumber = Convert.ToInt32(voseqnum.Text);

                    var totlvalidation = (from q in db.VSA_Txn_Invite
                                          where q.VoyageID == ddlVoyageId.SelectedValue
                                          where q.VoyageSegmentSequenceNumber == item.VoyageSegmentSequenceNumber - 1
                                          select new { q.TEUsTotal }).SingleOrDefault();

                    if(TEUsonship.Text != string.Empty)
                    { 
                    item.BeginTEUsonShip = Convert.ToInt32(TEUsonship.Text);
                    }
                    else
                    {
                        item.BeginTEUsonShip = 0;
                    }
                    if(TEUsDischarged.Text != string.Empty)
                    {
                        item.TEUsDischarged = Convert.ToInt32(TEUsDischarged.Text);
                    }
                    else
                    {
                        item.TEUsDischarged = 0;
                    }
                    if (TEUsLoaded.Text != string.Empty)
                    {
                        item.TEUsLoaded = Convert.ToInt32(TEUsLoaded.Text);
                    }
                    else
                    {
                        item.TEUsLoaded = 0;
                    }
                    if (TEUsDischarged.Text != string.Empty)
                    {
                        item.TEUsTotal = item.BeginTEUsonShip - Convert.ToInt32(TEUsDischarged.Text) + Convert.ToInt32(TEUsLoaded.Text);
                    }
                    else
                    {
                        item.TEUsTotal = 0;
                    }
                    item.AvailableSpaceTEU = ttues.VesselCapacityTEU - item.TEUsTotal;
                    item.Create_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                    item.IsPricingbyTEUorByContainerSize = chargesByTues.SelectedValue;
                    if (PriceperTues.Text.Trim() != string.Empty)
                    {
                        item.PricePerTEU = Convert.ToInt32(PriceperTues.Text);
                    }
                    else
                    {
                        item.PricePerTEU = 0;
                    }
                    if (Priceper20.Text.Trim() != string.Empty)
                    {
                        item.PricePer20FeetContainer = Convert.ToInt32(Priceper20.Text.Trim());
                        
                    }
                    else
                    {
                        item.PricePer20FeetContainer = 0;
                    }
                    if(Priceper40.Text.Trim() != string.Empty)
                    {
                        item.PricePer40FeetContainer = Convert.ToInt32(Priceper40.Text.Trim());
                    }
                    else
                    {
                        item.PricePer40FeetContainer = 0;
                    }
                    item.VoyageID = ddlVoyageId.SelectedValue;
                    item.Update_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                    if (i >= 1)
                    {

                        if (totlvalidation.TEUsTotal == item.BeginTEUsonShip)
                        {
                            if (item.BeginTEUsonShip >= item.TEUsDischarged)
                            {
                                if (ttues.VesselCapacityTEU >= item.TEUsLoaded)
                                {
                                    //var InviteDeadLine = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                    //                      where q.VoyageID == ddlVoyageId.SelectedValue
                                    //                      where q.VoyageSegmentSequenceNumber == 1
                                    //                      select new { q.ExpectedDepartureDateTime }).SingleOrDefault();

                                    //DateTime lastdatetoinvite = InviteDeadLine.ExpectedDepartureDateTime.AddDays(-4);

                                    //if (DateTime.Now < InviteDeadLine.ExpectedDepartureDateTime.AddDays(-4))
                                    //{
                                    db.VSA_Txn_Invite.Add(item);
                                db.SaveChanges();
                                    //}
                                    //else
                                    //{
                                    //    lblmsg.Text = "You Can not Invite after "+ lastdatetoinvite;
                                    //    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //}
                                }
                                else
                                {
                                    for (int j = 1; j < item.VoyageSegmentSequenceNumber; j++)
                                    {
                                        var delete = db.VSA_Txn_Invite.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == j).SingleOrDefault();
                                        db.VSA_Txn_Invite.Remove(delete);
                                        db.SaveChanges();
                                    }
                                    clear();
                                    vesselvoyagedtls();
                                    String strAppMsg = ConfigurationManager.AppSettings["INteusloadednotgreater"];
                                    lblerrfreespace.Text = strAppMsg;
                                    lblerrfreespace.ForeColor = System.Drawing.Color.Red;
                                    break;
                                }
                            }
                            else
                            {
                                for (int j = 1; j < item.VoyageSegmentSequenceNumber; j++)
                                {
                                    var delete = db.VSA_Txn_Invite.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == j).SingleOrDefault();
                                    db.VSA_Txn_Invite.Remove(delete);
                                    db.SaveChanges();
                                }
                                clear();
                                vesselvoyagedtls();
                                String strAppMsg = ConfigurationManager.AppSettings["INdischargenotgreater"];
                                lblerrfreespace.Text = strAppMsg;
                                lblerrfreespace.ForeColor = System.Drawing.Color.Red;
                                break;
                            }
                        }
                        else
                        {
                            for (int j = 1; j < item.VoyageSegmentSequenceNumber; j++)
                            {
                                var delete = db.VSA_Txn_Invite.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == j).SingleOrDefault();
                                db.VSA_Txn_Invite.Remove(delete);
                                db.SaveChanges();
                            }
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Teus on ship and total teus should be same')", true);
                            String strAppMsg = ConfigurationManager.AppSettings["INoriginteus"];
                            lblerrfreespace.Text = strAppMsg;
                            lblerrfreespace.ForeColor = System.Drawing.Color.Red;
                            break;
                        }

                    }
                    else
                    {
                        if(item.BeginTEUsonShip >= item.TEUsDischarged)
                        {
                            if(ttues.VesselCapacityTEU >= item.TEUsLoaded)
                            {

                        //var InviteDeadLine = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                        //                      where q.VoyageID == ddlVoyageId.SelectedValue
                        //                      where q.VoyageSegmentSequenceNumber == 1
                        //                      select new { q.ExpectedDepartureDateTime }).SingleOrDefault();

                        //DateTime lastdatetoinvite = InviteDeadLine.ExpectedDepartureDateTime.AddDays(-4);

                        //if (DateTime.Now < InviteDeadLine.ExpectedDepartureDateTime.AddDays(-4))
                        //{
                            db.VSA_Txn_Invite.Add(item);
                            db.SaveChanges();
                                //}
                                //else
                                //{
                                //    lblmsg.Text = "You Can not Invite after "+ lastdatetoinvite;
                                //    lblmsg.ForeColor = System.Drawing.Color.Red;
                                //}
                            }
                            else
                            {
                                clear();
                                vesselvoyagedtls();
                                String strAppMsg = ConfigurationManager.AppSettings["INteusloadednotgreater"];
                                lblerrfreespace.Text = strAppMsg;
                                lblerrfreespace.ForeColor = System.Drawing.Color.Red;
                                break;
                            }
                        }
                        else
                        {
                            clear();
                            vesselvoyagedtls();
                            String strAppMsg = ConfigurationManager.AppSettings["INdischargenotgreater"];
                            lblerrfreespace.Text = strAppMsg;
                            lblerrfreespace.ForeColor = System.Drawing.Color.Red;
                            break;
                        }
                    }

                }
                DbTrans.Commit();

            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                throw ex;
            }
        }

        // Method btnInvite_Click use :This method is to trigger the invite button click event
        protected void btnInvite_Click(object sender, EventArgs e)
        {
            try
            {
                InsertVoyageInvitationDetails();
                var db = new VesselAgreement();

                var orgportid = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                 where q.VoyageID == ddlVoyageId.SelectedValue
                                 where q.VoyageSegmentSequenceNumber == rptVoyageInvitationDetails.Items.Count
                                 select new { q.OriginTransitPortID }).SingleOrDefault();
                var invorgport = (from q in db.VSA_Config_Port
                                  where q.PortName == orginportname
                                  select new { q.PortID }).SingleOrDefault();
                if (invorgport.PortID == orgportid.OriginTransitPortID)
                {
                    String strAppMsg = ConfigurationManager.AppSettings["INinvite"];
                    lblerrfreespace.Text = strAppMsg;
                    lblerrfreespace.ForeColor = System.Drawing.Color.ForestGreen;

                    vesselvoyagedtls();
                    showVoyageInvitationDetails();
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void clear()
        {
            for (int i = 0; i < rptVoyageInvitationDetails.Items.Count; i++)
            {
                TextBox TxtTotalParticipants = (TextBox)rptVoyageInvitationDetails.Items[i].FindControl("TxtTotalParticipants");
                TextBox TxtVSANotes = (TextBox)rptVoyageInvitationDetails.Items[i].FindControl("TxtVSANotes");
                TextBox TxtTEUsonship = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsonship");

                TextBox TxtTEUsDischarged = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsDischarged");
                TextBox TxtTEUsLoaded = (TextBox)rptTEUs.Items[i].FindControl("TxtTEUsLoaded");
                Label lblsum = (Label)rptTEUs.Items[i].FindControl("lblsum");
                Label lblFreespace = (Label)rptTEUs.Items[i].FindControl("lblFreespace");
                TextBox TxtPricePerTue = (TextBox)rptcharges.Items[i].FindControl("TxtPricePerTue");
                TextBox Txt40Size = (TextBox)rptcharges.Items[i].FindControl("Txt40Size");
                TextBox Txt20Size = (TextBox)rptcharges.Items[i].FindControl("Txt20Size");
                CheckBox ckFees = (CheckBox)rptcharges.Items[i].FindControl("ckFees");
                DropDownList ddlchargesPer = (DropDownList)rptcharges.Items[i].FindControl("ddlchargesPer");

                TxtTotalParticipants.Text = "";
                TxtVSANotes.Text = "";
                TxtTEUsonship.Text = "";
                TxtTEUsDischarged.Text = "";
                TxtTEUsLoaded.Text = "";
                lblsum.Text = "";
                lblFreespace.Text = "";
                TxtPricePerTue.Text = "";
                Txt40Size.Text = "";
                Txt20Size.Text = "";
                ckFees.Checked = true;
                ddlchargesPer.SelectedIndex = 0;
            }
        }

        // Method Btncancel_Click use :This method is to trigger the cancel button click event, which will set the values default
        protected void Btncancel_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}