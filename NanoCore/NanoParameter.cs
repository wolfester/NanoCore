using System;

namespace NanoCore {
    internal class NanoParameter {
        public string Name;
        public bool IsRequired;
        public Type Type;

        public NanoParameter(string name, bool isRequired, Type type) {
            Name = name;
            IsRequired = isRequired;
            Type = type;
        }
    }
}