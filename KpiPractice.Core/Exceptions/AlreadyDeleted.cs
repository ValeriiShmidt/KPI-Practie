namespace KpiPractice.Core.Exceptions
{
    public class AlredyDeleted : System.Exception
    {
        public AlredyDeleted(string message = "") : base(message) { }
    }
}