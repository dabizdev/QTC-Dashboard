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
        private SqlConnection connection = new SqlConnection("Server=tcp:qtcstudents2022.database.windows.net,1433;Initial Catalog=DashboardDatabase;Persist Security Info=False;User ID=qtcUser;Password=#Classof2023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        // create a list of strings that will hold all of the applications
        private readonly List<OrgsWithApps> orgsWtihApplications = new List<OrgsWithApps>();

        // constructor that gets all the values
        public OrganizationItemViewComponent()
        {
            /*
             * call the database and pull all application names from the db
             */
            string query = "Select * FROM Organizations"; // query that we want to execute

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
                        OrganizationId = Convert.ToInt64(dt.Rows[i]["OrganizationId"]),
                        ParentOrganizationId = Convert.ToInt64(dt.Rows[i]["ParentOrganizationId"]),
                        OrgCode = Convert.ToString(dt.Rows[i]["OrgCode"]),
                        Lob = Convert.ToString(dt.Rows[i]["Lob"]),
                        Name = Convert.ToString(dt.Rows[i]["Name"]),
                        Description = Convert.ToString(dt.Rows[i]["Description"]),
                        Alias = Convert.ToString(dt.Rows[i]["Alias"]),
                        Address1 = Convert.ToString(dt.Rows[i]["Address1"]),
                        Address2 = Convert.ToString(dt.Rows[i]["Address2"]),
                        City = Convert.ToString(dt.Rows[i]["City"]),
                        State = Convert.ToString(dt.Rows[i]["State"]),
                        ZipCode = Convert.ToString(dt.Rows[i]["ZipCode"]),
                        Country = Convert.ToString(dt.Rows[i]["Country"]),
                        Latitude = Convert.ToDecimal(dt.Rows[i]["Latitude"]),
                        Longitude = Convert.ToDecimal(dt.Rows[i]["Longitude"]),
                        GeoCodeQuality = Convert.ToString(dt.Rows[i]["GeoCodeQuality"]),
                        GeoCodeSource = Convert.ToString(dt.Rows[i]["GeoCodeSource"]),
                        Phone = Convert.ToString(dt.Rows[i]["Phone"]),
                        Fax = Convert.ToString(dt.Rows[i]["Fax"]),
                        CreatedBy = Convert.ToString(dt.Rows[i]["CreatedBy"]),
                        CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]),
                        UpdatedDate = Convert.ToDateTime(dt.Rows[i]["UpdatedDate"]),
                        UpdatedBy = Convert.ToString(dt.Rows[i]["UpdatedBy"])
                    };

                    // add the current organization to the item to return 
                    newOrgToAdd.organization = currentOrg;

                    // create a query that gets the applicationid of the current org from the item above
                    string query1 = "Select * FROM IntegrationPointTable WHERE Tenant = '" + currentOrg.Lob+"'"; 

                    // retreive data from sql database and save below
                    SqlDataAdapter data1 = new SqlDataAdapter(query1, connection);

                    // create a data table and populate it with data above
                    DataTable dt1 = new DataTable();
                    data1.Fill(dt1);

                    // create a new list of applications to add
                    List<IntegrationPoints> listOfApplications = new List<IntegrationPoints>();

                    // if we have at least 1 row of Errors, retreive it and print 
                    if (dt1.Rows.Count > 0)
                    {
                        
                        // loop through all the rows in the data table
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            // create a new Error and populate it (reference Error.cs for format)
                            IntegrationPoints currentApplication = new IntegrationPoints
                            {
                                LOBId = Convert.ToInt64(dt1.Rows[j]["LOBId"]),
                                ApplicationId = Convert.ToInt64(dt1.Rows[j]["ApplicationId"]),
                                IntegrationPoint = Convert.ToString(dt1.Rows[j]["IntegrationPoint"]),
                                Address1 = Convert.ToString(dt1.Rows[j]["Address1"]),
                                Address2 = Convert.ToString(dt1.Rows[j]["Address2"]),
                                City = Convert.ToString(dt1.Rows[j]["City"]),
                                State = Convert.ToString(dt1.Rows[j]["State"]),
                                Country = Convert.ToString(dt1.Rows[j]["Country"]),
                                Description = Convert.ToString(dt1.Rows[j]["Description"]),
                                Name = Convert.ToString(dt1.Rows[j]["Name"]),
                                Active = Convert.ToBoolean(dt1.Rows[j]["Active"]),
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
            }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            // redirect user to errortable view under "views folder"
            return View("Index", orgsWtihApplications);
            // returns the list of organizations with applications to the view, used in /Shared/_Layout.cshtml
            //return View("Index", orgsWtihApplications);
        }
    }
}
