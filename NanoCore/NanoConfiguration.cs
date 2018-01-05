using System;
using System.Collections.Generic;
using System.Reflection;

namespace NanoCore {
    public class NanoConfiguration {
        public List<Type> Controllers;

        public NanoConfiguration(){
            Controllers = new List<Type>();
        }

        public void AddController(Type t) {
            Controllers.Add(t);
        }
    }
}