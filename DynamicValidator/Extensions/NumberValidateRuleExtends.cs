using System;
using System.Linq.Expressions;

namespace DynamicValidator.Extensions
{
    public class NumberValidateRuleExtends
    {
        public Expression<Func<int, bool>> Range(int lower, int upper) => (i) => i >= lower && i <= upper;

        public Expression<Func<int, bool>> GreaterThan(int num) => (i) => i > num;

        public Expression<Func<int, bool>> GreaterThanOrEqual(int num) => (i) => i >= num;

        public Expression<Func<int, bool>> LessThan(int num) => (i) => i < num;

        public Expression<Func<int, bool>> LessThanOrEqual(int num) => (i) => i <= num;
    }
}
