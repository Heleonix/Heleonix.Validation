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
    /// Represents the base class for comparison rules.
    /// </summary>
    public abstract class ComparisonRule : BooleanRule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">Determines whether to continue validation
        /// when rule value is <see langword="false"/>.
        /// </param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="comparer">A comparer to compare a target.</param>
        protected ComparisonRule(bool continueValidationWhenFalse,
            Func<RuleValidationContext, object> otherValueProvider, Func<IComparable, object, bool> comparer)
            : base(continueValidationWhenFalse)
        {
            OtherValueProvider = otherValueProvider;
            Comparer = comparer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets other value provider.
        /// </summary>
        public Func<RuleValidationContext, object> OtherValueProvider { get; set; }

        /// <summary>
        /// Gets or sets a comparer to compare a target.
        /// </summary>
        public Func<IComparable, object, bool> Comparer { get; set; }

        #endregion

        #region Rule Members

        /// <summary>
        /// Creates a rule result.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <param name="value">A rule value.</param>
        /// <returns>A rule result.</returns>
        public override RuleResult CreateResult(RuleValidationContext context, object value)
            => new ComparisonRuleResult(Name, value, OtherValueProvider?.Invoke(context));

        /// <summary>
        /// Executes validation.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <returns>A rule value.</returns>
        protected override object Execute(RuleValidationContext context)
        {
            if (Comparer == null)
            {
                return true;
            }

            var value = context.Target.GetValue(context.ValidationContext);

            if (value == null)
            {
                return true;
            }

            if (OtherValueProvider == null)
            {
                return false;
            }

            var comparable = value as IComparable;

            return comparable != null && Comparer(comparable, OtherValueProvider(context));
        }

        #endregion
    }
}