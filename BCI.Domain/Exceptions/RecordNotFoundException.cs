namespace BCI.Domain.Exceptions;

public class RecordNotFoundException : Exception
{
    public RecordNotFoundException() : base("Record is not found.") { }

    public RecordNotFoundException(string message) : base($"Record is not found. {message}") { }
}
