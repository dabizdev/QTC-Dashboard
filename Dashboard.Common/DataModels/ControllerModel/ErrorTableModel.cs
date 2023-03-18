namespace Dashboard.Common.DataModels.ControllerModel
{
    public class ErrorTableModel
    {
        // default constructor to create instance of model
        public ErrorTableModel() { }

        // create a list of string that will represent the headers for the error table
        public List<string> headers { get; set; }

        // create a list of errors that will be seen by the user
        public List<Errors> errors { get; set; }

        // get the name of both the org and application
        public string appName { get; set; }
        public string orgName { get; set; }

    }
}
