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
    public partial class VSAArrangementFeeStructure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                year();
                Currency();
                CustomerType();
            }
        }
        public void year()
        {
            try
            {
                var db = new VesselAgreement();

                ddlyear.DataSource = db.VSA_Year.ToList();
                ddlyear.DataTextField = "Year";
                ddlyear.DataValueField = "Year";
                ddlyear.DataBind();
                ddlyear.Items.Insert(0, new ListItem("--Select Year--", "0"));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public void Currency()
        {
            try
            {
                var db = new VesselAgreement();

                ddlCurrency.DataSource = db.VSA_Currency.ToList();
                ddlCurrency.DataTextField = "Currency";
                ddlCurrency.DataValueField = "CurrencyCode";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CustomerType()
        {
            try
            {
                var db = new VesselAgreement();

                ddlCustomerType.DataSource =db.VSA_Config_CustomerType.ToList();
                ddlCustomerType.DataTextField = "CustomerTypeID";
                ddlCustomerType.DataValueField = "CustomerTypeID";
                ddlCustomerType.DataBind();
                ddlCustomerType.Items.Insert(0, new ListItem("--Select Customer Type--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected void BtnFeeCreate_Click(object sender, EventArgs e)
        {
            var db = new VesselAgreement();
            var DbTrans = db.Database.BeginTransaction();
            try
            { 
           
                var FeeArrangement = new VSA_Master_VSA_Arrangement_Fee();

            FeeArrangement.Year = Convert.ToInt32(ddlyear.SelectedValue);
            FeeArrangement.Month = ddlMonth.SelectedValue;
            FeeArrangement.Currency = ddlCurrency.SelectedValue;
            FeeArrangement.CustomerTypeID = ddlCustomerType.SelectedValue;
            FeeArrangement.Customer_Rating = ddlCustomerRating.SelectedItem.Text;
            FeeArrangement.VSAArrangementFeePerTEU = Convert.ToInt32(TxtArrangeFee.Text);
            FeeArrangement.Create_ts= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:sszzz"));
            FeeArrangement.Update_ts= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:sszzz"));

            db.VSA_Master_VSA_Arrangement_Fee.Add(FeeArrangement);
            db.SaveChanges();
                String strAppMsg = ConfigurationManager.AppSettings["AFcreated"] + ddlMonth.SelectedItem.Text;
                lblmsg.Text = strAppMsg;
                lblmsg.ForeColor = System.Drawing.Color.ForestGreen;
                DbTrans.Commit();
                clear();
            }
            catch(Exception ex)
            {
                DbTrans.Rollback();
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void clear()
        {
            try
            { 
            ddlyear.SelectedIndex=0;
            ddlMonth.SelectedIndex = 0;
            ddlCurrency.SelectedIndex=0;
            ddlCustomerType.SelectedIndex = 0;
            ddlCustomerRating.SelectedIndex = 0;
            TxtArrangeFee.Text = "";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            { 
            clear();
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}