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
    /// Represents the base class for builders with value results.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
    /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
    public abstract class ValueResultBuilder<TObject, TTarget, TValue> : RuleBuilder<TObject, TTarget, TValue>
    {
        #region Fields

        /// <summary>
        /// Gets or sets a value result.
        /// </summary>
        private ValueResult _valueResult;

        #endregion

        #region Constructors

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

            ValueResult = valueResult;

            Rule.ValueResults.CollectionChanged += ValueResults_CollectionChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the CollectionChanged event of the ValueResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void ValueResults_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems.Contains(ValueResult))
            {
                _valueResult = null;
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace && e.OldItems.Contains(ValueResult))
            {
                _valueResult = e.NewItems[0] as ValueResult;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value result.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public ValueResult ValueResult
        {
            get { return _valueResult; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));

                var index = Rule.ValueResults.IndexOf(ValueResult);

                if (index >= 0)
                {
                    Rule.ValueResults[index] = value;
                }
            }
        }

        #endregion
    }
}