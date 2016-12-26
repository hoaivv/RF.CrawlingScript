using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Structures;
using RF.CrawlingScript.Utilities;
using System;
using System.IO;

namespace RF.CrawlingScript
{
    public abstract partial class Script
    {
        protected static x x { get; private set; } = new x();
        protected static f f { get; private set; } = new f();

        protected static OthersDirective others { get; private set; } = new OthersDirective();
        protected static Leave leave { get { return new Leave(); } }
    }

    partial class Script
    {
        public abstract block Implementation();

        public void Run(Context context)
        {
            if (context == null) throw new ArgumentNullException();

            if (SerializerContractAttribute.Serializers.Count == 0) throw new InvalidOperationException("Shark is not started");

            bool isBreaking;
            Implementation()?.Execute(context, out isBreaking);
        }

        public void Run()
        {
            Run(new Context());
        }

        private static void Run(Context context, BinaryReader input)
        {
            if (SerializerContractAttribute.Serializers.Count == 0) throw new InvalidOperationException("Shark is not started");

            bool isBreaking;
            (Deserialize(input) as ICode).Execute(context, out isBreaking);
        }

        public static Exception Run(Context context, byte[] data)
        {
            if (context == null) throw new ArgumentNullException();

            MemoryStream ms = null;
            BinaryReader reader = null;
            Exception result = null;

            try
            {
                ms = new MemoryStream(data);
                reader = new BinaryReader(ms);

                Run(context, reader);                
            }
            catch(Exception e)
            {
                result = e;    
            }

            ms.Close();
            reader.Close();

            return result;
        }

        public static Exception Run(byte[] data)
        {
            return Run(new Context(), data);
        }

        public static Exception Run(Context context, string file)
        {
            if (context == null) throw new ArgumentNullException();

            FileStream fs = null;
            BinaryReader reader = null;
            Exception result = null;

            try
            {
                fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(fs);

                Run(context, reader);
            }
            catch(Exception e)
            {
                result = e;
            }

            fs.Close();
            reader.Close();

            return result;
        }

        public static Exception Run(string file)
        {
            return Run(new Context(), file);
        }

        public bool Build(string file)
        {
            if (SerializerContractAttribute.Serializers.Count == 0) throw new InvalidOperationException("Shark is not started");

            FileStream fs = null;
            BinaryWriter writer = null;
            bool result = true;

            try
            {
                fs = new FileStream(file, FileMode.Create, FileAccess.Write);
                writer = new BinaryWriter(fs);

                Serialize(writer, Implementation());
            }
            catch (Exception e)
            {
                result = false;
            }

            fs?.Close();
            writer?.Close();

            return result;
        }

        public static void Serialize(BinaryWriter output, ISerializable code)
        {
            if (SerializerContractAttribute.Serializers.Count == 0) throw new InvalidOperationException("Shark is not started");

            if (code == null)
            {
                output.Write("null");
                return;
            }

            Type type = code.GetType();

            while(!SerializerContractAttribute.Keywords.ContainsKey(type) && type != typeof(object))
            {
                type = type.BaseType;
            }

            if (SerializerContractAttribute.Keywords.ContainsKey(type))
            {
                output.Write(SerializerContractAttribute.Keywords[type]);
                code.Serialize(output);
            }
        }

        public static ISerializable Deserialize(BinaryReader input)
        {
            if (SerializerContractAttribute.Serializers.Count == 0) throw new InvalidOperationException("Shark is not started");

            string keyword = input.ReadString();

            if (keyword != "null" && SerializerContractAttribute.Serializers.ContainsKey(keyword))
            {
                ISerializable result = SerializerContractAttribute.Serializers[keyword].GetConstructor(new Type[0]).Invoke(new object[0]) as ISerializable;
                result.Deserialize(input);

                return result;
            }

            return null;
        }
    }
}
