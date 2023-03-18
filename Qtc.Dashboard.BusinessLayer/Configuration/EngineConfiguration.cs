namespace Qtc.Dashboard.BusinessLayer.Configuration
{
    public class EngineConfiguration
    {
        public ClientSettings ClientSettings { get; set; }
        public UploadProcessor UploadProcessor { get; set; }
        public List<string> LobsList { get; set; }

    }

    public class ClientSettings
    {
        public string ServiceType { get; set; }
        public string Module { get; set; }
    }
    public class UploadProcessor : ProviderTypeElement
    {
    }

    public class ProviderTypeElement
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
