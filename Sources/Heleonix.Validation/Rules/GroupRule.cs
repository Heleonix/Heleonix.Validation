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
    /// Represents the group rule.
    /// </summary>
    public class GroupRule : Rule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupRule"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        public GroupRule(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets rules.
        /// </summary>
        public virtual ObservableCollection<Rule> Rules { get; } = new ObservableCollection<Rule>();

        #endregion

        #region Rule Members

        /// <summary>
        /// Performs validation.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        public override RuleResult Validate(RuleContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            context.Rule = this;

            var result = CreateResult(context, null) as GroupRuleResult;

            if (result == null)
            {
                return null;
            }

            foreach (var rule in Rules)
            {
                if (!context.TargetContext.ValidatorContext.ContinueValidation)
                {
                    return result;
                }

                var ruleResult = rule?.Validate(new RuleContext(null, context.TargetContext));

                if (ruleResult == null)
                {
                    continue;
                }

                if (!context.TargetContext.ValidatorContext.IgnoreEmptyResults || !ruleResult.IsEmpty())
                {
                    result.RuleResults.Add(ruleResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a rule result.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        protected override RuleResult CreateResult(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new GroupRuleResult(Name);
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
        /// Gets a name of a rule.
        /// </summary>
        public override string Name { get; }

        #endregion
    }
}