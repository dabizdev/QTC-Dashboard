using Dashboard.Common.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace QTC.Dashboard.WebApp.Views.ErrorTable
{
    public class IndexModel : PageModel
    {
        public List<Errors> errs;
        public SqlConnection connection = new SqlConnection("Server=tcp:qtcstudents2022.database.windows.net,1433;Initial Catalog=DashboardDatabase;Persist Security Info=False;User ID=qtcUser;Password=#Classof2023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        public string[] Headers = new string[] { "Application Name", "Layer", "Module", "Alert", "AlertTeam", "Severity", "ServerName", "ErrorCode", "Error Message", "Error Date", "User", "View" };

        public IndexModel()
        {
            List<Errors> errors = new List<Errors>();

            string query = "Select * FROM ErrorsTable"; // query that we want to execute

            // retreive data from sql database and save below
            SqlDataAdapter data = new SqlDataAdapter(query, connection);

            // create a data table and populate it with data above
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText= query;

            SqlDataReader sdr = cmd.ExecuteReader();

            // loop through all the rows in the data table
            while (sdr.Read())
            {
                // create a new Error and populate it (reference Error.cs for format)
                Errors currentError = new Errors
                {
                    ApplicationName = Convert.ToString(sdr["ApplicationName"]),
                    Layer = Convert.ToString(sdr["Layer"]),
                    Module = Convert.ToString(sdr["Module"]),
                    Alert = Convert.ToString(sdr["Alert"]),
                    AlertTeam = Convert.ToString(sdr["AlertTeam"]),
                    Severity = Convert.ToString(sdr["Severity"]),
                    ServerName = Convert.ToString(sdr["ServerName"]),
                    ErrorCode = Convert.ToInt32(sdr["ErrorCode"]),
                    ErrorMessage = Convert.ToString(sdr["ErrorMessage"]),
                    ErrorDate = Convert.ToDateTime(sdr["ErrorDate"]),
                    User = Convert.ToString(sdr["UserName"])
                };

                // add the current Error to the arraylist
                errors.Add(currentError);
            }
            

            errs = errors;
            //Headers = new string[] { "Application Name", "Layer", "Module", "Alert", "AlertTeam", "Severity", "ServerName", "ErrorCode", "Error Message", "Error Date", "User", "View" };

        }

        public void OnGet()
        {
            //Headers = new string[] { "Application Name", "Layer", "Module", "Alert", "AlertTeam", "Severity", "ServerName", "ErrorCode", "Error Message", "Error Date", "User", "View" };
        }


		/*public IActionResult Index()
		{
			//return View();
		}*/
	}
}