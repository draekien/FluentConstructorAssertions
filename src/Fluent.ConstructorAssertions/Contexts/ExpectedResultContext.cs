using System.Linq;

namespace Fluent.ConstructorAssertions.Contexts
{
    public abstract class ExpectedResultContext<T> where T : class
    {
        internal TestContext<T> TestContext { get; }

        protected ExpectedResultContext(TestContext<T> testContext)
        {
            TestContext = testContext;
        }

        public ConstructorArgumentContext<T> ForArgs(params object?[] args)
        {
            return !args.Any()
                ? new ConstructorArgumentContext<T>(this, new object?[TestContext.NumberOfConstructorArguments])
                : new ConstructorArgumentContext<T>(this, args);
        }
    }
}
