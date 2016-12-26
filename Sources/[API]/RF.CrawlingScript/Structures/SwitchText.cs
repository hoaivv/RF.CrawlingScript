using RF.CrawlingScript.Components;
using RF.CrawlingScript.Structures;
using System.IO;

[assembly: SerializerContract("switch.s", typeof(SwitchText))]

namespace RF.CrawlingScript.Structures
{
    public class SwitchText : Switch<string>
    {
        public SwitchText() { }

        public SwitchText(TextExpression exp) : base(exp) { }

        protected override string DeserializeData(BinaryReader input)
        {
            return input.ReadString();
        }

        protected override void SerializeData(BinaryWriter output, string data)
        {
            output.Write(data);
        }
    }
}
