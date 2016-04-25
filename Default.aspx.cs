using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Comment.Visible = false;
        if (User.Identity.IsAuthenticated)
        {
            UserPanel.Visible = true;
            VisitorPanel.Visible = false;
            if (ProfileExist())
            {
                CrtProBut.Visible = false;
                EditProfileBut.Visible = true;
                DltProfileBut.Visible = true;
                ViewProfileBut.Visible = true;
            }
            else
            {
                CrtProBut.Visible = true;
                EditProfileBut.Visible = false;
                DltProfileBut.Visible = false;
                ViewProfileBut.Visible = false;
            }
            Comment.Text = "Hi, " + Response.Cookies["UserInfo"]["Name"];
        }
        else
        {
            UserPanel.Visible = false;
            VisitorPanel.Visible = true;
        }
    }

    protected void SigninBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void CrtProBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Private/EditProfile.aspx?&action=create");
    }

    protected void CrtAccBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }

    protected void SignoutBut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
        //must click twice
    }

    protected void DltAccBut_Click(object sender, EventArgs e)
    {
        DialogResult result1 = MessageBox.Show("Are you sure to delete your account?", "Confirm", MessageBoxButtons.YesNo);
        if (result1 == DialogResult.OK)
        {
            if (ProfileExist())
            {
                SqlDataSource2.Delete();
            }
            SqlDataSource1.Delete();
            Comment.Visible = true;
            Comment.Text = "Delete successful!";
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }

    protected void EditProfileBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Private/EditProfile.aspx?&action=edit");
    }

    protected void EditAccountBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }

    protected void ViewProfileBut_Click(object sender, EventArgs e)
    {
        //direct to the public page
        HttpCookie cookie = Request.Cookies["UserInfo"];
        string emailTemp = cookie["Email"].ToString();
        Response.Redirect("ViewProfile.aspx?email=" + emailTemp);
    }

    protected void DltProfileBut_Click(object sender, EventArgs e)
    {
        //Delete the profile
        DialogResult result1 = MessageBox.Show("Are you sure to delete your profile?", "Confirm", MessageBoxButtons.YesNo);
        if (result1 == DialogResult.OK)
        {
            SqlDataSource2.Delete();
            Comment.Visible = true;
            Comment.Text = "Your profile has been deleted!";
        }
        //update button
    }

    protected void ViewOtherProfileBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewProfile.aspx?email=" + UsrNameList.SelectedValue.ToString());
    }

    protected bool ProfileExist()
    {
        HttpCookie cookie = Request.Cookies["UserInfo"];
        HiddenField1.Value = cookie["Id"];
        DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
        DataTable dataTable = dv.ToTable();
        foreach (DataRow row in dataTable.Rows)
        {
            if (row["UsrId"].ToString() == HiddenField1.Value)
            {
                return true;
            }
        }
        return false;
    }
}