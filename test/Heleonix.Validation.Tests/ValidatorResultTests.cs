// <copyright file="ValidatorResultTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="ValidatorResult"/>.
    /// </summary>
    public class ValidatorResultTests
    {
        /// <summary>
        /// Tests the <see cref="ValidatorResult.TargetResults"/>.
        /// </summary>
        [Test]
        public void TargetResults()
        {
            var instance = new ValidatorResult();

            Assert.That(instance.TargetResults, Is.Not.Null);
            Assert.That(instance.TargetResults, Is.Empty);
        }

        /// <summary>
        /// Tests the <see cref="ValidatorResult.IsEmpty"/>.
        /// </summary>
        /// <param name="isEmpty">Determines whether the result is empty.</param>
        [Test]
        public void IsEmpty([Values(true, false)] bool isEmpty)
        {
            var instance = new ValidatorResult();

            if (!isEmpty)
            {
                instance.TargetResults.Add(new TargetResult(string.Empty, null));
            }

            Assert.That(instance.IsEmpty(), Is.EqualTo(isEmpty));
        }
    }
}