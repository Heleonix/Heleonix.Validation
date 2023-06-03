// <copyright file="ItemTargetResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    /// <summary>
    /// Represents the item target result.
    /// </summary>
    [Serializable]
    public class ItemTargetResult : TargetResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTargetResult"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        public ItemTargetResult(string name)
            : base(name, null)
        {
        }

        /// <summary>
        /// Gets item targets results.
        /// </summary>
        public ICollection<TargetResult> ItemTargetResults { get; } = new List<TargetResult>();

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => this.ItemTargetResults.Count == 0;
    }
}