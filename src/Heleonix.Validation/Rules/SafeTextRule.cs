// <copyright file="SafeTextRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the required rule.
    /// </summary>
    public class SafeTextRule : BooleanRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeTextRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false" />.
        /// </param>
        public SafeTextRule(bool continueValidationWhenFalse)
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

            var value = context.TargetContext.Target.GetValue(context.TargetContext) as string;

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsLetterOrDigit(value[i]) && !char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}