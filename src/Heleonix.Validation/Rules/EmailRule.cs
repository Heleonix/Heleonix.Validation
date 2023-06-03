// <copyright file="EmailRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using System.ComponentModel.DataAnnotations;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the email rule.
    /// </summary>
    public class EmailRule : BooleanRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false" />.
        /// </param>
        public EmailRule(bool continueValidationWhenFalse)
            : base(continueValidationWhenFalse)
        {
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

            return new EmailAddressAttribute().IsValid(
                context.TargetContext.Target.GetValue(context.TargetContext)?.ToString());
        }
    }
}