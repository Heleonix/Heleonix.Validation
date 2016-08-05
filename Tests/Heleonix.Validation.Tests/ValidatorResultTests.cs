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
    /// Tests the <see cref="ValidatorResult"/>.
    /// </summary>
    public class ValidatorResultTests
    {
        #region Tests

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

        #endregion
    }
}