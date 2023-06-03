// <copyright file="BooleanRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the base class for all boolean rules.
    /// </summary>
    public abstract class BooleanRule : Rule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        protected BooleanRule(bool continueValidationWhenFalse)
        {
            this.ContinueValidationWhenFalse = continueValidationWhenFalse;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to continue validation
        /// when a value of a rule is <see langword="false"/>.
        /// </summary>
        public virtual bool ContinueValidationWhenFalse { get; set; }

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
            var result = base.Validate(context);

            if (result == null)
            {
                return null;
            }

            context.TargetContext.ValidatorContext.ContinueValidation
                = !(result.Value is bool) || (bool)result.Value
                  || (!(bool)result.Value && this.ContinueValidationWhenFalse);

            return result;
        }
    }
}