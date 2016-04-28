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
                string tempName = (string)row["FirstName"] + " " + (string)row["LastName"];
                string tempEmail = (string)row["Email"];
                string tempId = row["UsrId"].ToString();
                if (tempPass == HashPass.Value)
                {
                    //authenticated password should not be stored in cookie
                    //ASP.NET_SessionId
                    foreach (HttpCookie cookieTemp in Response.Cookies)
                    {
                        System.Diagnostics.Debug.Print(cookieTemp["Email"]);
                        if (cookieTemp["Email"] == tempEmail)
                        {

                            Comment.Text = "Your account was logged in from other places!";
                            return;
                        }
                    }
                    string sessionId = this.Session.SessionID;
                    HttpCookie cookie = new HttpCookie("sessionId");
                    cookie["Name"] = tempName;
                    cookie["Email"] = tempEmail;
                    cookie["Id"] = tempId;
                    Response.Cookies.Add(cookie);
                    Comment.Text = "Login success! Redirect to the main page in 5 second.";

                    if (RememberMe.Checked == true)
                    {
                        FormsAuthentication.RedirectFromLoginPage(tempEmail, true);
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(tempEmail, false);
                    }

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
        FormsAuthentication.SignOut();
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

    protected void CrtAccBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }
}