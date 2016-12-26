using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF.CrawlingScript.Components
{
    public abstract class TextPairExpression : Value<KeyValuePair<string, string>>
    {
        public TextPairKeyExpression Key { get { return new TextPairKeyExpression(this); } }
        public TextPairValueExpression Value { get { return new TextPairValueExpression(this); } }

        public static implicit operator TextPairExpression(KeyValuePair<string, string> value)
        {
            return new TextPairValue(value);
        }
    }
}
