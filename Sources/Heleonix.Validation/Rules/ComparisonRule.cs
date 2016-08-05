/*
The MIT License (MIT)

Copyright (c) 2015 Heleonix.Validation - Hennadii Lutsyshyn (Heleonix)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using Heleonix.Validation.Internal;

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the base class for comparison rules.
    /// </summary>
    public class ComparisonRule : BooleanRule
    {
        #region Fields

        /// <summary>
        /// Gets or sets other value provider.
        /// </summary>
        private Func<RuleContext, object> _otherValueProvider;

        /// <summary>
        /// Gets or sets a comparison operation to compare a target.
        /// </summary>
        private Comparison _comparison;

        #endregion

        #region Constructors

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
        public ComparisonRule(bool continueValidationWhenFalse, Func<RuleContext, object> otherValueProvider,
            Comparison comparison) : base(continueValidationWhenFalse)
        {
            Throw<ArgumentNullException>.IfNull(otherValueProvider, nameof(otherValueProvider));
            Throw<ArgumentOutOfRangeException>.If(!Enum.IsDefined(typeof (Comparison), comparison), nameof(comparison));

            _otherValueProvider = otherValueProvider;
            _comparison = comparison;
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

        #endregion

        #region Methods

        /// <summary>
        /// Gets a comparer.
        /// </summary>
        /// <param name="comparison">A comparison operation.</param>
        /// <returns>A comparer.</returns>
        protected Func<IComparable, object, bool> GetComparer(Comparison comparison)
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets other value provider.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Func<RuleContext, object> OtherValueProvider
        {
            get { return _otherValueProvider; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));

                _otherValueProvider = value;
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
            get { return _comparison; }
            set
            {
                Throw<ArgumentOutOfRangeException>.If(!Enum.IsDefined(typeof (Comparison), value), nameof(value));

                _comparison = value;
            }
        }

        #endregion

        #region Rule Members

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

            return new ComparisonRuleResult(Name, value, OtherValueProvider.Invoke(context));
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

            var otherValue = OtherValueProvider(context);

            if (otherValue == null)
            {
                return false;
            }

            var comparable = value as IComparable;

            var comparer = GetComparer(Comparison);

            return comparable != null && comparer != null && comparer(comparable, OtherValueProvider(context));
        }

        /// <summary>
        /// Gets a name of the rule.
        /// </summary>
        public override string Name => Comparison.ToString();

        #endregion
    }
}