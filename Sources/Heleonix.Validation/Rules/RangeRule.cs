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
using Heleonix.Validation.Internal;

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the range rule.
    /// </summary>
    public class RangeRule : BooleanRule
    {
        #region Fields

        /// <summary>
        /// Gets or sets minimum allowed value.
        /// </summary>
        private object _min;

        /// <summary>
        /// Gets or sets maximum allowed value.
        /// </summary>
        private object _max;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false" />.
        /// </param>
        /// <param name="min">Minimum allowed value.</param>
        /// <param name="max">Maximum allowed value.</param>
        public RangeRule(bool continueValidationWhenFalse, object min, object max) : base(continueValidationWhenFalse)
        {
            _min = min;
            _max = max;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets minimum allowed value.
        /// </summary>
        public virtual object Min
        {
            get { return _min; }
            set { _min = value; }
        }

        /// <summary>
        /// Gets or sets maximum allowed value.
        /// </summary>
        public virtual object Max
        {
            get { return _max; }
            set { _max = value; }
        }

        #endregion

        #region Rule Members

        /// <summary>
        /// Creates a range rule result.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A range rule result.</returns>
        protected override RuleResult CreateResult(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new RangeRuleResult(Name, value, Min, Max);
        }

        /// <summary>
        /// Executes validation.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A value of a rule.</returns>
        protected override object Execute(RuleContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            var value = context.TargetContext.Target.GetValue(context.TargetContext);

            if (value == null)
            {
                return true;
            }

            var comparable = value as IComparable;

            if (comparable == null)
            {
                return false;
            }

            return (Min == null || comparable.CompareTo(Min) >= 0) && (Max == null || comparable.CompareTo(Max) <= 0);
        }

        #endregion
    }
}