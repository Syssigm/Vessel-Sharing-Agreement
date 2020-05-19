using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models; 

namespace VesselSharingAgreement
{
    public partial class VSAChangepassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblmsg.Text = ""; 
        }
        public void changepassword()
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            { 
            
            var changepswd = (from q in db.VSA_Login
                              where q.CustomerID == TxtUsername.Text.Trim()
                              select new { q }).SingleOrDefault();

           if(TxtNewPassword.Text.Trim() == TxtConfirmPassword.Text.Trim())
            {
                changepswd.q.Password = TxtNewPassword.Text.Trim();

                db.SaveChanges();
                lblmsg.Text = "Password Changed Successfully";
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
                DbTrans.Commit();
                }
           else
            {
                lblmsg.Text = "New Password and Confrim Password is not matching";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            }
            catch(Exception ex)
            {
                DbTrans.Rollback();
                throw ex;
            }
        }

        protected void LnkSubmit_Click(object sender, EventArgs e)
        {
            try
            { 
            changepassword();
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void LnkReset_Click(object sender, EventArgs e)
        {
            try
            { 
            TxtUsername.Text = "";
            TxtNewPassword.Text = "";
            TxtConfirmPassword.Text = "";
            lblmsg.Text = "";
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}