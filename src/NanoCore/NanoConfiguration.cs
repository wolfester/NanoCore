using System;
using System.Collections.Generic;
using System.Linq;

namespace NanoCore {
    public class NanoConfiguration {
        private List<Type> _controllers = new List<Type>();

        public NanoConfiguration (){

        }

        public void AddController (Type type){
            if(_controllers.Where(t => t == type).Any()){
                return;
            }
            
            _controllers.Add(type);
        }

        public List<Type> GetControllers (){
            return _controllers;
        }
    }
}