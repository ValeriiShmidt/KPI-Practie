namespace KpiPractice.Core.Exceptions
{
    public class FailedToInsert : System.Exception
    {
        public FailedToInsert(string message = "") : base(message) { }
    }
}