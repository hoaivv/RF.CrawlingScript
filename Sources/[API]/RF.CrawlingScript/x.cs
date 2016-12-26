using RF.CrawlingScript.Components;
using RF.CrawlingScript.Structures;
using RF.CrawlingScript.Utilities;
using System.Collections.Generic;

namespace RF.CrawlingScript
{
    public partial class x
    {
        private int Counter = 0;
    }

    partial class x // expression
    {
        public TextVariable text
        {
            get
            {
                return new TextVariable(Counter++);
            }
        }

        public DataVariable data
        {
            get
            {
                return new DataVariable(Counter++);
            }
        }

        public TextPairVariable pair
        {
            get
            {
                return new TextPairVariable(Counter++);
            }
        }

        public LogicVariable logic
        {
            get
            {
                return new LogicVariable(Counter++);
            }
        }

        public NumberVariable number
        {
            get
            {
                return new NumberVariable(Counter++);
            }
        }

        public DictionaryVariable dict
        {
            get
            {
                return new DictionaryVariable(Counter++);
            }
        }

        public TextExpression this[string value]
        {
            get
            {
                return value;
            }
        }

        public NumberExpression this[decimal value]
        {
            get
            {
                return value;
            }
        }

        public LogicExpression this[bool value]
        {
            get
            {
                return value;
            }
        }

        public DataExpression this[byte[] value]
        {
            get
            {
                return value;
            }
        }
    }
}
