using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;

public partial class Login : System.Web.UI.Page
{
    private static DataTable dataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        PswBox.MaxLength = 6;
        if (!Page.IsPostBack)
        {
            PswBox.Attributes["type"] = "password";
        }
    }

    protected void LoginBut_Click(object sender, EventArgs e)
    {
        Comment.Visible = true;
        string errMes = "Login failed!<br />";
        bool emptyEmail = (EmailBox.Text == string.Empty);
        bool emptyPassword = (PswBox.Text == string.Empty);
        bool badEmail = !EmailVerify(EmailBox.Text);
        if (emptyEmail || emptyPassword || badEmail)
        {
            if (emptyEmail)
            {
                errMes += "Please input the Email!<br />";
            }
            else
            {
                if (badEmail)
                {
                    errMes += "Bad Email format!<br />";
                }
                if (emptyPassword)
                {
                    errMes += "Please input the password!";
                }
            }
            Comment.Text = errMes;
        }
        else
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            if (dv.Table.Rows.Count == 0)
            {
                //Cannot find the email
                Comment.Text = "Login failed!<br />The E-mail can not be found!";
            }
            else
            {
                HashPass.Value = FormsAuthentication.HashPasswordForStoringInConfigFile(PswBox.Text, "SHA1");
                DataRow row = dv.Table.Rows[0];
                string tempPass = (string)row["Password"];
                string tempName = (string)row["UsrName"];
                string tempEmail = (string)row["Email"];
                string tempId = row["UsrId"].ToString();
                System.Diagnostics.Debug.Print("tempPass " + tempPass);
                System.Diagnostics.Debug.Print("hashpass " + HashPass.Value);
                if (tempPass == HashPass.Value)
                {
                    //authenticated
                    HttpCookie cookie = new HttpCookie("UserInfo");
                    cookie["Name"] = tempName;
                    cookie["Email"] = tempEmail;
                    cookie["Id"] = tempId;
                    Response.Cookies.Add(cookie);
                    Comment.Text = "Login success! Redirect to the main page in 5 second.";
                    System.Threading.Thread.Sleep(5000);
                    FormsAuthentication.RedirectFromLoginPage(tempEmail, false);
                    return;
                }
                else
                {
                    Comment.Text = errMes + "The combination of E-mail and password can not be found!";
                }
            }
        }
    }

    protected void RetBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void DisplayPsw_CheckedChanged(object sender, EventArgs e)
    {
        if (DisplayPsw.Checked)
        {
            PswBox.Attributes["type"] = "singlline";
        }
        else
        {
            PswBox.Attributes["type"] = "password";
        }
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