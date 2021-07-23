using System;
using Xunit;

namespace Fluent.ConstructorAssertions.UnitTests
{
    public class MockClassTests
    {
        private class MockClass
        {
            private readonly int _num;
            private readonly string _word;

            public MockClass(int? num, string word)
            {
                _num = num ?? throw new ArgumentNullException(nameof(num));
                _word = word ?? throw new ArgumentNullException(nameof(word));

                if (num <= 0) throw new ArgumentException("num cannot be less than or equal to 0", nameof(num));
            }
        }

        [Fact]
        public void Test1()
        {
            ForConstructorOf<MockClass>
                .WithArgTypes(typeof(int?), typeof(string))
                .Throws<ArgumentNullException>("Null num should throw exception")
                .ForArgs(null, "test")
                .And.Throws<ArgumentNullException>()
                .ForArgs(1, null)
                .And.Throws<ArgumentNullException>()
                .ForArgs()
                .And.Succeeds()
                .ForArgs(1, "test")
                .Should.BeTrue();
        }
    }
}
