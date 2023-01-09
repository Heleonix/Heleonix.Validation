﻿using System;
using Heleonix.Validation.Internal;

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the high level entry point to validation processes.
    /// </summary>
    public class ValidationController
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationController"/> class.
        /// </summary>
        /// <param name="validatorProvider">A provider to get validators.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validatorProvider"/> is <see langword="null"/>.
        /// </exception>
        public ValidationController(IValidatorProvider validatorProvider)
        {
            Throw<ArgumentNullException>.IfNull(validatorProvider, nameof(validatorProvider));

            ValidatorProvider = validatorProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates an object within the specified context.
        /// </summary>
        /// <param name="context">A context of a validator.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context" /> is <see langword="null"/>.
        /// </exception>
        /// <returns>A validator result.</returns>
        public virtual ValidatorResult Validate(ValidatorContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            var validator = ValidatorProvider.GetValidator(context.Object.GetType());

            if (validator == null)
            {
                return null;
            }

            if (!ValidatorProvider.IsCached)
            {
                validator.Setup();
            }

            return validator.Validate(context);
        }

        /// <summary>
        /// Validates an object.
        /// </summary>
        /// <param name="obj">An object to validate.</param>
        /// <returns>A validator result.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="obj" /> is <see langword="null"/>.</exception>
        public virtual ValidatorResult Validate(object obj)
            => Validate(new ValidatorContext(obj, null, ValidatorProvider));

        #endregion

        #region Properties

        /// <summary>
        /// Gets a validator provider.
        /// </summary>
        protected virtual IValidatorProvider ValidatorProvider { get; }

        #endregion
    }
}