// <copyright file="ValueResultBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    using System.Collections.Specialized;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class for builders with value results.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
    /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
    public abstract class ValueResultBuilder<TObject, TTarget, TValue> : RuleBuilder<TObject, TTarget, TValue>
    {
        /// <summary>
        /// Gets or sets a value result.
        /// </summary>
        private ValueResult valueResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueResultBuilder{TObject, TTarget, TValue}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <param name="target">A target.</param>
        /// <param name="rule">A rule.</param>
        /// <param name="valueResult">A value result.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">The <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="rule"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="valueResult"/> is <see langword="null"/>.
        /// </exception>
        protected ValueResultBuilder(Validator<TObject> validator, Target target, Rule rule, ValueResult valueResult)
            : base(validator, target, rule)
        {
            Throw<ArgumentNullException>.IfNull(valueResult, nameof(valueResult));

            this.ValueResult = valueResult;

            this.Rule.ValueResults.CollectionChanged += this.ValueResults_CollectionChanged;
        }

        /// <summary>
        /// Gets or sets a value result.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public ValueResult ValueResult
        {
            get
            {
                return this.valueResult;
            }

#pragma warning disable S4275 // Getters and setters should access the expected fields
            set
#pragma warning restore S4275 // Getters and setters should access the expected fields
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                var index = this.Rule.ValueResults.IndexOf(this.valueResult);
                if (index >= 0)
                {
                    this.Rule.ValueResults[index] = value;
                }
            }
        }

        /// <summary>
        /// Handles the CollectionChanged event of the ValueResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void ValueResults_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems.Contains(this.ValueResult))
            {
                this.valueResult = null;
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace && e.OldItems.Contains(this.ValueResult))
            {
                this.valueResult = e.NewItems[0] as ValueResult;
            }
        }
    }
}