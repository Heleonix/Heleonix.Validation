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

namespace NLion.Validation.Rules
{
    /// <summary>
    /// Represents the length rule.
    /// </summary>
    public class LengthRule : BooleanRule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LengthRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">Determines whether to continue validation
        /// when rule value is <see langword="false" />.
        /// </param>
        /// <param name="min">Minimum allowed length.</param>
        /// <param name="max">Maximum allowed length.</param>
        public LengthRule(bool continueValidationWhenFalse, int? min, int? max) : base(continueValidationWhenFalse)
        {
            Min = min;
            Max = max;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets minimum allowed length.
        /// </summary>
        public int? Min { get; set; }

        /// <summary>
        /// Gets or sets maximum allowed length.
        /// </summary>
        public int? Max { get; set; }

        #endregion

        #region Rule Members

        /// <summary>
        /// Creates a length rule result.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <param name="value">A rule value.</param>
        /// <returns>A length rule result.</returns>
        public override RuleResult CreateResult(RuleValidationContext context, object value)
            => new LengthRuleResult(Name, value, Min, Max);

        /// <summary>
        /// Executes validation.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <returns>A rule value.</returns>
        protected override object Execute(RuleValidationContext context)
        {
            var length = context.Target.GetValue(context.ValidationContext)?.ToString().Length ?? 0;

            return (!Min.HasValue || length >= Min) && (!Max.HasValue || length <= Max);
        }

        #endregion
    }
}