namespace Dashboard.Common.Modules
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DashboardModuleAttribute : Attribute
    {
        public string Name { get; set; }
        public string Lob { get; set; }
    }
}
