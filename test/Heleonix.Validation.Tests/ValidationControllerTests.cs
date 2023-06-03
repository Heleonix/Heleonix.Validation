// <copyright file="ValidationControllerTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests
{
    using Heleonix.Validation.Tests.Common;

    /// <summary>
    /// Tests the <see cref="ValidationController"/>.
    /// </summary>
    public class ValidationControllerTests
    {
        /// <summary>
        /// Tests the <see cref="ValidationController"/>.
        /// </summary>
        /// <param name="passValidatorProvider">True if the validator provider should be passed, otherwise false.</param>
        [Test]
        public void Constructor([Values(true, false)] bool passValidatorProvider)
        {
            if (passValidatorProvider)
            {
                var provider = new DefaultValidatorProvider(false);
                var mock = new Mock<ValidationController>(provider) { CallBase = true };

                Assert.That(() => mock.InvokeGetter("ValidatorProvider"), Is.EqualTo(provider));
            }
            else
            {
                Assert.That(
                    Assert.Catch<ArgumentNullException>(() => new ValidationController(null)).ParamName,
                    Is.EqualTo("validatorProvider"));
            }
        }
    }
}