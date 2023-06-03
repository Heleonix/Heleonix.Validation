// <copyright file="ConditionalTarget.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    using System.Collections.ObjectModel;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class for conditional targets.
    /// </summary>
    public abstract class ConditionalTarget : Target
    {
        /// <summary>
        /// Gets or sets a target to wrap with the <see cref="Condition"/>.
        /// </summary>
        private Target target;

        /// <summary>
        /// Gets or sets a condition to perform validation.
        /// </summary>
        private Predicate<TargetContext> condition;

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
        protected ConditionalTarget(Target target, Predicate<TargetContext> condition)
            : base(target?.Name)
        {
            Throw<ArgumentNullException>.IfNull(target, nameof(target));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            this.target = target;
            this.condition = condition;
        }

        /// <summary>
        /// Gets or sets a target to wrap with the <see cref="Condition"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Target Target
        {
            get
            {
                return this.target;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.target = value;
            }
        }

        /// <summary>
        /// Gets or sets a condition to perform validation.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Predicate<TargetContext> Condition
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
        /// Gets or sets a name of a target.
        /// </summary>
        public override string Name
        {
            get { return this.Target.Name; }

            set { this.Target.Name = value; }
        }

        /// <summary>
        /// Gets rules.
        /// </summary>
        public override ObservableCollection<Rule> Rules => this.Target.Rules;

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns>The <see cref="NotImplementedException"/>.</returns>
        public override object GetValue(TargetContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="NotSupportedException">Should be overridden in a derived class.</exception>
        /// <returns>The <see cref="NotSupportedException"/>.</returns>
        public override TargetResult Validate(TargetContext context)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns>The <see cref="NotImplementedException"/>.</returns>
        protected override TargetResult CreateResult(TargetContext context)
        {
            throw new NotImplementedException();
        }
    }
}