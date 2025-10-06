namespace SharedLibrary.Exceptions;

public class TokenExpiredException : Exception
{
    public TokenExpiredException() : base()
    {
        
    }

    public TokenExpiredException(string message) : base(message)
    {
        
    }

    public TokenExpiredException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}