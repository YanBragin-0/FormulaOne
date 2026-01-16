namespace FormulaOne.Application.Exceptions
{
    public class RegistrationException : Exception
    {
        public IEnumerable<string> Errors { get; } = null!;
        public RegistrationException(IEnumerable<string> Exceptions) : base("Registration Exception")
        {
            Errors = Exceptions;
        }
    }
}
