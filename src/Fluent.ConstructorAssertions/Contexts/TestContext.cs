using System.Collections.Generic;
using System.Reflection;
using Fluent.ConstructorAssertions.TestCases;

namespace Fluent.ConstructorAssertions.Contexts
{
    /// <summary>
    /// The test context is responsible for storing the constructor and the test cases defined by the chaining methods.
    /// </summary>
    /// <typeparam name="TClass">The class with the constructor to test.</typeparam>
    public sealed class TestContext<TClass> where TClass : class
    {
        internal ConstructorInfo Constructor { get; }
        internal int NumberOfConstructorArguments { get; }
        internal IList<TestCase<TClass>> TestCases { get; } = new List<TestCase<TClass>>();

        internal TestContext(ConstructorInfo constructor, int numberOfConstructorArguments)
        {
            Constructor = constructor;
            NumberOfConstructorArguments = numberOfConstructorArguments;
        }

        /// <summary>
        /// Begins construction of a test case that asserts an exception of type TException will be thrown.
        /// Specify an expected message if you want to catch a specific exception message, and not just the type of exception thrown.
        /// </summary>
        /// <param name="expectedMessage">The expected exception message.</param>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <returns>A new <see cref="ExceptionContext{TClass}"/> for TClass.</returns>
        public ExceptionContext<TClass> Throws<TException>(string? expectedMessage = default)
        {
            return new(this, typeof(TException), expectedMessage);
        }

        /// <summary>
        /// Begins construction of a test case which asserts that no errors are thrown when instantiation TClass.
        /// </summary>
        /// <param name="because">The reason the test should pass.</param>
        /// <returns>A new <see cref="SuccessContext{TClass}"/> for TClass.</returns>
        public SuccessContext<TClass> Succeeds(string? because = default)
        {
            return new(this, because);
        }
    }
}
