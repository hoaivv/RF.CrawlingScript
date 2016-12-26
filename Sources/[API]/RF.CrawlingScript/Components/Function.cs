using RF.CrawlingScript.Definitions;

namespace RF.CrawlingScript.Components
{
    public abstract class Function<T> : Code, IExpression<T>
    {
        public sealed override void Execute(Context context, out bool isBreaking)
        {
            isBreaking = false;
            T result;

            Evaluate(context, out result);
        }

        public void Evaluate(Context context, out object result)
        {
            T TEval;

            Evaluate(context, out TEval);

            result = TEval;
        }

        public abstract void Evaluate(Context context, out T result);
    }
}
