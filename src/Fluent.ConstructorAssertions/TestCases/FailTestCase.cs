using System;
using System.Reflection;

namespace Fluent.ConstructorAssertions.TestCases
{
    internal class FailTestCase<T> : TestCase<T> where T : class
    {
        private readonly Type _exceptionType;

        /// <inheritdoc />
        internal FailTestCase(
            ConstructorInfo constructor,
            string? because,
            Type exceptionType,
            params object?[] args
        )
            : base(constructor, because, args)
        {
            _exceptionType = exceptionType;
        }

        /// <inheritdoc />
        public override string Execute()
        {
            try
            {
                InvokeConstructor();
                return Fail($"{_exceptionType.Name} not thrown when expected.");
            }
            catch (Exception ex)
            {
                if (ex.GetType() != _exceptionType)
                {
                    return Fail($"{ex.GetType().Name} thrown when {_exceptionType.Name} was expected.");
                }
            }

            return Success();
        }
    }
}
