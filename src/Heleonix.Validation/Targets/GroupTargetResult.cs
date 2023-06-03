// <copyright file="GroupTargetResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    /// <summary>
    /// Represents the group target result.
    /// </summary>
    public class GroupTargetResult : TargetResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupTargetResult"/> class.
        /// </summary>
        /// <param name="name">A name of a group.</param>
        public GroupTargetResult(string name)
            : base(name, null)
        {
        }

        /// <summary>
        /// Gets targets results.
        /// </summary>
        public ICollection<TargetResult> TargetResults { get; } = new List<TargetResult>();

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => this.TargetResults.Count == 0;
    }
}