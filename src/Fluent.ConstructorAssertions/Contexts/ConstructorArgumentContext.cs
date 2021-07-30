using System;
using Fluent.ConstructorAssertions.Runners;
using Fluent.ConstructorAssertions.TestCases;

namespace Fluent.ConstructorAssertions.Contexts
{
    /// <summary>
    /// The context that handles creation of test cases based on the provided constructor arguments and the expected
    /// test results.
    /// </summary>
    /// <typeparam name="TClass">The class with the constructor to test.</typeparam>
    public sealed class ConstructorArgumentContext<TClass> where TClass : class
    {
        internal ExceptionContext<TClass>? ExceptionContext { get; }
        internal SuccessContext<TClass>? SuccessContext { get; }

        internal ConstructorArgumentContext(ExpectedResultContext<TClass> expectedResultContext, object?[] args)
        {
            switch (expectedResultContext)
            {
                case ExceptionContext<TClass> exceptionContext:
                {
                    ExceptionContext = exceptionContext;

                    TestCase<TClass> testCase = new FailTestCase<TClass>(
                        exceptionContext.TestContext.Constructor,
                        exceptionContext.ExpectedMessage,
                        exceptionContext.ExceptionType,
                        args
                    );

                    exceptionContext.TestContext.TestCases.Add(testCase);
                    break;
                }

                case SuccessContext<TClass> successContext:
                {
                    SuccessContext = successContext;

                    TestCase<TClass> testCase = new SuccessTestCase<TClass>(
                        successContext.TestContext.Constructor,
                        successContext.Because,
                        args
                    );

                    SuccessContext.TestContext.TestCases.Add(testCase);
                    break;
                }
            }
        }

        /// <summary>
        /// The chaining API call to add additional scenarios to the Test Context.
        /// </summary>
        /// <exception cref="InvalidOperationException">No test context exists.</exception>
        public TestContext<TClass> And => (ExceptionContext?.TestContext ?? SuccessContext?.TestContext)
                                  ?? throw new InvalidOperationException("No valid test context.");

        /// <summary>
        /// The chaining API call to start the test runner.
        /// </summary>
        public TestRunner<TClass> Should => new(this);
    }
}
