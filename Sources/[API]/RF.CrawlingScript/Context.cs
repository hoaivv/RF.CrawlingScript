using RF.CrawlingScript.Components;
using System.Collections.Generic;

namespace RF.CrawlingScript
{
    public class Context
    {
        private Dictionary<int, object> Variables = new Dictionary<int, object>();
        private Dictionary<string, object> Objects = new Dictionary<string, object>();

        internal object GetVariable(int name)
        {
            if (Variables.ContainsKey(name)) return Variables[name];
            return null;
        }

        internal bool HasVariable(int name)
        {
            return Variables.ContainsKey(name);
        }

        internal void SetVariable(int name, object value)
        {
            Variables[name] = value;
        }

        internal object GetObject(string name)
        {
            return Objects?[name] ?? null;
        }

        internal void SetObject(string name, object value)
        {
            Objects[name] = value;
        }

        internal bool HasObject(string name)
        {
            return Objects.ContainsKey(name);
        }

        public void Set(string name, decimal value)
        {
            SetObject("custom:" + name, value);
        }

        public void Set(string name, Dictionary<string, string> value)
        {
            SetObject("custom:" + name, value);
        }

        public void Set(string name, bool value)
        {
            SetObject("custom:" + name, value);
        }

        public void Set(string name, string value)
        {
            SetObject("custom:" + name, value);
        }

        public void Set(string name, KeyValuePair<string, string> value)
        {
            SetObject("custom:" + name, value);
        }

        public void Set(string name, byte[] value)
        {
            SetObject("custom:" + name, value);
        }

        public static ContextDataValue GetData(TextExpression name)
        {
            return new ContextDataValue(name);
        }

        public static ContextDictionaryValue GetDict(TextExpression name)
        {
            return new ContextDictionaryValue(name);
        }

        public static ContextTextPairValue GetPair(TextExpression name)
        {
            return new ContextTextPairValue(name);
        }

        public static ContextTextValue GetText(TextExpression name)
        {
            return new ContextTextValue(name);
        }

        public static ContextNumberValue GetNumber(TextExpression name)
        {
            return new ContextNumberValue(name);
        }

        public static ContextLogicValue GetLogic(TextExpression name)
        {
            return new ContextLogicValue(name);
        }

    }
}
