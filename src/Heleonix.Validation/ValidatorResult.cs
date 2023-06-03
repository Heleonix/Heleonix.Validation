// <copyright file="ValidatorResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents a result of a validator.
    /// </summary>
    [Serializable]
    public class ValidatorResult : Result
    {
        /// <summary>
        /// Gets a list of <see cref="TargetResult"/>.
        /// </summary>
        public ICollection<TargetResult> TargetResults { get; } = new List<TargetResult>();

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => this.TargetResults.Count == 0;
    }
}