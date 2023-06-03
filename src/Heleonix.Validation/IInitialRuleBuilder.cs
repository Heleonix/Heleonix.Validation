// <copyright file="IInitialRuleBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the <see langword="interface"/> to start building rules.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
#pragma warning disable S2326 // Unused type parameters should be removed
    public interface IInitialRuleBuilder<TObject, TTarget> : IBuilder<TObject>
#pragma warning restore S2326 // Unused type parameters should be removed
    {
        /// <summary>
        /// Gets a target.
        /// </summary>
        Target Target { get; }
    }
}