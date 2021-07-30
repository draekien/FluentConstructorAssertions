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
            string? expectedExceptionMessage,
            Type exceptionType,
            params object?[] args
        )
            : base(constructor, expectedExceptionMessage, args)
        {
            _exceptionType = exceptionType;
        }

        /// <inheritdoc />
        public override string Execute()
        {
            try
            {
                InvokeConstructor();
                return Fail($"\"{_exceptionType.Name}\" not thrown when expected.");
            }
            catch (Exception ex) when (ex.GetType() != _exceptionType)
            {
                return Fail($"\"{ex.GetType().Name}\" thrown when \"{_exceptionType.Name}\" was expected.");
            }
            catch (Exception ex) when (!string.IsNullOrWhiteSpace(ExpectedExceptionMessage) && !ex.Message.Equals(ExpectedExceptionMessage))
            {
                return Fail($"Expected \"{ExpectedExceptionMessage}\" but instead received \"{ex.Message}\".");
            }
            catch (Exception)
            {
                return Success();
            }
        }
    }
}
