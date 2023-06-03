// <copyright file="IValidatorProvider.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the <see langword="interface"/> for validators providers.
    /// </summary>
    public interface IValidatorProvider
    {
        /// <summary>
        /// Gets a value indicating whether validators are cached.
        /// </summary>
        bool IsCached { get; }

        /// <summary>
        /// Gets a validator.
        /// </summary>
        /// <param name="objectType">A type of an object to validate.</param>
        /// <returns>A validator.</returns>
        IValidator GetValidator(Type objectType);
    }
}