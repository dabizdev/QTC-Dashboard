using Dashboard.Common.DataModels; // gets the Application Model to format list of applications
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// TODO: DELETE THIS, no longer needed thanks to OrganizationItemViewComponent
/*
 * This file allows us to pull applications to view under the sidebar in _layoyt.cshtml
 */
namespace QTC.Dashboard.WebApp.Views.ViewComponents
{
    [ViewComponent(Name = "Applications")]
    public class ApplicationViewComponent : ViewComponent
    {
        //private readonly ApplicationDbContext _context;
        private SqlConnection connection = new SqlConnection("Server=tcp:qtcstudents2022.database.windows.net,1433;Initial Catalog=DashboardDatabase;Persist Security Info=False;User ID=qtcUser;Password=#Classof2023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        // create a list of strings that will hold all of the applications
        private readonly List<Application> applications = new List<Application>();
        public ApplicationViewComponent()
        {
            /*
             * call the database and pull all application names from the db
             */

            string query = "Select * FROM ApplicationTable"; // query that we want to execute

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
                    Application currentApplication = new Application
                    {
                        ApplicationId = Convert.ToInt64(dt.Rows[i]["ApplicationId"]),
                        ApplicationName = Convert.ToString(dt.Rows[i]["ApplicationName"]),
                        UserId = Convert.ToInt64(dt.Rows[i]["UserId"]),
                        APIUrl = Convert.ToString(dt.Rows[i]["APIUrl"]),
                        UserType = Convert.ToString(dt.Rows[i]["UserType"]),
                        CreatedBy = Convert.ToString(dt.Rows[i]["CreatedBy"]),
                        CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]),
                        UpdatedDate = Convert.ToDateTime(dt.Rows[i]["UpdatedDate"]),
                        UpdatedBy = Convert.ToString(dt.Rows[i]["UpdatedBy"])
                    };

                    // add the current Error to the arraylist
                    applications.Add(currentApplication);
                }
            }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // returns the list of applications to the view, used in /Shared/_Layout.cshtml
            return View("Index", applications);
        }
    }
}
