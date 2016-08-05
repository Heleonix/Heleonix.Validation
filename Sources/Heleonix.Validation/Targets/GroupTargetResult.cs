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

using System.Collections.Generic;

namespace Heleonix.Validation.Targets
{
    /// <summary>
    /// Represents the group target result.
    /// </summary>
    public class GroupTargetResult : TargetResult
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupTargetResult"/> class.
        /// </summary>
        /// <param name="name">A name of a group.</param>
        public GroupTargetResult(string name) : base(name, null)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets targets results.
        /// </summary>
        public ICollection<TargetResult> TargetResults { get; } = new List<TargetResult>();

        #endregion

        #region TargetResult Members

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => TargetResults.Count == 0;

        #endregion
    }
}