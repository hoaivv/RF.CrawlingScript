using System;
using System.IO;
using RF.CrawlingScript.Components;
using RF.CrawlingScript.Structures;

[assembly: SerializerContract("leave", typeof(Leave))]

namespace RF.CrawlingScript.Structures
{
    public class Leave : Code
    {
        public Leave() { }

        public override void Deserialize(BinaryReader input)
        {
        }

        public override void Serialize(BinaryWriter output)
        {
        }

        public override void Execute(Context context, out bool isBreaking)
        {
            isBreaking = true;
        }
    }
}
