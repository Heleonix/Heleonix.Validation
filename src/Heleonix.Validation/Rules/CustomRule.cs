// <copyright file="CustomRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the custom rule.
    /// </summary>
    public class CustomRule : Rule
    {
        /// <summary>
        /// Gets or sets a delegate to perform validation.
        /// </summary>
        private Func<RuleContext, RuleResult> ruleValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRule"/> class.
        /// </summary>
        /// <param name="ruleValidator">A delegate to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="ruleValidator"/> is <see langword="null"/>.
        /// </exception>
        public CustomRule(Func<RuleContext, RuleResult> ruleValidator)
        {
            Throw<ArgumentNullException>.IfNull(ruleValidator, nameof(ruleValidator));

            this.ruleValidator = ruleValidator;
        }

        /// <summary>
        /// Gets or sets a delegate to perform validation.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Func<RuleContext, RuleResult> RuleValidator
        {
            get
            {
                return this.ruleValidator;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.ruleValidator = value;
            }
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

            var result = this.RuleValidator(context);

            if (result == null)
            {
                return null;
            }

            var valueResults = this.SelectValueResults(context, result.Value);

            if (valueResults == null)
            {
                return result;
            }

            foreach (var valueResult in valueResults.Where(valueResult => valueResult != null))
            {
                result.ValueResults.Add(valueResult);
            }

            return result;
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

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns>The <see cref="NotImplementedException"/>.</returns>
        protected override RuleResult CreateResult(RuleContext context, object value)
        {
            throw new NotImplementedException();
        }
    }
}