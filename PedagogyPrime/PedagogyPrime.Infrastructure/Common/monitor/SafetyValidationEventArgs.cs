namespace PedagogyPrime.Infrastructure.Common.monitor
{
    public class SafetyValidationEventArgs : EventArgs
    {
        public string Message { get; }

        public SafetyValidationEventArgs(string message)
        {
            Message = message;
        }
    }
}
