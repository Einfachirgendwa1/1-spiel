using System;
using System.Reflection;

namespace Validation {
    [AttributeUsage(AttributeTargets.Field)]
    public class Validate : Attribute {
        public readonly Func<FieldInfo, string> Message;
        public Func<object, bool> Condition;

        protected Validate(Func<FieldInfo, string> message) {
            Message = message;
        }
    }

    public class ValidateNumber : Validate {
        public ValidateNumber(Func<double, bool> condition, Func<FieldInfo, string> message) : base(message) {
            Condition = obj => obj switch {
                int intValue       => condition(intValue),
                float floatValue   => condition(floatValue),
                double doubleValue => condition(doubleValue),
                _                  => throw new ArgumentException($"{condition.GetType().Name} is not a number")
            };
        }
    }

    public class PositiveNonZero : ValidateNumber {
        public PositiveNonZero() : base(num => num > 0, field => $"{field.Name} > 0") { }
    }

    public class Positive : ValidateNumber {
        public Positive() : base(num => num >= 0, field => $"{field.Name} >= 0") { }
    }

    public class NonNull : Validate {
        public NonNull() : base(field => $"{field.Name} != null") {
            Condition = obj => obj is not null;
        }
    }
}