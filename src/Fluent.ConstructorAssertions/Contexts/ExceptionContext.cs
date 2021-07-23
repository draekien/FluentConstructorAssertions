using System;

namespace Fluent.ConstructorAssertions.Contexts
{
    public sealed class ExceptionContext<T> : ExpectedResultContext<T> where T : class
    {
        internal string? Because { get; }
        internal Type ExceptionType { get; }

        internal ExceptionContext(TestContext<T> testContext, Type exceptionType, string? because) : base(testContext)
        {
            ExceptionType = exceptionType;
            Because = because;
        }
    }
}
