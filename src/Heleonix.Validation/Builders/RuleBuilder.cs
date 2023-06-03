// <copyright file="RuleBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    using System.Collections.Specialized;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class for builders with rules.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
    /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
#pragma warning disable S2326 // Unused type parameters should be removed
    public abstract class RuleBuilder<TObject, TTarget, TValue> : TargetBuilder<TObject, TTarget>
#pragma warning restore S2326 // Unused type parameters should be removed
    {
        /// <summary>
        /// Gets or sets a rule.
        /// </summary>
        private Rule rule;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleBuilder{TObject, TTarget, TValue}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <param name="target">A target.</param>
        /// <param name="rule">A rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="rule"/> is <see langword="null"/>.
        /// </exception>
        protected RuleBuilder(Validator<TObject> validator, Target target, Rule rule)
            : base(validator, target)
        {
            Throw<ArgumentNullException>.IfNull(rule, nameof(rule));

            this.rule = rule;

            this.Target.Rules.CollectionChanged += this.Rules_CollectionChanged;
        }

        /// <summary>
        /// Gets or sets a rule.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public Rule Rule
        {
            get
            {
                return this.rule;
            }

#pragma warning disable S4275 // Getters and setters should access the expected fields
            set
#pragma warning restore S4275 // Getters and setters should access the expected fields
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));

                var index = this.Target.Rules.IndexOf(this.rule);

                if (index >= 0)
                {
                    this.Target.Rules[index] = value;
                }
            }
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Rules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void Rules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems.Contains(this.Rule))
            {
                this.rule = null;
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace && e.OldItems.Contains(this.Rule))
            {
                this.rule = e.NewItems[0] as Rule;
            }
        }
    }
}