// <copyright file="TargetBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    using System.Collections.Specialized;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class builders with targets.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a built target.</typeparam>
#pragma warning disable S2326 // Unused type parameters should be removed
    public abstract class TargetBuilder<TObject, TTarget> : Builder<TObject>
#pragma warning restore S2326 // Unused type parameters should be removed
    {
        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        private Target target;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetBuilder{TObject, TTarget}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <param name="target">A target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        protected TargetBuilder(Validator<TObject> validator, Target target)
            : base(validator)
        {
            Throw<ArgumentNullException>.IfNull(target, nameof(target));

            this.target = target;

            this.Validator.Targets.CollectionChanged += this.Targets_CollectionChanged;
        }

        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public Target Target
        {
            get
            {
                return this.target;
            }

#pragma warning disable S4275 // Getters and setters should access the expected fields
            set
#pragma warning restore S4275 // Getters and setters should access the expected fields
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));

                var index = this.Validator.Targets.IndexOf(this.target);

                if (index >= 0)
                {
                    this.Validator.Targets[index] = value;
                }
            }
        }

        /// <summary>
        /// Handles the CollectionChanged event of the <see cref="Targets"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void Targets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems.Contains(this.Target))
            {
                this.target = null;
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace && e.OldItems.Contains(this.Target))
            {
                this.target = e.NewItems[0] as Target;
            }
        }
    }
}