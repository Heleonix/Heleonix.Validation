// <copyright file="Result.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the base class for all results.
    /// </summary>
    [Serializable]
    public abstract class Result
    {
        /// <summary>
        /// When overridden in a derived class, indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public abstract bool IsEmpty();
    }
}