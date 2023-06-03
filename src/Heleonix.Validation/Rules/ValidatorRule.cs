// <copyright file="ValidatorRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the validator rule.
    /// </summary>
    public class ValidatorRule : Rule
    {
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

            context.Rule = this;

            var result = this.CreateResult(context, null) as ValidatorRuleResult;

            if (result == null)
            {
                return null;
            }

            var targetValue = context.TargetContext.Target.GetValue(context.TargetContext);

            var targetType = targetValue?.GetType();

            var validator = targetType != null
                ? context.TargetContext.ValidatorContext
                    .ValidatorProvider.GetValidator(targetType)
                : null;

            result.ValidatorResult = validator?.Validate(new ValidatorContext(
                targetValue,
                null,
                context.TargetContext.ValidatorContext.ValidatorProvider,
                context.TargetContext.ValidatorContext,
                context.TargetContext.ValidatorContext.ContinueValidation,
                context.TargetContext.ValidatorContext.IgnoreEmptyResults));

            return result;
        }

        /// <summary>
        /// Creates a rule result.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        protected override RuleResult CreateResult(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new ValidatorRuleResult(this.Name, null);
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns>The <see cref="NotImplementedException"/>.</returns>
        protected override object Execute(RuleContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns>The <see cref="NotImplementedException"/>.</returns>
        protected override IEnumerable<ValueResult> SelectValueResults(RuleContext context, object value)
        {
            throw new NotImplementedException();
        }
    }
}