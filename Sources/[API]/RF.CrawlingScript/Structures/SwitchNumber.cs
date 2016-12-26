using RF.CrawlingScript.Components;
using RF.CrawlingScript.Structures;
using System.IO;

[assembly: SerializerContract("switch.m", typeof(SwitchNumber))]

namespace RF.CrawlingScript.Structures
{
    public class SwitchNumber : Switch<decimal>
    {
        public SwitchNumber() { }

        public SwitchNumber(NumberExpression exp) : base(exp) { }

        protected override decimal DeserializeData(BinaryReader input)
        {
            return input.ReadDecimal();
        }

        protected override void SerializeData(BinaryWriter output, decimal data)
        {
            output.Write(data);
        }
    }
}
