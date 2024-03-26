namespace Squares.DTO.Memory.Exceptions;

public class NoSuchUserException : Exception
{
    public NoSuchUserException() {}
    
    public NoSuchUserException(string message) : base(message) {}
    
    public NoSuchUserException(string message, Exception inner) : base(message, inner) {}
}