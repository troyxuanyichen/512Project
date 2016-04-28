using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditProfile : System.Web.UI.Page
{
    private static int startYear = 1900;
    private static string action;

    protected void Page_Load(object sender, EventArgs e)
    {
        string sessionId = this.Session.SessionID;
        HttpCookie cookie = Request.Cookies["sessionId"];
        System.Diagnostics.Debug.Print(cookie["Id"]);
        if (cookie == null)
        {
            Response.Redirect("../Login.aspx");
        }
        HiddenEmail.Value = cookie["Email"].ToString();
        action = Request.QueryString["action"];
        if(Request.QueryString["action"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!Page.IsPostBack)
        {
            //bind the year and month
            YearList.DataSource = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1).Reverse();
            YearList.DataBind();
            MonthList.DataSource = Enumerable.Range(1, 12);
            MonthList.DataBind();
            if (action == "edit")
            {
                //display profile
                //using the email to get the profile, display
                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                DataTable dataTable = dv.ToTable();
                DataRow row = dataTable.Rows[0];
                string DOBTemp = row["DOB"].ToString();
                string yearTemp = DateTime.Parse(DOBTemp).Year.ToString();
                string monthTemp = DateTime.Parse(DOBTemp).Month.ToString();
                string dayTemp = DateTime.Parse(DOBTemp).Day.ToString();
                string genderTemp = row["Gender"].ToString();
                string telephoneTemp = row["Telephone"].ToString();
                string stateTemp = row["State"].ToString();
                string cityTemp = row["City"].ToString();
                string streetTemp = row["Street"].ToString();
                string privacyTemp = row["Privacy"].ToString();
                DayList.DataSource = Enumerable.Range(1, GetDate(int.Parse(yearTemp), int.Parse(monthTemp)));
                DayList.DataBind();
                YearList.SelectedIndex = YearList.Items.IndexOf(YearList.Items.FindByValue(yearTemp));
                MonthList.SelectedIndex = MonthList.Items.IndexOf(MonthList.Items.FindByValue(monthTemp));
                DayList.SelectedIndex = DayList.Items.IndexOf(DayList.Items.FindByValue(dayTemp));
                GenderList.SelectedIndex = GenderList.Items.IndexOf(GenderList.Items.FindByValue(genderTemp));
                TelephoneInput.Text = telephoneTemp;
                StateList.SelectedIndex = (StateList.Items.IndexOf(StateList.Items.FindByValue(stateTemp)));
                CityInput.Text = cityTemp;
                StreetInput.Text = streetTemp;
                if (privacyTemp == "N")
                {
                    PrivacyInput.Checked = false;
                }
                else
                {
                    PrivacyInput.Checked = true;
                }
            }
        }
        /*
        if (action == "create")
        {
        */
        //bind the date, display nothing
        Comment.Visible = false;
        int year = int.Parse(YearList.SelectedValue);
        int month = int.Parse(MonthList.SelectedValue);
        DayList.DataSource = Enumerable.Range(1, GetDate(year, month));
        DayList.DataBind();
        //}
    }

    protected void SavBut_Click(object sender, EventArgs e)
    {
        Comment.Visible = true;
        string errorMes = "<br />Save Failed!<br />";
        int year = int.Parse(YearList.SelectedValue);
        int month = int.Parse(MonthList.SelectedValue);
        int day = int.Parse(DayList.SelectedValue);
        DateTime dateTime = new DateTime(year, month, day);
        bool invalidDate = !dateVerify(dateTime);
        bool emptyItem = ((TelephoneInput.Text == string.Empty) || (StreetInput.Text == string.Empty));
        int telephoneTemp;
        bool invalidTelephone = ((TelephoneInput.Text.Length != 10) || (int.TryParse(TelephoneInput.Text, out telephoneTemp)));
        if (emptyItem)
        {
            errorMes += "Please fill in all of the blank!<br />";
            Comment.Text = errorMes;
            return;
        }
        else
        {
            if (invalidDate || invalidTelephone)
            {
                if (invalidDate)
                {
                    errorMes += "Please enter valid date!<br />";
                }

                if (invalidTelephone)
                {
                    errorMes += "Please enter valid telephone!<br />";
                }
                Comment.Text = errorMes;
                return;
            }
            else
            {             
                DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
                DataTable dataTable = dv.ToTable();
                string uid = dataTable.Rows[0][0].ToString();
                string privacyTemp;
                if (PrivacyInput.Checked == false)
                {
                    privacyTemp = "N";
                }
                else
                {
                    privacyTemp = "Y";
                }
                if(action == "create")
                {
                    SqlDataSource1.InsertCommand = "INSERT INTO Profile(Email, UsrId, DOB, State, City, Street, Telephone, Gender, Privacy) VALUES (@Val1, @Val2, @val3, @val4, @val5, @val6, @val7, @val8, @val9)";
                    SqlDataSource1.InsertParameters.Add("Val1", HiddenEmail.Value);
                    SqlDataSource1.InsertParameters.Add("Val2", uid);             
                    SqlDataSource1.InsertParameters.Add("Val3", dateTime.ToString());                   
                    SqlDataSource1.InsertParameters.Add("Val4", StateList.SelectedValue.ToString());
                    SqlDataSource1.InsertParameters.Add("Val5", CityInput.Text);
                    SqlDataSource1.InsertParameters.Add("Val6", StreetInput.Text);
                    SqlDataSource1.InsertParameters.Add("Val7", TelephoneInput.Text);
                    SqlDataSource1.InsertParameters.Add("Val8", GenderList.SelectedValue);
                    SqlDataSource1.InsertParameters.Add("Val9", privacyTemp);
                    SqlDataSource1.Insert();
                }
                else
                {
                    SqlDataSource1.UpdateCommand = "UPDATE Profile SET DOB = @DOBUpdate, Street = @StreetUpdate, State = @StateUpdate, Telephone = @TelephoneUpdate, Gender = @GenderUpdate, Privacy = @PrivacyUpdate WHERE (Email = @EmailUpdate)";
                    SqlDataSource1.UpdateParameters.Add("DOBUpdate", dateTime.ToString());
                    SqlDataSource1.UpdateParameters.Add("StreetUpdate", StreetInput.Text);
                    SqlDataSource1.UpdateParameters.Add("StateUpdate", StateList.SelectedValue.ToString());
                    SqlDataSource1.UpdateParameters.Add("TelephoneUpdate", TelephoneInput.Text);
                    SqlDataSource1.UpdateParameters.Add("GenderUpdate", GenderList.SelectedValue);
                    SqlDataSource1.UpdateParameters.Add("PrivacyUpdate", privacyTemp);
                    SqlDataSource1.UpdateParameters.Add("EmailUpdate", HiddenEmail.Value);
                    SqlDataSource1.Update();
                }
                Comment.Text = "Your profile was saved successfully! Your will be redirect to the default page after 5 seconds";
                Response.AddHeader("REFRESH", "5;URL=../Default.aspx");
            }
        }
    }

    protected void RetBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected int GetDate(int year, int month)
    {
        int[] bigMonth = { 1, 3, 5, 7, 8, 10, 12 };
        if (bigMonth.Contains(month))
        {
            return 31;
        }
        else
        {
            if (month == 2)
            {
                if (IsLeapYear(year))
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }
        }
        return 30;
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

    protected bool IsLeapYear(int year)
    {
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