// <copyright file="RegexRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using System.Text.RegularExpressions;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the regular expression rule.
    /// </summary>
    public class RegexRule : BooleanRule
    {
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
            this.Regex = regex;
            this.RegexOptions = regexOptions;
        }

        /// <summary>
        /// Gets or sets a regular expression to test match.
        /// </summary>
        public virtual string Regex { get; set; }

        /// <summary>
        /// Gets or sets regular expression options.
        /// </summary>
        public virtual RegexOptions RegexOptions { get; set; }

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

            return new RegexRuleResult(this.Name, value, this.Regex, this.RegexOptions);
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

            if (value == null || string.IsNullOrEmpty(this.Regex))
            {
                return true;
            }

            var match = new Regex(this.Regex).Match(value);

            return match.Success && match.Index == 0 && match.Length == value.Length;
        }
    }
}