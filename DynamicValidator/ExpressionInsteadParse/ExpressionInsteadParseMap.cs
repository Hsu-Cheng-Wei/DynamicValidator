using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DynamicValidator.ExpressionInsteadParse
{
    internal static class ExpressionInsteadParseMap
    {
        public static Dictionary<ExpressionType, Type> ParesMap
            = new Dictionary<ExpressionType, Type>
            {
                { ExpressionType.Call, typeof(MethodCallExpressionInsteadParse) },
                { ExpressionType.Not, typeof(UnaryExpressionInsteadParse) }
            };
    }
}
