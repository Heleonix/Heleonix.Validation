// <copyright file="Builder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Implements the <see cref="IBuilder{TObject}"/>.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public abstract class Builder<TObject> : IBuilder<TObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Builder{TObject}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        protected Builder(Validator<TObject> validator)
        {
            Throw<ArgumentNullException>.IfNull(validator, nameof(validator));

            this.Validator = validator;
        }

        /// <summary>
        /// Gets a validator.
        /// </summary>
        public Validator<TObject> Validator { get; }
    }
}