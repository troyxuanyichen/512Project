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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            //already login edit account use datasource to display all the information of the user
            string sessionId = this.Session.SessionID;
            HttpCookie cookie = Request.Cookies["sessionId"];
            if (cookie == null)
            {
                Response.Redirect("Login.aspx");
            }
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            dataTable = dv.ToTable();
            foreach (DataRow row in dataTable.Rows)
            {
                if ((row["Email"].ToString() == cookie["Email"]))
                {
                    if (!IsPostBack)
                    {
                        UsrNameInput.Text = row["UsrName"].ToString();
                        EmailInput.Text = row["Email"].ToString();
                        break;
                    }
                }
            }
        }
    }

    protected void SavBut_Click(object sender, EventArgs e)
    {
        Comment.Visible = true;
        string userName = UsrNameInput.Text;
        string password = PswInput.Text;
        string email = EmailInput.Text; //emailInput.text doesn't change
        string errMessage = "Save Failed!<br />";
        //emtpy item
        if ((UsrNameInput.Text == string.Empty) || (PswInput.Text == string.Empty) || (EmailInput.Text == string.Empty))
        {
            errMessage += "Please fill in all of the item!";
            Comment.Text = errMessage;
            return;
        }
        bool invalidEmailFlag = !EmailVerify(email);
        bool invalidPassword = !PasswordVerify(password);
        bool emailRedFlag = false;

        HashPass.Value = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PswInput.Text, "SHA1");

        //invalid email address
        if (invalidEmailFlag)
        {
            errMessage += "Invalid Email address!<br />";
            Comment.Text = errMessage;
            return;
        }
        //short password
        if (invalidPassword)
        {
            errMessage += "Your password is too short! Please include at least 6 letters.<br />";
            Comment.Text = errMessage;
            return;
        }

        if (User.Identity.IsAuthenticated)
        {
            //email already belong to other user
            string sessionId = this.Session.SessionID;
            HttpCookie cookie = Request.Cookies["sessionId"];
            foreach (DataRow row in dataTable.Rows)
            {
                if ((row["Email"].ToString() == email) && (row["UsrId"].ToString() != cookie["Id"]))
                {
                    emailRedFlag = true;
                    break;
                }
            }
        }
        else
        {
            //initialize the data table
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            dataTable = dv.ToTable();
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Email"].ToString() == email)
                {
                    emailRedFlag = true;
                }
            }
        }

        if (emailRedFlag)
        {
            errMessage += "The E-mail already exists!";
            Comment.Text = errMessage;
            return;
        }

        //can insert or update check empty
        if (User.Identity.IsAuthenticated)
        {
            string sessionId = this.Session.SessionID;
            HttpCookie cookie = Request.Cookies["sessionId"];
            SqlDataSource1.UpdateCommand= "UPDATE [User] SET UsrName = @val4, Password = @val5, Email = @val6 WHERE (UsrId = @val7)";
            SqlDataSource1.UpdateParameters.Add("val4", userName);         
            SqlDataSource1.UpdateParameters.Add("val5", HashPass.Value);
            SqlDataSource1.UpdateParameters.Add("val6", email);
            SqlDataSource1.UpdateParameters.Add("val7", cookie["Id"]);
            SqlDataSource1.Update();
        }
        else
        {
            SqlDataSource1.Insert();
        }

        //update cookie
        if (User.Identity.IsAuthenticated)
        {
            //update name and email
            string sessionId = this.Session.SessionID;
            Response.Cookies["sessionId"]["Name"] = userName;
            Response.Cookies["sessionId"]["Email"] = email;
            Comment.Text = "All change saved!";
            Response.Redirect("Default.aspx");
        }
        else
        {
            //try to get the user Id
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            dataTable = dv.ToTable();
            DataRow row = dataTable.Rows[0];
            string sessionId = this.Session.SessionID;
            HttpCookie cookie = new HttpCookie("sessionId");
            cookie["Name"] = row["UsrName"].ToString();
            cookie["Email"] = row["Email"].ToString();
            cookie["Id"] = row["UsrId"].ToString();
            //have the id
            Response.Cookies.Add(cookie);
            Comment.Text = "Create account success!";
            FormsAuthentication.RedirectFromLoginPage(row["Email"].ToString(), false);
            //remember me set true
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

    protected bool PasswordVerify(string psw)
    {
        if (psw.Length < 6)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void BackBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}