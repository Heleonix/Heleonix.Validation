// <copyright file="ComparisonRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class for comparison rules.
    /// </summary>
    public class ComparisonRule : BooleanRule
    {
        /// <summary>
        /// Gets or sets other value provider.
        /// </summary>
        private Func<RuleContext, object> otherValueProvider;

        /// <summary>
        /// Gets or sets a comparison operation to compare a target.
        /// </summary>
        private Comparison comparison;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="comparison">A comparison operation to compare a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherValueProvider"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="comparison"/> is out of the <see cref="Comparison"/>.
        /// </exception>
        public ComparisonRule(
            bool continueValidationWhenFalse,
            Func<RuleContext, object> otherValueProvider,
            Comparison comparison)
            : base(continueValidationWhenFalse)
        {
            Throw<ArgumentNullException>.IfNull(otherValueProvider, nameof(otherValueProvider));
            Throw<ArgumentOutOfRangeException>.If(!Enum.IsDefined(typeof(Comparison), comparison), nameof(comparison));

            this.otherValueProvider = otherValueProvider;
            this.comparison = comparison;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <param name="otherValue">Other value to compare with.</param>
        /// <param name="comparison">A comparison operation to compare a target.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="comparison"/> is out of the <see cref="Comparison"/>.
        /// </exception>
        public ComparisonRule(bool continueValidationWhenFalse, object otherValue, Comparison comparison)
            : this(continueValidationWhenFalse, context => otherValue, comparison)
        {
        }

        /// <summary>
        /// Gets or sets other value provider.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Func<RuleContext, object> OtherValueProvider
        {
            get
            {
                return this.otherValueProvider;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.otherValueProvider = value;
            }
        }

        /// <summary>
        /// Gets or sets a comparison operation to compare a target.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="value"/> is out of the <see cref="Comparison"/>.
        /// </exception>
        public virtual Comparison Comparison
        {
            get
            {
                return this.comparison;
            }

            set
            {
                Throw<ArgumentOutOfRangeException>.If(!Enum.IsDefined(typeof(Comparison), value), nameof(value));
                this.comparison = value;
            }
        }

        /// <summary>
        /// Gets a name of the rule.
        /// </summary>
        public override string Name => this.Comparison.ToString();

        /// <summary>
        /// Gets a comparer.
        /// </summary>
        /// <param name="comparison">A comparison operation.</param>
        /// <returns>A comparer.</returns>
        protected static Func<IComparable, object, bool> GetComparer(Comparison comparison)
        {
            switch (comparison)
            {
                case Comparison.Equal:
                    return (value, other) => value.CompareTo(other) == 0;
                case Comparison.NotEqual:
                    return (value, other) => value.CompareTo(other) != 0;
                case Comparison.LessThan:
                    return (value, other) => value.CompareTo(other) < 0;
                case Comparison.LessThanOrEqual:
                    return (value, other) => value.CompareTo(other) <= 0;
                case Comparison.GreaterThan:
                    return (value, other) => value.CompareTo(other) > 0;
                case Comparison.GreaterThanOrEqual:
                    return (value, other) => value.CompareTo(other) >= 0;
                default:
                    return null;
            }
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

            return new ComparisonRuleResult(this.Name, value, this.OtherValueProvider.Invoke(context));
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

            var otherValue = this.OtherValueProvider(context);

            if (otherValue == null)
            {
                return false;
            }

            var comparable = value as IComparable;

            var comparer = ComparisonRule.GetComparer(this.Comparison);

            return comparable != null && comparer != null && comparer(comparable, this.OtherValueProvider(context));
        }
    }
}