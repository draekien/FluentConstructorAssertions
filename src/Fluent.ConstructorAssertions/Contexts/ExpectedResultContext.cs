using System.Linq;

namespace Fluent.ConstructorAssertions.Contexts
{
    /// <summary>
    /// The context responsible for telling the test runner which outcome is expected and which assertion should be executed.
    /// </summary>
    /// <typeparam name="TClass">The class with the constructor to test.</typeparam>
    public abstract class ExpectedResultContext<TClass> where TClass : class
    {
        internal TestContext<TClass> TestContext { get; }

        /// <summary>
        /// Instantiates a new <see cref="ExpectedResultContext{TClass}"/>
        /// </summary>
        /// <param name="testContext">The current <see cref="TestContext"/></param>
        protected ExpectedResultContext(TestContext<TClass> testContext)
        {
            TestContext = testContext;
        }

        /// <summary>
        /// Creates the <see cref="ConstructorArgumentContext{TClass}"/> for TClass with the provided argument parameters.
        /// </summary>
        /// <param name="args">The arguments to instantiate TClass with.</param>
        /// <remarks>Not passing through any params will instantiate TClass with all arguments set to null.</remarks>
        /// <returns>The constructor argument context for TClass.</returns>
        public ConstructorArgumentContext<TClass> ForArgs(params object?[] args)
        {
            return !args.Any()
                ? new ConstructorArgumentContext<TClass>(this, new object?[TestContext.NumberOfConstructorArguments])
                : new ConstructorArgumentContext<TClass>(this, args);
        }
    }
}
