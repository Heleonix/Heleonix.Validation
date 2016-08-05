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

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the base class for all boolean rules.
    /// </summary>
    public abstract class BooleanRule : Rule
    {
        #region Fields

        /// <summary>
        /// Gets or sets a value determining whether to continue validation
        /// when a value of a rule is <see langword="false"/>.
        /// </summary>
        private bool _continueValidationWhenFalse;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        protected BooleanRule(bool continueValidationWhenFalse)
        {
            _continueValidationWhenFalse = continueValidationWhenFalse;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value determining whether to continue validation
        /// when a value of a rule is <see langword="false"/>.
        /// </summary>
        public virtual bool ContinueValidationWhenFalse
        {
            get { return _continueValidationWhenFalse; }
            set { _continueValidationWhenFalse = value; }
        }

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
            var result = base.Validate(context);

            if (result == null)
            {
                return null;
            }

            context.TargetContext.ValidatorContext.ContinueValidation
                = !(result.Value is bool) || (bool) result.Value
                  || (!(bool) result.Value && ContinueValidationWhenFalse);

            return result;
        }

        #endregion
    }
}