using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public partial class ShipRoute : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Route();
        }

        public void Route()
        {
            string Originport, Destinationport;
            var db = new VesselAgreement();

            string voyageid = Convert.ToString(Session["Voyageid"]);
            Originport= Convert.ToString(Session["Originport"]);
            Destinationport= Convert.ToString(Session["Destinationport"]);
            DateTime Fromdate = Convert.ToDateTime(Session["DeptFromdate"]);
            DateTime Todate = Convert.ToDateTime(Session["DeptTodate"]);

            var shiproute = (from q in db.VSA_Txn_VesselVoyageTransitShipRoute
                             join u in db.VSA_Config_Port on q.OriginTransitPortID equals u.PortID
                             join t in db.VSA_Config_Port on q.DestinationTransitPortID equals t.PortID
                             where q.VoyageID == voyageid
                             select new
                             {
                                 voyagesequence =  q.VoyageSegmentSequenceNumber,
                                    OriginPort= u.PortName,
                                    DestinationPort=t.PortName,
                                 Startdate = q.ExpectedDepartureDateTime,
                                 Enddate = q.ExpectedArrivalDateTime,

                             }).ToList();

            rptshiproute.DataSource = shiproute;
            rptshiproute.DataBind();

            for (int i = 0; i < rptshiproute.Items.Count; i++)
            {
                Label lblOriginport = (Label)rptshiproute.Items[i].FindControl("lblOriginport");
                Label lblDestinationport = (Label)rptshiproute.Items[i].FindControl("lblDestinationport");
                Label lblStartdate = (Label)rptshiproute.Items[i].FindControl("lblStartdate");
                Label lblEnddate = (Label)rptshiproute.Items[i].FindControl("lblEnddate");

                if (lblOriginport.Text == Originport.Trim())
                {
                    lblOriginport.Text = "<font color='Black' Bold='true'>Route Origin: </font>" + lblOriginport.Text;
                    lblOriginport.ForeColor = System.Drawing.Color.Blue;
                    lblOriginport.Font.Bold = true;
                    lblStartdate.ForeColor = System.Drawing.Color.Blue;
                    lblStartdate.Font.Bold = true;
                }
                if (lblDestinationport.Text == Destinationport.Trim())
                {
                    lblDestinationport.Text = "<font color='Black' Bold='true'>Route Destination: </font>" + lblDestinationport.Text;
                    lblDestinationport.ForeColor = System.Drawing.Color.Blue;
                    lblDestinationport.Font.Bold = true;
                    lblEnddate.ForeColor = System.Drawing.Color.Blue;
                    lblEnddate.Font.Bold = true;
                }

                //if(Convert.ToDateTime(lblStartdate.Text) == Fromdate)
                //{
                //    lblStartdate.ForeColor = System.Drawing.Color.Blue;
                //    lblStartdate.Font.Bold = true;
                //}
                //if (Convert.ToDateTime(lblStartdate.Text) == Todate)
                //{
                //    lblStartdate.ForeColor = System.Drawing.Color.Blue;
                //    lblStartdate.Font.Bold = true;
                //}
            }

        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }
    }
}