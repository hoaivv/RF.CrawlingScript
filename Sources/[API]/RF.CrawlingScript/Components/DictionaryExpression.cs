using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using RF.CrawlingScript.Structures;
using System.Collections;
using System.Collections.Generic;

namespace RF.CrawlingScript.Components
{
    public abstract partial class DictionaryExpression : Value<Dictionary<string, string>>, ISet
    {
        public static implicit operator DictionaryExpression(Dictionary<string, string> value)
        {
            return new DictionaryValue(value);
        }

        public void GetEnumerator(Context context, out IEnumerator result)
        {
            Dictionary<string, string> dict; Evaluate(context, out dict);
            result = dict.GetEnumerator();
        }

        public object Convert(object element)
        {
            return element;
        }
    }

    partial class DictionaryExpression // implement [] operator
    {
        public DictionaryExpressionValue this[int key]
        {
            get
            {
                return new DictionaryExpressionValue(this, key.ToString());
            }
        }

        public DictionaryExpressionValue this[TextExpression key]
        {
            get
            {
                return new DictionaryExpressionValue(this, key);
            }
        }

        public Count Count
        {
            get
            {
                return new Count(this);
            }
        }

        public abstract void DoCount(Context context, out int result);
    }

}
