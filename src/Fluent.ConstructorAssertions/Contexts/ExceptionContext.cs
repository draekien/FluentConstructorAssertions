using System;

namespace Fluent.ConstructorAssertions.Contexts
{
    /// <inheritdoc />
    public sealed class ExceptionContext<TClass> : ExpectedResultContext<TClass> where TClass : class
    {
        internal string? ExpectedMessage { get; }
        internal Type ExceptionType { get; }

        internal ExceptionContext(TestContext<TClass> testContext, Type exceptionType, string? expectedMessage)
            : base(testContext)
        {
            ExceptionType = exceptionType;
            ExpectedMessage = expectedMessage;
        }
    }
}
