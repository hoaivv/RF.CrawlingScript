using RF.CrawlingScript.Components;
using RF.CrawlingScript.Structures;
using System.IO;

[assembly: SerializerContract("switch.b", typeof(SwitchLogic))]

namespace RF.CrawlingScript.Structures
{
    public class SwitchLogic : Switch<bool>
    {
        public SwitchLogic() { }

        public SwitchLogic(LogicExpression exp) : base(exp) { }

        protected override bool DeserializeData(BinaryReader input)
        {
            return input.ReadBoolean();
        }

        protected override void SerializeData(BinaryWriter output, bool data)
        {
            output.Write(data);
        }
    }
}
