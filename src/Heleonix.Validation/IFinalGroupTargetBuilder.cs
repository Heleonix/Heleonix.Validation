// <copyright file="IFinalGroupTargetBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the <see langword="interface"/> to finalize building group targets.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public interface IFinalGroupTargetBuilder<TObject> : IInitialTargetBuilder<TObject>
    {
        /// <summary>
        /// Gets or sets a group target.
        /// </summary>
        Target Target { get; set; }
    }
}