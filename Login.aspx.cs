using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    private static DataTable dataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        PswBox.MaxLength = 6;
        
    }

    protected void RetBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void LoginBut_Click(object sender, EventArgs e)
    {
        Comment.Visible = true;

        /*
        string Email = EmailBox.Text;
        string passWord = PswBox.Text;
        foreach (DataRow row in dataTable.Rows)
        {
            if ((row["Email"].ToString() == Email) && (row["Password"].ToString() == passWord))
            {
                Response.Redirect("Default.aspx?login=true&Id=" + row["UsrId"].ToString());
            }
        }
        Comment.Visible = true;
        Comment.Text = "<br />The combination of E-mail and password can not be found!";
        */
        DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        if (dv.Table.Rows.Count == 0)
        {
            //Cannot find the email
            Comment.Text = "Login failed!<br />The E-mail can not be found!";
        }
        else
        {
            string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(PswBox.Text, "SHA1");
            DataRow row = dv.Table.Rows[0];
            string tempPass = (string)row["Password"];
            string tempName = (string)row["UsrName"];
            string tempEmail = (string)row["Email"];
            string tempId = row["UsrId"].ToString();
            System.Diagnostics.Debug.Print("tempPass " + tempPass);
            System.Diagnostics.Debug.Print("hashpass " + hashpass);
            if (tempPass == hashpass)
            {
                //authenticated
                HttpCookie cookie = new HttpCookie("UserInfo");
                cookie["Name"] = tempName;
                cookie["Email"] = tempEmail;              
                cookie["Id"] = tempId;
                Response.Cookies.Add(cookie);
                Comment.Text = "Login success! Redirect to the main page in 5 second.";
                System.Threading.Thread.Sleep(5000);
                FormsAuthentication.RedirectFromLoginPage(tempEmail, true);
                return;
            }
            else
            {
                Comment.Text= "Login failed!<br />The combination of E-mail and password can not be found!";
            }
        }
    }

    protected void DisplayPsw_CheckedChanged(object sender, EventArgs e)
    {
        //TO DO
    }
}