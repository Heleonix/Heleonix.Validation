// <copyright file="GroupRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using System.Collections.ObjectModel;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the group rule.
    /// </summary>
    public class GroupRule : Rule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupRule"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        public GroupRule(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets rules.
        /// </summary>
        public virtual ObservableCollection<Rule> Rules { get; } = new ObservableCollection<Rule>();

        /// <summary>
        /// Gets a name of a rule.
        /// </summary>
        public override string Name { get; }

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

            var result = this.CreateResult(context, null) as GroupRuleResult;

            if (result == null)
            {
                return null;
            }

            foreach (var rule in this.Rules)
            {
                if (!context.TargetContext.ValidatorContext.ContinueValidation)
                {
                    return result;
                }

                var ruleResult = rule?.Validate(new RuleContext(null, context.TargetContext));

                if (ruleResult == null)
                {
                    continue;
                }

                if (!context.TargetContext.ValidatorContext.IgnoreEmptyResults || !ruleResult.IsEmpty())
                {
                    result.RuleResults.Add(ruleResult);
                }
            }

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

            return new GroupRuleResult(this.Name);
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
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns>The <see cref="NotImplementedException"/>.</returns>
        protected override object Execute(RuleContext context)
        {
            throw new NotImplementedException();
        }
    }
}