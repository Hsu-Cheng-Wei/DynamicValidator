using System;
using System.Linq.Expressions;

namespace DynamicValidator.ExpressionInsteadParse
{
    internal abstract class ExpressionInsteadParseBase : DynamicExpressionVisitor
    {
        protected readonly Expression @New;

        protected readonly Expression @Old;

        public ExpressionInsteadParseBase(Expression @new, Expression old)
        {
            @New = @new;
            @Old = old;
        }

        public override Expression Visit(Expression node)
        {
            if (node == Old) return New;

            return base.Visit(node);
        }

        public abstract Expression Parse(Expression node);

        protected bool TryFindParse(Expression node, out ExpressionInsteadParseBase parse)
        {
            parse = null;
            if (ExpressionInsteadParseMap.ParesMap.ContainsKey(node.NodeType))
            {
                parse = (ExpressionInsteadParseBase)Activator.CreateInstance(ExpressionInsteadParseMap.ParesMap[node.NodeType], @New, @Old);

                return true;
            }
            return false;
        }

    }
}
