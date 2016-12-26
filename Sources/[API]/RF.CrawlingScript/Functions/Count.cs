using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;
using System;
using System.IO;

[assembly: SerializerContract("f.Count", typeof(Count))]

namespace RF.CrawlingScript.Functions
{
    public class Count : NumberExpression
    {
        private ISet Set { get; set; }

        public Count() { }

        public Count(ISet set)
        {
            if (set == null) throw new ArgumentNullException();

            Set = set;
        }

        public override void Evaluate(Context context, out object result)
        {
            int count; Set.DoCount(context, out count);
            result = (decimal)count;            
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Set);
        }

        public override void Deserialize(BinaryReader input)
        {
            Set = Script.Deserialize(input) as ISet;
        }
    }
}
