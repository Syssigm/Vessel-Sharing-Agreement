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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ports();
                //Portddls.Visible = true;
                Selection.Visible = false;
            }
            lblmsg.Text = "";
            lblVisitor.Text = "Visitor Count : " + Convert.ToString(Application["NoOfVisitors"]);
        }
        
        public void ports()
        {
            var db = new VesselAgreement();

            ddloriginport.DataSource =db.VSA_Config_Port.ToList().OrderBy(x => x.PortName);
            ddloriginport.DataTextField = "PortName";
            ddloriginport.DataValueField = "PortID";
            ddloriginport.DataBind();
            ddloriginport.Items.Insert(0, new ListItem("--Select Port--", "0"));

            ddlDestinationPort.DataSource = db.VSA_Config_Port.ToList().OrderBy(x => x.PortName);
            ddlDestinationPort.DataTextField = "PortName";
            ddlDestinationPort.DataValueField = "PortID";
            ddlDestinationPort.DataBind();
            ddlDestinationPort.Items.Insert(0, new ListItem("--Select Port", "0"));
        }
        
        protected void BtnCheckVSAAvailability_click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();

            Session["Originport"] = ddloriginport.SelectedItem.Text;
            Session["Destinationport"] = ddlDestinationPort.SelectedItem.Text;


            if (TxtFromDate.Text.Trim()==string.Empty || TxtToDate.Text.Trim()==string.Empty)
            {

                var avaorgin = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                where q.OriginTransitPortID == ddloriginport.SelectedValue
                                join p in db.VSA_Txn_Invite on q.VoyageID equals p.VoyageID
                                where q.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber
                                select new { q.VoyageID,q.OriginTransitPortID, q.VoyageSegmentSequenceNumber,q.ExpectedDepartureDateTime }).ToList();

                var avadest = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                               where  q.DestinationTransitPortID == ddlDestinationPort.SelectedValue
                               join p in db.VSA_Txn_Invite on q.VoyageID equals p.VoyageID
                               where q.VoyageSegmentSequenceNumber == p.VoyageSegmentSequenceNumber
                               select new { q.VoyageID,q.DestinationTransitPortID, q.VoyageSegmentSequenceNumber,q.ExpectedArrivalDateTime }).ToList();

                var availbleship = (from q in avaorgin
                                    join p in avadest on q.VoyageID equals p.VoyageID
                                    join r in db.VSA_Txn_VesselVoyage on p.VoyageID equals r.VoyageID
                                    join d in db.VSA_Master_Vessel on r.VesselID equals d.VesselID
                                    join t in db.VSA_Config_Port on q.OriginTransitPortID equals t.PortID
                                    join u in db.VSA_Config_Port on p.DestinationTransitPortID equals u.PortID
                                    select new
                                    {
                                        Port = t.PortName + "-" + u.PortName,
                                        VesselName = d.NameoftheVessel,

                                        Startdate = q.ExpectedDepartureDateTime,
                                        Enddate = p.ExpectedArrivalDateTime,
                                        VoyageID=q.VoyageID,
                                    }).Where(x => x.Startdate > DateTime.Now).OrderBy(x=> x.Startdate).ToList();
                if (availbleship.Count !=0)
            { 
            rptVSAAvailability.DataSource = availbleship;
            rptVSAAvailability.DataBind();
            }
            else
            {
                    String strAppMsg = ConfigurationManager.AppSettings["DFnorecord"];
                    lblmsg.Text = strAppMsg;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                Selection.Visible = true;

                lblValue1.Text = "Origin port : ";
                lblResValue1.Text = ddloriginport.SelectedItem.Text;

                lblValue2.Text = "Destination port : ";
                lblResValue2.Text = ddlDestinationPort.SelectedItem.Text;
            }
            else
            {
                DateTime Fromdate = DateTime.ParseExact(TxtFromDate.Text.Trim(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Todate = DateTime.ParseExact(TxtToDate.Text.Trim(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                Session["DeptFromdate"] = Fromdate;
                Session["DeptTodate"] = Todate;

                if (ExpectedeparturedateRB.Checked)
                {
                    var availbleship = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                        where (q.ExpectedDepartureDateTime >= Fromdate && q.ExpectedDepartureDateTime <= Todate)
                                        join t in db.VSA_Config_Port on q.OriginTransitPortID equals t.PortID
                                        join u in db.VSA_Config_Port on q.DestinationTransitPortID equals u.PortID
                                        join p in db.VSA_Txn_VesselVoyage on q.VoyageID equals p.VoyageID
                                        where q.VoyageID == p.VoyageID
                                        join r in db.VSA_Master_Vessel on p.VesselID equals r.VesselID
                                        join s in db.VSA_Txn_Invite on q.VoyageID equals s.VoyageID
                                        where s.VoyageID == q.VoyageID
                                        where s.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber
                                        select new
                                        {
                                            VoyageID = q.VoyageID,
                                            Port = t.PortName + "-" + u.PortName,
                                            VesselName = r.NameoftheVessel,
                                            Startdate = q.ExpectedDepartureDateTime,
                                            Enddate = q.ExpectedArrivalDateTime,
                                        }).ToList();

                    if (availbleship.Count != 0)
                    {
                        rptVSAAvailability.DataSource = availbleship;
                        rptVSAAvailability.DataBind();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "datetab();", true);
                    }
                    else
                    {
                        String strAppMsg = ConfigurationManager.AppSettings["DFnorecord"];
                        lblmsg.Text = strAppMsg;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (ExpectedarrivaldateRB.Checked)
                {
                    var availbleship = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                                        where (q.ExpectedArrivalDateTime >= Fromdate && q.ExpectedArrivalDateTime <= Todate)
                                        join t in db.VSA_Config_Port on q.OriginTransitPortID equals t.PortID
                                        join u in db.VSA_Config_Port on q.DestinationTransitPortID equals u.PortID
                                        join p in db.VSA_Txn_VesselVoyage on q.VoyageID equals p.VoyageID
                                        where q.VoyageID == p.VoyageID
                                        join r in db.VSA_Master_Vessel on p.VesselID equals r.VesselID
                                        join s in db.VSA_Txn_Invite on q.VoyageID equals s.VoyageID
                                        where s.VoyageID == q.VoyageID
                                        where s.VoyageSegmentSequenceNumber == q.VoyageSegmentSequenceNumber
                                        select new
                                        {
                                            VoyageID=q.VoyageID,
                                            Port = t.PortName + "-" + u.PortName,
                                            VesselName = r.NameoftheVessel,
                                            Startdate = q.ExpectedDepartureDateTime,
                                            Enddate = q.ExpectedArrivalDateTime,
                                        }).ToList();

                    if (availbleship.Count != 0)
                    {
                        rptVSAAvailability.DataSource = availbleship;
                        rptVSAAvailability.DataBind();
                    }
                    else
                    {
                        String strAppMsg = ConfigurationManager.AppSettings["DFnorecord"];
                        lblmsg.Text = strAppMsg;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }

                Selection.Visible = true;

                lblValue1.Text = "From Date : ";
                lblResValue1.Text = TxtFromDate.Text.Trim();

                lblValue2.Text = "To Date : ";
                lblResValue2.Text = TxtToDate.Text.Trim();
            }
            ddloriginport.ClearSelection();
            ddlDestinationPort.ClearSelection();

            TxtFromDate.Text = string.Empty;
            TxtToDate.Text = string.Empty;

            ExpectedeparturedateRB.Checked = false;
            ExpectedarrivaldateRB.Checked = false;
        }
        protected void lnkRoute_Click(object sender, EventArgs e)
        {
            string Voyageid = (sender as LinkButton).CommandArgument;

            Session["Voyageid"]= Voyageid;
            Response.Redirect("ShipRoute.aspx",false);
            
        }
    }
}