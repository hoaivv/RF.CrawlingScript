using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("foreach", typeof(ForEach))]

namespace RF.CrawlingScript.Structures
{
    public class ForEach : Code
    {
        private ISet Set { get; set; }
        private IVariable Var { get; set; }
        private ICode Code { get; set; }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Set);
            Script.Serialize(output, Var);
            Script.Serialize(output, Code);
        }

        public override void Deserialize(BinaryReader input)
        {
            Set = Script.Deserialize(input) as ISet;
            Var = Script.Deserialize(input) as IVariable;
            Code = Script.Deserialize(input) as ICode;
        }

        public ForEach() { }

        public ForEach(ISet set, IVariable var)
        {
            if (set == null || var == null) throw new ArgumentNullException();

            Set = set;
            Var = var;
        }

        public void Do(ICode code)
        {
            if (code == null) throw new ArgumentNullException();

            Code = code;
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            IEnumerator i; Set.GetEnumerator(context, out i);

            isBreaking = false;

            while(i.MoveNext())
            {
                Var.Set(context, Set.Convert(i.Current));

                Code?.Execute(context, out isBreaking);

                if (isBreaking) break;
            }

            isBreaking = false;
        }
    }
}
