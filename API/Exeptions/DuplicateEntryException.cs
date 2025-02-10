namespace Microondas.API.Exeptions
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message) : base(message) { }
    }
}
