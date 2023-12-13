namespace PedagogyPrime.Infrastructure.Common.monitor
{
    public class SafetyViolationEventArgs : EventArgs
    {
        public string Message { get; }

        public SafetyViolationEventArgs(string message)
        {
            Message = message;
        }
    }
}
