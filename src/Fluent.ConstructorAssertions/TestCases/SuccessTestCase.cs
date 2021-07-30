using System;
using System.Reflection;

namespace Fluent.ConstructorAssertions.TestCases
{
    internal class SuccessTestCase<T> : TestCase<T> where T : class
    {
        /// <inheritdoc />
        internal SuccessTestCase(ConstructorInfo constructor, string? expectedExceptionMessage, object?[] args)
            : base(constructor, expectedExceptionMessage, args) { }

        /// <inheritdoc />
        public override string Execute()
        {
            try
            {
                InvokeConstructor();
            }
            catch (Exception ex)
            {
                return Fail($"{ex.GetType().Name} occured: {ex.Message}");
            }

            return Success();
        }
    }
}
