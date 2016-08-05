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
using System.Collections;
using System.Linq;
using Heleonix.Validation.Internal;
using Heleonix.Validation.Targets;

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the base class for target comparison rules.
    /// </summary>
    public class TargetComparisonRule : ComparisonRule
    {
        #region Fields

        /// <summary>
        /// Gets or sets other target.
        /// </summary>
        private Target _otherTarget;

        #endregion

        #region Constructors

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

            _otherTarget = otherTarget;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Extracts a value as an array.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        /// <returns>A value as an array</returns>
        private static object[] ExtractValueAsArray(Target target, object value)
        {
            IEnumerable values;

            if (target is ItemTarget)
            {
                values = value as IEnumerable;

                if (values == null)
                {
                    return null;
                }
            }
            else
            {
                values = new[] {value};
            }

            return values.Cast<object>().ToArray();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets other target.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Target OtherTarget
        {
            get { return _otherTarget; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                _otherTarget = value;
            }
        }

        #endregion

        #region ComparisonRule Members

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

            if (values == null || OtherTarget == null)
            {
                return false;
            }

            var otherValue = OtherTarget.GetValue(context.TargetContext);

            if (otherValue == null)
            {
                return false;
            }

            var otherValues = ExtractValueAsArray(OtherTarget, otherValue);

            if (otherValues == null)
            {
                return false;
            }

            var comparer = GetComparer(Comparison);

            if (comparer == null)
            {
                return false;
            }

            if (context.TargetContext.Target is AnyOfTarget)
            {
                if (OtherTarget is AnyOfTarget)
                {
                    return values.Any(v => v is IComparable && otherValues.Any(o => comparer((IComparable) v, o)));
                }

                return values.Any(v => v is IComparable && otherValues.All(o => comparer((IComparable) v, o)));
            }

            if (OtherTarget is AnyOfTarget)
            {
                return values.All(v => v is IComparable && otherValues.Any(o => comparer((IComparable) v, o)));
            }

            return values.All(v => v is IComparable && otherValues.All(o => comparer((IComparable) v, o)));
        }

        /// <summary>
        /// Gets a name of the rule.
        /// </summary>
        public override string Name => base.Name + "ToTarget";

        #endregion
    }
}