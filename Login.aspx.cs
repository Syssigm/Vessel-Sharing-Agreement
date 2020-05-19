using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                Response.Redirect("VesselApplication.aspx", false);
            }
        }

        // Method LnkLogin_Click: Use : To trigger the login method by click of login button
        protected void LnkLogin_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var login = (from q in db.VSA_Login
                         where q.CustomerID == TxtUsername.Text && q.Password == TxtPassword.Text
                         select new { q.CustomerID }).ToList();
            if (login.Count != 0)
            {
                Session["CustomerID"] = login[0].CustomerID;
                FormsAuthentication.RedirectFromLoginPage(TxtUsername.Text, true);
                Response.Redirect("VesselApplication.aspx", false);
            }
            else
            {
                String strAppMsg = ConfigurationManager.AppSettings["LGinvaliedlogin"];
                lblmsg.Text = strAppMsg;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method LnkReset_Click: Use : To reset the values in the page to default
        protected void LnkReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        // Method Btnforgotpasswordsubmit_Click: Use : To reset the forgotten password
        protected void Btnforgotpasswordsubmit_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var username = (from q in db.VSA_Login
                            where q.CustomerID == TxtForgotpasswordUsername.Text.Trim()
                            join p in db.VSA_Master_Customer on q.CustomerID equals p.CustomerID
                            where p.EmailID == TxtForgotpasswordemailid.Text.Trim()
                            select new { }).ToList();
            if(username.Count==1)
            { 
            var resetpassword = (from q in db.VSA_Login
                                 where q.CustomerID == TxtForgotpasswordUsername.Text.Trim()
                                 select new { q }).SingleOrDefault();
            resetpassword.q.Password = UniqueNumber();
            db.SaveChanges();
                sendMail();
            }
            else
            {
                String strAppMsg = ConfigurationManager.AppSettings["LGinvaliedlogin"];
                lblmsg.Text = strAppMsg;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method UniqueNumber(): To generate automatic password for the forgot password functionality

        public String characters = "abcdeCDEfghijkzMABFHIJKLNOlmnopqrPQRSTstuvwxyUVWXYZ";
        public string UniqueNumber()
        {
            try
            {
                Random unique1 = new Random();
                string s = "IN";
                int unique;
                int n = 0;
                while (n < 6)
                {
                    if (n % 2 == 0)
                    {
                        s += unique1.Next(10).ToString();
                    }
                    else
                    {
                        unique = unique1.Next(52);
                        if (unique < this.characters.Length)
                            s = String.Concat(s, this.characters[unique]);
                    }
                    n++;
                }
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method sendMail(): To send the userid/passwd details to the customer mail id

        void sendMail()
        {
            try
            {

                var db = new VesselAgreement();
                var password = (from q in db.VSA_Login
                                where q.CustomerID == TxtForgotpasswordUsername.Text.Trim()
                                select new { q.Password }).SingleOrDefault();
                string strNewPassword = password.Password;

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("syssigma12345@gmail.com");
                msg.To.Add(TxtForgotpasswordemailid.Text.Trim());
                msg.Subject = "New Password for your Account";
                msg.Body = "Your password is:" + strNewPassword +
                    "  ------To Change password fallow the : <a href='http://vsa.syssigma.com/VSAChangepassword.aspx'>Link</a>";

                msg.IsBodyHtml = true;

                SmtpClient smt = new SmtpClient();
                //smt.Host = "smtp.gmail.com";
                smt.Host = "relay-hosting.secureserver.net";
                System.Net.NetworkCredential ntwd = new NetworkCredential();
                ntwd.UserName = "syssigma12345@gmail.com"; //Your Email ID  
                ntwd.Password = "sys123456"; // Your Password 
                smt.UseDefaultCredentials = false;
                smt.Credentials = ntwd;
                smt.Port = 25;
                smt.EnableSsl = false;
                smt.Send(msg);
                String strAppMsg = ConfigurationManager.AppSettings["LGpasswordconfrim"];
                lblmsg.Text = strAppMsg;
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}