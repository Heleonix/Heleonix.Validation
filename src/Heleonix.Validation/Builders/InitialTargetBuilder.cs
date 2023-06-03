// <copyright file="InitialTargetBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    /// <summary>
    /// Implements the <see cref="IInitialTargetBuilder{TObject}"/>.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public class InitialTargetBuilder<TObject> : Builder<TObject>, IInitialTargetBuilder<TObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitialTargetBuilder{TObject}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        public InitialTargetBuilder(Validator<TObject> validator)
            : base(validator)
        {
        }
    }
}