using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    private static string uid;

    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenField1.Value = User.Identity.Name;
        Comment.Visible = false;
        SignoutBut.Visible = false;
        DltAccBut.Visible = false;
        EditAccountBut.Visible = false;
        string loginFlag = Request.QueryString["login"];
        System.Diagnostics.Debug.Print(loginFlag);
        if (loginFlag == "true")
        {
            //Comment.Visible = true;
            uid = Request.QueryString["Id"];
            
            SignoutBut.Visible = true;
            
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

    protected void CrtProBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profile.aspx?Id=" + uid +"&Action=create");
    }

    protected void SignoutBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?login=false");
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