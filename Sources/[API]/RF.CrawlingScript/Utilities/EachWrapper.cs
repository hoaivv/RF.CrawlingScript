using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF.CrawlingScript.Utilities
{
    public class EachWrapper
    {
        private ISet Set { get; set; }
        private IVariable Variable { get; set; }

        internal EachWrapper(ISet set, IVariable var)
        {
            if ((object)set == null || (object)var == null) throw new ArgumentNullException();

            Set = set;
            Variable = var;
        }

        public static ForEach operator > (EachWrapper wrapper, exec code)
        {
            if (wrapper == null || (object)code == null) throw new ArgumentNullException();

            ForEach result = new ForEach(wrapper.Set, wrapper.Variable);
            result.Do(code);

            return result;
        }

        public static ForEach operator <(EachWrapper wrapper, exec code)
        {
            throw new InvalidOperationException();
        }
    }
}
