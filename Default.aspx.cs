﻿using System;
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
        else
        {
            UserPanel.Visible = false;
            VisitorPanel.Visible = true;
        }
    }

    //login button
    protected void SigninBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void CrtProBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profile.aspx?Id=" + uid + "&action=create");
    }

    protected void CrtAccBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }

    protected void SignoutBut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
    }

    protected bool ProfileExist()
    {
        HttpCookie cookie = Request.Cookies["UserInfo"];
        Parameter p = SqlDataSource1.SelectParameters["uid"];
        SqlDataSource1.SelectParameters.Remove(p);
        SqlDataSource1.SelectParameters.Add("uid", cookie["Id"]);
        DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        if (dv.Table.Rows.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void DltAccBut_Click(object sender, EventArgs e)
    {
        /*
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
        */
    }

    protected void EditProfileBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profile.aspx?id=" + uid + "&action=edit");
    }

    protected void EditAccountBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }
}