namespace Orion.Mining
{
    public class ProgramFailedException : Exception
    {
        public ProgramFailedException(string message) : base(message)
        {
        }
    }
}