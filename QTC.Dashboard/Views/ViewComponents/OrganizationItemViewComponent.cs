using Dashboard.Common.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Qtc.Dashboard.ViewModelLayer.Dashboard;
using System.Data;

namespace QTC.Dashboard.WebApp.Views.ViewComponents
{
    [ViewComponent(Name = "OrganizationItems")]
    public class OrganizationItemViewComponent : ViewComponent
    {
        //private SqlConnection connection = new SqlConnection("Server=tcp:qtcstudents2022.database.windows.net,1433;Initial Catalog=DashboardDatabase;Persist Security Info=False;User ID=qtcUser;Password=#Classof2023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        // create a list of strings that will hold all of the applications
        //private readonly List<OrgsWithApps> orgsWtihApplications = new List<OrgsWithApps>();

        // constructor that gets all the values
        public OrganizationItemViewComponent()
        {
            /*
             * call the database and pull all application names from the db
             *//*
            string query = "Select * FROM OrganizationTable"; // query that we want to execute

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
                    // create a new organization that we want to add
                    OrgsWithApps newOrgToAdd = new OrgsWithApps();

                    // get the information of the current organization
                    Organization currentOrg = new Organization
                    {
                        LOBId = Convert.ToInt64(dt.Rows[i]["LOBId"]),
                        ApplicationId = Convert.ToInt64(dt.Rows[i]["ApplicationId"]),
                        IntegrationPoint = Convert.ToString(dt.Rows[i]["IntegrationPoint"]),
                        Address1 = Convert.ToString(dt.Rows[i]["Address1"]),
                        Address2 = Convert.ToString(dt.Rows[i]["Address2"]),
                        City = Convert.ToString(dt.Rows[i]["City"]),
                        State = Convert.ToString(dt.Rows[i]["State"]),
                        Country = Convert.ToString(dt.Rows[i]["Country"]),
                        Description = Convert.ToString(dt.Rows[i]["Description"]),
                        Name = Convert.ToString(dt.Rows[i]["Name"]),
                        Active = Convert.ToBoolean(dt.Rows[i]["Active"]),
                        CreatedBy = Convert.ToString(dt.Rows[i]["CreatedBy"]),
                        CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]),
                        UpdatedDate = Convert.ToDateTime(dt.Rows[i]["UpdatedDate"]),
                        UpdatedBy = Convert.ToString(dt.Rows[i]["UpdatedBy"])
                    };

                    // add the current organization to the item to return 
                    newOrgToAdd.organization = currentOrg;

                    // create a query that gets the applicationid of the current org from the item above
                    string query1 = "Select * FROM ApplicationTable WHERE ApplicationId = "+currentOrg.ApplicationId; 

                    // retreive data from sql database and save below
                    SqlDataAdapter data1 = new SqlDataAdapter(query1, connection);

                    // create a data table and populate it with data above
                    DataTable dt1 = new DataTable();
                    data1.Fill(dt1);

                    // create a new list of applications to add
                    List<Application> listOfApplications = new List<Application>();

                    // if we have at least 1 row of Errors, retreive it and print 
                    if (dt1.Rows.Count > 0)
                    {
                        
                        // loop through all the rows in the data table
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            // create a new Error and populate it (reference Error.cs for format)
                            Application currentApplication = new Application
                            {
                                ApplicationId = Convert.ToInt64(dt1.Rows[j]["ApplicationId"]),
                                ApplicationName = Convert.ToString(dt1.Rows[j]["ApplicationName"]),
                                UserId = Convert.ToInt64(dt1.Rows[j]["UserId"]),
                                APIUrl = Convert.ToString(dt1.Rows[j]["APIUrl"]),
                                UserType = Convert.ToString(dt1.Rows[j]["UserType"]),
                                CreatedBy = Convert.ToString(dt1.Rows[j]["CreatedBy"]),
                                CreatedDate = Convert.ToDateTime(dt1.Rows[j]["CreatedDate"]),
                                UpdatedDate = Convert.ToDateTime(dt1.Rows[j]["UpdatedDate"]),
                                UpdatedBy = Convert.ToString(dt1.Rows[j]["UpdatedBy"])
                            };
                            // add the current application to the list of applications
                            listOfApplications.Add(currentApplication);
                        }
                    }

                    // set the list of applications to add into the model in the beginning of the class
                    newOrgToAdd.applications = listOfApplications;

                    // add the current orgs with applications to the arraylist
                    orgsWtihApplications.Add(newOrgToAdd);
                }
            }*/
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new DashboardViewModel();
            vm.Init();

            //SetMVCCommonViewModelProperties(vm);

            // this shows a spinning logo while the information is being loaded
            if (vm.ShowSpinner.HasValue && vm.ShowSpinner.Value)
            {
                return View("~/Views/Shared/ShowSpinner.cshtml");
            }

            // redirect user to errortable view under "views folder"
            return View("Index", vm);
            // returns the list of organizations with applications to the view, used in /Shared/_Layout.cshtml
            //return View("Index", orgsWtihApplications);
        }
    }
}
