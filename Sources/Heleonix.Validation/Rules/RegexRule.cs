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
using System.Text.RegularExpressions;
using Heleonix.Validation.Internal;

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the regular expression rule.
    /// </summary>
    public class RegexRule : BooleanRule
    {
        #region Fields

        /// <summary>
        /// Gets or sets a regular expression to test match.
        /// </summary>
        private string _regex;

        /// <summary>
        /// Gets or sets regular expression options.
        /// </summary>
        private RegexOptions _regexOptions;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false" />.
        /// </param>
        /// <param name="regex">A regular expression to test match.</param>
        /// <param name="regexOptions">Regular expression options.</param>
        public RegexRule(bool continueValidationWhenFalse, string regex, RegexOptions regexOptions)
            : base(continueValidationWhenFalse)
        {
            _regex = regex;
            _regexOptions = regexOptions;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a regular expression to test match.
        /// </summary>
        public virtual string Regex
        {
            get { return _regex; }
            set { _regex = value; }
        }

        /// <summary>
        /// Gets or sets regular expression options.
        /// </summary>
        public virtual RegexOptions RegexOptions
        {
            get { return _regexOptions; }
            set { _regexOptions = value; }
        }

        #endregion

        #region Rule Members

        /// <summary>
        /// Creates a length rule result.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A length rule result.</returns>
        protected override RuleResult CreateResult(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new RegexRuleResult(Name, value, Regex, RegexOptions);
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

            var value = context.TargetContext.Target.GetValue(context.TargetContext)?.ToString();

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