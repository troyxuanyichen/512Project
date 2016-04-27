using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewProfile : System.Web.UI.Page
{
    private static string email;

    protected void Page_Load(object sender, EventArgs e)
    {
        //should not use cookie
        string sessionId = this.Session.SessionID;
        HttpCookie cookie = Request.Cookies["sessionId"];
        HiddenField1.Value = Request.QueryString["email"];
        DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
        DataTable dataTable = dv.ToTable();
        DataRow row = dataTable.Rows[0];
        if ((row["Privacy"].ToString() == "Y") && (row["Email"].ToString() != cookie["Email"]))
        {
            UserPanel.Visible = false;
            Comment.Visible = true;
            Comment.Text = "You do not have the right to see the profile!";
        }
        else
        {
            //display
            string DOBTemp = row["DOB"].ToString();
            DOB.Text = DateTime.Parse(DOBTemp).Date.ToString("d");
            if (row["Gender"].ToString() == "M")
            {
                Gender.Text = "Male";
            }
            else
            {
                Gender.Text = "Female";
            }
            Telephone.Text = row["Telephone"].ToString();
            Address.Text = row["Address"].ToString();
        }
    }

    protected void RetBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}