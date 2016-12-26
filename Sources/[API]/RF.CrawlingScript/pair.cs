using RF.CrawlingScript.Components;
using System.Collections.Generic;

namespace RF.CrawlingScript
{
    public class pair
    {
        private string Key { get; set; }
        private string Value { get; set; }

        public pair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator TextPairExpression(pair value)
        {
            return new KeyValuePair<string, string>(value.Key, value.Value);
        }
    }
}
