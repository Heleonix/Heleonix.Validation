// <copyright file="TargetComparisonRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using System.Collections;
    using Heleonix.Validation.Internal;
    using Heleonix.Validation.Targets;

    /// <summary>
    /// Represents the base class for target comparison rules.
    /// </summary>
    public class TargetComparisonRule : ComparisonRule
    {
        /// <summary>
        /// Gets or sets other target.
        /// </summary>
        private Target otherTarget;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetComparisonRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <param name="otherTarget">Other target to compare to.</param>
        /// <param name="comparison">A comparison operation to compare a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTarget"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="comparison"/> is out of the <see cref="Comparison"/>.
        /// </exception>
        public TargetComparisonRule(bool continueValidationWhenFalse, Target otherTarget, Comparison comparison)
            : base(continueValidationWhenFalse, context => otherTarget.GetValue(context.TargetContext), comparison)
        {
            Throw<ArgumentNullException>.IfNull(otherTarget, nameof(otherTarget));

            this.otherTarget = otherTarget;
        }

        /// <summary>
        /// Gets or sets other target.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Target OtherTarget
        {
            get
            {
                return this.otherTarget;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.otherTarget = value;
            }
        }

        /// <summary>
        /// Gets a name of the rule.
        /// </summary>
        public override string Name => base.Name + "ToTarget";

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

            var values = ExtractValueAsArray(context.TargetContext.Target, value);

            if (values == null || this.OtherTarget == null)
            {
                return false;
            }

            var otherValue = this.OtherTarget.GetValue(context.TargetContext);

            if (otherValue == null)
            {
                return false;
            }

            var otherValues = ExtractValueAsArray(this.OtherTarget, otherValue);

            if (otherValues == null)
            {
                return false;
            }

            var comparer = ComparisonRule.GetComparer(this.Comparison);

            if (comparer == null)
            {
                return false;
            }

            if (context.TargetContext.Target is AnyOfTarget)
            {
                if (this.OtherTarget is AnyOfTarget)
                {
                    return values.Any(v => v is IComparable && otherValues.Any(o => comparer((IComparable)v, o)));
                }

                return values.Any(v => v is IComparable && otherValues.All(o => comparer((IComparable)v, o)));
            }

            if (this.OtherTarget is AnyOfTarget)
            {
                return values.All(v => v is IComparable && otherValues.Any(o => comparer((IComparable)v, o)));
            }

            return values.All(v => v is IComparable && otherValues.All(o => comparer((IComparable)v, o)));
        }

        /// <summary>
        /// Extracts a value as an array.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        /// <returns>A value as an array.</returns>
        private static object[] ExtractValueAsArray(Target target, object value)
        {
            IEnumerable values;

            if (target is ItemTarget)
            {
                values = value as IEnumerable;

                if (values == null)
                {
#pragma warning disable S1168 // Empty arrays and collections should be returned instead of null
                    return null;
#pragma warning restore S1168 // Empty arrays and collections should be returned instead of null
                }
            }
            else
            {
                values = new[] { value };
            }

            return values.Cast<object>().ToArray();
        }
    }
}