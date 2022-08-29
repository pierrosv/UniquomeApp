namespace UniquomeApp.Application.Common.Exceptions;

public class UnableToInstantiateEngineException : Exception
{
    public UnableToInstantiateEngineException() : base() { }

    public UnableToInstantiateEngineException(string message)
        : base(message)
    {
    }

    public UnableToInstantiateEngineException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

}