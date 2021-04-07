using DynamicValidator.ExpressionInsteadParse;
using System.Linq.Expressions;

namespace DynamicValidator
{
    internal class InsteadParamterExpression : ExpressionInsteadParseBase
    {
        public InsteadParamterExpression(Expression @new, Expression @old) : base (@new, old) { }

        public override Expression Parse(Expression node)
        {
            if (node is LambdaExpression lambda) return Parse(lambda.Body);

            if (node == Old) return @New;

            if (TryFindParse(node, out var parse)) return parse.Parse(node);

            return base.Visit(node);
        }
    }
}
