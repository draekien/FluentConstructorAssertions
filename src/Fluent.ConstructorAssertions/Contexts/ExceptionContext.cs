using System;

namespace Fluent.ConstructorAssertions.Contexts
{
    /// <inheritdoc />
    public sealed class ExceptionContext<TClass> : ExpectedResultContext<TClass> where TClass : class
    {
        internal string? Because { get; }
        internal Type ExceptionType { get; }

        internal ExceptionContext(TestContext<TClass> testContext, Type exceptionType, string? because)
            : base(testContext)
        {
            ExceptionType = exceptionType;
            Because = because;
        }
    }
}
