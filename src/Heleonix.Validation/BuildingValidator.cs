// <copyright file="BuildingValidator.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the building validator for building other validators.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public class BuildingValidator<TObject> : Validator<TObject>
    {
        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context to validate an object.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns>The <see cref="NotImplementedException"/>.</returns>
        public override ValidatorResult Validate(ValidatorContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        public override void Setup()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context of a validator.</param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        /// <returns>The <see cref="NotImplementedException"/>.</returns>
        protected override ValidatorResult CreateResult(ValidatorContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="builder">
        /// An instance of the <see cref="IInitialTargetBuilder{TObject}"/> to set up a validator.
        /// </param>
        /// <exception cref="NotImplementedException">Should not be called.</exception>
        protected override void Setup(IInitialTargetBuilder<TObject> builder)
        {
            throw new NotImplementedException();
        }
    }
}