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
using System.Diagnostics.Contracts;

namespace NLion.Validation
{
    /// <summary>
    /// Represents a high level entry point to validation processes.
    /// </summary>
    public class ValidationController
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationController"/> class.
        /// </summary>
        /// <param name="validatorProvider">A provider to get a validator.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validatorProvider"/> is <see langword="null"/>.
        /// </exception>
        public ValidationController(IValidatorProvider validatorProvider)
        {
            Contract.Requires<ArgumentNullException>(validatorProvider != null);

            ValidatorProvider = validatorProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates an object inside a specified context.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <returns>A validator result.</returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context" /> is <see langword="null"/>.
        /// </exception>
        public virtual ValidatorResult Validate(ValidationContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            var validator = ValidatorProvider.GetValidator(context.Object.GetType());

            if (validator == null)
            {
                return null;
            }

            if (!ValidatorProvider.IsCached)
            {
                validator.Build();
            }

            return validator.Validate(context);
        }

        /// <summary>
        /// Validates an object.
        /// </summary>
        /// <param name="obj">An object to validate.</param>
        /// <returns>A validator result.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="obj" /> is <see langword="null"/>.</exception>
        public virtual ValidatorResult Validate(object obj) => Validate(new ValidationContext(obj, ValidatorProvider));

        #endregion

        #region Properties

        /// <summary>
        /// Gets a validator provider.
        /// </summary>
        protected IValidatorProvider ValidatorProvider { get; }

        #endregion
    }
}