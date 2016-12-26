using RF.CrawlingScript.Definitions;
using Shark.Components;
using System;
using System.Collections.Generic;

namespace RF.CrawlingScript.Components
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class SerializerContractAttribute : SharkComponentAttribute
    {
        internal static Dictionary<string, Type> Serializers { get; private set; } = new Dictionary<string, Type>();
        internal static Dictionary<Type, string> Keywords { get; private set; } = new Dictionary<Type, string>();

        public SerializerContractAttribute(string uniqueKeyword, Type serializableType)
        {
            if (string.IsNullOrEmpty(uniqueKeyword) || serializableType == null) throw new ArgumentNullException();

            if (Serializers.ContainsKey(uniqueKeyword)) throw new InvalidOperationException("Type " + serializableType + " try to register with registered keyword \"" + uniqueKeyword + "\" of type " + Serializers[uniqueKeyword]);
            if (Keywords.ContainsKey(serializableType)) throw new InvalidOperationException("Type " + serializableType + " try to register with multiple keywords");

            Type[] interfaces = serializableType.GetInterfaces();

            if (serializableType.GetConstructor(new Type[0]) == null) throw new InvalidOperationException("Type " + serializableType + " does not have public parameterless constructor");

            bool pass = false;

            foreach (Type i in interfaces)
            {
                if (i == typeof(ISerializable))
                {
                    pass = true;
                    break;
                }
            }

            if (!pass) throw new InvalidOperationException("Type " + serializableType + " does not implement interface ISerializable");

            

            Serializers[uniqueKeyword] = serializableType;
            Keywords[serializableType] = uniqueKeyword;
        }
    }
}
