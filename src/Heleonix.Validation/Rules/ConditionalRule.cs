// <copyright file="ConditionalRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using System.Collections.ObjectModel;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class for conditional rules.
    /// </summary>
    public abstract class ConditionalRule : Rule
    {
        /// <summary>
        /// Gets or sets a rule to wrap with the <see cref="Condition"/>.
        /// </summary>
        private Rule rule;

        /// <summary>
        /// Gets or sets a condition to perform validation.
        /// </summary>
        private Predicate<RuleContext> condition;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalRule"/> class.
        /// </summary>
        /// <param name="rule">A rule to wrap with the <paramref name="condition"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="rule"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        protected ConditionalRule(Rule rule, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(rule, nameof(rule));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            this.rule = rule;
            this.condition = condition;
        }

        /// <summary>
        /// Gets or sets a rule to wrap with the <see cref="Condition"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Rule Rule
        {
            get
            {
                return this.rule;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.rule = value;
            }
        }

        /// <summary>
        /// Gets or sets a condition to perform validation.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Predicate<RuleContext> Condition
        {
            get
            {
                return this.condition;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.condition = value;
            }
        }

        /// <summary>
        /// Gets value results.
        /// </summary>
        public override ObservableCollection<ValueResult> ValueResults => this.Rule.ValueResults;

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="NotSupportedException">Should be overridden in a derived class.</exception>
        /// <returns>The <see cref="NotSupportedException"/>.</returns>
        public override RuleResult Validate(RuleContext context)
        {
            throw new NotSupportedException();
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