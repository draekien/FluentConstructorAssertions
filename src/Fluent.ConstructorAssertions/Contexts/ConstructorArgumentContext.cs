using System;
using Fluent.ConstructorAssertions.Runners;
using Fluent.ConstructorAssertions.TestCases;

namespace Fluent.ConstructorAssertions.Contexts
{
    public sealed class ConstructorArgumentContext<T> where T : class
    {
        internal ExceptionContext<T>? ExceptionContext { get; }
        internal SuccessContext<T>? SuccessContext { get; }

        internal ConstructorArgumentContext(ExpectedResultContext<T> expectedResultContext, params object?[] args)
        {
            switch (expectedResultContext)
            {
                case ExceptionContext<T> exceptionContext:
                {
                    ExceptionContext = exceptionContext;

                    TestCase<T> testCase = new FailTestCase<T>(
                        exceptionContext.TestContext.Constructor,
                        exceptionContext.Because,
                        exceptionContext.ExceptionType,
                        args
                    );

                    exceptionContext.TestContext.TestCases.Add(testCase);
                    break;
                }

                case SuccessContext<T> successContext:
                {
                    SuccessContext = successContext;

                    TestCase<T> testCase = new SuccessTestCase<T>(
                        successContext.TestContext.Constructor,
                        successContext.Because,
                        args
                    );

                    SuccessContext.TestContext.TestCases.Add(testCase);
                    break;
                }
            }
        }

        public TestContext<T> And => (ExceptionContext?.TestContext ?? SuccessContext?.TestContext) ?? throw new InvalidOperationException();

        public TestRunner<T> Should => new(this);
    }
}
