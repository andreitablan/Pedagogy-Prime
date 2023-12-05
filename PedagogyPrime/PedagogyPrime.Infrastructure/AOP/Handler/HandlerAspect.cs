using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PedagogyPrime.Infrastructure.AOP.Handler
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class HandlerAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("====Started Handler====");
            var className = args.Method.DeclaringType?.Name ?? "UnknownClass";
            var methodName = args.Method.Name;
            Console.WriteLine(args.Arguments[0]);

            // Log method entry
            Console.WriteLine($"Entering {className}.{methodName}");

            // Log input parameters (if needed)
            LogInputParameters(args);

            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var className = args.Method.DeclaringType?.Name ?? "UnknownClass";
            var methodName = args.Method.Name;
            var success = args.Exception == null ? "successfully" : $"with failure: {args.Exception}";

            // Log method exit
            Console.WriteLine($"Exiting {className}.{methodName} {success}");
            Console.WriteLine("====Ended Handler====");
            base.OnExit(args);
        }

        private void LogInputParameters(MethodExecutionArgs args)
        {
            var parameters = args.Method.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                var paramName = parameters[i].Name;
                var paramValue = args.Arguments[i];
                Console.WriteLine($"  {paramName}: {paramValue}");
            }
        }
    }

}
