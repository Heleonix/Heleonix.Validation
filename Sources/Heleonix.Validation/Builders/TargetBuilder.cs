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
using System.Collections.Specialized;
using Heleonix.Validation.Internal;

namespace Heleonix.Validation.Builders
{
    /// <summary>
    /// Represents the base class builders with targets.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a built target.</typeparam>
    public abstract class TargetBuilder<TObject, TTarget> : Builder<TObject>
    {
        #region Fields

        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        private Target _target;

        #endregion

        #region Constructors

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
        protected TargetBuilder(Validator<TObject> validator, Target target) : base(validator)
        {
            Throw<ArgumentNullException>.IfNull(target, nameof(target));

            _target = target;

            Validator.Targets.CollectionChanged += Targets_CollectionChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the CollectionChanged event of the <see cref="Targets"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void Targets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems.Contains(Target))
            {
                _target = null;
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace && e.OldItems.Contains(Target))
            {
                _target = e.NewItems[0] as Target;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public Target Target
        {
            get { return _target; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));

                var index = Validator.Targets.IndexOf(Target);

                if (index >= 0)
                {
                    Validator.Targets[index] = value;
                }
            }
        }

        #endregion
    }
}