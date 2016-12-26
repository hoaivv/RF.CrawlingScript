using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace RF.CrawlingScript.Structures
{
    public abstract class Switch<T> : Code
    {
        private IExpression<T> Expression { get; set; }
        private Dictionary<T, ICode> Cases { get; set; } = new Dictionary<T, ICode>();
        private ICode Default { get; set; }

        protected abstract void SerializeData(BinaryWriter output, T data);
        protected abstract T DeserializeData(BinaryReader input);

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);

            output.Write(Cases.Count);

            foreach (KeyValuePair<T, ICode> p in Cases)
            {
                SerializeData(output, p.Key);
                Script.Serialize(output, p.Value);
            }
            Script.Serialize(output, Default);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as IExpression<T>;

            int count = input.ReadInt32();

            Cases.Clear();

            for (int i = 0; i < count; i++)
            {
                T key = DeserializeData(input);
                ICode value = Script.Deserialize(input) as ICode;

                Cases[key] = value;
            }

            Default = Script.Deserialize(input) as ICode;
        }

        public Switch() { }

        public Switch(IExpression<T> exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp;
        }

        public void Case(T value, ICode code)
        {
            if ((object)value == null || (object)code == null) throw new ArgumentNullException();

            Cases[value] = code;
        }

        public void Others(ICode code)
        {
            if ((object)code == null) throw new ArgumentNullException();

            Default = code;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            isBreaking = false;

            T test; Expression.Evaluate(context, out test);

            ICode code = Cases.ContainsKey(test) ? Cases[test] : Default;

            code?.Execute(context, out isBreaking);
        }
    }
}
