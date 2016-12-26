using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[assembly: SerializerContract("block", typeof(CodeBlock))]

namespace RF.CrawlingScript.Components
{
    /// <summary>
    /// Describes an executable block of code of RFCScript
    /// </summary>
    public partial class CodeBlock : Code, IEnumerable
    {
        private List<ICode> Code { get; set; } = new List<ICode>();
    }

    partial class CodeBlock // constructors
    {
        /// <summary>
        /// Construct an empty block of RFCScript code. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        public CodeBlock() { }

        /// <summary>
        /// Construct a block of RFCScript code
        /// </summary>
        /// <param name="code">Codes, belong to current block</param>
        public CodeBlock(params ICode[] code)
        {
            Code.AddRange(code);
        }
    }

    partial class CodeBlock // IEnumerable
    {
        /// <summary>
        /// Returns an enumerator that iterates through code contained within the block.
        /// </summary>
        /// <returns>an enumerator that iterates through code contained within the block</returns>
        public IEnumerator GetEnumerator()
        {
            return Code.GetEnumerator();
        }

        /// <summary>
        /// Add a RFCScript to the the end of current code block
        /// </summary>
        /// <param name="code">Code to be added</param>
        public void Add(ICode code)
        {
            Code.Add(code);
        }
    }

    partial class CodeBlock // override
    {
        /// <summary>
        /// Execute code, contained within the block. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="context">Context, on which the script is running</param>
        /// <param name="isBreaking">Indicates whether the code is demanded to be broken half way</param>
        public override void Execute(Context context, out bool isBreaking)
        {
            isBreaking = false;

            foreach(ICode code in Code)
            {
                code.Execute(context, out isBreaking);
                if (isBreaking) break;
            }
        }

        /// <summary>
        /// Serialize <see cref="CodeBlock"/> data to a specified output. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="output">Ouput, to which the component's data sould be written</param>
        public override void Serialize(BinaryWriter output)
        {
            output.Write(Code.Count);
            
            foreach(ICode code in Code) Script.Serialize(output, code);
        }

        /// <summary>
        /// Deserialize <see cref="CodeBlock"/> data from a specified input. This method is designed to be invoked internally by RFCScript components only.
        /// </summary>
        /// <param name="input">Input, on which component's data is available to read</param>
        public override void Deserialize(BinaryReader input)
        {
            int count = input.ReadInt32();

            Code.Clear();

            for(int i = 0; i < count; i++)
            {
                Code.Add((ICode)Script.Deserialize(input));
            }
        }
    }
}
