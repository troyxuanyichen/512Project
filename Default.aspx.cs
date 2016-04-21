using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private static string uid;

    protected void Page_Load(object sender, EventArgs e)
    {
        Comment.Visible = false;
        if (User.Identity.IsAuthenticated)
        {
            UserPanel.Visible = true;
            VisitorPanel.Visible = false;
        }
        else
        {
            UserPanel.Visible = false;
            VisitorPanel.Visible = true;
        }
        string loginFlag = Request.QueryString["login"];

        if (loginFlag == "true")
        {
            //Comment.Visible = true;
            uid = Request.QueryString["Id"];
            SigninBut.Visible = false;
            SignoutBut.Visible = true;
            CrtAccBut.Visible = false;
            EditAccountBut.Visible = true;
            DltAccBut.Visible = true;
            //don't need
            if (ProfileExist())
            {
                CrtProBut.Visible = false;

                EditProfilBut.Visible = true;
            }
            else
            {
                CrtProBut.Visible = true;

                EditProfilBut.Visible = false;
            }
        }
    }

    //login button
    protected void SigninBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void CrtProBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profile.aspx?Id=" + uid + "&Action=create");
    }

    protected void CrtAccBut_Click(object sender, EventArgs e)
    {
        if (true)
        {
            Response.Redirect("CreateAccount.aspx?Action=create");
        }
    }

    protected void SignoutBut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        UserPanel.Visible = false;
        VisitorPanel.Visible = true;
    }

    protected bool ProfileExist()
    {
        DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        DataTable dataTable = dv.ToTable();
        foreach (DataRow row in dataTable.Rows)
        {
            if ((row["UsrId"].ToString() == uid))
            {
                return true;
            }
        }
        return false;
    }

    protected void DltAccBut_Click(object sender, EventArgs e)
    {
        //delete profile
        try
        {
            SqlDataSource1.DeleteParameters.Add("val1", uid);
            SqlDataSource1.Delete();
        }
        catch (SqlException ex)
        {
            System.Diagnostics.Debug.Print("Delete profile failed! " + ex.Message);
        }
        //delete account
        try
        {
            SqlDataSource2.DeleteParameters.Add("val1", uid);
            SqlDataSource2.Delete();
        }
        catch (SqlException ex)
        {
            System.Diagnostics.Debug.Print("Delete account failed! " + ex.Message);
        }
        Comment.Visible = true;
        Comment.Text = "Delete successful!";
    }

    protected void EditProfileBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profile.aspx?Id=" + uid + "&Action=edit");
    }

    protected void EditAccountBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx?Action=edit&Id=" + uid);
    }
}