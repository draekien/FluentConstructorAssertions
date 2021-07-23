using System.Reflection;

namespace Fluent.ConstructorAssertions.TestCases
{
    internal abstract class TestCase<T> where T : class
    {
        private readonly object?[] _arguments;
        private readonly string? _because;
        private readonly ConstructorInfo _constructor;

        protected TestCase(ConstructorInfo constructor, string? because, object?[] args)
        {
            _constructor = constructor;
            _because = because;
            _arguments = args;
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
            return string.IsNullOrWhiteSpace(_because)
                ? $"Test failed: {message}"
                : $"Test failed ({_because}): {message}";
        }

        protected string Success()
        {
            return string.Empty;
        }

        public abstract string Execute();
    }
}
