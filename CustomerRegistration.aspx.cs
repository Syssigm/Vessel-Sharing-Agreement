using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VesselSharingAgreement.Models;

namespace VesselSharingAgreement
{
    public partial class CustomerRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlcontryreg();
                ddlcontrybill();
                ddlcontrydba();
            }
            lblerrCustomerType.Text = "";
        }

        // Method custregistration(): Use : To Register the details of a new customer
        public void custregistration()
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();

            try
            {
                var mstrcust = new VSA_Master_Customer();
                var custaddress = new VSA_Address();
                var login = new VSA_Login();
                var mstraddress = new VSA_Master_Customer_Addresses();
                var custTyp = new VSA_Config_CustomerType();

                var custvalid = (from q in db.VSA_Master_Customer
                                 where q.CustomerID == TxtCustomerId.Text
                                 select new { q.CustomerID }).ToList();
                if (custvalid.Count == 0)
                {
                    mstrcust.CustomerID = TxtCustomerId.Text;
                    mstrcust.CompanyName = TxtCompanyName.Text;
                    mstrcust.IMO_companyNumber = TxtIMOShipId.Text;
                    mstrcust.ContactFirstName = TxtFirstName.Text;
                    mstrcust.ContactLastName = TxtLastName.Text;
                    mstrcust.ContactNumber = TxtPhoneNumber.Text;
                    mstrcust.AlternateContactNumber = TxtAltPhoneNumber.Text;
                    mstrcust.RegisteredTS = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                    mstrcust.Customer_Status = "Active";
                    mstrcust.EmailID = TxtEmailID.Text;
                    mstrcust.Customer_Rating = "1";

                    if (ckSSLine.Checked)
                    {
                        var mstrTyp = new VSA_Master_Customer_and_CustomerTypes();


                        mstrTyp.CustomerID = mstrcust.CustomerID;
                        mstrTyp.CustomerTypeID = "SSLINE";
                        custTyp.CustomerTypeID = "SSLINE";

                        db.VSA_Master_Customer_and_CustomerTypes.Add(mstrTyp);

                        if(login.CustomerTypeID ==null)
                        { 
                        login.CustomerTypeID = custTyp.CustomerTypeID;
                        }
                    }
                    if (ckVeselOperator.Checked)
                    {
                        var mstrTyp1 = new VSA_Master_Customer_and_CustomerTypes();
                        mstrTyp1.CustomerID = mstrcust.CustomerID;
                        mstrTyp1.CustomerTypeID = "VSLOPR";
                        custTyp.CustomerTypeID = "VSLOPR";

                        db.VSA_Master_Customer_and_CustomerTypes.Add(mstrTyp1);

                        if (login.CustomerTypeID == null)
                        {
                            login.CustomerTypeID = custTyp.CustomerTypeID;
                        }
                    }
                    if (ckCargOperator.Checked)
                    {
                        var mstrTyp2 = new VSA_Master_Customer_and_CustomerTypes();
                        mstrTyp2.CustomerID = mstrcust.CustomerID;
                        mstrTyp2.CustomerTypeID = "CRGOOP";
                        custTyp.CustomerTypeID = "CRGOOP";

                        db.VSA_Master_Customer_and_CustomerTypes.Add(mstrTyp2);
                        if (login.CustomerTypeID == null)
                        {
                            login.CustomerTypeID = custTyp.CustomerTypeID;
                        }
                    }
                    if (ckAgent.Checked)
                    {
                        var mstrTyp3 = new VSA_Master_Customer_and_CustomerTypes();
                        mstrTyp3.CustomerID = mstrcust.CustomerID;
                        mstrTyp3.CustomerTypeID = "AGENTS";
                        custTyp.CustomerTypeID = "AGENTS";

                        db.VSA_Master_Customer_and_CustomerTypes.Add(mstrTyp3);

                        if (login.CustomerTypeID == null)
                        {
                            login.CustomerTypeID = custTyp.CustomerTypeID;
                        }
                    }


                    if (reg.Checked)
                    {
                        mstraddress.AddressID = custaddress.AddressID;
                        mstraddress.CustomerID = mstrcust.CustomerID;

                        custaddress.AddressType = "Registered";
                        custaddress.BuildingNumber = TxtBuildingNum.Text;
                        custaddress.CityName = TxtCity.Text;
                        custaddress.StreetName1 = TxtStreet.Text;
                        custaddress.StreetName2 = TxtStreet.Text;
                        custaddress.State = TxtState.Text;
                        custaddress.Zipcode = TxtPostZip.Text;
                        custaddress.Country_Code = ddlcountryReg.SelectedValue;
                        custaddress.AddressStatus = "A";
                        custaddress.Registered_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                    }

                    //login.CustomerTypeID = custTyp.CustomerTypeID;
                    login.CustomerID = TxtCustomerId.Text;
                    login.UserExpire_Flag = "N";
                    login.Create_Ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                    string imo = TxtIMOShipId.Text;

                    if (imo.Length == 7)
                    {
                        int a = Convert.ToInt32(imo.Substring(0, 1));
                        var b = Convert.ToInt32(imo.Substring(1, 1));
                        var c = Convert.ToInt32(imo.Substring(2, 1));
                        var d = Convert.ToInt32(imo.Substring(3, 1));
                        var e = Convert.ToInt32(imo.Substring(4, 1));
                        var f = Convert.ToInt32(imo.Substring(5, 1));
                        var g = Convert.ToInt32(imo.Substring(6, 1));

                        var verifiedsum = (a * 7) + (b * 6) + (c * 5) + (d * 4) + (e * 3) + (f * 2);

                        int verifiedsumCount = verifiedsum.ToString().Length;

                        int matchcount = Convert.ToInt32(verifiedsum.ToString().Substring(verifiedsumCount - 1, 1));

                        if (matchcount != g)
                        {
                            String strAppMsg = ConfigurationManager.AppSettings["CRimonotvalid"];
                            lblmsg.Text = strAppMsg;
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            if ((ckSSLine.Checked == false) && (ckVeselOperator.Checked == false) && (ckCargOperator.Checked == false) && (ckAgent.Checked == false))
                            {
                                String strAppMsg = ConfigurationManager.AppSettings["CRselectone"];
                                lblerrCustomerType.Text = strAppMsg;
                                lblerrCustomerType.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                login.Password = UniqueNumber();

                                db.VSA_Master_Customer_Addresses.Add(mstraddress);
                                db.VSA_Address.Add(custaddress);
                                db.VSA_Login.Add(login);
                                db.VSA_Master_Customer.Add(mstrcust);
                                db.SaveChanges();


                                if (bill.Checked)
                                {
                                    try
                                    {
                                        var custaddbill = new VSA_Address();
                                        var mstraddress2 = new VSA_Master_Customer_Addresses();

                                        custaddbill.AddressType = "Billing";
                                        custaddbill.BuildingNumber = TxtBillBuildingNum.Text;
                                        custaddbill.CityName = TxtBillCity.Text;
                                        custaddbill.StreetName1 = TxtBillStreet.Text;
                                        custaddbill.StreetName2 = TxtBillStreet.Text;
                                        custaddbill.State = TxtBillState.Text;
                                        custaddbill.Zipcode = TxtBillPostZip.Text;
                                        custaddbill.Country_Code = ddlcountryBill.SelectedValue;
                                        custaddbill.AddressStatus = "A";
                                        custaddbill.Registered_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                                        mstraddress2.AddressID = custaddbill.AddressID;
                                        mstraddress2.CustomerID = mstrcust.CustomerID;

                                        db.VSA_Master_Customer_Addresses.Add(mstraddress2);
                                        db.VSA_Address.Add(custaddbill);
                                        db.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }

                                }
                                if (dba.Checked)
                                {
                                    try
                                    {
                                        var custadddba = new VSA_Address();
                                        var mstraddress3 = new VSA_Master_Customer_Addresses();

                                        custadddba.AddressType = "DBA";
                                        custadddba.BuildingNumber = TxtDBABuildingNum.Text;
                                        custadddba.CityName = TxtDBACity.Text;
                                        custadddba.StreetName1 = TxtDBAStreet.Text;
                                        custadddba.StreetName2 = TxtDBAStreet.Text;
                                        custadddba.State = TxtDBAState.Text;
                                        custadddba.Zipcode = TxtDBAPostZip.Text;
                                        custadddba.Country_Code = ddlCountrydba.SelectedValue;
                                        custadddba.AddressStatus = "A";
                                        custadddba.Registered_ts = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                                        mstraddress3.AddressID = custadddba.AddressID;
                                        mstraddress3.CustomerID = mstrcust.CustomerID;

                                        db.VSA_Master_Customer_Addresses.Add(mstraddress3);
                                        db.VSA_Address.Add(custadddba);
                                        db.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                DbTrans.Commit();
                                sendMail();

                                String strAppMsg = ConfigurationManager.AppSettings["CRcustRegistration"];
                                lblmsg.Text = strAppMsg;
                                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
                                
                                clear();
                            }
                        }
                    }
                    else
                    {
                        String strAppMsg = ConfigurationManager.AppSettings["CRimonotvalid"];
                        lblmsg.Text = strAppMsg;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        DbTrans.Rollback();
                    }
                
                }

                else
                {
                    String strAppMsg = ConfigurationManager.AppSettings["CRduplicateCustomer"];
                    lblmsg.Text = strAppMsg;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    DbTrans.Rollback();
                }
            }
            catch (Exception ex)
            {
                DbTrans.Rollback();
                throw ex;
            }
        }

        // Method ddlcontryreg(): Use : To Populate Country Code Values in the registered address dropdown
        public void ddlcontryreg()
        {
            try
            {
                var db = new VesselAgreement();

                ddlcountryReg.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlcountryReg.DataTextField = "Country_Name";
                ddlcountryReg.DataValueField = "Country_Code";
                ddlcountryReg.DataBind();
                ddlcountryReg.Items.Insert(0, new ListItem("--Select country--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlcontrybill(): Use : To Populate Country Code Values in the billing address dropdown
        public void ddlcontrybill()
        {
            try
            {
                var db = new VesselAgreement();

                ddlcountryBill.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlcountryBill.DataTextField = "Country_Name";
                ddlcountryBill.DataValueField = "Country_Code";
                ddlcountryBill.DataBind();
                ddlcountryBill.Items.Insert(0, new ListItem("--Select country--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method ddlcontrydba(): Use : To Populate Country Code Values in the dba address dropdown
        public void ddlcontrydba()
        {
            try
            {
                var db = new VesselAgreement();

                ddlCountrydba.DataSource = db.VSA_Config_Country_Code.ToList().OrderBy(x => x.Country_Name);
                ddlCountrydba.DataTextField = "Country_Name";
                ddlCountrydba.DataValueField = "Country_Code";
                ddlCountrydba.DataBind();
                ddlCountrydba.Items.Insert(0, new ListItem("--Select country--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method clear(): To clear the values of different fields in the registration page


        public void clear()
        {
            try
            {
                TxtAltPhoneNumber.Text = "";
                TxtBuildingNum.Text = "";
                TxtCity.Text = "";
                TxtBillBuildingNum.Text = "";
                TxtBillCity.Text = "";
                TxtBillPostZip.Text = "";
                TxtBillState.Text = "";
                TxtBillStreet.Text = "";
                TxtBuildingNum.Text = "";
                TxtCompanyName.Text = "";
                TxtIMOShipId.Text = "";
                TxtCustomerId.Text = "";
                TxtDBABuildingNum.Text = "";
                TxtDBACity.Text = "";
                TxtDBAPostZip.Text = "";
                TxtDBAState.Text = "";
                TxtDBAStreet.Text = "";
                TxtEmailID.Text = "";
                TxtFirstName.Text = "";
                TxtLastName.Text = "";
                TxtPhoneNumber.Text = "";
                TxtPostZip.Text = "";
                TxtState.Text = "";
                TxtStreet.Text = "";
                ckSSLine.Checked = false;
                ckVeselOperator.Checked = false;
                ckCargOperator.Checked = false;
                ckAgent.Checked = false;
                sameBilladres.Checked = false;
                samedbaAddres.Checked = false;
                bill.Checked = false;
                dba.Checked = false;
                ddlcontryreg();
                ddlcontrybill();
                ddlcontrydba();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method btnRegister_Click: To register the customer details
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                custregistration();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method btnCancel_Click: To cancel the entered customer detail values
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method UniqueNumber(): To generate automatic password for the new customer

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
                                where q.CustomerID == TxtCustomerId.Text
                                select new { q.Password }).SingleOrDefault();
                string strNewPassword = password.Password;
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new System.Net.Mail.MailAddress("enquirysyssigma@gmail.com");
                msg.To.Add(TxtEmailID.Text);
                msg.Subject = "Password for your Account";
                msg.Body = "Your password is:" + strNewPassword +
                    "  ------To Change password fallow the : <a href='http://vsa.syssigma.com/VSAChangepassword.aspx'>Link</a>";

                msg.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smt = new System.Net.Mail.SmtpClient("relay-hosting.secureserver.net", 25);
                System.Net.NetworkCredential ntwd = new NetworkCredential();
                ntwd.UserName ="enquirysyssigma@gmail.com";// "syssigma12345@gmail.com"; //Your Email ID  
                ntwd.Password = "Friday@5"; //"sys123456"; // Your Password 
                smt.UseDefaultCredentials = false;
                smt.Credentials = ntwd;
                
                smt.EnableSsl = false;
                smt.Send(msg);
                String strAppMsg = ConfigurationManager.AppSettings["CRpasswordconfrim"];
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