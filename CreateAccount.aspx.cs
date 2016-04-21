using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAccount : System.Web.UI.Page
{
    private static DataTable dataTable;
    private static string action;
    private static string uid;

    protected void Page_Load(object sender, EventArgs e)
    {
        action = Request.QueryString["Action"];
        if (action == "edit")
        {

        }

    }

    protected void SavBut_Click(object sender, EventArgs e)
    {
        string userName = UsrNameInput.Text;
        string password = PswInput.Text;
        string email = EmailInput.Text;
        bool invalidEmailFlag = !EmailVerify(email); //problem
        bool emailRedFlag = false;
        string errMessage = "Save Failed!<br />";
        updateDataTable();
        HashPass.Value = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PswInput.Text, "SHA1");
        //check if the email is already exist
        foreach (DataRow row in dataTable.Rows)
        {
            if ((row["Email"].ToString() == email))
            {
                emailRedFlag = true;
            }
        }
        if (emailRedFlag || invalidEmailFlag)
        {
            //bad email
            Comment.Visible = true;
            if (invalidEmailFlag)
            {
                errMessage += "Invalid Email address!<br />";
            }
            else
            {
                if (emailRedFlag)
                {
                    errMessage += "The E-mail already exists!";
                }
            }
            Comment.Text = errMessage;
        }
        else
        {

            if (action == "create")
            {
                SqlDataSource1.Insert();
                //get user infor for cookie
                /*
                DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
                */
                updateDataTable();
                DataRow row = dataTable.Rows[0];
                string tempName = (string)row["UsrName"];
                string tempEmail = (string)row["Email"];
                string tempId = row["UsrId"].ToString();
                HttpCookie cookie = new HttpCookie("UserInfo");
                cookie["Name"] = tempName;
                cookie["Email"] = tempEmail;
                cookie["Id"] = tempId;
                Response.Cookies.Add(cookie);
                //Response.Redirect("Default.aspx?login=true&Id=" + uid);
                FormsAuthentication.RedirectFromLoginPage(tempEmail, true);
            }
            else //modify
            {
                /*
                SqlDataSource1.UpdateCommand = "UPDATE [user] (UsrName, Password, Email) VALUES(@val1, @val2, @val3) WHERE (UsrId=@val4) ";
                SqlDataSource1.UpdateParameters.Add("val1", userName);
                SqlDataSource1.UpdateParameters.Add("val2", password);
                SqlDataSource1.UpdateParameters.Add("val3", email);
                SqlDataSource1.UpdateParameters.Add("val4", uid);
                Response.Redirect("Default.aspx?login=true&Id=" + uid);
                //update success? comment or redirect
                */
            }
        }

    }
    //get the user info of email input
    protected void updateDataTable()
    {
        DataView dataView = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
        if (dataView == null)
        {
            Comment.Visible = true;
            Comment.Text = "<br />Fail to initialize the data view, please check your connection to the database";
        }
        dataTable = dataView.ToTable();
    }

    protected bool EmailVerify(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }

    }
}