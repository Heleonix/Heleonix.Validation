// <copyright file="IfRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the conditional rule.
    /// </summary>
    public class IfRule : ConditionalRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IfRule"/> class.
        /// </summary>
        /// <param name="rule">A rule to wrap with the <paramref name="condition"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="rule"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        public IfRule(Rule rule, Predicate<RuleContext> condition)
            : base(rule, condition)
        {
        }

        /// <summary>
        /// Performs validation.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        public override RuleResult Validate(RuleContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return this.Condition(context) ? this.Rule.Validate(context) : null;
        }
    }
}