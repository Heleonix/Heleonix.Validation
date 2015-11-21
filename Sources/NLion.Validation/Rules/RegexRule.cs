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

using System.Text.RegularExpressions;

namespace NLion.Validation.Rules
{
    /// <summary>
    /// Represents the regular expression rule.
    /// </summary>
    public class RegexRule : BooleanRule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">Determines whether to continue validation
        /// when rule value is <see langword="false" />.
        /// </param>
        /// <param name="regex">A regular expression to test match.</param>
        /// <param name="regexOptions">Regular expression options.</param>
        public RegexRule(bool continueValidationWhenFalse, string regex, RegexOptions regexOptions)
            : base(continueValidationWhenFalse)
        {
            Regex = regex;
            RegexOptions = regexOptions;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a regular expression to test match.
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// Gets or sets regular expression options.
        /// </summary>
        public RegexOptions RegexOptions { get; set; }

        #endregion

        #region Rule Members

        /// <summary>
        /// Creates a length rule result.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <param name="value">A rule value.</param>
        /// <returns>A length rule result.</returns>
        public override RuleResult CreateResult(RuleValidationContext context, object value)
            => new RegexRuleResult(Name, value, Regex, RegexOptions);

        /// <summary>
        /// Executes validation.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <returns>A rule value.</returns>
        protected override object Execute(RuleValidationContext context)
        {
            var value = context.Target.GetValue(context.ValidationContext)?.ToString();

            if (value == null || string.IsNullOrEmpty(Regex))
            {
                return true;
            }

            var match = new Regex(Regex).Match(value);

            return match.Success && match.Index == 0 && match.Length == value.Length;
        }

        #endregion
    }
}