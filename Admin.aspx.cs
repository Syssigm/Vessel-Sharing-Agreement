using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public partial class Admin : System.Web.UI.Page
    {
        public static string ddlval = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
            var db = new VesselAgreement();
            string Customer = Convert.ToString(Session["CustomerID"]);
            
            if(Customer != "Admin")
            {
                Response.Redirect("VesselApplication.aspx", false);
            }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlval = ddlCategory.SelectedValue;
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 1)
            {
                try
                {
                Port.Visible = true;
                Vessel.Visible = false;
                Customer.Visible = false;
                VesselVoyage.Visible = false;
                invitedtls.Visible = false;
                VsaApplication.Visible = false;
                rptPortDetails.DataSource = "";
                rptPortDetails.DataBind();
                 lblCategoryType.Text = ddlCategory.SelectedItem.Text;
                TxtSearchBox.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 2)
            {
                try
                {
                Customer.Visible = true;
                Port.Visible = false;
                Vessel.Visible = false;
                VesselVoyage.Visible = false;
                invitedtls.Visible = false;
                VsaApplication.Visible = false;
                rptCustomerDetails.DataSource = "";
                rptCustomerDetails.DataBind();
                lblCategoryType.Text = ddlCategory.SelectedItem.Text;
                TxtSearchBox.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 3)
            {
                try
                {
                Vessel.Visible = true;
                Port.Visible = false;
                Customer.Visible = false;
                VesselVoyage.Visible = false;
                invitedtls.Visible = false;
                VsaApplication.Visible = false;
                rptVesselDetails.DataSource = "";
                rptVesselDetails.DataBind();
                lblCategoryType.Text = ddlCategory.SelectedItem.Text;
                TxtSearchBox.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) ==4 )
            {
                try
                {
                VesselVoyage.Visible = true;
                Vessel.Visible = false;
                Port.Visible = false;
                Customer.Visible = false;
                invitedtls.Visible = false;
                VsaApplication.Visible = false;
                rptVoyageDetails.DataSource = "";
                rptVoyageDetails.DataBind();
                lblCategoryType.Text = ddlCategory.SelectedItem.Text;
                TxtSearchBox.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 5)
            {
                try
                {
                invitedtls.Visible = true;
                VesselVoyage.Visible = false;
                Vessel.Visible = false;
                Port.Visible = false;
                Customer.Visible = false;
                VsaApplication.Visible = false;
                rptinviteDetails.DataSource = "";
                rptinviteDetails.DataBind();
                lblCategoryType.Text = ddlCategory.SelectedItem.Text;
                TxtSearchBox.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 6)
            {
                try
                {
                VsaApplication.Visible = true;
                invitedtls.Visible = false;
                VesselVoyage.Visible = false;
                Vessel.Visible = false;
                Port.Visible = false;
                Customer.Visible = false;
                rptvsaapplicationDetails.DataSource = "";
                rptvsaapplicationDetails.DataBind();
                lblCategoryType.Text = ddlCategory.SelectedItem.Text;
                TxtSearchBox.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            { 
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 1)
            {
                portdetails();
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 2)
            {
                Cuatomerdetails();
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 3)
            {
                vesseldetails();
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 4)
            {
                Voyagedetails();
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 5)
            {
                invitedetails();
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) == 6)
            {
                applicationdetails();
            }
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void portdetails()
        {
            try
            { 
            var db = new VesselAgreement();
            var portdtls = (from q in db.VSA_Config_Port
                            where q.PortName.Contains(TxtSearchBox.Text.Trim())||q.PortID.Contains(TxtSearchBox.Text.Trim())
                            select new
                            {
                                PortID= q.PortID,
                                PortName=q.PortName
                            }).ToList();
                            
            rptPortDetails.DataSource = portdtls;
            rptPortDetails.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void vesseldetails()
        {
            try
            {
                var db = new VesselAgreement();
            var Vesseldtls = (from q in db.VSA_Master_Vessel
                            where q.NameoftheVessel.Contains(TxtSearchBox.Text.Trim()) || q.IMOShipID.Contains(TxtSearchBox.Text.Trim())
                            select new
                            {
                                PortID = q.NameoftheVessel,
                                PortName = q.IMOShipID,
                                VesselID=q.VesselID
                            }).ToList();

            rptVesselDetails.DataSource = Vesseldtls;
            rptVesselDetails.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Cuatomerdetails()
        {
            try
            {
                var db = new VesselAgreement();
            var Customerdtls = (from q in db.VSA_Master_Customer
                              where q.ContactFirstName.Contains(TxtSearchBox.Text.Trim())|| q.ContactLastName.Contains(TxtSearchBox.Text.Trim()) || q.CompanyName.Contains(TxtSearchBox.Text.Trim())
                              select new
                              {
                                  CustomerName = q.ContactFirstName+" "+q.ContactLastName,
                                  CompanyName = q.CompanyName,
                                  CustomerID = q.CustomerID
                              }).ToList();

            rptCustomerDetails.DataSource = Customerdtls;
            rptCustomerDetails.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Voyagedetails()
        {
            try
            {
                var db = new VesselAgreement();
            var Voyagedtls = (from q in db.VSA_Txn_VesselVoyage
                                where q.VoyageID.Contains(TxtSearchBox.Text.Trim()) || q.VesselID.Contains(TxtSearchBox.Text.Trim()) 
                                select new
                                {
                                    VoyageID = q.VoyageID,
                                    VesselID = q.VesselID
                                }).ToList();

            rptVoyageDetails.DataSource = Voyagedtls;
            rptVoyageDetails.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void invitedetails()
        {
            try
            {
                var db = new VesselAgreement();
            var Voyagedtls = (from q in db.VSA_Txn_Invite
                              where q.VoyageID.Contains(TxtSearchBox.Text.Trim())
                              select new
                              {
                                  VoyageID = q.VoyageID,
                                  Freespace = q.AvailableSpaceTEU
                              }).ToList();

            rptinviteDetails.DataSource = Voyagedtls;
            rptinviteDetails.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void applicationdetails()
        {
            try
            {
                var db = new VesselAgreement();
            var Voyagedtls = (from q in db.VSA_Txn_Participant_Application
                              where q.VoyageID.Contains(TxtSearchBox.Text.Trim()) || q.VSAParticipantCustomerID.Contains(TxtSearchBox.Text.Trim())
                              select new
                              {
                                  VoyageID = q.VoyageID,
                                  Customer = q.VSAParticipantCustomerID
                              }).ToList();

            rptvsaapplicationDetails.DataSource = Voyagedtls;
            rptvsaapplicationDetails.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Search(string prefixText, int count)
        {
            try
            {
                List<string> li = new List<string>();
                var ddl= li;
               
               var db = new VesselAgreement();
                if(ddlval == "1")
                { 
                var port = db.VSA_Config_Port.Where(x => x.PortName.StartsWith(prefixText)).OrderBy(x => x.PortName).Select(x => x.PortName).Distinct().Take(count).ToList();
                    ddl = port;
                }
                if (ddlval == "2")
                {
                    var Customer = db.VSA_Master_Customer.Where(x => x.CompanyName.StartsWith(prefixText)).OrderBy(x => x.CompanyName).Select(x => x.CompanyName).Distinct().Take(count).ToList();
                    ddl = Customer;
                }
                if (ddlval == "3")
                {
                    var Vessel = db.VSA_Master_Vessel.Where(x => x.NameoftheVessel.StartsWith(prefixText)).OrderBy(x => x.NameoftheVessel).Select(x => x.NameoftheVessel).Distinct().Take(count).ToList();
                    ddl = Vessel;
                }
                if (ddlval == "4")
                {
                    var Voyage = db.VSA_Txn_VesselVoyage.Where(x => x.VoyageID.StartsWith(prefixText)).OrderBy(x => x.VoyageID).Select(x => x.VoyageID).Distinct().Take(count).ToList();
                    ddl = Voyage;
                }
                if (ddlval == "5")
                {
                    var VSAInvite = db.VSA_Txn_Invite.Where(x => x.VoyageID.StartsWith(prefixText)).OrderBy(x => x.VoyageID).Select(x => x.VoyageID).Distinct().Take(count).ToList();
                    ddl = VSAInvite;
                }
                if (ddlval == "6")
                {
                    var VSAApplications = db.VSA_Txn_Participant_Application.Where(x => x.VSAParticipantCustomerID.StartsWith(prefixText)).OrderBy(x => x.VSAParticipantCustomerID).Select(x => x.VSAParticipantCustomerID).Distinct().Take(count).ToList();
                    ddl = VSAApplications;
                }
                if (ddlval == "7")
                {
                    var FinalVSA = db.VSA_Txn_Participant_Review_and_Approved.Where(x => x.VSAParticipantCustomerID.StartsWith(prefixText)).OrderBy(x => x.VSAParticipantCustomerID).Select(x => x.VSAParticipantCustomerID).Distinct().Take(count).ToList();
                    ddl = FinalVSA;
                }
                return ddl;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            }
        protected void btnEditCust_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();

            string Customerid = (sender as LinkButton).CommandArgument;

            Response.Redirect("ViewModifyCustomer.aspx?Name=" + Customerid);
        }
        protected void btnDeleteCust_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            {
                string Customerid = (sender as LinkButton).CommandArgument;

                var addrsid = (from q in db.VSA_Master_Customer_Addresses
                               where q.CustomerID == Customerid
                               select new { q.AddressID }).SingleOrDefault();

                var deleteaddressid = db.VSA_Address.Where(x => x.AddressID == addrsid.AddressID).SingleOrDefault();
                db.VSA_Address.Remove(deleteaddressid);

                var deletecustaddress = db.VSA_Master_Customer_Addresses.Where(x => x.CustomerID == Customerid).SingleOrDefault();
                db.VSA_Master_Customer_Addresses.Remove(deletecustaddress);

                var deletecustomertype = db.VSA_Master_Customer_and_CustomerTypes.Where(x => x.CustomerID == Customerid).SingleOrDefault();
                db.VSA_Master_Customer_and_CustomerTypes.Remove(deletecustomertype);

                var deleteCustomer = db.VSA_Master_Customer.Where(x => x.CustomerID == Customerid).SingleOrDefault();
                db.VSA_Master_Customer.Remove(deleteCustomer);

                db.SaveChanges();

                lblmsg.Text = "Deleted Successfully";
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;

                DbTrans.Commit();

                Cuatomerdetails();
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                throw ex;
                //lblmsg.Text = ex.Message;
            }
        }
        protected void lnkEditPort_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();

            string Portid = (sender as LinkButton).CommandArgument;

            Response.Redirect("ViewModifyPort.aspx?Name=" + Portid);
        }
        protected void lnkDeletePort_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            {
                string Portid = (sender as LinkButton).CommandArgument;

                var deletePort = db.VSA_Config_Port.Where(x => x.PortID == Portid).SingleOrDefault();
                db.VSA_Config_Port.Remove(deletePort);

                db.SaveChanges();

                lblmsg.Text = "Deleted Successfully";
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;

                DbTrans.Commit();

                portdetails();
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                //throw ex;
                lblmsg.Text = ex.Message;
            }
        }
        protected void lnkEditVes_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();

            string vesselid = (sender as LinkButton).CommandArgument;

            Response.Redirect("ViewModifyVessel.aspx?Name=" + vesselid);
        }
        protected void lnkDeleteVes_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            {
                string vesselid = (sender as LinkButton).CommandArgument;

                var deletePort = db.VSA_Master_Vessel.Where(x => x.VesselID == vesselid).SingleOrDefault();
                db.VSA_Master_Vessel.Remove(deletePort);

                db.SaveChanges();

                lblmsg.Text = "Deleted Successfully";
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;

                DbTrans.Commit();

                vesseldetails();
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                //throw ex;
                lblmsg.Text = ex.Message;
            }
        }
    }
    }
