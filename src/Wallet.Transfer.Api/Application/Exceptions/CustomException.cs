namespace WalletTransfer.Api.Application.Exceptions;

public class CustomException : Exception
{
    public int StatusCode { get; set; }
    public List<string> Failures { get; set; }

    public CustomException(string message) : base(message)
    {
        Failures = new List<string>();
    }

    public CustomException(string message, int statusCode, List<string>? failures = null) : base(message)
    {
        this.StatusCode = statusCode;
        this.Failures = failures!;
    }
}
