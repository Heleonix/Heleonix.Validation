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
using System.Linq;

namespace NLion.Validation.Rules
{
    /// <summary>
    /// Represents the custom rule.
    /// </summary>
    public class CustomRule : Rule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRule"/> class.
        /// </summary>
        /// <param name="ruleValidator">A delegate to perform validation.</param>
        public CustomRule(Func<RuleValidationContext, RuleResult> ruleValidator)
        {
            RuleValidator = ruleValidator;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a delegate to perform validation.
        /// </summary>
        public Func<RuleValidationContext, RuleResult> RuleValidator { get; set; }

        #endregion

        #region Rule Members

        /// <summary>
        /// Performs validation.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        public override RuleResult Validate(RuleValidationContext context)
        {
            Throw.ArgumentNullException(context == null, nameof(context));

            var result = RuleValidator?.Invoke(context);

            if (result == null)
            {
                return null;
            }

            var valueResults = SelectValueResults(context, result.Value);

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
        /// Does nothing.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <exception cref="NotImplementedException">The method should not be called.</exception>
        /// <returns></returns>
        protected override object Execute(RuleValidationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}