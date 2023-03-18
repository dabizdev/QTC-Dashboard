using Dashboard.Common.DataModels;
using Dashboard.Common.DataModels.ControllerModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QTC.Dashboard.WebApp.Controllers
{
    public class ErrorTableController : Controller
    {
        public ErrorTableModel vals = new ErrorTableModel();
        private SqlConnection connection = new SqlConnection("Server=tcp:qtcstudents2022.database.windows.net,1433;Initial Catalog=DashboardDatabase;Persist Security Info=False;User ID=qtcUser;Password=#Classof2023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public ErrorTableController()
        {
            // create an array of strings with the headers needed
            string[] neededHeaders = new string[] { "Application Name", "Layer", "Module", "Alert", "AlertTeam", "Severity", "ServerName", "ErrorCode", "Error Message", "Error Date", "User", "View" };

            // save the needed headers as a list of strings
            vals.headers = neededHeaders.ToList();

        }

        // takes in the org and application that is sent to the page when user clicks on specific application
        public IActionResult Index(string org, string app)
        {
            // pass in the values of the org name and app name to the model to send to view (displayed on page)
            vals.orgName = org;
            vals.appName = app;

            List<Errors> errors = new List<Errors>();

            string query = "Select * FROM ErrorsTable WHERE ApplicationName = '" + app + "'"; // query that we want to execute

            // retreive data from sql database and save below
            SqlDataAdapter data = new SqlDataAdapter(query, connection);

            // create a data table and populate it with data above
            DataTable dt = new DataTable();
            data.Fill(dt);

            // if we have at least 1 row of Errors, retreive it and print 
            if (dt.Rows.Count > 0)
            {
                // loop through all the rows in the data table
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // create a new Error and populate it (reference Error.cs for format)
                    Errors currentError = new Errors
                    {
                        ApplicationName = Convert.ToString(dt.Rows[i]["ApplicationName"]),
                        Layer = Convert.ToString(dt.Rows[i]["Layer"]),
                        Module = Convert.ToString(dt.Rows[i]["Module"]),
                        Alert = Convert.ToString(dt.Rows[i]["Alert"]),
                        AlertTeam = Convert.ToString(dt.Rows[i]["AlertTeam"]),
                        Severity = Convert.ToString(dt.Rows[i]["Severity"]),
                        ServerName = Convert.ToString(dt.Rows[i]["ServerName"]),
                        ErrorCode = Convert.ToInt32(dt.Rows[i]["ErrorCode"]),
                        ErrorMessage = Convert.ToString(dt.Rows[i]["ErrorMessage"]),
                        ErrorDate = Convert.ToDateTime(dt.Rows[i]["ErrorDate"]),
                        User = Convert.ToString(dt.Rows[i]["UserName"])
                    };

                    // add the current Error to the arraylist
                    errors.Add(currentError);
                }
            }

            // set the errors to send to view 
            vals.errors = errors;

            // return the values needed for the error table
            return View(vals);
        }
    }
}
