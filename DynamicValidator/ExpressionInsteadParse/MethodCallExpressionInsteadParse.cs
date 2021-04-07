using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DynamicValidator.ExpressionInsteadParse
{
    internal class MethodCallExpressionInsteadParse : ExpressionInsteadParseBase
    {
        public MethodCallExpressionInsteadParse(Expression @new, Expression old) : base(@new, old) { }

        public override Expression Parse(Expression node)
        {
            if (!(node is MethodCallExpression call)) throw new InvalidOperationException();

            var args = new List<Expression>();

            foreach(var arg in call.Arguments)
            {
                var newArg = TryFindParse(arg, out var parse) ? parse.Parse(arg) : base.Visit(arg);

                args.Add(newArg);
            }

            return Expression.Call(call.Object, call.Method, args);
        }
    }
}
