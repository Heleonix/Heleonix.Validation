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

namespace NLion.Validation.Rules
{
    /// <summary>
    /// Represents the less than or equal to other value rules.
    /// </summary>
    public class LessThanOrEqualRule : ComparisonRule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanOrEqualRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <param name="otherValueProvider">Other value provider.</param>
        public LessThanOrEqualRule(bool continueValidationWhenFalse,
            Func<RuleValidationContext, object> otherValueProvider)
            : base(continueValidationWhenFalse, otherValueProvider, (value, other) => value.CompareTo(other) <= 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanOrEqualRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <param name="otherValue">Other value to compare with.</param>
        public LessThanOrEqualRule(bool continueValidationWhenFalse, object otherValue)
            : base(continueValidationWhenFalse, context => otherValue, (value, other) => value.CompareTo(other) <= 0)
        {
        }

        #endregion
    }
}