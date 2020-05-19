using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public partial class FinalVSA : System.Web.UI.Page
    {
        string custid;
        protected void Page_Load(object sender, EventArgs e)
        {
            custid = Convert.ToString(Session["CustomerID"]);
            if (!IsPostBack)
            {
                var db = new VesselAgreement();
                
                var ifcustomer = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                  where q.CustomerID == custid
                                  where q.CustomerTypeID == "VSLOPR"
                                  select new { q.CustomerTypeID }).SingleOrDefault();
                Session["ifcustomer"] = ifcustomer;
                if (ifcustomer != null)
                {
                    ddlcustomerID.Visible = true;
                    labelCustomer.Visible = true;
                }
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

                var vesseloptr = Session["ifcustomer"];

                if (vesseloptr == null)
                {
                    var Vesselidname = (from q in db.VSA_Txn_Participant_Review_and_Approved
                                        join r in db.VSA_Txn_VesselVoyage on q.VoyageID equals r.VoyageID
                                        join p in db.VSA_Master_Vessel on r.VesselID equals p.VesselID
                                        where p.VesselID == r.VesselID
                                        where q.VSAParticipantCustomerID == custid
                                        select new
                                        {
                                            vesselidname = p.VesselID + "-" + p.NameoftheVessel,
                                            vesselid = p.VesselID,
                                        }).ToList().Distinct();

                    ddlVesselId.DataSource = Vesselidname.OrderBy(x => x.vesselidname);
                }
                else
                {
                    var Vesselidname = (from q in db.VSA_Txn_Participant_Review_and_Approved
                                        join r in db.VSA_Txn_VesselVoyage on q.VoyageID equals r.VoyageID
                                        join p in db.VSA_Master_Vessel on r.VesselID equals p.VesselID
                                        where p.VesselID == r.VesselID
                                        where p.VesselOperatorId == custid
                                        select new
                                        {
                                            vesselidname = p.VesselID + "-" + p.NameoftheVessel,
                                            vesselid = p.VesselID,

                                        }).ToList().Distinct();
                    ddlVesselId.DataSource = Vesselidname.OrderBy(x => x.vesselidname);
                }
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

                var vesseloptr = Session["ifcustomer"];

                if (vesseloptr == null)
                { 
                if (ddlVesselId.SelectedValue != "0")
                {
                    var voyid = (from q in db.VSA_Txn_Participant_Review_and_Approved
                                 join p in db.VSA_Txn_VesselVoyage on q.VoyageID equals p.VoyageID
                                 where p.VesselID == ddlVesselId.SelectedValue
                                 where q.VSAParticipantCustomerID == custid
                                 select new { q.VoyageID }).ToList().Distinct();

                    ddlVoyageId.DataSource = voyid.OrderBy(x => x.VoyageID);
                }
                }
                else
                {
                    if (ddlVesselId.SelectedValue != "0")
                    {
                        var voyid = (from q in db.VSA_Txn_Participant_Review_and_Approved
                                 join p in db.VSA_Txn_VesselVoyage on q.VoyageID equals p.VoyageID
                                 where p.VesselID == ddlVesselId.SelectedValue
                                 //where q.VSAParticipantCustomerID == custid
                                 select new { q.VoyageID }).ToList().Distinct();

                    ddlVoyageId.DataSource = voyid.OrderBy(x => x.VoyageID);
                    }
                }
                ddlVoyageId.DataTextField = "VoyageID";
                ddlVoyageId.DataValueField = "VoyageID";
                ddlVoyageId.DataBind();
                ddlVoyageId.Items.Insert(0, new ListItem("Select Voyage Id", "0"));
                ddlcustomerID.Items.Insert(0, new ListItem("Select Customer Id", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void customer()
        {
            var db = new VesselAgreement();

            var customerddl = (from q in db.VSA_Txn_Participant_Review_and_Approved
                               where q.VoyageID == ddlVoyageId.SelectedValue
                               select new { q.VSAParticipantCustomerID }).ToList().Distinct();

            ddlcustomerID.DataSource = customerddl;
            ddlcustomerID.DataTextField = "VSAParticipantCustomerID";
            ddlcustomerID.DataValueField = "VSAParticipantCustomerID";
            ddlcustomerID.DataBind();
            ddlcustomerID.Items.Insert(0, new ListItem("Select Customer Id", "0"));
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
                    customer();
                    finalvsa();
                    vesselvoyagedtls();
                }

            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void finalvsa()
        {
            try
            {
                var db = new VesselAgreement();

                //var restotprice = db.VSA_Txn_Participant_Review_and_Approved.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == 1 && x.IsPricingbyTEUorByContainerSize == "").Select(x => (x.NetTEUSApprovedforVSAParticipantforVoyageSegment * x.PricePerTEU)).FirstOrDefault();


                var fee = (from q in db.VSA_Master_Customer_and_CustomerTypes
                           join p in db.VSA_Master_VSA_Arrangement_Fee on q.CustomerTypeID equals p.CustomerTypeID
                           where q.CustomerID == custid
                           where p.Month == DateTime.Now.Month.ToString()
                           where p.Year == DateTime.Now.Year
                           select new { p.Month, p.Year, p.VSAArrangementFeePerTEU }).ToList().Min(x => x.VSAArrangementFeePerTEU);

                var vesseloptr = Session["ifcustomer"];
                if (vesseloptr == null)
                {
                    var finalVsa = (from q in db.VSA_Txn_Participant_Review_and_Approved
                                    join p in db.VSA_Txn_Invite on q.VoyageID equals p.VoyageID
                                    join a in db.VSA_Txn_Participant_Application on q.VoyageID equals a.VoyageID
                                    join r in db.VSA_Txn_VesselVoyageTransitShipRoute on q.VoyageID equals r.VoyageID
                                    join u in db.VSA_Config_Port on r.OriginTransitPortID equals u.PortID
                                    join f in db.VSA_Config_Port on r.DestinationTransitPortID equals f.PortID
                                    where r.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber
                                    where p.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber
                                    where q.VoyageID == ddlVoyageId.SelectedValue
                                    where q.VSAParticipantCustomerID == custid
                                    where a.VoyageID == ddlVoyageId.SelectedValue
                                    where a.VoyageSegmentSequencenumber == q.VoyageSegmentSequenceNumber
                                    where a.VSAParticipantCustomerID == custid
                                    select new
                                    {
                                        VoyageSegmentSequenceNumber = p.VoyageSegmentSequenceNumber,
                                        shiporignport = u.PortName + "-" + f.PortName,
                                        VSAParticipantsFlag = p.InviteVSAParticipantsFlag,
                                        MaxNumberofParticipants = p.MaxNumberofParticipants,
                                        InitialAvailableSpaceTEU = p.AvailableSpaceTEU,
                                        lblchargesPerTue = q.IsPricingbyTEUorByContainerSize,
                                        lblPricePerTue = q.PricePerTEU,
                                        lbl40Size = q.PricePer40FeetContainer,
                                        lbl20Size = q.PricePer20FeetContainer,
                                        VSANotes = q.VSANotes,
                                        lblAccepted40Load = q.Load_40Feet_Containers_ApprovedforCargoOperator,
                                        lblAccepted20Load = q.Load_20Feet_Containers_ApprovedforCargoOperator,
                                        lblAccepted40Disch = q.Discharge_40Feet_Containers_ApprovedforCargoOperator,
                                        lblAccepted20Disch = q.Discharge_20Feet_Containers_ApprovedforCargoOperator,
                                        NetTEUSowned = a.NetTEUSownedByVSAParticipantforVoyageSegment,
                                        lblNetTEUsApp = q.NetTEUSApprovedforVSAParticipantforVoyageSegment,
                                        VesselAgreementId = q.VSA_Partcipant_Agreement_ID,
                                        lblEquivalent40FtContainers = q.Net_40Feet_Containers_AllowableforCargoOperator,
                                        lblEquivalent20FtContainers = q.Net_20Feet_Containers_AllowableforCargoOperator,
                                        lblTotalprice = (q.NetTEUSApprovedforVSAParticipantforVoyageSegment * q.PricePerTEU) + ((q.Net_40Feet_Containers_AllowableforCargoOperator * q.PricePer40FeetContainer) + (q.Net_20Feet_Containers_AllowableforCargoOperator * q.PricePer20FeetContainer)),
                                        //db.VSA_Txn_Participant_Review_and_Approved.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber && x.IsPricingbyTEUorByContainerSize == q.IsPricingbyTEUorByContainerSize).Select(x => (x.NetTEUSApprovedforVSAParticipantforVoyageSegment * x.PricePerTEU)).FirstOrDefault() != null ? db.VSA_Txn_Participant_Review_and_Approved.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber && x.IsPricingbyTEUorByContainerSize == q.IsPricingbyTEUorByContainerSize).Select(x => (x.NetTEUSApprovedforVSAParticipantforVoyageSegment * x.PricePerTEU)).FirstOrDefault() : 30,// db.VSA_Txn_Participant_Review_and_Approved.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber && x.IsPricingbyTEUorByContainerSize == q.IsPricingbyTEUorByContainerSize).Select(x => (x.Net_40Feet_Containers_AllowableforCargoOperator * x.PricePer40FeetContainer)+(x.Net_20Feet_Containers_AllowableforCargoOperator * x.PricePer20FeetContainer)).FirstOrDefault(),
                                        lblckFees = p.VSAArrangementFeeAgreed_VO,
                                        lblAgreementfee = (q.NetTEUSApprovedforVSAParticipantforVoyageSegment * fee),
                                        lblpriceofallTeus = ((q.NetTEUSApprovedforVSAParticipantforVoyageSegment * q.PricePerTEU) + ((q.Net_40Feet_Containers_AllowableforCargoOperator * q.PricePer40FeetContainer) + (q.Net_20Feet_Containers_AllowableforCargoOperator * q.PricePer20FeetContainer))+ (q.NetTEUSApprovedforVSAParticipantforVoyageSegment * fee)),
                                    }).ToList();



                    rptVoyageInvitationDetails.DataSource = finalVsa;
                    rptVoyageInvitationDetails.DataBind();

                    rptTEUs.DataSource = finalVsa;
                    rptTEUs.DataBind();

                    rptcharges.DataSource = finalVsa;
                    rptcharges.DataBind();
                }
                else
                {
                    var finalVsa = (from q in db.VSA_Txn_Participant_Review_and_Approved
                                    join p in db.VSA_Txn_Invite on q.VoyageID equals p.VoyageID
                                    join a in db.VSA_Txn_Participant_Application on q.VoyageID equals a.VoyageID
                                    join r in db.VSA_Txn_VesselVoyageTransitShipRoute on q.VoyageID equals r.VoyageID
                                    join u in db.VSA_Config_Port on r.OriginTransitPortID equals u.PortID
                                    join f in db.VSA_Config_Port on r.DestinationTransitPortID equals f.PortID
                                    where r.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber
                                    where p.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber
                                    where q.VoyageID == ddlVoyageId.SelectedValue
                                    where q.VSAParticipantCustomerID == ddlcustomerID.SelectedValue
                                    where a.VoyageID == ddlVoyageId.SelectedValue
                                    where a.VoyageSegmentSequencenumber == q.VoyageSegmentSequenceNumber
                                    where a.VSAParticipantCustomerID == ddlcustomerID.SelectedValue
                                    select new
                                    {
                                        VoyageSegmentSequenceNumber = p.VoyageSegmentSequenceNumber,
                                        shiporignport = u.PortName + "-" + f.PortName,
                                        VSAParticipantsFlag = p.InviteVSAParticipantsFlag,
                                        MaxNumberofParticipants = p.MaxNumberofParticipants,
                                        InitialAvailableSpaceTEU = p.AvailableSpaceTEU,
                                        lblchargesPerTue = q.IsPricingbyTEUorByContainerSize,
                                        lblPricePerTue = q.PricePerTEU,
                                        lbl40Size = q.PricePer40FeetContainer,
                                        lbl20Size = q.PricePer20FeetContainer,
                                        VSANotes = q.VSANotes,
                                        lblAccepted40Load = q.Load_40Feet_Containers_ApprovedforCargoOperator,
                                        lblAccepted20Load = q.Load_20Feet_Containers_ApprovedforCargoOperator,
                                        lblAccepted40Disch = q.Discharge_40Feet_Containers_ApprovedforCargoOperator,
                                        lblAccepted20Disch = q.Discharge_20Feet_Containers_ApprovedforCargoOperator,
                                        NetTEUSowned = a.NetTEUSownedByVSAParticipantforVoyageSegment,
                                        lblNetTEUsApp = q.NetTEUSApprovedforVSAParticipantforVoyageSegment,
                                        VesselAgreementId = q.VSA_Partcipant_Agreement_ID,
                                        lblEquivalent40FtContainers = q.Net_40Feet_Containers_AllowableforCargoOperator,
                                        lblEquivalent20FtContainers = q.Net_20Feet_Containers_AllowableforCargoOperator,
                                        lblTotalprice = (q.NetTEUSApprovedforVSAParticipantforVoyageSegment * q.PricePerTEU) + ((q.Net_40Feet_Containers_AllowableforCargoOperator * q.PricePer40FeetContainer) + (q.Net_20Feet_Containers_AllowableforCargoOperator * q.PricePer20FeetContainer)),
                                        //db.VSA_Txn_Participant_Review_and_Approved.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber && x.IsPricingbyTEUorByContainerSize == q.IsPricingbyTEUorByContainerSize).Select(x => (x.NetTEUSApprovedforVSAParticipantforVoyageSegment * x.PricePerTEU)).FirstOrDefault() != null ? db.VSA_Txn_Participant_Review_and_Approved.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber && x.IsPricingbyTEUorByContainerSize == q.IsPricingbyTEUorByContainerSize).Select(x => (x.NetTEUSApprovedforVSAParticipantforVoyageSegment * x.PricePerTEU)).FirstOrDefault() : 30,// db.VSA_Txn_Participant_Review_and_Approved.Where(x => x.VoyageID == ddlVoyageId.SelectedValue && x.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber && x.IsPricingbyTEUorByContainerSize == q.IsPricingbyTEUorByContainerSize).Select(x => (x.Net_40Feet_Containers_AllowableforCargoOperator * x.PricePer40FeetContainer)+(x.Net_20Feet_Containers_AllowableforCargoOperator * x.PricePer20FeetContainer)).FirstOrDefault(),
                                        lblckFees = p.VSAArrangementFeeAgreed_VO,
                                        lblAgreementfee = (q.NetTEUSApprovedforVSAParticipantforVoyageSegment * fee),
                                        lblpriceofallTeus = ((q.NetTEUSApprovedforVSAParticipantforVoyageSegment * q.PricePerTEU) + ((q.Net_40Feet_Containers_AllowableforCargoOperator * q.PricePer40FeetContainer) + (q.Net_20Feet_Containers_AllowableforCargoOperator * q.PricePer20FeetContainer)) + (q.NetTEUSApprovedforVSAParticipantforVoyageSegment * fee)),
                                    }).ToList();



                    rptVoyageInvitationDetails.DataSource = finalVsa;
                    rptVoyageInvitationDetails.DataBind();

                    rptTEUs.DataSource = finalVsa;
                    rptTEUs.DataBind();

                    rptcharges.DataSource = finalVsa;
                    rptcharges.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlcustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                finalvsa();
                vesselvoyagedtls();
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }
    }
}