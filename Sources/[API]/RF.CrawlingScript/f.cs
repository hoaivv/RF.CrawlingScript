using RF.CrawlingScript.Components;
using RF.CrawlingScript.Definitions;
using RF.CrawlingScript.Functions;

namespace RF.CrawlingScript
{
    public class f
    {
        public WriteLine WriteLine(string str)
        {
            return new WriteLine(new TextValue(str));
        }

        public WriteLine WriteLine()
        {
            return WriteLine("");
        }

        public WriteLine WriteLine(IExpression exp)
        {
            return new WriteLine(exp);
        }

        public Write Write(string str)
        {
            return new Write(new TextValue(str));
        }

        public Write Write(IExpression exp)
        {
            return new Write(exp);
        }

        public Log Log(TextExpression exp)
        {
            return new Log(exp);
        }

        public WriteFile WriteFile(TextExpression name, DataExpression value)
        {
            return new WriteFile(name, value);
        }

        public ReadFile ReadFile(TextExpression file)
        {
            return new ReadFile(file);
        }

        public Max Max(params NumberExpression[] exps)
        {
            return new Max(exps);
        }

        public Min Min(params NumberExpression[] exps)
        {
            return new Min(exps);
        }

        public ReadLine ReadLine()
        {
            return new ReadLine();
        }

        public Read Read()
        {
            return new Read();
        }

        public ReadKey ReadKey()
        {
            return new ReadKey(false);
        }

        public ReadKey Readkey(LogicExpression intercept)
        {
            return new ReadKey(intercept);
        }

        public Matches Matches(TextExpression input,TextExpression pattern)
        {
            return new Matches(input, pattern);
        }

        public Match Match(TextExpression input, TextExpression pattern)
        {
            return new Match(input, pattern);
        }

        public static Get Get(TextExpression service, TextVariable storage, DictionaryExpression request = null)
        {
            return new Get(service, request, storage);
        }

        public static Get Get(TextExpression service, NumberVariable storage, DictionaryExpression request = null)
        {
            return new Get(service, request, storage);
        }

        public static Get Get(TextExpression service, LogicVariable storage, DictionaryExpression request = null)
        {
            return new Get(service, request, storage);
        }

        public static Get Get(TextExpression service, DictionaryVariable storage, DictionaryExpression request = null)
        {
            return new Get(service, request, storage);
        }

        public static Get Get(TextExpression service, DataVariable storage, DictionaryExpression request = null)
        {
            return new Get(service, request, storage);
        }

        public static HtmlDecode HtmlDecode(TextExpression exp)
        {
            return new HtmlDecode(exp);
        }
    }
}
