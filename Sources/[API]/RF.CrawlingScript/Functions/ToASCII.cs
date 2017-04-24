using RF.CrawlingScript.Components;
using RF.CrawlingScript.Functions;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

[assembly: SerializerContract("f.ToASCII", typeof(ToASCII))]

namespace RF.CrawlingScript.Functions
{
    public class ToASCII : TextExpression
    {
        private TextExpression Expression { get; set; }

        private static string[] UTF8Lookup = new string[]
        {
            "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ"
        };

        private static string[] ASCIILookup = new string[]
        {
            "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y"
        };

        public ToASCII() { }

        public ToASCII(TextExpression exp)
        {
            if ((object)exp == null) throw new ArgumentNullException();

            Expression = exp;
        }



        public override void Evaluate(Context context, out object result)
        {
            string text; Expression.Evaluate(context, out text);

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            text = text.Normalize(NormalizationForm.FormD);
            text = regex.Replace(text, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');

            for (int i = 0; i < UTF8Lookup.Length; i++)
            {
                text = text.Replace(UTF8Lookup[i], ASCIILookup[i]);
                text = text.Replace(UTF8Lookup[i].ToUpper(), ASCIILookup[i].ToUpper());
            }

            result = text;
        }

        public override void Serialize(BinaryWriter output)
        {
            Script.Serialize(output, Expression);
        }

        public override void Deserialize(BinaryReader input)
        {
            Expression = Script.Deserialize(input) as TextExpression;
        }
    }
}
