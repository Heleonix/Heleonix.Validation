// <copyright file="ValidatorTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests
{
    using Heleonix.Validation.Targets;
    using Heleonix.Validation.Tests.Common;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="Validator{TObject}"/>.
    /// </summary>
    public class ValidatorTests
    {
        /// <summary>
        /// Tests the <see cref="Validator{TObject}"/>.
        /// </summary>
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Mock<Validator<ObjectOne>> { CallBase = true }.Object.Targets, Is.Not.Null.And.Empty);
        }

        /// <summary>
        /// Tests the <see cref="Validator{TObject}.CreateResult"/>.
        /// </summary>
        /// <param name="passContext">Determines whether to pass a <see cref="ValidatorContext"/>.</param>
        [Test]
        public void CreateResult([Values(true, false)] bool passContext)
        {
            var mock = new Mock<Validator<ObjectOne>> { CallBase = true };

            if (passContext)
            {
                Assert.That(
                    () => mock.Invoke(
                        nameof(this.CreateResult),
                        new ValidatorContext(new ObjectOne(), null, new DefaultValidatorProvider(false))), Is.Not.Null);
            }
            else
            {
                Assert.That(
                    Assert.Catch<ArgumentNullException>(
                        () => mock.Invoke(nameof(this.CreateResult), new object[] { null })).ParamName, Is.EqualTo("context"));
            }
        }

        /// <summary>
        /// Tests the <see cref="Validator{TObject}.Setup()"/>.
        /// </summary>
        [Test]
        public void Setup()
        {
            var mock = new Mock<Validator<ObjectOne>> { CallBase = true };

            mock.Protected().Setup(nameof(this.Setup), ItExpr.IsAny<IInitialTargetBuilder<ObjectOne>>());

            mock.Object.Setup();

            mock.Protected().Verify(nameof(this.Setup), Times.Once(), ItExpr.IsAny<IInitialTargetBuilder<ObjectOne>>());
        }

        /// <summary>
        /// Tests the <see cref="Validator{TObject}.Validate"/>.
        /// </summary>
        /// <param name="passContext">Determines whether to pass a <see cref="ValidatorContext"/>.</param>
        /// <param name="createResult">Determines whether to create a <see cref="ValidatorResult"/>.</param>
        /// <param name="continueValidation">Determines whether to continue validation.</param>
        /// <param name="ignoreEmptyResults">Determines whether to ignore empty results.</param>
        [Test]
        [Combinatorial]
        public void Validate(
            [Values(true, false)] bool passContext,
            [Values(true, false)] bool createResult,
            [Values(true, false)] bool continueValidation,
            [Values(true, false)] bool ignoreEmptyResults)
        {
            var mock = new Mock<Validator<ObjectOne>> { CallBase = true };
            var targetWithEmptyResultsMock = new Mock<ObjectTarget>("name");
            var context = new ValidatorContext(
                new ObjectOne { Int = 10, String = "string" },
                null,
                new DefaultValidatorProvider(false),
                null,
                continueValidation,
                ignoreEmptyResults);
            var error = new ValueResult(false, "ErrorName", "ErrorKey");
            var success = new ValueResult(true, "SuccessName", "SuccessKey");

            targetWithEmptyResultsMock.Protected().Setup<TargetResult>(
                nameof(this.CreateResult),
                ItExpr.IsAny<TargetContext>()).Returns<TargetContext>(ctxt => null);

            mock.Protected().Setup(nameof(this.Setup), ItExpr.IsAny<IInitialTargetBuilder<ObjectOne>>())
                .Callback<IInitialTargetBuilder<ObjectOne>>(builder =>
                {
                    builder.Member(o => o.Int).IsEqualTo(10).WithResult(success);
                    builder.Member(o => o.String).HasLength(4, 4, continueValidation).WithResult(error);
                    builder.Member(o => o.Int).HasRange(5, 15);
                });
            mock.Object.Targets.Add(null);
            mock.Object.Targets.Add(targetWithEmptyResultsMock.Object);

            if (!createResult)
            {
                mock.Protected().Setup<ValidatorResult>(nameof(this.CreateResult), context).Returns(() => null);
            }

            mock.Object.Setup();

            if (passContext)
            {
                var result = mock.Object.Validate(context);

                Assert.That(context.Validator, Is.EqualTo(mock.Object));

                if (createResult)
                {
                    Assert.That(result, Is.Not.Null);

                    if (continueValidation)
                    {
                        Assert.That(
                            result.TargetResults,
                            ignoreEmptyResults ? Has.Count.EqualTo(2) : Has.Count.EqualTo(3));
                    }
                    else
                    {
                        Assert.That(result.TargetResults, Is.Empty);
                    }
                }
                else
                {
                    Assert.That(result, Is.Null);
                }
            }
            else
            {
                Assert.That(
                    Assert.Catch<ArgumentNullException>(
                        () => mock.Object.Validate(null)).ParamName, Is.EqualTo(nameof(context)));
            }
        }
    }
}