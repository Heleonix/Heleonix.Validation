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

using NUnit.Framework;

namespace Heleonix.Validation.Tests
{
    /// <summary>
    /// Tests the <see cref="ValueResult"/>.
    /// </summary>
    public class ValueResultTests
    {
        #region Tests

        /// <summary>
        /// Tests the <see cref="ValueResult.IsEmpty"/>.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="resourceKey">The resource key.</param>
        [Test, Combinatorial]
        public void IsEmpty([Values(null, "", "name")] string resourceName,
            [Values(null, "", "key")] string resourceKey)
        {
            Assert.That(new ValueResult(false, resourceName, resourceKey).IsEmpty(),
                Is.EqualTo(string.IsNullOrEmpty(resourceName) && string.IsNullOrEmpty(resourceKey)));
        }

        /// <summary>
        /// Tests the <see cref="ValueResult"/>
        /// </summary>
        /// <param name="matchValue">The match value.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="resourceKey">The resource key.</param>
        [Test, Combinatorial]
        public void Constructor([Values(null, true, false)] object matchValue,
            [Values(null, "name")] string resourceName, [Values(null, "key")] string resourceKey)
        {
            var instance = new ValueResult(matchValue, resourceName, resourceKey);

            Assert.That(instance.MatchValue, Is.EqualTo(matchValue));
            Assert.That(instance.ResourceName, Is.EqualTo(resourceName));
            Assert.That(instance.ResourceKey, Is.EqualTo(resourceKey));
        }

        #endregion
    }
}