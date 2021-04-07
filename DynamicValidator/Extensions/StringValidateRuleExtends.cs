using System;
using System.Linq.Expressions;

namespace DynamicValidator.Extensions
{
    public class StringValidateRuleExtends
    {
        public Expression<Func<string, bool>> IsNullOrEmpty => (s) => string.IsNullOrEmpty(s);

        public Expression<Func<string, bool>> IsNotNullOrEmpty => (s) => !string.IsNullOrEmpty(s);
    }
}
