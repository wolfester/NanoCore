using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace NanoCore {
    public class NanoMethod {
        internal bool HasReturnValue = true;
        internal Type ReturnType { get { return _methodInfo?.ReturnType; } }
        private MethodInfo _methodInfo;
        private Dictionary<string, NanoParameter> _params = new Dictionary<string, NanoParameter>();

        private TypeConverter _typeConverter = new TypeConverter();

        private bool _isAsync = false;

        public NanoMethod(MethodInfo methodInfo) {
            _methodInfo = methodInfo;

            var asyncAttributeType = typeof(AsyncStateMachineAttribute);

            if((AsyncStateMachineAttribute)_methodInfo.GetCustomAttribute(asyncAttributeType) != null) _isAsync = true;

            if(_methodInfo.ReturnType == typeof(void) || (_methodInfo.ReturnType == typeof(Task) && !_methodInfo.ReturnType.IsGenericType)) HasReturnValue = false;

            Init();
        }

        public async Task<object> Invoke (HttpContext context) {
            var queryParams = context.Request.Query;

            var methodParameters = _methodInfo.GetParameters();

            int paramCount = _methodInfo.GetParameters().Length;

            object[] invokeParameters = new object[_methodInfo.GetParameters().Length];

            for(int x = 0; x < methodParameters.Length; x++){
                var paramName = methodParameters[x].Name;

                StringValues queryParam;
                if(!queryParams.TryGetValue(paramName, out queryParam) || string.IsNullOrWhiteSpace(queryParam)){
                    if(_params[paramName].IsRequired) throw new Exception($"Missing parameter {paramName}.");

                    invokeParameters[x] = null;
                    continue;
                }

                object invokeParameter;

                try {
                    invokeParameter = _typeConverter.ConvertTo(queryParam, methodParameters[x].ParameterType);
                    invokeParameters[x] = invokeParameter;
                    continue;
                }
                catch(Exception){}

                try {
                    invokeParameter = JsonConvert.DeserializeObject(queryParam, methodParameters[x].ParameterType);
                    invokeParameters[x] = invokeParameter;
                    continue;
                }
                catch(Exception){
                    throw new Exception($"Unable to parse parameter {paramName}");
                }
            }

            if(HasReturnValue) {
                return new NanoResponse<dynamic> { Result = await (dynamic)_methodInfo.Invoke(null, invokeParameters) }; 
            }
            else {
                await (dynamic)_methodInfo.Invoke(null, invokeParameters);
                return new NanoResponse();
            }
        }

        private void Init(){
            foreach(var x in _methodInfo.GetParameters()){
                bool isRequired = !(x.ParameterType.IsGenericType && x.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>)) && !x.HasDefaultValue;

                _params.Add(x.Name, new NanoParameter(x.Name, isRequired, x.ParameterType));
            }
        }
    }
}