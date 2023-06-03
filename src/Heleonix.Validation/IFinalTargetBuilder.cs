// <copyright file="IFinalTargetBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the <see langword="interface"/> to finalize building targets.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a built target.</typeparam>
    public interface IFinalTargetBuilder<TObject, TTarget> : IInitialRuleBuilder<TObject, TTarget>
    {
        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        new Target Target { get; set; }
    }
}