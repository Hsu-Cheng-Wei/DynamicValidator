using DynamicValidator;
using DynamicValidator.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicValidatorTests
{
    [TestClass]
    public class UnitTest1
    {
        public class Demo
        {
            public int Id { get; set; }

            public string Address { get; set; }
        }

        [TestMethod]
        public void ValidateTrueTest()
        {
            var rule = Validator.NewRule<Demo>()
                .SetRule(d => d.Id, i => i.Range(1, 10))
                .SetRule(d => d.Address, i => i.IsNullOrEmpty);

            var result = rule.Validate(new Demo
            {
                Id = 10,
                Address = "",
            });

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ValidateFalseTest()
        {
            var rule = Validator.NewRule<Demo>()
                .SetRule(d => d.Id, i => i > 10)
                .SetRule(d => d.Address, i => i.IsNotNullOrEmpty);

            var result = rule.Validate(new Demo
            {
                Id = 20,
                Address = "",
            });

            Assert.AreEqual(false, result);
        }
    }
}
