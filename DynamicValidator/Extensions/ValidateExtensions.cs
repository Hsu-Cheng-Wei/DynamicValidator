using System;
using System.Linq.Expressions;

namespace DynamicValidator.Extensions
{
    public static class ValidateExtensions
    {
        public static Validator<T> SetRule<T>(this Validator<T> validator, Expression<Func<T, string>> property, Func<StringValidateRuleExtends, Expression<Func<string, bool>>> rule) where T : class
        {
            return validator.SetRule(property, rule(new StringValidateRuleExtends()));
        }

        public static Validator<T> SetRule<T>(this Validator<T> validator, Expression<Func<T, int>> property, Func<NumberValidateRuleExtends, Expression<Func<int, bool>>> rule) where T : class
        {
            return validator.SetRule(property, rule(new NumberValidateRuleExtends()));
        }
    }
}
