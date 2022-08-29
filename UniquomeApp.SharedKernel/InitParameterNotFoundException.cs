namespace UniquomeApp.SharedKernel;

public class InitParameterNotFoundException : Exception
{
    public InitParameterNotFoundException()
        : base()
    {
    }

    public InitParameterNotFoundException(string message)
        : base(message)
    {
    }

    public InitParameterNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public InitParameterNotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
    public InitParameterNotFoundException(string paramName, string entityName)
        : base($"Init Param \"{paramName}\" for {entityName} was not found.")
    {
    }
}