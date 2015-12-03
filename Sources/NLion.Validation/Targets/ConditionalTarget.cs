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
using System.Collections.ObjectModel;

namespace NLion.Validation.Targets
{
    /// <summary>
    /// Represents the base class for conditional targets.
    /// </summary>
    public abstract class ConditionalTarget : Target
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalTarget"/> class.
        /// </summary>
        /// <param name="target">A target to wrap with the <paramref name="condition"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        protected ConditionalTarget(Target target, Predicate<TargetContext> condition) : base(target?.Name)
        {
            Throw.ArgumentNullException(target == null, nameof(target));
            Throw.ArgumentNullException(condition == null, nameof(condition));

            Target = target;
            Condition = condition;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a condition to perform validation.
        /// </summary>
        public virtual Predicate<TargetContext> Condition { get; }

        /// <summary>
        /// Gets a target to wrap with the <see cref="Condition"/>.
        /// </summary>
        public virtual Target Target { get; }

        #endregion

        #region Target Members

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="NotImplementedException">The method should not be called.</exception>
        /// <returns></returns>
        public override object GetValue(TargetContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="NotSupportedException">The method should be overridden in a derived class.</exception>
        /// <returns></returns>
        public override TargetResult Validate(TargetContext context)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="NotImplementedException">The method should not be called.</exception>
        /// <returns></returns>
        protected override TargetResult CreateResult(TargetContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets a name of a target.
        /// </summary>
        public override string Name
        {
            get { return Target.Name; }
            set { Target.Name = value; }
        }

        /// <summary>
        /// Gets rules.
        /// </summary>
        public override ObservableCollection<Rule> Rules => Target.Rules;

        #endregion
    }
}