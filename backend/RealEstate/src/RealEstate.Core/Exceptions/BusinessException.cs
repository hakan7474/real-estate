namespace RealEstate.Core.Exceptions;

public class BusinessException : Exception
{
    public string ErrorKey { get; set; }

    public BusinessException(string errorKey, string message) : base(message)
    {
        ErrorKey = errorKey;
    }
}