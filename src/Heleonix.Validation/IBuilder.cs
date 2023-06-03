// <copyright file="IBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the base <see langword="interface"/> for all builders inside validators.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public interface IBuilder<TObject>
    {
        /// <summary>
        /// Gets a validator.
        /// </summary>
        Validator<TObject> Validator { get; }
    }
}