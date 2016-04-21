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
    private static int startYear = 1900;
    private static string uid;
    private static string action;

    protected void Page_Load(object sender, EventArgs e)
    {
        uid = Request.QueryString["Id"];
        action = Request.QueryString["Action"];
        if (!Page.IsPostBack)
        {
            YearList.DataSource = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1).Reverse();
            YearList.DataBind();
            MonthList.DataSource = Enumerable.Range(1, 12);
            MonthList.DataBind();
        }
        if (action == "create")
        {
            
        }
        else
        {
            
        }
        Comment.Visible = false;
        Boolean isLeapYear = IsLeapYear();
        int year = int.Parse(YearList.SelectedValue);
        int month = int.Parse(MonthList.SelectedValue);
        int endDate = 30;
        int[] bigMonth = { 1, 3, 5, 7, 8, 10, 12 };
        if (bigMonth.Contains(month))
        {
            endDate = 31;
        }
        if (month == 2)
        {
            if (isLeapYear == true)
            {
                endDate = 29;
            }
            else
            {
                endDate = 28;
            }
        }
        DayList.DataSource = Enumerable.Range(1, endDate);
        DayList.DataBind();
    }

    protected void SavBut_Click(object sender, EventArgs e)
    {
        Comment.Visible = true;
        string errorMes = "<br />Insert Failed!<br />";
        int year = int.Parse(YearList.SelectedValue);
        int month = int.Parse(MonthList.SelectedValue);
        int day = int.Parse(DayList.SelectedValue);
        DateTime dateTime = new DateTime(year, month, day);
        Boolean invalidDate = !dateVerify(dateTime);
        if (invalidDate)
        {
            if (invalidDate)
            {
                errorMes += "Please enter valid date!<br />";
            }
            Comment.Text = errorMes;
        }
        else
        {
            SqlDataSource2.SelectParameters.Add("val1", uid);
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            DataTable dataTable = dv.ToTable();
            string Name = dataTable.Rows[0][1].ToString();
            string Email = dataTable.Rows[0][2].ToString();
            SqlDataSource1.InsertCommand = "INSERT INTO Profile(Email, UsrId, Name, DOB, Address, Telephon, Gender) VALUES (@Val1, @Val2, @val3, @val4, @val5, @val6, @val7)";
            SqlDataSource1.InsertParameters.Add("Val1", Email);
            SqlDataSource1.InsertParameters.Add("Val2", uid);
            SqlDataSource1.InsertParameters.Add("Val3", Name);
            SqlDataSource1.InsertParameters.Add("Val4", dateTime.ToString());
            SqlDataSource1.InsertParameters.Add("Val5", AddressInput.Text);
            SqlDataSource1.InsertParameters.Add("Val6", TelephoneInput.Text);
            SqlDataSource1.InsertParameters.Add("Val7", GenderList.SelectedValue);
            SqlDataSource1.Insert();
            Comment.Text = "Your profile was created successfully!";
        }

    }


    protected bool dateVerify(DateTime dateTime)
    {
        if (dateTime <= DateTime.Now)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool IsLeapYear()
    {
        int year = int.Parse(YearList.SelectedValue);
        if (year % 4 == 0)
        {
            if (year % 100 == 0)
            {
                if (year % 400 == 0)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }    
        }
        return false;
    }

}