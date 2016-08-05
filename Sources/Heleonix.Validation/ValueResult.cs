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

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents a result for a specific rule validation value.
    /// </summary>
    [Serializable]
    public class ValueResult : Result
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueResult"/> class.
        /// </summary>
        /// <param name="matchValue">A value to select the result when it equals to a rule validation result.</param>
        /// <param name="resourceName">A resource name.</param>
        /// <param name="resourceKey">A resource key.</param>
        public ValueResult(object matchValue, string resourceName, string resourceKey)
        {
            MatchValue = matchValue;
            ResourceName = resourceName;
            ResourceKey = resourceKey;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value to select the result when it equals to a rule validation result.
        /// </summary>
        public object MatchValue { get; set; }

        /// <summary>
        /// Gets or sets a resource name.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets a resource key.
        /// </summary>
        public string ResourceKey { get; set; }

        #endregion

        #region Result Members

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => string.IsNullOrEmpty(ResourceName) && string.IsNullOrEmpty(ResourceKey);

        #endregion
    }
}