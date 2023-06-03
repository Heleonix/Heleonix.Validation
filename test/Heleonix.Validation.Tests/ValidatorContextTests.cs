// <copyright file="ValidatorContextTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests
{
    using Heleonix.Validation.Tests.Common;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="ValidatorContext"/>.
    /// </summary>
    public class ValidatorContextTests
    {
        /// <summary>
        /// Gets the object source.
        /// </summary>
        public static IEnumerable<object> ObjectSource { get; } = new List<object> { null, new object() };

        /// <summary>
        /// Gets the validator source.
        /// </summary>
        public static IEnumerable<object> ValidatorSource { get; } = new List<IValidator>
        {
            null,
            new Mock<Validator<ObjectOne>>().Object,
        };

        /// <summary>
        /// Gets the validator provider source.
        /// </summary>
        public static IEnumerable<object> ValidatorProviderSource { get; } = new List<IValidatorProvider>
        {
            null,
            new DefaultValidatorProvider(false),
        };

        /// <summary>
        /// Gets the parent source.
        /// </summary>
        public static IEnumerable<ValidatorContext> ParentSource { get; } = new List<ValidatorContext>
        {
            null,
            new ValidatorContext(new object(), null, new DefaultValidatorProvider(false)),
        };

        /// <summary>
        /// Tests the <see cref="ValidatorContext"/>.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="validator">The <see cref="IValidator"/>.</param>
        /// <param name="validatorProvider">The <see cref="IValidatorProvider"/>.</param>
        /// <param name="parent">The parent context.</param>
        [Test]
        [Combinatorial]
        public void Constructor(
            [ValueSource(nameof(ObjectSource))] object obj,
            [ValueSource(nameof(ValidatorSource))] IValidator validator,
            [ValueSource(nameof(ValidatorProviderSource))] IValidatorProvider validatorProvider,
            [ValueSource(nameof(ParentSource))] ValidatorContext parent)
        {
            AssertConstructor(obj, validator, validatorProvider, parent, true, true);
        }

        /// <summary>
        /// Tests the <see cref="ValidatorContext"/>.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="validator">The <see cref="IValidator"/>.</param>
        /// <param name="validatorProvider">The <see cref="IValidatorProvider"/>.</param>
        /// <param name="parent">The parent context.</param>
        /// <param name="continueValidation">Determines whether to continue validation.</param>
        /// <param name="ignoreEmptyResults">Determines whether to ignore empty results.</param>
        [Test]
        [Combinatorial]
        public void Constructor(
            [ValueSource(nameof(ObjectSource))] object obj,
            [ValueSource(nameof(ValidatorSource))] IValidator validator,
            [ValueSource(nameof(ValidatorProviderSource))] IValidatorProvider validatorProvider,
            [ValueSource(nameof(ParentSource))] ValidatorContext parent,
            [Values(true, false)] bool continueValidation,
            [Values(true, false)] bool ignoreEmptyResults)
        {
            AssertConstructor(obj, validator, validatorProvider, parent, continueValidation, ignoreEmptyResults);
        }

        /// <summary>
        /// Tests the <see cref="ValidatorContext.Validator"/>.
        /// </summary>
        /// <param name="validator">The <see cref="IValidator"/>.</param>
        [Test]
        public void Validator([ValueSource(nameof(ValidatorSource))] IValidator validator)
        {
            var oldValidator = new Mock<Validator<ObjectOne>>().Object;

            var instance = new ValidatorContext(new object(), oldValidator, new DefaultValidatorProvider(false));

            if (validator == null)
            {
                var exception = Assert.Catch<ArgumentNullException>(() => instance.Validator = null);

                Assert.That(instance.Validator, Is.EqualTo(oldValidator));
                Assert.That(exception.ParamName, Is.EqualTo("value"));
                Assert.That(exception.Message, Is.Not.Null.And.Not.Empty);
            }
            else
            {
                instance.Validator = validator;
                Assert.That(instance.Validator, Is.EqualTo(validator));
            }
        }

        /// <summary>
        /// Tests the <see cref="ValidatorContext.ContinueValidation"/>.
        /// </summary>
        /// <param name="continueValidation">Determines whether to continue validation.</param>
        [Test]
        public void ContinueValidation([Values(true, false)] bool continueValidation)
        {
            var instance = new ValidatorContext(new ObjectOne(), null, new DefaultValidatorProvider(false))
            {
                ContinueValidation = continueValidation,
            };

            Assert.That(instance.ContinueValidation, Is.EqualTo(continueValidation));
        }

        /// <summary>
        /// Tests the <see cref="ValidatorContext.IgnoreEmptyResults"/>.
        /// </summary>
        /// <param name="ignoreEmptyResults">Determines whether to ignore empty results.</param>
        [Test]
        public void IgnoreEmptyResults([Values(true, false)] bool ignoreEmptyResults)
        {
            var instance = new ValidatorContext(new ObjectOne(), null, new DefaultValidatorProvider(false))
            {
                IgnoreEmptyResults = ignoreEmptyResults,
            };

            Assert.That(instance.IgnoreEmptyResults, Is.EqualTo(ignoreEmptyResults));
        }

        /// <summary>
        /// Tests the <see cref="ValidatorContext.Parent"/>.
        /// </summary>
        /// <param name="setParent">Determines whether to set a parent <see cref="ValidatorContext"/>.</param>
        [Test]
        public void Parent([Values(true, false)] bool setParent)
        {
            var instance = new ValidatorContext(new ObjectOne(), null, new DefaultValidatorProvider(false));

            if (setParent)
            {
                var parent = new ValidatorContext(new ObjectOne(), null, new DefaultValidatorProvider(false));

                instance.Parent = parent;

                Assert.That(instance.Parent, Is.EqualTo(parent));
            }
            else
            {
                Assert.That(instance.Parent, Is.Null);
            }
        }

        /// <summary>
        /// Asserts the constructor.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="validator">The <see cref="IValidator"/>.</param>
        /// <param name="validatorProvider">The <see cref="IValidatorProvider"/>.</param>
        /// <param name="parent">The parent context.</param>
        /// <param name="continueValidation">Determines whether to continue validation.</param>
        /// <param name="ignoreEmptyResults">Determines whether to ignore empty results.</param>
        private static void AssertConstructor(
            object obj,
            IValidator validator,
            IValidatorProvider validatorProvider,
            ValidatorContext parent,
            bool continueValidation,
            bool ignoreEmptyResults)
        {
            if (obj == null)
            {
                var exception = Assert.Catch<ArgumentNullException>(
                    () => new ValidatorContext(null, validator, validatorProvider, parent));

                Assert.That(exception.ParamName, Is.EqualTo(nameof(obj)));
                Assert.That(exception.Message, Is.Not.Null.And.Not.Empty);
            }
            else if (validatorProvider == null)
            {
                var exception = Assert.Catch<ArgumentNullException>(
                    () => new ValidatorContext(null, validator, null, parent));

                Assert.That(exception.ParamName, Is.EqualTo(nameof(obj)));
                Assert.That(exception.Message, Is.Not.Null.And.Not.Empty);
            }
            else
            {
                var instance = new ValidatorContext(
                    obj,
                    validator,
                    validatorProvider,
                    parent,
                    continueValidation,
                    ignoreEmptyResults);

                Assert.That(instance.Object, Is.EqualTo(obj));
                Assert.That(instance.Validator, Is.EqualTo(validator));
                Assert.That(instance.ValidatorProvider, Is.EqualTo(validatorProvider));
                Assert.That(instance.Parent, Is.EqualTo(parent));
                Assert.That(instance.ContinueValidation, Is.EqualTo(continueValidation));
                Assert.That(instance.IgnoreEmptyResults, Is.EqualTo(ignoreEmptyResults));
            }
        }
    }
}