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
using System.Collections.Generic;

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the target result.
    /// </summary>
    [Serializable]
    public class TargetResult : Result
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetResult"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        /// <param name="value">A value of a target.</param>
        public TargetResult(string name, object value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a name of a target.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value of a target.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets rule results.
        /// </summary>
        public ICollection<RuleResult> RuleResults { get; } = new List<RuleResult>();

        #endregion

        #region Result Members

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => RuleResults.Count == 0;

        #endregion
    }
}