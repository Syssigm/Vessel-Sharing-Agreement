using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            var db = new VesselAgreement();

            Session.Add("CustomerID", "");
            Response.Redirect("~/Default.aspx");

            //Application.Lock();
            //var Count = (from q in db.tbl_15thStreetPizzaVisitors
            //             select new { q.VisitorCount }).SingleOrDefault();
            //Application["NoOfVisitors"] = (Count.VisitorCount + 1);
            //Application.UnLock();
            //var vistorupdate = (from q in db.tbl_15thStreetPizzaVisitors
            //                    where q.VisitorId == 1
            //                    select new { q }).SingleOrDefault();
            //vistorupdate.q.VisitorCount = Convert.ToInt32(Application["NoOfVisitors"]);
            //vistorupdate.q.UpDate_ts = System.DateTime.Now;
            //db.SaveChanges();
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session.RemoveAll();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}