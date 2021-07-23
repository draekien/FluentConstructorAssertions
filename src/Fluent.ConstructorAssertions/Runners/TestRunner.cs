using System.Collections.Generic;
using Fluent.ConstructorAssertions.Contexts;
using Fluent.ConstructorAssertions.TestCases;
using FluentAssertions;

namespace Fluent.ConstructorAssertions.Runners
{
    /// <summary>
    /// The class responsible for running the tests.
    /// </summary>
    /// <typeparam name="TClass">The class with the constructor to test.</typeparam>
    public sealed class TestRunner<TClass> where TClass : class
    {
        private readonly List<TestCase<TClass>> _testCases = new();

        internal TestRunner(ConstructorArgumentContext<TClass> context)
        {
            if (context.ExceptionContext != null)
                _testCases.AddRange(context.ExceptionContext.TestContext.TestCases);

            if (context.SuccessContext != null)
                _testCases.AddRange(context.SuccessContext.TestContext.TestCases);
        }

        /// <summary>
        /// Executes the registered test cases from the context and asserts that all scenarios are true.
        /// </summary>
        public void BeTrue()
        {
            List<string> errorContext = new();
            ExecuteTestCases(errorContext);
            Assert(errorContext);
        }

        private void ExecuteTestCases(ICollection<string> errors)
        {
            foreach (TestCase<TClass> testCase in _testCases)
            {
                ExecuteTestCase(errors, testCase);
            }
        }

        private static void ExecuteTestCase(ICollection<string> errorContext, TestCase<TClass> testCase)
        {
            string error = testCase.Execute();

            if (!string.IsNullOrWhiteSpace(error))
            {
                errorContext.Add($"----> {error}");
            }
        }

        private static void Assert(List<string> errorContext)
        {
            errorContext.Count.Should()
                        .Be(0, $"{errorContext.Count} scenarios(s) should be true:\n{string.Join("\n", errorContext.ToArray())}\n");
        }
    }
}
