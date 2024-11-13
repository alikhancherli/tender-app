namespace Tender.App.Domain.Shared;

public class ResultHandler<T>
{
    public bool IsSuccess { get; private set; }
    public T Value { get; private set; }
    public string Error { get; private set; }

    private ResultHandler(bool isSuccess, T value, string error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static ResultHandler<T> Success(T value)
    {
        return new ResultHandler<T>(true, value, null);
    }

    public static ResultHandler<T> Failure(string error)
    {
        return new ResultHandler<T>(false, default, error);
    }
}
