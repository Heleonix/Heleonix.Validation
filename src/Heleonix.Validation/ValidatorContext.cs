// <copyright file="ValidatorContext.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents a context of a validator.
    /// </summary>
    public class ValidatorContext
    {
        /// <summary>
        /// Gets or sets a validator.
        /// </summary>
        private IValidator validator;

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
        public ValidatorContext(
            object obj,
            IValidator validator,
            IValidatorProvider validatorProvider,
            ValidatorContext parent = null)
            : this(obj, validator, validatorProvider, parent, true, true)
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
        public ValidatorContext(
            object obj,
            IValidator validator,
            IValidatorProvider validatorProvider,
            ValidatorContext parent,
            bool continueValidation,
            bool ignoreEmptyResults)
        {
            Throw<ArgumentNullException>.IfNull(obj, nameof(obj));
            Throw<ArgumentNullException>.IfNull(validatorProvider, nameof(validatorProvider));

            this.Object = obj;
            this.validator = validator;
            this.ValidatorProvider = validatorProvider;
            this.Parent = parent;
            this.ContinueValidation = continueValidation;
            this.IgnoreEmptyResults = ignoreEmptyResults;
        }

        /// <summary>
        /// Gets or sets an <see cref="IValidator"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual IValidator Validator
        {
            get
            {
                return this.validator;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.validator = value;
            }
        }

        /// <summary>
        /// Gets an <see cref="IValidatorProvider"/>.
        /// </summary>
        public virtual IValidatorProvider ValidatorProvider { get; }

        /// <summary>
        /// Gets or sets a parent <see cref="ValidatorContext"/>.
        /// </summary>
        public virtual ValidatorContext Parent { get; set; }

        /// <summary>
        /// Gets an object to validate.
        /// </summary>
        public virtual object Object { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to continue validation.
        /// </summary>
        public virtual bool ContinueValidation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore empty results.
        /// </summary>
        public virtual bool IgnoreEmptyResults { get; set; }
    }
}