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

namespace NLion.Validation
{
    /// <summary>
    /// Represents the context of validation process.
    /// </summary>
    public class ValidationContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationContext"/> class.
        /// </summary>
        /// <param name="obj">An object to validate.</param>
        /// <param name="validatorProvider">A validator provider.</param>
        /// <param name="parent">A parent context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="obj"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validatorProvider"/> is <see langword="null"/>.
        /// </exception>
        public ValidationContext(object obj, IValidatorProvider validatorProvider, ValidationContext parent = null)
        {
            Throw.ArgumentNullException(obj == null, nameof(obj));
            Throw.ArgumentNullException(validatorProvider == null, nameof(validatorProvider));

            Object = obj;
            ValidatorProvider = validatorProvider;
            Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationContext"/> class.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        /// <param name="validatorProvider">A validator provider.</param>
        /// <param name="parent">A parent context.</param>
        /// <param name="continueValidation">Determines whether to continue validation.</param>
        /// <param name="ignoreEmptyResults">Determines whether to ignore empty results.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="obj"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validatorProvider"/> is <see langword="null"/>.
        /// </exception>
        public ValidationContext(object obj, IValidatorProvider validatorProvider, ValidationContext parent,
            bool continueValidation, bool ignoreEmptyResults) : this(obj, validatorProvider, parent)
        {
            ContinueValidation = continueValidation;
            IgnoreEmptyResults = ignoreEmptyResults;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a parent context.
        /// </summary>
        public ValidationContext Parent { get; set; }

        /// <summary>
        /// Gets a validator provider.
        /// </summary>
        public IValidatorProvider ValidatorProvider { get; }

        /// <summary>
        /// Gets an object to validate.
        /// </summary>
        public object Object { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to continue validation.
        /// </summary>
        public bool ContinueValidation { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to ignore empty results.
        /// </summary>
        public bool IgnoreEmptyResults { get; set; } = true;

        #endregion
    }
}