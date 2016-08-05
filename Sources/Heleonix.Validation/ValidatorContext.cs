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
using Heleonix.Validation.Internal;

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents a context of a validator.
    /// </summary>
    public class ValidatorContext
    {
        #region Fields

        /// <summary>
        /// Gets or sets a validator.
        /// </summary>
        private IValidator _validator;

        /// <summary>
        /// Gets or sets a parent context.
        /// </summary>
        private ValidatorContext _parent;

        /// <summary>
        /// Gets or sets a value determining whether to continue validation.
        /// </summary>
        private bool _continueValidation;

        /// <summary>
        /// Gets or sets a value determining whether to ignore empty results.
        /// </summary>
        private bool _ignoreEmptyResults;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorContext"/> class.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        /// <param name="validator">An <see cref="IValidator"/>, which preforms validation.</param>
        /// <param name="validatorProvider">An <see cref="IValidatorProvider"/> of validators.</param>
        /// <param name="parent">A parent <see cref="ValidatorContext"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="obj"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validatorProvider"/> is <see langword="null"/>.
        /// </exception>
        public ValidatorContext(object obj, IValidator validator, IValidatorProvider validatorProvider,
            ValidatorContext parent = null) : this(obj, validator, validatorProvider, parent, true, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorContext"/> class.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        /// <param name="validator">An <see cref="IValidator"/>, which preforms validation.</param>
        /// <param name="validatorProvider">An <see cref="IValidatorProvider"/> of validators.</param>
        /// <param name="parent">A parent <see cref="ValidatorContext"/>.</param>
        /// <param name="continueValidation">Determines whether to continue validation.</param>
        /// <param name="ignoreEmptyResults">Determines whether to ignore empty results.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="obj"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validatorProvider"/> is <see langword="null"/>.
        /// </exception>
        public ValidatorContext(object obj, IValidator validator, IValidatorProvider validatorProvider,
            ValidatorContext parent, bool continueValidation, bool ignoreEmptyResults)
        {
            Throw<ArgumentNullException>.IfNull(obj, nameof(obj));
            Throw<ArgumentNullException>.IfNull(validatorProvider, nameof(validatorProvider));

            Object = obj;
            _validator = validator;
            ValidatorProvider = validatorProvider;
            _parent = parent;
            _continueValidation = continueValidation;
            _ignoreEmptyResults = ignoreEmptyResults;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets an <see cref="IValidator"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual IValidator Validator
        {
            get { return _validator; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));

                _validator = value;
            }
        }

        /// <summary>
        /// Gets an <see cref="IValidatorProvider"/>.
        /// </summary>
        public virtual IValidatorProvider ValidatorProvider { get; }

        /// <summary>
        /// Gets or sets a parent <see cref="ValidatorContext"/>.
        /// </summary>
        public virtual ValidatorContext Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Gets an object to validate.
        /// </summary>
        public virtual object Object { get; }

        /// <summary>
        /// Gets or sets a value determining whether to continue validation.
        /// </summary>
        public virtual bool ContinueValidation
        {
            get { return _continueValidation; }
            set { _continueValidation = value; }
        }

        /// <summary>
        /// Gets or sets a value determining whether to ignore empty results.
        /// </summary>
        public virtual bool IgnoreEmptyResults
        {
            get { return _ignoreEmptyResults; }
            set { _ignoreEmptyResults = value; }
        }

        #endregion
    }
}