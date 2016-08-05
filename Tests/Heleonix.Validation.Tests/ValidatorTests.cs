/*
The MIT License (MIT)

Copyright (c) 2015 Heleonix.Validation - Hennadii Lutsyshyn (Heleonix)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using Heleonix.Validation.Targets;
using Heleonix.Validation.Tests.Common;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Heleonix.Validation.Tests
{
    /// <summary>
    /// Tests the <see cref="Validator{TObject}"/>
    /// </summary>
    public class ValidatorTests
    {
        #region Tests

        /// <summary>
        /// Tests the <see cref="Validator{TObject}"/>.
        /// </summary>
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Mock<Validator<ObjectOne>> {CallBase = true}.Object.Targets, Is.Not.Null.And.Empty);
        }

        /// <summary>
        /// Tests the <see cref="Validator{TObject}.CreateResult"/>.
        /// </summary>
        /// <param name="passContext">Determines whether to pass a <see cref="ValidatorContext"/>.</param>
        [Test]
        public void CreateResult([Values(true, false)] bool passContext)
        {
            var mock = new Mock<Validator<ObjectOne>> {CallBase = true};

            if (passContext)
            {
                Assert.That(() => mock.Invoke("CreateResult", new ValidatorContext(
                    new ObjectOne(), null, new DefaultValidatorProvider(false))), Is.Not.Null);
            }
            else
            {
                Assert.That(Assert.Catch<ArgumentNullException>(()
                    => mock.Invoke("CreateResult", new object[] {null})).ParamName, Is.EqualTo("context"));
            }
        }

        /// <summary>
        /// Tests the <see cref="Validator{TObject}.Setup"/>.
        /// </summary>
        [Test]
        public void Setup()
        {
            var mock = new Mock<Validator<ObjectOne>> {CallBase = true};

            mock.Protected().Setup("Setup", ItExpr.IsAny<IInitialTargetBuilder<ObjectOne>>());

            mock.Object.Setup();

            mock.Protected().Verify("Setup", Times.Once(), ItExpr.IsAny<IInitialTargetBuilder<ObjectOne>>());
        }

        /// <summary>
        /// Tests the <see cref="Validator{TObject}.Validate"/>.
        /// </summary>
        /// <param name="passContext">Determines whether to pass a <see cref="ValidatorContext"/>.</param>
        /// <param name="createResult">Determines whether to create a <see cref="ValidatorResult"/>.</param>
        /// <param name="continueValidation">Determines whether to continue validation.</param>
        /// <param name="ignoreEmptyResults">Determines whether to ignore empty results.</param>
        [Test, Combinatorial]
        public void Validate([Values(true, false)] bool passContext, [Values(true, false)] bool createResult,
            [Values(true, false)] bool continueValidation, [Values(true, false)] bool ignoreEmptyResults)
        {
            var mock = new Mock<Validator<ObjectOne>> {CallBase = true};
            var targetWithEmptyResultsMock = new Mock<ObjectTarget>("name");
            var context = new ValidatorContext(new ObjectOne {Int = 10, String = "string"}, null,
                new DefaultValidatorProvider(false), null, continueValidation, ignoreEmptyResults);
            var error = new ValueResult(false, "ErrorName", "ErrorKey");
            var success = new ValueResult(true, "SuccessName", "SuccessKey");

            targetWithEmptyResultsMock.Protected().Setup<TargetResult>("CreateResult",
                ItExpr.IsAny<TargetContext>()).Returns<TargetContext>(ctxt => null);

            mock.Protected().Setup("Setup", ItExpr.IsAny<IInitialTargetBuilder<ObjectOne>>())
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
                mock.Protected().Setup<ValidatorResult>("CreateResult", context).Returns(() => null);
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
                        Assert.That(result.TargetResults,
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
                Assert.That(Assert.Catch<ArgumentNullException>(()
                    => mock.Object.Validate(null)).ParamName, Is.EqualTo("context"));
            }
        }

        #endregion
    }
}