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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NLion.Validation
{
    /// <summary>
    /// Represents the base class for all rules.
    /// </summary>
    public abstract class Rule
    {
        #region Methods

        /// <summary>
        /// Performs validation.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        public virtual RuleResult Validate(RuleValidationContext context)
        {
            Throw.ArgumentNullException(context == null, nameof(context));

            var value = Execute(context);

            var result = CreateResult(context, value);

            if (result == null)
            {
                return null;
            }

            var valueResults = SelectValueResults(context, value);

            if (valueResults == null)
            {
                return result;
            }

            foreach (var valueResult in valueResults.Where(valueResult => valueResult != null))
            {
                result.ValueResults.Add(valueResult);
            }

            return result;
        }

        /// <summary>
        /// Creates a rule result.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <param name="value">A rule value.</param>
        /// <returns>A rule result.</returns>
        public virtual RuleResult CreateResult(RuleValidationContext context, object value)
            => new RuleResult(Name, value);

        /// <summary>
        /// Selects value results.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <param name="value">A rule value.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>Selected value results.</returns>
        public virtual IEnumerable<ValueResult> SelectValueResults(RuleValidationContext context, object value)
        {
            Throw.ArgumentNullException(context == null, nameof(context));

            return from c in ValueResultContainers
                where c?.ValueResult?.MatchValue?.Equals(value) ?? false
                select c.ValueResult;
        }

        /// <summary>
        /// When overridden in a derived class, executes validation.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <returns>A rule value.</returns>
        protected abstract object Execute(RuleValidationContext context);

        #endregion

        #region Properties

        /// <summary>
        /// Gets a rule name.
        /// </summary>
        public virtual string Name => GetType().Name.TrimEnd('R', 'u', 'l', 'e');

        /// <summary>
        /// Gets value results containers.
        /// </summary>
        public ObservableCollection<ValueResultContainer> ValueResultContainers { get; }
            = new ObservableCollection<ValueResultContainer>();

        #endregion
    }
}