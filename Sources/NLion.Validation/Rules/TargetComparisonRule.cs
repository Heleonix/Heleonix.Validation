/*
The MIT License (MIT)

Copyright (c) 2015 NLion.Validation - Hennadii Lutsyshyn (NLion)

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
using NLion.Validation.Targets;

namespace NLion.Validation.Rules
{
    /// <summary>
    /// Represents the base class for target comparison rules.
    /// </summary>
    public abstract class TargetComparisonRule : ComparisonRule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetComparisonRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <param name="otherTarget">Other target to compare to.</param>
        /// <param name="comparer">A comparer to compare a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTarget"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="comparer"/> is <see langword="null"/>.
        /// </exception>
        protected TargetComparisonRule(bool continueValidationWhenFalse, Target otherTarget,
            Func<IComparable, object, bool> comparer)
            : base(continueValidationWhenFalse, context => otherTarget.GetValue(context.TargetContext), comparer)
        {
            Throw.ArgumentNullException(otherTarget == null, nameof(otherTarget));

            OtherTarget = otherTarget;
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
        /// Gets other target.
        /// </summary>
        public virtual Target OtherTarget { get; }

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
            Throw.ArgumentNullException(context == null, nameof(context));

            if (Comparer == null)
            {
                return true;
            }

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

            if (context.TargetContext.Target is AnyOfTarget)
            {
                if (OtherTarget is AnyOfTarget)
                {
                    return values.Any(v => v is IComparable && otherValues.Any(o => Comparer((IComparable) v, o)));
                }

                return values.Any(v => v is IComparable && otherValues.All(o => Comparer((IComparable) v, o)));
            }

            if (OtherTarget is AnyOfTarget)
            {
                return values.All(v => v is IComparable && otherValues.Any(o => Comparer((IComparable) v, o)));
            }

            return values.All(v => v is IComparable && otherValues.All(o => Comparer((IComparable) v, o)));
        }

        #endregion
    }
}