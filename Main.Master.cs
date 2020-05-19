using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public partial class Main : System.Web.UI.MasterPage
    {
        // Method Page_Load: To initiate customer session variables
        protected void Page_Load(object sender, EventArgs e)
        {

            string Customer = Convert.ToString(Session["CustomerID"]);
            if (Customer != "")
            {
                var db = new VesselAgreement();

                var SuperAdmin = (from q in db.VSA_Login
                                  where q.CustomerID == Customer
                                  where q.CustomerTypeID == "ADMIN"
                                  select new { }).Count();
                if (SuperAdmin == 0)
                {
                    var cmpyname = (from q in db.VSA_Master_Customer
                                    where q.CustomerID == Customer
                                    select new { q.CompanyName }).SingleOrDefault();
                    lblcompanyname.Text = " Hi " + cmpyname.CompanyName;
                    lblcompanyname.ForeColor = System.Drawing.Color.Brown;
                }
                else
                {
                    lblcompanyname.Text = " Hi Admin";
                    lblcompanyname.ForeColor = System.Drawing.Color.Brown;
                }
                AccessMenu();
            }
            else
            {
                Response.Redirect("Logout.aspx", false);
            }

        }
        public void AccessMenu()
        {
            var db = new VesselAgreement();

            string Customer = Convert.ToString(Session["CustomerID"]);

            var SSLINEcustomerType = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                      where q.CustomerID == Customer
                                      where q.CustomerTypeID == "SSLINE"
                                      select new { q.CustomerTypeID }).ToList();

            var VSLOPRcustomerType = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                      where q.CustomerID == Customer
                                      where q.CustomerTypeID == "VSLOPR"
                                      select new { q.CustomerTypeID }).ToList();

            var CRGOOPcustomerType = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                      where q.CustomerID == Customer
                                      where q.CustomerTypeID == "CRGOOP"
                                      select new { q.CustomerTypeID }).ToList();

            var AGENTScustomerType = (from q in db.VSA_Master_Customer_and_CustomerTypes
                                      where q.CustomerID == Customer
                                      where q.CustomerTypeID == "AGENTS"
                                      select new { q.CustomerTypeID }).ToList();

            var SuperAdmin = (from q in db.VSA_Login
                              where q.CustomerID == Customer
                              where q.CustomerTypeID == "SADM"
                              select new { q.CustomerTypeID }).ToList();

            if (SSLINEcustomerType.Count == 1 && VSLOPRcustomerType.Count == 0)
            {
                SubMenuCreatePort.Visible = false;
                SubMenuCreateVesselVoyage.Visible = false;
                SubMenuInviteVSA.Visible = false;
                SubMenuViewModifyPort.Visible = false;

                string currentUrl = Request.Url.AbsoluteUri;
                if (currentUrl == "http://localhost:50886/PortRegistration.aspx" || currentUrl == "http://localhost:50886/VesselVoyageDetails.aspx" || currentUrl == "http://localhost:50886/VsaInvite.aspx" || currentUrl == "http://localhost:50886/ViewModifyPort.aspx")
                {
                    Response.Redirect("VesselApplication.aspx", false);
                }
            }

            if (VSLOPRcustomerType.Count == 1 && SSLINEcustomerType.Count == 0)
            {

                SubMenuCreateVessel.Visible = false;
                SubMenuViewModifyVessel.Visible = false;

                string currentUrl = Request.Url.AbsoluteUri;
                if (currentUrl == "http://localhost:50886/VesselRegistration.aspx" || currentUrl == "http://localhost:50886/ViewModifyVessel.aspx")
                {
                    Response.Redirect("VesselApplication.aspx", false);
                }
            }
            if (CRGOOPcustomerType.Count == 1 && VSLOPRcustomerType.Count == 0 && SSLINEcustomerType.Count == 0 || AGENTScustomerType.Count == 1 && VSLOPRcustomerType.Count == 0 && SSLINEcustomerType.Count == 0)
            {
                MenuVsaVoyage.Visible = false;

                string currentUrl = Request.Url.AbsoluteUri;
                if (currentUrl == "http://localhost:50886/VesselRegistration.aspx" || currentUrl == "http://localhost:50886/ViewModifyVessel.aspx" || currentUrl == "http://localhost:50886/AddBillAddress.aspx" || currentUrl == "http://localhost:50886/AddDBAAddress.aspx" || currentUrl == "http://localhost:50886/PortRegistration.aspx" || currentUrl == "http://localhost:50886/VesselVoyageDetails.aspx" || currentUrl == "http://localhost:50886/VsaInvite.aspx" || currentUrl == "http://localhost:50886/ViewModifyPort.aspx" || currentUrl == "http://localhost:50886/VSAArrangementFeeStructure.aspx")
                {
                    Response.Redirect("VesselApplication.aspx", false);
                }
            }
            if (SuperAdmin.Count == 1)
            {
                MenuVsaVoyage.Visible = false;
                SubMenuVsaVoyage.Visible = false;
                Menucustomer.Visible = false;

                string currentUrl = Request.Url.AbsoluteUri;
                if (currentUrl == "http://localhost:50886/VesselApplication.aspx")
                {
                    Response.Redirect("Admin.aspx", false);
                }
                if (currentUrl == "http://localhost:50886/Admin.aspx")
                {
                    Response.Redirect("ViewModifyCustomer.aspx", false);
                }
            }
        }
    }
}