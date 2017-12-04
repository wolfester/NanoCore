using System;
using System.Collections.Generic;
using System.Reflection;

namespace NanoCore {
    public class RequestHandler {
        private Dictionary<string, Type> _parameters = new Dictionary<string, Type>();
        private MethodInfo _method;

        public RequestHandler(MethodInfo method){
            _method = method;

            foreach (var x in method.GetParameters()){
                _parameters.Add(x.Name, x.ParameterType);
            }
        }
    }
}