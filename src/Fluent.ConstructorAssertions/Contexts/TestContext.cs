using System.Collections.Generic;
using System.Reflection;
using Fluent.ConstructorAssertions.TestCases;

namespace Fluent.ConstructorAssertions.Contexts
{
    public sealed class TestContext<T> where T : class
    {
        internal ConstructorInfo Constructor { get; }
        internal int NumberOfConstructorArguments { get; }
        internal IList<TestCase<T>> TestCases { get; } = new List<TestCase<T>>();

        internal TestContext(ConstructorInfo constructor, int numberOfConstructorArguments)
        {
            Constructor = constructor;
            NumberOfConstructorArguments = numberOfConstructorArguments;
        }

        public ExceptionContext<T> Throws<TException>(string? because = default)
        {
            return new(this, typeof(TException), because);
        }

        public SuccessContext<T> Succeeds(string? because = default)
        {
            return new(this, because);
        }
    }
}
