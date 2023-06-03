// <copyright file="LoginFormTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests.Examples
{
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="LoginForm"/>.
    /// </summary>
    public class LoginFormTests
    {
        /// <summary>
        /// Tests the <see cref="LoginForm"/>.
        /// </summary>
        [Test]
        public void Validate()
        {
            var form = new LoginForm
            {
                Username = "\" or \"\"=\"",
                Password = "$ecureP@ssw0rd",
            };

            var controller = new ValidationController(new DefaultValidatorProvider(true));

            var results = controller.Validate(form);

            var error = results.TargetResults.Single().RuleResults.Single().ValueResults.Single();

            Assert.That(error.ResourceName, Is.EqualTo("Errors"));
            Assert.That(error.ResourceKey, Is.EqualTo("Username.Unsafe"));
        }
    }
}