# DynamicValidator

**Help dynamic generate rule of object equal**

## How to use.
    public class Demo
    {
        public int Id { get; set; }

        public string Address { get; set; }
    }

    void Main()
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