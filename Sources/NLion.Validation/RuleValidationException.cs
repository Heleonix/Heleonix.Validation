/*
The MIT License (MIT)

Copyright (c) 2015 NLion.Validation - Hennadii Lutsyshyn (NLion)

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

namespace NLion.Validation
{
    /// <summary>
    /// An exception for failed rule validation.
    /// </summary>
    [Serializable]
    public class RuleValidationException : ValidationException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleValidationException"/> class.
        /// </summary>
        /// <param name="message">A message.</param>
        public RuleValidationException(string message = "") : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleValidationException"/> class.
        /// </summary>
        /// <param name="inner">An inner exception.</param>
        public RuleValidationException(Exception inner = null) : this(string.Empty, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleValidationException"/> class.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <param name="inner">An inner exception.</param>
        public RuleValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        #endregion
    }
}