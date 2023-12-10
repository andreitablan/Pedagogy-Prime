using PostSharp.Aspects;
using PostSharp.Serialization;
using System;

namespace PedagogyPrime.Persistence.AOP
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class TraceRepositoryAspect : OnMethodBoundaryAspect
    {
        private string methodName;
        private Type classType;

        public override void OnEntry(MethodExecutionArgs args)
        {
            methodName = args.Method.Name;
            classType = args.Instance?.GetType() ?? args.Method.DeclaringType;
            Console.WriteLine("====Started Repository====");
            Console.WriteLine($"Entering method {methodName} in class {classType}");
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            bool success = !args.Exception?.GetType().IsSubclassOf(typeof(Exception)) ?? true;
            Console.WriteLine($"Exiting method {methodName} in class {classType}. Successful: {success}");
            Console.WriteLine("====Ended Repository====");
            base.OnExit(args);
        }
    }
}
