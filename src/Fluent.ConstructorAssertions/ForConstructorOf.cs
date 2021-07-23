using System;
using System.Reflection;
using Fluent.ConstructorAssertions.Contexts;
using FluentAssertions;
using JetBrains.Annotations;

namespace Fluent.ConstructorAssertions
{
    /// <summary>
    /// Declares a new constructor test statement.
    /// </summary>
    /// <typeparam name="TClass">The class with the constructor to test.</typeparam>
    [PublicAPI]
    public sealed class ForConstructorOf<TClass> where TClass : class
    {
        /// <summary>
        /// Defines the argument types expected by the constructor of TClass.
        /// </summary>
        /// <param name="argTypes">The argument types.</param>
        /// <returns>A new <see cref="TestContext{TClass}"/> for the constructor of TClass.</returns>
        public static TestContext<TClass> WithArgTypes(params Type[] argTypes)
        {
            ConstructorInfo? constructor = typeof(TClass).GetConstructor(argTypes);

            constructor.Should()
                       .NotBeNull("A constructor must exist for the provided class type.");

            return new TestContext<TClass>(constructor!, argTypes.Length);
        }
    }
}
