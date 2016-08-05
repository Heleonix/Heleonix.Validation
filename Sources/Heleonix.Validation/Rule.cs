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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Heleonix.Validation.Internal;

namespace Heleonix.Validation
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
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        public virtual RuleResult Validate(RuleContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            context.Rule = this;

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
        /// When overridden in a derived class, executes validation.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <returns>A value of a rule.</returns>
        protected abstract object Execute(RuleContext context);

        /// <summary>
        /// Creates a rule result.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        protected virtual RuleResult CreateResult(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new RuleResult(Name, value);
        }

        /// <summary>
        /// Selects value results.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>Selected value results.</returns>
        protected virtual IEnumerable<ValueResult> SelectValueResults(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return from r in ValueResults
                where r != null && ((r.MatchValue == null && value == null)
                                    || (r.MatchValue != null && r.MatchValue.Equals(value)))
                select r;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a name of a rule.
        /// </summary>
        public virtual string Name
        {
            get
            {
                var name = GetType().Name;

                if (name.EndsWith("Rule", StringComparison.OrdinalIgnoreCase))
                {
                    name = name.Remove(name.Length - 4, 4);
                }

                return name;
            }
        }

        /// <summary>
        /// Gets value results.
        /// </summary>
        public virtual ObservableCollection<ValueResult> ValueResults { get; }
            = new ObservableCollection<ValueResult>();

        #endregion
    }
}