using System.Reflection;

namespace Fluent.ConstructorAssertions.TestCases
{
    internal abstract class TestCase<T> where T : class
    {
        private readonly object?[] _arguments;
        private readonly ConstructorInfo _constructor;
        protected readonly string? ExpectedExceptionMessage;

        protected TestCase(ConstructorInfo constructor, string? expectedExceptionMessage, object?[] args)
        {
            _constructor = constructor;
            _arguments = args;

            ExpectedExceptionMessage = expectedExceptionMessage;
        }

        protected T InvokeConstructor()
        {
            try
            {
                return (T)_constructor.Invoke(_arguments);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException!;
            }
        }

        protected string Fail(string message)
        {
            return string.IsNullOrWhiteSpace(ExpectedExceptionMessage)
                ? $"Test failed: {message}"
                : $"Test failed ({ExpectedExceptionMessage}): {message}";
        }

        protected string Success()
        {
            return string.Empty;
        }

        public abstract string Execute();
    }
}
