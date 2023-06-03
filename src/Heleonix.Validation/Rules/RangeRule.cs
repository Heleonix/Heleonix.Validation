// <copyright file="RangeRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the range rule.
    /// </summary>
    public class RangeRule : BooleanRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false" />.
        /// </param>
        /// <param name="min">Minimum allowed value.</param>
        /// <param name="max">Maximum allowed value.</param>
        public RangeRule(bool continueValidationWhenFalse, object min, object max)
            : base(continueValidationWhenFalse)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets minimum allowed value.
        /// </summary>
        public virtual object Min { get; set; }

        /// <summary>
        /// Gets or sets maximum allowed value.
        /// </summary>
        public virtual object Max { get; set; }

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

            return new RangeRuleResult(this.Name, value, this.Min, this.Max);
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

            return (this.Min == null || comparable.CompareTo(this.Min) >= 0) && (this.Max == null || comparable.CompareTo(this.Max) <= 0);
        }
    }
}