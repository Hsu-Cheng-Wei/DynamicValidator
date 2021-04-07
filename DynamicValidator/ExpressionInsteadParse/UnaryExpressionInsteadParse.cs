using System;
using System.Linq.Expressions;

namespace DynamicValidator.ExpressionInsteadParse
{
    internal class UnaryExpressionInsteadParse : ExpressionInsteadParseBase
    {
        public UnaryExpressionInsteadParse(Expression @new, Expression old) : base(@new, old) { }

        public override Expression Parse(Expression node)
        {
            if (node is UnaryExpression unary)
            {
                var op = (TryFindParse(unary.Operand, out var parse)) ? parse.Parse(unary.Operand) : base.Visit(unary.Operand);

                return Expression.MakeUnary(unary.NodeType, op, unary.Type);
                  
            }

            throw new InvalidOperationException();
        }
    }
}
