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
using Heleonix.Validation.Internal;

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the base class for conditional rules.
    /// </summary>
    public abstract class ConditionalRule : Rule
    {
        #region Fields

        /// <summary>
        /// Gets or sets a rule to wrap with the <see cref="Condition"/>.
        /// </summary>
        private Rule _rule;

        /// <summary>
        /// Gets or sets a condition to perform validation.
        /// </summary>
        private Predicate<RuleContext> _condition;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalRule"/> class.
        /// </summary>
        /// <param name="rule">A rule to wrap with the <paramref name="condition"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="rule"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        protected ConditionalRule(Rule rule, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(rule, nameof(rule));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            _rule = rule;
            _condition = condition;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a rule to wrap with the <see cref="Condition"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Rule Rule
        {
            get { return _rule; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                _rule = value;
            }
        }

        /// <summary>
        /// Gets or sets a condition to perform validation.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Predicate<RuleContext> Condition
        {
            get { return _condition; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                _condition = value;
            }
        }

        #endregion

        #region Rule Members

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="NotSupportedException">Should be overridden in a derived class.</exception>
        /// <returns></returns>
        public override RuleResult Validate(RuleContext context)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns></returns>
        protected override RuleResult CreateResult(RuleContext context, object value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns></returns>
        protected override object Execute(RuleContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns></returns>
        protected override IEnumerable<ValueResult> SelectValueResults(RuleContext context, object value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets value results.
        /// </summary>
        public override ObservableCollection<ValueResult> ValueResults => Rule.ValueResults;

        #endregion
    }
}