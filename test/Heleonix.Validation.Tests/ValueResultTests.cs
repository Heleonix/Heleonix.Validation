// <copyright file="ValueResultTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="ValueResult"/>.
    /// </summary>
    public class ValueResultTests
    {
        /// <summary>
        /// Tests the <see cref="ValueResult.IsEmpty"/>.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="resourceKey">The resource key.</param>
        [Test]
        [Combinatorial]
        public void IsEmpty(
            [Values(null, "", "name")] string resourceName,
            [Values(null, "", "key")] string resourceKey)
        {
            Assert.That(
                new ValueResult(false, resourceName, resourceKey).IsEmpty(),
                Is.EqualTo(string.IsNullOrEmpty(resourceName) && string.IsNullOrEmpty(resourceKey)));
        }

        /// <summary>
        /// Tests the <see cref="ValueResult"/>.
        /// </summary>
        /// <param name="matchValue">The match value.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="resourceKey">The resource key.</param>
        [Test]
        [Combinatorial]
        public void Constructor(
            [Values(null, true, false)] object matchValue,
            [Values(null, "name")] string resourceName,
            [Values(null, "key")] string resourceKey)
        {
            var instance = new ValueResult(matchValue, resourceName, resourceKey);

            Assert.That(instance.MatchValue, Is.EqualTo(matchValue));
            Assert.That(instance.ResourceName, Is.EqualTo(resourceName));
            Assert.That(instance.ResourceKey, Is.EqualTo(resourceKey));
        }
    }
}