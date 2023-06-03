// <copyright file="LengthRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the length rule.
    /// </summary>
    public class LengthRule : BooleanRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LengthRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false" />.
        /// </param>
        /// <param name="min">Minimum allowed length.</param>
        /// <param name="max">Maximum allowed length.</param>
        public LengthRule(bool continueValidationWhenFalse, int? min, int? max)
            : base(continueValidationWhenFalse)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets minimum allowed length.
        /// </summary>
        public virtual int? Min { get; set; }

        /// <summary>
        /// Gets or sets maximum allowed length.
        /// </summary>
        public virtual int? Max { get; set; }

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

            return new LengthRuleResult(this.Name, value, this.Min, this.Max);
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

            var length = context.TargetContext.Target.GetValue(context.TargetContext)?.ToString().Length ?? 0;

            return (!this.Min.HasValue || length >= this.Min) && (!this.Max.HasValue || length <= this.Max);
        }
    }
}