namespace PedagogyPrime.Infrastructure.Common.monitor
{
    public class SafetyMonitor
    {
        public event EventHandler<SafetyViolationEventArgs> SafetyPropertyViolated;

        public event EventHandler<SafetyValidationEventArgs> SafetyPropertyValidated;

        public virtual void OnSafetyPropertyViolated(string message)
        {
            SafetyPropertyViolated?.Invoke(this, new SafetyViolationEventArgs(message));
        }

        public virtual void OnSafetyPropertyValidated(string message)
        {
            SafetyPropertyValidated?.Invoke(this, new SafetyValidationEventArgs(message));
        }
    }
}
