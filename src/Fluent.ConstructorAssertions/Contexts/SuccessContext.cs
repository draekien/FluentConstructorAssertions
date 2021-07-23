namespace Fluent.ConstructorAssertions.Contexts
{
    public sealed class SuccessContext<T> : ExpectedResultContext<T> where T : class
    {
        internal string? Because { get; }

        internal SuccessContext(TestContext<T> testContext, string? because) : base(testContext)
        {
            Because = because;
        }
    }
}
