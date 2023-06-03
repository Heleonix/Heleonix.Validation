// <copyright file="Comparison.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents comparison operations.
    /// </summary>
    public enum Comparison
    {
        /// <summary>
        /// The equal operation.
        /// </summary>
        Equal,

        /// <summary>
        /// The not equal operation.
        /// </summary>
        NotEqual,

        /// <summary>
        /// The less than operation.
        /// </summary>
        LessThan,

        /// <summary>
        /// The less than or equal operation.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// The greater than operation.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// The greater than or equal operation.
        /// </summary>
        GreaterThanOrEqual,
    }
}