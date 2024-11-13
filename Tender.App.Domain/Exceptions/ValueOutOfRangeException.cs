namespace Tender.App.Domain.Exceptions;

public class ValueOutOfRangeException : Exception
{
    public ValueOutOfRangeException(string parameter):base($"[{parameter}]'s value is out of range.")
    {
    }
    public ValueOutOfRangeException(string value, string parameter) : base($"The value:[{value}] for parameter:[{parameter}] is out of range.")
    {
    }
}
