namespace ProgramTan.WebApi;

public class DuplicateException : Exception
{
	public DuplicateException(string message) : base(message) { }
}

public class NotFoundException : Exception
{
	public NotFoundException(string message) : base(message) { }
}

public class RestrictedException : Exception
{
	public RestrictedException(string message) : base(message) { }
}
