// <copyright file="FinalGroupTargetBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    /// <summary>
    /// Implements the <see cref="IFinalGroupTargetBuilder{TObject}"/>.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public class FinalGroupTargetBuilder<TObject> : FinalTargetBuilder<TObject, object>,
        IFinalGroupTargetBuilder<TObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinalGroupTargetBuilder{TObject}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <param name="target">A group target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        public FinalGroupTargetBuilder(Validator<TObject> validator, Target target)
            : base(validator, target)
        {
        }
    }
}