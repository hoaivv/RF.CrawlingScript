using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("f.Min", typeof(Min))]

namespace RF.CrawlingScript.Functions
{
    public class Min : NumberExpression
    {
        private List<NumberExpression> Expressions { get; set; } = new List<NumberExpression>();

        public Min() { }

        public Min(params NumberExpression[] exps)
        {
            if (exps.Length == 0) throw new ArgumentOutOfRangeException();

            Expressions.AddRange(exps);
        }

        public override void Evaluate(Context context, out object result)
        {
            decimal test = decimal.MaxValue;

            foreach (NumberExpression exp in Expressions)
            {
                decimal one; exp.Evaluate(context, out one);

                if (one < test) test = one;
            }

            result = test;
        }

        public override void Serialize(BinaryWriter output)
        {
            output.Write(Expressions.Count);
            foreach (NumberExpression i in Expressions) Script.Serialize(output, i);
        }

        public override void Deserialize(BinaryReader input)
        {
            int count = input.ReadInt32();

            Expressions.Clear();
            for (int i = 0; i < count; i++) Expressions.Add(Script.Deserialize(input) as NumberExpression);
        }

    }
}
