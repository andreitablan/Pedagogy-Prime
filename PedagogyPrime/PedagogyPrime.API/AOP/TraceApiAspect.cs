using Microsoft.AspNetCore.Mvc;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PedagogyPrime.API.AOP
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class TraceApiAspect : OnMethodBoundaryAspect
    {
        public string ControllerName { get; set; }

        public TraceApiAspect(string controllerName)
        {
            this.ControllerName = controllerName;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            LogApiEntry(args);
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            LogApiExit(args);
            base.OnExit(args);
        }

        private void LogApiEntry(MethodExecutionArgs args)
        {
            var methodName = args.Method.Name;
            var methodParameters = GetMethodParameters((MethodInfo)args.Method);
            var parameterValues = GetParameterValues(args);
            Console.WriteLine("====Started API====");
            Console.WriteLine($"API Request in {ControllerName}.{methodName}");
            Console.WriteLine($"Parameters: {string.Join(", ", parameterValues)}");
        }

        private void LogApiExit(MethodExecutionArgs args)
        {
            var methodName = args.Method.Name;
            var returnValue = args.ReturnValue;

            Console.WriteLine($"API Response from {ControllerName}.{methodName}");
            Console.WriteLine($"Returned value: {returnValue}");
            Console.WriteLine("====Ended API====");
        }

        private Dictionary<string, Type> GetMethodParameters(MethodInfo method)
        {
            return method.GetParameters().ToDictionary(p => p.Name, p => p.ParameterType);
        }

        private Dictionary<string, object> GetParameterValues(MethodExecutionArgs args)
        {
            var methodParameters = GetMethodParameters((MethodInfo)args.Method);
            var parameterValues = new Dictionary<string, object>();

            for (int i = 0; i < methodParameters.Count; i++)
            {
                var paramName = methodParameters.ElementAt(i).Key;
                var paramValue = args.Arguments[i];
                parameterValues.Add(paramName, paramValue);
            }

            return parameterValues;
        }

    }
}
