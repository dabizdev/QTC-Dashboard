namespace Dashboard.Common.Interfaces
{
    public interface ITenant
    {
        public string Name { get; }

        public IErrorTypeModule GetErrorTypeProcessor();

    }
}
