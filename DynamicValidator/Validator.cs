using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DynamicValidator
{
    public class ValidatorRule
    {
        public LambdaExpression MemberSelectExpression { get; set; }

        public LambdaExpression MemberRuleExpressoin { get; set; }
    }


    public abstract class Validator
    {
        protected readonly List<ValidatorRule> Rules = new List<ValidatorRule>();

        internal bool Validate(object instance, Type instanceType)
        {
            var paramterExpression = Expression.Parameter(typeof(object));

            var parameterConvert = Expression.Convert(paramterExpression, instanceType);

            var targetLabel = Expression.Label(typeof(bool));

            var returnFalse = Expression.Return(targetLabel, Expression.Constant(false));

            var returnTrue = Expression.Return(targetLabel, Expression.Constant(true));

            var conditions = new List<Expression>();

            foreach(var rule in Rules)
            {
                var member = new InsteadParamterExpression(parameterConvert, rule.MemberSelectExpression.Parameters[0])
                    .Parse(rule.MemberSelectExpression);

                var newRule = new InsteadParamterExpression(member, rule.MemberRuleExpressoin.Parameters[0])
                    .Parse(Expression.Not(rule.MemberRuleExpressoin.Body));

                conditions.Add(IfExpression(newRule, returnFalse));
            }

            conditions.Add(returnTrue);

            conditions.Add(Expression.Label(targetLabel, Expression.Constant(false)));

            var block = Expression.Block(conditions);

            var lambda = Expression.Lambda<Func<object, bool>>(block, paramterExpression);

            var compile = lambda.Compile();

            return lambda.Compile().Invoke(instance);
        }

        public static Validator<T> NewRule<T>() where T : class
            => new Validator<T>();

        protected static Expression IfExpression(Expression condition, Expression returnExpression)
        {
            return Expression.IfThen(condition, returnExpression);
        }
    }

    public class Validator<T> : Validator where T : class
    {
        public Validator<T> SetRule<TProperty>(Expression<Func<T, TProperty>> selectExpression, Expression<Func<TProperty, bool>> ruleExpression)
        {

            Rules.Add(new ValidatorRule
            {
                MemberSelectExpression = selectExpression,
                MemberRuleExpressoin = ruleExpression
            });

            return this;
        }

        public bool Validate(T parameter)
        {
            return Validate(parameter, typeof(T));
        }
    }
}
