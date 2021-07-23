using System;
using System.Reflection;
using Fluent.ConstructorAssertions.Contexts;
using FluentAssertions;
using JetBrains.Annotations;

namespace Fluent.ConstructorAssertions
{
    [PublicAPI]
    public sealed class ForConstructorOf<T> where T : class
    {
        public static TestContext<T> WithArgTypes(params Type[] argTypes)
        {
            ConstructorInfo? constructor = typeof(T).GetConstructor(argTypes);

            constructor.Should()
                       .NotBeNull("A constructor must exist for the provided class type.");

            return new TestContext<T>(constructor!, argTypes.Length);
        }
    }
}
