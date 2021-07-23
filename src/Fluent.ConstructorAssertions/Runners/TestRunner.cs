using System.Collections.Generic;
using Fluent.ConstructorAssertions.Contexts;
using Fluent.ConstructorAssertions.TestCases;
using FluentAssertions;

namespace Fluent.ConstructorAssertions.Runners
{
    public sealed class TestRunner<T> where T : class
    {
        private readonly List<TestCase<T>> _testCases = new List<TestCase<T>>();

        internal TestRunner(ConstructorArgumentContext<T> context)
        {
            if (context.ExceptionContext != null)
                _testCases.AddRange(context.ExceptionContext.TestContext.TestCases);

            if (context.SuccessContext != null)
                _testCases.AddRange(context.SuccessContext.TestContext.TestCases);
        }

        public void BeTrue()
        {
            List<string> errorContext = new();
            ExecuteTestCases(errorContext);
            Assert(errorContext);
        }

        private void ExecuteTestCases(ICollection<string> errors)
        {
            foreach (TestCase<T> testCase in _testCases)
            {
                ExecuteTestCase(errors, testCase);
            }
        }

        private static void ExecuteTestCase(ICollection<string> errorContext, TestCase<T> testCase)
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
